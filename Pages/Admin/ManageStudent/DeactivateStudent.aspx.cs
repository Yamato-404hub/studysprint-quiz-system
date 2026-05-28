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
    public partial class DeactivateStudent : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if page getting rendered first time
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int studentId;
                    if (int.TryParse(Request.QueryString["id"], out studentId))
                    {
                        LoadStudentData(studentId);
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
                    conn.Close();
                }
            }
        }

        //Change student active status to 0
        protected void btnDeactivate_Click1(object sender, EventArgs e)
        {
            int studentId;
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out studentId))
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "UPDATE dbo.Student SET IsActive = 0 WHERE StudentID = @StudentID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", studentId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                Response.Redirect("ManageStudents.aspx?status=deactivated");
            }
        }
    }
}