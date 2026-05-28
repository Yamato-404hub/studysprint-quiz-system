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

namespace StudySprint.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Show a success message if the user just arrived from the registration page
            if (!IsPostBack && Request.QueryString["status"] == "registered")
            {
                lblMessage.Text = "Registration successful! You can now login.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string inputUser = txtUsername.Text.Trim();

                // Hash the user's input so we can match it against the stored hash in the DB
                string secretPass = HashPassword(txtPassword.Text);

                string connectionString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // --- Step 1: Check Admin Table ---
                        string adminSql = "SELECT AdminID FROM Admin WHERE Username = @User AND PasswordHash = @Pass";
                        SqlCommand adminCmd = new SqlCommand(adminSql, conn);
                        adminCmd.Parameters.AddWithValue("@User", inputUser);
                        adminCmd.Parameters.AddWithValue("@Pass", secretPass);

                        object adminId = adminCmd.ExecuteScalar();

                        if (adminId != null)
                        {
                            // Initialize Admin Session
                            Session["Role"] = "Admin";
                            Session["AdminID"] = adminId.ToString();
                            Session["Username"] = inputUser;

                            Response.Redirect("~/Pages/Admin/AdminDashboard.aspx");
                            return;
                        }

                        // --- Step 2: Check Student Table (if not an admin) ---
                        string studentSql = "SELECT StudentID FROM Student WHERE Username = @User AND PasswordHash = @Pass";
                        SqlCommand studentCmd = new SqlCommand(studentSql, conn);
                        studentCmd.Parameters.AddWithValue("@User", inputUser);
                        studentCmd.Parameters.AddWithValue("@Pass", secretPass);

                        object studentId = studentCmd.ExecuteScalar();

                        if (studentId != null)
                        {
                            // Initialize Student Session
                            Session["Role"] = "Student";
                            Session["StudentID"] = studentId.ToString();
                            Session["Username"] = inputUser;

                            Response.Redirect("~/Pages/Student/StudentDashboard.aspx");
                        }
                        else
                        {
                            // Fallback if no records match
                            lblMessage.Text = "Invalid username or password. Please try again.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error and notify user
                        lblMessage.Text = "System Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        // Standard SHA-256 hashing method
        // NOTE: This must remain identical to the one in Register.aspx.cs
        private string HashPassword(string rawPassword)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

                StringBuilder hexResult = new StringBuilder();
                foreach (byte b in data)
                {
                    hexResult.Append(b.ToString("x2"));
                }
                return hexResult.ToString();
            }
        }
    }
}