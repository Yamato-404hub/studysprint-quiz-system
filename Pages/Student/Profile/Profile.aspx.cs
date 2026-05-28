using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Student.Profile
{
    public partial class Profile : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Ensure the user is logged in
                if (Session["StudentID"] != null)
                {
                    int studentId = Convert.ToInt32(Session["StudentID"]);
                    LoadProfileData(studentId);
                }
                else
                {
                    // Redirect to login if session is missing
                    Response.Redirect("~/Pages/Login.aspx");
                }
            }
        }

        private void LoadProfileData(int studentId)
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
                            lblEmail.Text = reader["Email"].ToString();
                            lblUsername.Text = reader["Username"].ToString();
                            lblFullName.Text = reader["FullName"].ToString();
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateInformation.aspx");
        }
    }
}