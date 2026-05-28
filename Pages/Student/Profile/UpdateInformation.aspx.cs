using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace StudySprint.Pages.Student.Profile
{
    public partial class EditProfile : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StudentID"] != null)
                {
                    int studentId = Convert.ToInt32(Session["StudentID"]);
                    PopulateFields(studentId);
                }
                else
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
            }
        }

        private void PopulateFields(int studentId)
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
                            txtFullName.Text = reader["FullName"].ToString();
                            txtUsername.Text = reader["Username"].ToString();
                            txtEmail.Text = reader["Email"].ToString();
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["StudentID"] != null)
            {
                int studentId = Convert.ToInt32(Session["StudentID"]);
                UpdateStudentData(studentId);
            }
        }

        private void UpdateStudentData(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                // Dynamic query building to handle password updates conditionally
                string query = "UPDATE dbo.Student SET FullName = @FullName, Username = @Username, Email = @Email";

                // If the user typed a new password, append it to the update query
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    query += ", PasswordHash = @Password";
                }

                query += " WHERE StudentID = @StudentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@StudentID", studentId);

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        string hashedPaswd = HashPassword(txtPassword.Text);
                        cmd.Parameters.AddWithValue("@Password", hashedPaswd);
                    }

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            lblMessage.Text = "Information updated successfully.";
                            lblMessage.CssClass = "form-success-msg";
                        }
                        else
                        {
                            lblMessage.Text = "Update failed. Please try again.";
                            lblMessage.CssClass = "form-error-msg";
                        }
                    }
                    catch (SqlException ex)
                    {
                        // Error numbers 2627 and 2601 represent Unique Constraint Violations
                        if (ex.Number == 2627 || ex.Number == 2601)
                        {
                            lblMessage.Text = "Update failed: That email or username is already taken.";
                            lblMessage.CssClass = "form-error-msg";
                        }
                        else
                        {
                            lblMessage.Text = "A database error occurred. Please try again.";
                            lblMessage.CssClass = "form-error-msg";
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }

        //Hash password for better security
        private string HashPassword(string plainText)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                StringBuilder hexBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hexBuilder.Append(b.ToString("x2"));
                }
                return hexBuilder.ToString();
            }
        }
    }
}