using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StudySprint.Pages.Admin.ManageQuizzes
{
    public partial class ManageQuizzes : System.Web.UI.Page
    {
        private string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckIncomingStatus();
                BindQuizGrid();
            }
        }

        private void CheckIncomingStatus()
        {
            if (Request.QueryString["status"] != null)
            {
                if (Request.QueryString["status"] == "updated")
                {
                    lblStatus.Text = "Success: The quiz details were successfully updated.";
                    lblStatus.CssClass = "alert alert-success";
                    lblStatus.Visible = true;
                }
            }
        }

        private void BindQuizGrid()
        {
            string query = "SELECT QuizID, Title, Description, TimeLimit, DateCreated FROM Quiz ORDER BY QuizID DESC";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            gvQuizzes.DataSource = dt;
                            gvQuizzes.DataBind();
                            gvQuizzes.Visible = true;
                            pnlNoData.Visible = false;
                        }
                        else
                        {
                            gvQuizzes.Visible = false;
                            pnlNoData.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Data Fetch Error: " + ex.Message;
                        lblStatus.CssClass = "alert alert-danger";
                        lblStatus.Visible = true;
                    }
                }
            }
        }

        protected void gvQuizzes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int quizId = Convert.ToInt32(e.CommandArgument);
                DeleteQuizRecord(quizId);
            }
        }

        private void DeleteQuizRecord(int quizId)
        {
            string query = @"
                BEGIN TRY
                    BEGIN TRANSACTION;

                    DELETE FROM StudentAnswer 
                    WHERE QuestionID IN (SELECT QuestionID FROM Question WHERE QuizID = @QuizID);

                    DELETE FROM Result WHERE QuizID = @QuizID;

                    DELETE FROM [Option] 
                    WHERE QuestionID IN (SELECT QuestionID FROM Question WHERE QuizID = @QuizID);

                    DELETE FROM Question WHERE QuizID = @QuizID;

                    DELETE FROM Quiz WHERE QuizID = @QuizID;

                    COMMIT TRANSACTION;

                END TRY
                BEGIN CATCH
                    ROLLBACK TRANSACTION;
                    THROW; 
                END CATCH
            ";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@QuizID", quizId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();

                        lblStatus.Text = "Success: Quiz and all its related records have been removed securely.";
                        lblStatus.CssClass = "alert alert-success";
                        lblStatus.Visible = true;

                        BindQuizGrid();
                    }
                    catch (Exception ex)
                    {
                        lblStatus.Text = "Delete failed: " + ex.Message;
                        lblStatus.CssClass = "alert alert-danger";
                        lblStatus.Visible = true;
                    }
                }
            }
        }

        protected void gvQuizzes_OnRowDeleting(object sender, GridViewDeleteEventArgs e) { }
        protected void gvQuizzes_RowDeleting(object sender, GridViewDeleteEventArgs e) { }
    }
}