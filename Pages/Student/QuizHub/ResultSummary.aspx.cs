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
    public partial class ResultSummary : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadResultSummary();
            }
        }

        // Load quiz result summary
        private void LoadResultSummary()
        {
            int resultID;

            // Validate ResultID from URL
            if (!int.TryParse(Request.QueryString["ResultID"], out resultID))
            {
                Response.Redirect("QuizHub.aspx");
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT Score
                    FROM Result
                    WHERE ResultID = @ResultID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@ResultID", resultID);

                con.Open();

                object result = cmd.ExecuteScalar();

                // Prevent null error
                if (result == null)
                {
                    Response.Redirect("QuizHub.aspx");
                    return;
                }

                int score = Convert.ToInt32(result);

                int totalQuestions = GetTotalQuestions();

                DisplayResult(score, totalQuestions);
            }
        }

        // Display result information
        private void DisplayResult(int score, int totalQuestions)
        {
            lblScore.Text =
                score + " / " + totalQuestions;

            lblCorrect.Text =
                score.ToString();

            lblIncorrect.Text =
                (totalQuestions - score).ToString();
        }

        // Get total questions for selected quiz
        private int GetTotalQuestions()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT COUNT(*)
                    FROM Question
                    WHERE QuizID = @QuizID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@QuizID",
                    Session["QuizID"]);

                con.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Return to quiz hub
        protected void btnTryAgain_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuizHub.aspx");
        }

        // Go to Review Answers
        protected void btnReviewAnswers_Click(object sender, EventArgs e)
        {
            string resultID = Request.QueryString["ResultID"];

            Response.Redirect(
                "~/Pages/Student/MyProgress/ReviewAnswers.aspx?ResultID=" + resultID);
        }
    }
}