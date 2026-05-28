using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudySprint.Pages.Admin.ManageStudent
{
    public partial class DeleteStudent : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Response.Redirect("ManageStudents.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadStudentDetails(Convert.ToInt32(Request.QueryString["id"]));
            }
        }

        private void LoadStudentDetails(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT FullName, Username, Email FROM dbo.Student WHERE StudentID = @StudentID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblFullName.Text = reader["FullName"].ToString();
                            lblUsername.Text = reader["Username"].ToString();
                            lblEmail.Text = reader["Email"].ToString();
                        }
                        else
                        {
                            Response.Redirect("ManageStudents.aspx");
                        }
                    }
                }
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int studentId = Convert.ToInt32(Request.QueryString["id"]);

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    string deleteAnswersSql = "DELETE FROM StudentAnswer WHERE ResultID IN (SELECT ResultID FROM Result WHERE StudentID = @StudentID)";
                    using (SqlCommand delAnsCmd = new SqlCommand(deleteAnswersSql, conn, transaction))
                    {
                        delAnsCmd.Parameters.AddWithValue("@StudentID", studentId);
                        delAnsCmd.ExecuteNonQuery();
                    }

                    string deleteResultsSql = "DELETE FROM Result WHERE StudentID = @StudentID";
                    using (SqlCommand delResCmd = new SqlCommand(deleteResultsSql, conn, transaction))
                    {
                        delResCmd.Parameters.AddWithValue("@StudentID", studentId);
                        delResCmd.ExecuteNonQuery();
                    }

                    string deleteStudentSql = "DELETE FROM dbo.Student WHERE StudentID = @StudentID";
                    using (SqlCommand delStuCmd = new SqlCommand(deleteStudentSql, conn, transaction))
                    {
                        delStuCmd.Parameters.AddWithValue("@StudentID", studentId);
                        delStuCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    Response.Redirect("ManageStudents.aspx?status=deleted");
                }
                catch (System.Threading.ThreadAbortException)
                {

                }
                catch (Exception ex)
                {
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Rollback();
                    }
                    lblMessage.Text = "An error occurred while deleting the student: " + ex.Message;
                    lblMessage.Visible = true;
                }
            }
        }
    }
}