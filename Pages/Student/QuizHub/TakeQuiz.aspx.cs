using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Student.QuizHub
{
    public partial class TakeQuiz : System.Web.UI.Page
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;
        protected int QuizTimeLimitSeconds = 60;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string checkUserQuery = "SELECT COUNT(*) FROM dbo.Student WHERE StudentID = @StudentID";
                using (SqlCommand checkCmd = new SqlCommand(checkUserQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(Session["StudentID"]));
                    con.Open();
                    int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userExists == 0)
                    {
                        Session["StudentID"] = null;
                        Response.Redirect("~/Pages/Login.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                }
            }

            if (Session["QuizID"] == null)
            {
                Response.Redirect("QuizHub.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                InitializeQuiz();
            }
            else
            {
                // Calculate the real remaining time based on the fixed End Time
                if (Session["QuizEndTime"] != null)
                {
                    DateTime endTime = (DateTime)Session["QuizEndTime"];
                    int remainingSeconds = (int)(endTime - DateTime.Now).TotalSeconds;

                    if (remainingSeconds <= 0)
                    {
                        // Time is up! Prevent them from answering and auto-submit/reset
                        QuizTimeLimitSeconds = 0;
                        ResetQuiz();
                    }
                    else
                    {
                        // Pass the exact remaining seconds to the frontend timer
                        QuizTimeLimitSeconds = remainingSeconds;
                    }
                }
            }
        }

        private void InitializeQuiz()
        {
            Session["Score"] = 0;
            ViewState["QuestionNo"] = 1;

            int resultID = CreateResult();
            Session["ResultID"] = resultID;

            LoadQuizDetails();

            LoadQuestion(1);
        }

        private void LoadQuizDetails()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT Title, TimeLimit FROM Quiz WHERE QuizID = @QuizID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@QuizID", Session["QuizID"]);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblQuizTitle.Text = reader["Title"].ToString();

                        if (reader["TimeLimit"] != DBNull.Value)
                        {
                            int timeLimitMinutes = Convert.ToInt32(reader["TimeLimit"]);
                            QuizTimeLimitSeconds = timeLimitMinutes * 60;

                            // Record the exact absolute End Time for this quiz attempt
                            Session["QuizEndTime"] = DateTime.Now.AddMinutes(timeLimitMinutes);
                        }
                    }
                }
            }
        }

        protected void quizTimer_Tick(object sender, EventArgs e)
        {
            ResetQuiz();
        }

        private void ResetQuiz()
        {
            Session["Score"] = 0;
            ViewState["QuestionNo"] = 1;

            Response.Redirect("TakeQuiz.aspx?QuizID=" + Session["QuizID"], false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private void LoadQuestion(int questionNo)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT QuestionID, QuestionText
                    FROM Question
                    WHERE QuizID = @QuizID
                    ORDER BY QuestionID
                    OFFSET @QuestionNo - 1 ROWS
                    FETCH NEXT 1 ROWS ONLY";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@QuizID", Session["QuizID"]);
                cmd.Parameters.AddWithValue("@QuestionNo", questionNo);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ViewState["QuestionID"] = Convert.ToInt32(reader["QuestionID"]);
                    lblQuestionText.Text = reader["QuestionText"].ToString();
                    lblQuestionNo.Text = "Question " + questionNo + " of " + GetTotalQuestions();
                }

                reader.Close();
            }

            LoadOptions();
        }

        private void LoadOptions()
        {
            if (ViewState["QuestionID"] == null) return;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT OptionID, OptionText
                    FROM [Option]
                    WHERE QuestionID = @QuestionID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@QuestionID", ViewState["QuestionID"]);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                rblAnswers.DataSource = reader;
                rblAnswers.DataTextField = "OptionText";
                rblAnswers.DataValueField = "OptionID";
                rblAnswers.DataBind();
            }

            lblError.Text = "";
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (!IsAnswerSelected())
            {
                return;
            }

            int selectedOptionID = Convert.ToInt32(rblAnswers.SelectedValue);
            int questionID = Convert.ToInt32(ViewState["QuestionID"]);
            int resultID = Convert.ToInt32(Session["ResultID"]);

            bool isCorrect = CheckAnswer(selectedOptionID);

            if (isCorrect)
            {
                AddScore();
            }

            SaveStudentAnswer(resultID, questionID, selectedOptionID, isCorrect);

            MoveToNextQuestionOrFinish();
        }

        private bool IsAnswerSelected()
        {
            if (rblAnswers.SelectedIndex == -1)
            {
                lblError.Text = "Please select an answer.";
                return false;
            }

            return true;
        }

        private bool CheckAnswer(int selectedOptionID)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT IsCorrect
                    FROM [Option]
                    WHERE OptionID = @OptionID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@OptionID", selectedOptionID);

                con.Open();

                object result = cmd.ExecuteScalar();

                return result != null && Convert.ToBoolean(result);
            }
        }

        private void AddScore()
        {
            int score = Convert.ToInt32(Session["Score"]);
            score++;

            Session["Score"] = score;
        }

        private void MoveToNextQuestionOrFinish()
        {
            int questionNo = Convert.ToInt32(ViewState["QuestionNo"]);
            int totalQuestions = GetTotalQuestions();

            if (questionNo >= totalQuestions)
            {
                int resultID = Convert.ToInt32(Session["ResultID"]);

                UpdateFinalScore(resultID);

                Response.Redirect("ResultSummary.aspx?ResultID=" + resultID, false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                questionNo++;
                ViewState["QuestionNo"] = questionNo;
                LoadQuestion(questionNo);
            }
        }

        private int CreateResult()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    INSERT INTO Result
                    (StudentID, QuizID, Score, DateTaken)
                    OUTPUT INSERTED.ResultID
                    VALUES
                    (@StudentID, @QuizID, @Score, @DateTaken)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@StudentID", Convert.ToInt32(Session["StudentID"]));
                cmd.Parameters.AddWithValue("@QuizID", Convert.ToInt32(Session["QuizID"]));
                cmd.Parameters.AddWithValue("@Score", Convert.ToInt32(Session["Score"]));
                cmd.Parameters.AddWithValue("@DateTaken", DateTime.Now);

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void SaveStudentAnswer(int resultID, int questionID, int selectedOptionID, bool isCorrect)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    INSERT INTO StudentAnswer
                    (ResultID, QuestionID, SelectedOptionID, IsCorrect)
                    VALUES
                    (@ResultID, @QuestionID, @SelectedOptionID, @IsCorrect)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ResultID", resultID);
                cmd.Parameters.AddWithValue("@QuestionID", questionID);
                cmd.Parameters.AddWithValue("@SelectedOptionID", selectedOptionID);
                cmd.Parameters.AddWithValue("@IsCorrect", isCorrect);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        private int GetTotalQuestions()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM Question
                    WHERE QuizID = @QuizID";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@QuizID", Session["QuizID"]);

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void UpdateFinalScore(int resultID)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    UPDATE Result
                    SET Score = @Score
                    WHERE ResultID = @ResultID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Score", Convert.ToInt32(Session["Score"]));
                cmd.Parameters.AddWithValue("@ResultID", resultID);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }
    }
}