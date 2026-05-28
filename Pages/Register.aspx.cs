using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace StudySprint.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                /* RESERVED USERNAME CHECK(Blacklist)*/
                string inputUser = txtUsername.Text.Trim();
                string inputEmail = txtEmail.Text.Trim();

                string[] reservedNames = { "admin", "administrator", "staff", "root", "studysprint" };

                foreach (string name in reservedNames)
                {
                    if (inputUser.ToLower() == name)
                    {
                        lblMessage.Text = "This username is reserved and cannot be used.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }

                string dbConnection = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(dbConnection))
                {
                    string checkSql = @"SELECT 
                                (SELECT COUNT(*) FROM Student WHERE Username = @User OR Email = @Email) + 
                                (SELECT COUNT(*) FROM Admin WHERE Username = @User) AS TotalCount";

                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@User", inputUser);
                    checkCmd.Parameters.AddWithValue("@Email", inputEmail);

                    try
                    {
                        conn.Open();
                        int totalMatch = (int)checkCmd.ExecuteScalar();

                        if (totalMatch > 0)
                        {
                            lblMessage.Text = "Username or Email is already taken or reserved.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            return;
                        }

                        string securePassword = HashPassword(txtPassword.Text);

                        string insertSql = "INSERT INTO Student (Username, Email, PasswordHash, FullName, DateRegistered) " +
                                           "VALUES (@User, @Email, @Pass, @FullName, GETDATE())";

                        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                        insertCmd.Parameters.AddWithValue("@User", inputUser);
                        insertCmd.Parameters.AddWithValue("@Email", inputEmail);
                        insertCmd.Parameters.AddWithValue("@Pass", securePassword);
                        insertCmd.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());

                        insertCmd.ExecuteNonQuery();

                        Response.Redirect("Login.aspx?status=registered");
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        // ---------------------------------------------------------
        // Helper method to generate a SHA-256 hash string
        // ---------------------------------------------------------
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