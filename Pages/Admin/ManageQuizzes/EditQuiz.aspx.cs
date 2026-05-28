using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace StudySprint.Pages.Admin.ManageQuizzes
{
    public partial class EditQuiz : System.Web.UI.Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["QuizID"] != null)
                {
                    int quizId;
                    if (int.TryParse(Request.QueryString["QuizID"], out quizId))
                    {
                        LoadQuizDetails(quizId);
                    }
                    else
                    {
                        Response.Redirect("ManageQuizzes.aspx");
                    }
                }
                else
                {
                    Response.Redirect("ManageQuizzes.aspx");
                }
            }
        }

        private void LoadQuizDetails(int quizId)
        {
            string query = "SELECT QuizID, Title, Description, TimeLimit FROM Quiz WHERE QuizID = @QuizID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@QuizID", quizId);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtQuizID.Text = reader["QuizID"].ToString();
                            txtTitle.Text = reader["Title"].ToString();
                            txtDescription.Text = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "";
                            txtDuration.Text = reader["TimeLimit"].ToString();
                        }
                        else
                        {
                            lblMessage.Text = "Error: Quiz record not found.";
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                            btnUpdate.Enabled = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Database error: " + ex.Message;
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                    }
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                int quizId = Convert.ToInt32(txtQuizID.Text);
                string title = txtTitle.Text.Trim();
                string description = txtDescription.Text.Trim();
                int timeLimit = Convert.ToInt32(txtDuration.Text.Trim());

                string query = "UPDATE Quiz SET Title = @Title, Description = @Description, TimeLimit = @TimeLimit WHERE QuizID = @QuizID";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@QuizID", quizId);
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrEmpty(description) ? (object)DBNull.Value : description);
                        cmd.Parameters.AddWithValue("@TimeLimit", timeLimit);

                        try
                        {
                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Response.Redirect("ManageQuizzes.aspx?status=updated");
                            }
                            else
                            {
                                lblMessage.Text = "Error: No data was modified.";
                                lblMessage.CssClass = "alert alert-danger";
                                lblMessage.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Update error: " + ex.Message;
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                        }
                    }
                }
            }
        }
    }
}