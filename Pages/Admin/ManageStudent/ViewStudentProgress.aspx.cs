using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Admin.ManageStudent
{
    public partial class ViewStudentProgress : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the URL contains the student ID
                if (Request.QueryString["id"] != null)
                {
                    int studentId;
                    if (int.TryParse(Request.QueryString["id"], out studentId))
                    {
                        LoadStudentData(studentId);
                        BindProgressGrid(studentId);
                    }
                    else
                    {
                        Response.Redirect("ManageStudents.aspx");
                    }
                }
                else
                {
                    Response.Redirect("ManageStudents.aspx");
                }
            }
        }

        private void LoadStudentData(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT FullName FROM dbo.Student WHERE StudentID = @StudentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        lblStudentName.Text = result.ToString();
                    }
                    conn.Close();
                }
            }
        }

        //Fetch student quiz data for display
        private void BindProgressGrid(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    SELECT 
                        q.Title, 
                        r.Score, 
                        (SELECT COUNT(*) FROM dbo.Question WHERE QuizID = q.QuizID) AS ActualTotalQuestions, 
                        r.DateTaken 
                    FROM dbo.Result r
                    INNER JOIN dbo.Quiz q ON r.QuizID = q.QuizID
                    WHERE r.StudentID = @StudentID
                    ORDER BY r.DateTaken DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        dt.Columns.Add("DisplayScore", typeof(string));
                        foreach (DataRow row in dt.Rows)
                        {
                            row["DisplayScore"] = row["Score"].ToString() + " / " + row["ActualTotalQuestions"].ToString();
                        }

                        gvProgress.DataSource = dt;
                        gvProgress.DataBind();
                    }
                }
            }
        }
    }
}