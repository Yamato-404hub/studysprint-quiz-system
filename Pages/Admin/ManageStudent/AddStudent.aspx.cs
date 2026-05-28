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

namespace StudySprint.Pages.Admin.ManageStudent
{
    public partial class AddStudent : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateStudent_Click1(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = @"
                    INSERT INTO dbo.Student (FullName, Username, Email, PasswordHash, DateRegistered, IsActive) 
                    VALUES (@FullName, @Username, @Email, @Password, GETDATE(), 1)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                    cmd.Parameters.AddWithValue("@Password", HashPassword(txtPassword.Text));

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lblMessage.Text = "Student added successfully!";
                        lblMessage.CssClass = "form-success-msg";
                        lblMessage.Visible = true;

                        txtFullName.Text = "";
                        txtUsername.Text = "";
                        txtEmail.Text = "";
                        txtPassword.Text = "";
                    }
                    catch (SqlException ex)
                    {
                        lblMessage.CssClass = ".form-error-msg";
                        lblMessage.Visible = true;

                        if (ex.Number == 2627 || ex.Number == 2601)
                        {
                            lblMessage.Text = "Registration failed: This email or username is already in use.";
                        }
                        else
                        {
                            lblMessage.Text = "A database error occurred. Please try again.";
                        }
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