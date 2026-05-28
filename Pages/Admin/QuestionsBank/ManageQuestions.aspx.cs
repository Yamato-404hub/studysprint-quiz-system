using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace StudySprint.Pages.Admin.QuestionsBank
{
    public partial class ManageQuestions : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdmin();

            if (!IsPostBack)
            {
                LoadQuizList();
                LoadQuestions();
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

                ddlQuiz.Items.Insert(0, new ListItem("-- All Quizzes --", "0"));
            }
        }

        private void LoadQuestions()
        {
            int quizId = Convert.ToInt32(ddlQuiz.SelectedValue);
            string keyword = txtSearch.Text.Trim();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT 
                        q.QuestionID,
                        z.Title AS QuizTitle,
                        q.QuestionText,
                        COUNT(o.OptionID) AS OptionCount
                    FROM Question q
                    INNER JOIN Quiz z ON q.QuizID = z.QuizID
                    LEFT JOIN [Option] o ON q.QuestionID = o.QuestionID
                    WHERE (@QuizID = 0 OR q.QuizID = @QuizID)
                    AND (@Keyword = '' OR q.QuestionText LIKE '%' + @Keyword + '%' OR z.Title LIKE '%' + @Keyword + '%')
                    GROUP BY q.QuestionID, z.Title, q.QuestionText
                    ORDER BY z.Title, q.QuestionID DESC";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@QuizID", quizId);
                cmd.Parameters.AddWithValue("@Keyword", keyword);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                gvQuestions.DataSource = dt;
                gvQuestions.DataBind();

                lblMessage.Text = dt.Rows.Count + " question(s) found.";
                lblMessage.CssClass = "alert alert-success";
                lblMessage.Visible = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadQuestions();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlQuiz.SelectedIndex = 0;
            LoadQuestions();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlQuiz.SelectedValue != "0")
            {
                Response.Redirect("AddQuestion.aspx?QuizID=" + ddlQuiz.SelectedValue);
            }
            else
            {
                Response.Redirect("AddQuestion.aspx");
            }
        }

        protected void gvQuestions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditQuestion")
            {
                Response.Redirect("EditQuestion.aspx?QuestionID=" + e.CommandArgument.ToString());
            }

            if (e.CommandName == "DeleteQuestion")
            {
                int questionId = Convert.ToInt32(e.CommandArgument);
                DeleteQuestion(questionId);
                LoadQuestions();
            }
        }

        private void DeleteQuestion(int questionId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string checkSql = "SELECT COUNT(*) FROM StudentAnswer WHERE QuestionID = @QuestionID";

                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@QuestionID", questionId);

                int answerCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (answerCount > 0)
                {
                    lblMessage.Text = "This question cannot be deleted because it already has student answers.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                string deleteOptionsSql = "DELETE FROM [Option] WHERE QuestionID = @QuestionID";

                SqlCommand optionCmd = new SqlCommand(deleteOptionsSql, con);
                optionCmd.Parameters.AddWithValue("@QuestionID", questionId);
                optionCmd.ExecuteNonQuery();

                string deleteQuestionSql = "DELETE FROM Question WHERE QuestionID = @QuestionID";

                SqlCommand questionCmd = new SqlCommand(deleteQuestionSql, con);
                questionCmd.Parameters.AddWithValue("@QuestionID", questionId);
                questionCmd.ExecuteNonQuery();

                lblMessage.Text = "Question deleted successfully.";
                lblMessage.CssClass = "alert alert-success";
                lblMessage.Visible = true;
            }
        }
    }
}