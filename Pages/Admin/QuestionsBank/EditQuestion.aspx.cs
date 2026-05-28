using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace StudySprint.Pages.Admin.QuestionsBank
{
    public partial class EditQuestion : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAdmin();

            if (!IsPostBack)
            {
                LoadQuestionList();
                SelectQuestionFromQueryString();
                LoadSelectedQuestion();
            }
        }

        private void CheckAdmin()
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void LoadQuestionList()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = @"
                    SELECT 
                        q.QuestionID,
                        z.Title + ' - ' + q.QuestionText AS DisplayText
                    FROM Question q
                    INNER JOIN Quiz z ON q.QuizID = z.QuizID
                    ORDER BY q.QuestionID DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();

                da.Fill(dt);

                ddlQuestion.DataSource = dt;
                ddlQuestion.DataTextField = "DisplayText";
                ddlQuestion.DataValueField = "QuestionID";
                ddlQuestion.DataBind();

                ddlQuestion.Items.Insert(0, new ListItem("-- Select Question --", ""));
            }
        }

        private void SelectQuestionFromQueryString()
        {
            string questionId = Request.QueryString["QuestionID"];

            if (!String.IsNullOrEmpty(questionId))
            {
                if (ddlQuestion.Items.FindByValue(questionId) != null)
                {
                    ddlQuestion.SelectedValue = questionId;
                }
            }
        }

        protected void ddlQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSelectedQuestion();
        }

        private void LoadSelectedQuestion()
        {
            ClearForm();

            if (ddlQuestion.SelectedValue == "")
            {
                return;
            }

            int questionId = Convert.ToInt32(ddlQuestion.SelectedValue);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string questionSql = @"
                    SELECT QuestionText 
                    FROM Question 
                    WHERE QuestionID = @QuestionID";

                SqlCommand questionCmd = new SqlCommand(questionSql, con);
                questionCmd.Parameters.AddWithValue("@QuestionID", questionId);

                object questionText = questionCmd.ExecuteScalar();

                if (questionText == null)
                {
                    lblMessage.Text = "Question not found.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                txtQuestion.Text = questionText.ToString();

                string optionSql = @"
                    SELECT OptionID, OptionText, IsCorrect
                    FROM [Option]
                    WHERE QuestionID = @QuestionID
                    ORDER BY OptionID";

                SqlCommand optionCmd = new SqlCommand(optionSql, con);
                optionCmd.Parameters.AddWithValue("@QuestionID", questionId);

                SqlDataAdapter da = new SqlDataAdapter(optionCmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count < 4)
                {
                    lblMessage.Text = "This question does not have four options. Please check the database.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                hfOptionAID.Value = dt.Rows[0]["OptionID"].ToString();
                txtOptionA.Text = dt.Rows[0]["OptionText"].ToString();

                if (Convert.ToBoolean(dt.Rows[0]["IsCorrect"]))
                {
                    ddlCorrect.SelectedValue = "A";
                }

                hfOptionBID.Value = dt.Rows[1]["OptionID"].ToString();
                txtOptionB.Text = dt.Rows[1]["OptionText"].ToString();

                if (Convert.ToBoolean(dt.Rows[1]["IsCorrect"]))
                {
                    ddlCorrect.SelectedValue = "B";
                }

                hfOptionCID.Value = dt.Rows[2]["OptionID"].ToString();
                txtOptionC.Text = dt.Rows[2]["OptionText"].ToString();

                if (Convert.ToBoolean(dt.Rows[2]["IsCorrect"]))
                {
                    ddlCorrect.SelectedValue = "C";
                }

                hfOptionDID.Value = dt.Rows[3]["OptionID"].ToString();
                txtOptionD.Text = dt.Rows[3]["OptionText"].ToString();

                if (Convert.ToBoolean(dt.Rows[3]["IsCorrect"]))
                {
                    ddlCorrect.SelectedValue = "D";
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            if (ddlQuestion.SelectedValue == "")
            {
                lblMessage.Text = "Please select a question.";
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
                return;
            }

            if (hfOptionAID.Value == "" || hfOptionBID.Value == "" || hfOptionCID.Value == "" || hfOptionDID.Value == "")
            {
                lblMessage.Text = "Option information is missing. Please reload the question.";
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
                return;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string updateQuestionSql = @"
                    UPDATE Question 
                    SET QuestionText = @QuestionText 
                    WHERE QuestionID = @QuestionID";

                SqlCommand questionCmd = new SqlCommand(updateQuestionSql, con);
                questionCmd.Parameters.AddWithValue("@QuestionText", txtQuestion.Text.Trim());
                questionCmd.Parameters.AddWithValue("@QuestionID", Convert.ToInt32(ddlQuestion.SelectedValue));

                questionCmd.ExecuteNonQuery();

                UpdateOption(con, Convert.ToInt32(hfOptionAID.Value), txtOptionA.Text.Trim(), ddlCorrect.SelectedValue == "A");
                UpdateOption(con, Convert.ToInt32(hfOptionBID.Value), txtOptionB.Text.Trim(), ddlCorrect.SelectedValue == "B");
                UpdateOption(con, Convert.ToInt32(hfOptionCID.Value), txtOptionC.Text.Trim(), ddlCorrect.SelectedValue == "C");
                UpdateOption(con, Convert.ToInt32(hfOptionDID.Value), txtOptionD.Text.Trim(), ddlCorrect.SelectedValue == "D");

                lblMessage.Text = "Question updated successfully.";
                lblMessage.CssClass = "alert alert-success";
                lblMessage.Visible = true;
            }

            string selectedQuestionId = ddlQuestion.SelectedValue;

            LoadQuestionList();

            if (ddlQuestion.Items.FindByValue(selectedQuestionId) != null)
            {
                ddlQuestion.SelectedValue = selectedQuestionId;
            }

            LoadSelectedQuestion();
        }

        private void UpdateOption(SqlConnection con, int optionId, string optionText, bool isCorrect)
        {
            string sql = @"
                UPDATE [Option]
                SET OptionText = @OptionText,
                    IsCorrect = @IsCorrect
                WHERE OptionID = @OptionID";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@OptionText", optionText);
            cmd.Parameters.AddWithValue("@IsCorrect", isCorrect);
            cmd.Parameters.AddWithValue("@OptionID", optionId);

            cmd.ExecuteNonQuery();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (ddlQuestion.SelectedValue == "")
            {
                lblMessage.Text = "Please select a question.";
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
                return;
            }

            int questionId = Convert.ToInt32(ddlQuestion.SelectedValue);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string checkSql = @"
                    SELECT COUNT(*) 
                    FROM StudentAnswer 
                    WHERE QuestionID = @QuestionID";

                SqlCommand checkCmd = new SqlCommand(checkSql, con);
                checkCmd.Parameters.AddWithValue("@QuestionID", questionId);

                int answerCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (answerCount > 0)
                {
                    lblMessage.Text = "This question cannot be deleted because it already has student answers.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                string deleteOptionsSql = @"
                    DELETE FROM [Option] 
                    WHERE QuestionID = @QuestionID";

                SqlCommand optionCmd = new SqlCommand(deleteOptionsSql, con);
                optionCmd.Parameters.AddWithValue("@QuestionID", questionId);
                optionCmd.ExecuteNonQuery();

                string deleteQuestionSql = @"
                    DELETE FROM Question 
                    WHERE QuestionID = @QuestionID";

                SqlCommand questionCmd = new SqlCommand(deleteQuestionSql, con);
                questionCmd.Parameters.AddWithValue("@QuestionID", questionId);
                questionCmd.ExecuteNonQuery();
            }

            lblMessage.Text = "Question deleted successfully.";
            lblMessage.CssClass = "alert alert-success";
            lblMessage.Visible = true;

            LoadQuestionList();
            ClearForm();
        }

        private void ClearForm()
        {
            txtQuestion.Text = "";
            txtOptionA.Text = "";
            txtOptionB.Text = "";
            txtOptionC.Text = "";
            txtOptionD.Text = "";

            ddlCorrect.SelectedIndex = 0;

            hfOptionAID.Value = "";
            hfOptionBID.Value = "";
            hfOptionCID.Value = "";
            hfOptionDID.Value = "";

            lblMessage.Visible = false;
        }
    }
}