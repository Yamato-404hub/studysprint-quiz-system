using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace StudySprint.Pages.Admin.ManageQuizzes
{
    public partial class AddQuiz : System.Web.UI.Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string title = txtTitle.Text.Trim();
                string description = txtDescription.Text.Trim();

                int timeLimit = Convert.ToInt32(txtDuration.Text.Trim());

                int currentAdminID = 1;

                string query = "INSERT INTO Quiz (AdminID, Title, Description, TimeLimit, IsActive, IsPublished, DateCreated) VALUES (@AdminID, @Title, @Description, @TimeLimit, @IsActive, @IsPublished, GETDATE())";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AdminID", currentAdminID);
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                        cmd.Parameters.AddWithValue("@TimeLimit", timeLimit);

                        cmd.Parameters.AddWithValue("@IsActive", 1);
                        cmd.Parameters.AddWithValue("@IsPublished", 0);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();

                            lblMessage.Text = "Status: Quiz created successfully!";
                            lblMessage.CssClass = "alert alert-success";
                            lblMessage.Visible = true;
                            ClearForm();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error: Unable to save data. " + ex.Message;
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                        }
                    }
                }
            }
        }

        private void ClearForm()
        {
            txtTitle.Text = "";
            txtDescription.Text = "";
            txtDuration.Text = "";
        }
    }
}