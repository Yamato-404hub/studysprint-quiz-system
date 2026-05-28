using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Student.MyProgress
{
    public partial class MyProgress : System.Web.UI.Page
    {
        private readonly string connStr =
            ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check student login
            if (Session["StudentID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadResults();
            }
        }

        // Load student's quiz history
        private void LoadResults()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT
                        r.ResultID,
                        q.Title,

                        CAST(r.Score AS varchar)
                        + ' / ' +
                        CAST(COUNT(ques.QuestionID) AS varchar)
                        AS ScoreText,

                        r.DateTaken

                    FROM Result r

                    INNER JOIN Quiz q
                        ON r.QuizID = q.QuizID

                    INNER JOIN Question ques
                        ON q.QuizID = ques.QuizID

                    WHERE r.StudentID = @StudentID

                    GROUP BY
                        r.ResultID,
                        q.Title,
                        r.Score,
                        r.DateTaken

                    ORDER BY r.DateTaken DESC";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue(
                    "@StudentID",
                    Convert.ToInt32(Session["StudentID"]));

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable resultTable = new DataTable();

                da.Fill(resultTable);

                gvResults.DataSource = resultTable;
                gvResults.DataBind();
            }
        }

        // Review selected result
        protected void gvResults_RowCommand(
            object sender,
            GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewResult")
            {
                string resultID =
                    e.CommandArgument.ToString();

                Response.Redirect(
                    "ReviewAnswers.aspx?ResultID=" + resultID);
            }
        }

        // Back to dashboard
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}