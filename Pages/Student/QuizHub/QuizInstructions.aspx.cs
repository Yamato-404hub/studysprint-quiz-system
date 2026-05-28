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
    public partial class QuizInstructions : System.Web.UI.Page
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                InitializePage();
            }
        }

        private void InitializePage()
        {
            int quizID;

            if (!int.TryParse(Request.QueryString["QuizID"], out quizID))
            {
                Response.Redirect("QuizHub.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            Session["QuizID"] = quizID;
            LoadQuizInfo(quizID);
        }

        private void LoadQuizInfo(int quizID)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT
                        q.Title,
                        q.Description,
                        q.TimeLimit,
                        COUNT(ques.QuestionID) AS QuestionCount
                    FROM Quiz q
                    LEFT JOIN Question ques
                        ON q.QuizID = ques.QuizID
                    WHERE q.QuizID = @QuizID
                    GROUP BY q.Title, q.Description, q.TimeLimit";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@QuizID", quizID);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblTitle.Text = reader["Title"].ToString();
                    lblDescription.Text = reader["Description"].ToString();
                    lblTimeLimit.Text = reader["TimeLimit"].ToString();
                    lblQuestionCount.Text = reader["QuestionCount"].ToString();
                }

                reader.Close();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuizHub.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void btnStartQuiz_Click(object sender, EventArgs e)
        {
            if (Session["QuizID"] == null && Request.QueryString["QuizID"] != null)
            {
                Session["QuizID"] = Convert.ToInt32(Request.QueryString["QuizID"]);
            }

            Response.Redirect("TakeQuiz.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}