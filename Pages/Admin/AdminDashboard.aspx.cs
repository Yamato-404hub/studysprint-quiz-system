using System;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDashboardMetrics();
            }
        }

        private void LoadDashboardMetrics()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Student", conn))
                {
                    lblTotalStudents.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                }

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Quiz", conn))
                {
                    lblTotalQuestions.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                }

                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM StudentAnswer", conn))
                {
                    lblQuestionsTaken.Text = cmd.ExecuteScalar()?.ToString() ?? "0";
                }

                string avgQuery = @"
                    SELECT 
                        CASE 
                            WHEN COUNT(*) = 0 THEN 0 
                            ELSE (SUM(CAST(IsCorrect AS INT)) * 100.0) / COUNT(*) 
                        END AS AvgScore 
                    FROM StudentAnswer";

                using (SqlCommand cmd = new SqlCommand(avgQuery, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        lblAvgScore.Text = Convert.ToDouble(result).ToString("0.0") + "%";
                    }
                    else
                    {
                        lblAvgScore.Text = "0%";
                    }
                }
            }
        }
    }
}