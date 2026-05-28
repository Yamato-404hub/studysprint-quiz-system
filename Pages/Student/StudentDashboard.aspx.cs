using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Student
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                int studentId = Convert.ToInt32(Session["StudentID"]);
                LoadStudentName(studentId);
                LoadRecommendedQuizzes();
                LoadPastResults(studentId);
                DisplayRandomQuote();
            }
        }

        private void LoadStudentName(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FullName FROM Student WHERE StudentID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", studentId);
                conn.Open();
                lblStudentName.Text = cmd.ExecuteScalar()?.ToString() ?? "Student";
            }
        }

        private void LoadRecommendedQuizzes()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 4 QuizID, Title FROM Quiz ORDER BY QuizID DESC";
                SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                rptRecommended.DataSource = dt;
                rptRecommended.DataBind();
            }
        }

        private void LoadPastResults(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT TOP 5 q.Title, r.Score, 
                    (SELECT COUNT(*) FROM Question WHERE QuizID = q.QuizID) as MaxScore, 
                    r.DateTaken 
                    FROM Result r
                    JOIN Quiz q ON r.QuizID = q.QuizID
                    WHERE r.StudentID = @ID
                    ORDER BY r.DateTaken DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", studentId);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                gvPastResults.DataSource = dt;
                gvPastResults.DataBind();
            }
        }

        private void DisplayRandomQuote()
        {
            var quotes = new List<Tuple<string, string>>
            {
                Tuple.Create("Success is not final, failure is not fatal: it is the courage to continue that counts.", "Winston Churchill"),
                Tuple.Create("Believe you can and you're halfway there.", "Theodore Roosevelt"),
                Tuple.Create("The only way to do great work is to love what you do.", "Steve Jobs"),
                Tuple.Create("Don't let what you cannot do interfere with what you can do.", "John Wooden"),
                Tuple.Create("Education is the most powerful weapon which you can use to change the world.", "Nelson Mandela"),
                Tuple.Create("Your time is limited, so don't waste it living someone else's life.", "Steve Jobs")
            };

            Random rand = new Random();
            int index = rand.Next(quotes.Count);
            lblQuote.Text = quotes[index].Item1;
            lblAuthor.Text = quotes[index].Item2;
        }
    }
}