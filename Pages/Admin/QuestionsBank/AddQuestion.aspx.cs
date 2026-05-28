using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace StudySprint.Pages.Admin.QuestionsBank
{
    public partial class AddQuestion : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdmin();

            if (!IsPostBack)
            {
                LoadQuizList();
                SelectQuizFromQueryString();
            }
        }

        private void CheckAdmin()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadQuizList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "SELECT QuizID, Title FROM Quiz ORDER BY Title";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();

                da.Fill(dt);

                ddlQuiz.DataSource = dt;
                ddlQuiz.DataTextField = "Title";
                ddlQuiz.DataValueField = "QuizID";
                ddlQuiz.DataBind();

                ddlQuiz.Items.Insert(0, new ListItem("-- Select Quiz --", ""));
            }
        }

        private void SelectQuizFromQueryString()
        {
            string quizId = Request.QueryString["QuizID"];

            if (!String.IsNullOrEmpty(quizId))
            {
                if (ddlQuiz.Items.FindByValue(quizId) != null)
                {
                    ddlQuiz.SelectedValue = quizId;
                }
            }
        }

        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string insertQuestionSql = @"
                    INSERT INTO Question (QuizID, QuestionText)
                    VALUES (@QuizID, @QuestionText);
                    SELECT SCOPE_IDENTITY();";

                SqlCommand questionCmd = new SqlCommand(insertQuestionSql, con);
                questionCmd.Parameters.AddWithValue("@QuizID", Convert.ToInt32(ddlQuiz.SelectedValue));
                questionCmd.Parameters.AddWithValue("@QuestionText", txtQuestion.Text.Trim());

                int questionId = Convert.ToInt32(questionCmd.ExecuteScalar());

                InsertOption(con, questionId, txtOptionA.Text.Trim(), ddlCorrect.SelectedValue == "A");
                InsertOption(con, questionId, txtOptionB.Text.Trim(), ddlCorrect.SelectedValue == "B");
                InsertOption(con, questionId, txtOptionC.Text.Trim(), ddlCorrect.SelectedValue == "C");
                InsertOption(con, questionId, txtOptionD.Text.Trim(), ddlCorrect.SelectedValue == "D");

                lblMessage.Text = "Question added successfully.";
                lblMessage.CssClass = "alert alert-success";
                lblMessage.Visible = true;

                ClearForm();
            }
        }

        private void InsertOption(SqlConnection con, int questionId, string optionText, bool isCorrect)
        {
            string sql = @"
                INSERT INTO [Option] (QuestionID, OptionText, IsCorrect)
                VALUES (@QuestionID, @OptionText, @IsCorrect)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@QuestionID", questionId);
            cmd.Parameters.AddWithValue("@OptionText", optionText);
            cmd.Parameters.AddWithValue("@IsCorrect", isCorrect);

            cmd.ExecuteNonQuery();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMessage.Visible = false;
        }

        private void ClearForm()
        {
            txtQuestion.Text = "";
            txtOptionA.Text = "";
            txtOptionB.Text = "";
            txtOptionC.Text = "";
            txtOptionD.Text = "";
            ddlCorrect.SelectedIndex = 0;
        }
    }
}