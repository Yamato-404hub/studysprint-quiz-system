using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace StudySprint.Pages.Student.MyProgress
{
    public partial class ReviewAnswers : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        private DataTable reviewTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeReview();
            }
        }

        // Initialize review page
        private void InitializeReview()
        {
            LoadAnswers();

            Session["ReviewIndex"] = 0;

            ShowQuestion();
        }

        // Load all review answers
        private void LoadAnswers()
        {
            int resultID;

            // Validate ResultID
            if (!int.TryParse(
                Request.QueryString["ResultID"],
                out resultID))
            {
                Response.Redirect("MyProgress.aspx");
                return;
            }

            using (SqlConnection con =
                new SqlConnection(connStr))
            {
                string query = @"
                    SELECT
                        q.QuestionText,

                        selectedOption.OptionText
                            AS YourAnswer,

                        correctOption.OptionText
                            AS CorrectAnswer,

                        sa.IsCorrect

                    FROM StudentAnswer sa

                    INNER JOIN Question q
                        ON sa.QuestionID = q.QuestionID

                    INNER JOIN [Option] selectedOption
                        ON sa.SelectedOptionID =
                        selectedOption.OptionID

                    INNER JOIN [Option] correctOption
                        ON q.QuestionID =
                        correctOption.QuestionID
                        AND correctOption.IsCorrect = 1

                    WHERE sa.ResultID = @ResultID";

                SqlCommand cmd =
                    new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@ResultID",
                    resultID);

                SqlDataAdapter da =
                    new SqlDataAdapter(cmd);

                reviewTable = new DataTable();

                da.Fill(reviewTable);

                Session["ReviewTable"] = reviewTable;
            }
        }

        // Display current question
        private void ShowQuestion()
        {
            reviewTable =
                Session["ReviewTable"] as DataTable;

            // No data
            if (reviewTable == null ||
                reviewTable.Rows.Count == 0)
            {
                lblQuestionNo.Text =
                    "No answers found.";

                lblQuestionText.Text = "";
                lblYourAnswer.Text = "";
                lblCorrectAnswer.Text = "";

                return;
            }

            int currentIndex =
                Convert.ToInt32(
                    Session["ReviewIndex"]);

            // Current row
            DataRow currentRow =
                reviewTable.Rows[currentIndex];

            // Question number
            lblQuestionNo.Text =
                "Question " +
                (currentIndex + 1);

            // Question text
            lblQuestionText.Text =
                currentRow["QuestionText"].ToString();

            // Your answer
            lblYourAnswer.Text =
                currentRow["YourAnswer"].ToString();

            // Correct answer
            lblCorrectAnswer.Text =
                currentRow["CorrectAnswer"].ToString();

            // Check correct or wrong
            bool isCorrect =
                Convert.ToBoolean(
                    currentRow["IsCorrect"]);

            // Change color
            if (isCorrect)
            {
                yourAnswerBox.Attributes["style"] =
                    "background:#ECFDF5;" +
                    "border:2px solid #22C55E;" +
                    "padding:25px;" +
                    "border-radius:18px;" +
                    "margin-bottom:25px;";
            }
            else
            {
                yourAnswerBox.Attributes["style"] =
                    "background:#FEF2F2;" +
                    "border:2px solid #EF4444;" +
                    "padding:25px;" +
                    "border-radius:18px;" +
                    "margin-bottom:25px;";
            }

            // Last question button text
            if (currentIndex ==
                reviewTable.Rows.Count - 1)
            {
                btnNext.Text =
                    "Back to Progress";
            }
            else
            {
                btnNext.Text =
                    "Next Question";
            }
        }

        // Next question button
        protected void btnNext_Click(
            object sender,
            EventArgs e)
        {
            reviewTable =
                Session["ReviewTable"] as DataTable;

            int currentIndex =
                Convert.ToInt32(
                    Session["ReviewIndex"]);

            currentIndex++;

            // Next question
            if (currentIndex <
                reviewTable.Rows.Count)
            {
                Session["ReviewIndex"] =
                    currentIndex;

                ShowQuestion();
            }
            else
            {
                // Return to progress page
                Response.Redirect(
                    "MyProgress.aspx");
            }
        }
    }
}