using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace StudySprint.Pages.Student.QuizHub
{
    public partial class QuizHub : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["StudentID"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                LoadQuizzes();
            }
        }

        private void LoadQuizzes()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT QuizID, Title, Description, TimeLimit FROM Quiz";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();

                da.Fill(dt);

                rptQuizzes.DataSource = dt;
                rptQuizzes.DataBind();
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string quizID = btn.CommandArgument;

            Response.Redirect("QuizInstructions.aspx?QuizID=" + quizID, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}