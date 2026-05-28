using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace StudySprint.Pages.Admin.ManageStudent
{
    public partial class ManageStudents : System.Web.UI.Page
    {
        string connString = ConfigurationManager.ConnectionStrings["StudySprintDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudentGrid();

                if (Request.QueryString["status"] == "deactivated")
                {
                    lblMessage.Text = "Student account deactivated successfully.";
                    lblMessage.CssClass = "form-success-msg";
                    lblMessage.Visible = true;
                }
            }
        }

        private void BindStudentGrid()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string statusFilter = ddlStudentStatus.SelectedValue;
                string query = "SELECT StudentID, FullName, Email, DateRegistered FROM dbo.Student";

                if (statusFilter == "1")
                {
                    query += " WHERE IsActive = 1";
                }
                else if (statusFilter == "0")
                {
                    query += " WHERE IsActive = 0";
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        gvStudents.DataSource = dt;
                        gvStudents.DataBind();
                    }
                }

                
                if (statusFilter == "0")
                {
                    btnStatusAction.Text = "Activate Account";
                }
                else
                {
                    btnStatusAction.Text = "Deactivate Account";
                }
            }
        }

        protected void ddlStudentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Visible = false;
            BindStudentGrid();
        }

        private List<int> GetSelectedStudentIDs()
        {
            List<int> selectedIds = new List<int>();

            foreach (GridViewRow row in gvStudents.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    selectedIds.Add(Convert.ToInt32(gvStudents.DataKeys[row.RowIndex].Value));
                }
            }
            return selectedIds;
        }

        protected void btnAddStudent_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddStudent.aspx");
        }

        protected void btnStatusAction_Click(object sender, EventArgs e)
        {
            List<int> selectedIds = GetSelectedStudentIDs();

            if (selectedIds.Count == 0)
            {
                lblMessage.Text = $"Please select a student to {btnStatusAction.Text.ToLower()}.";
                lblMessage.CssClass = "form-error-msg";
                lblMessage.Visible = true;
                return;
            }

            if (selectedIds.Count > 1)
            {
                lblMessage.Text = $"Please select only ONE student at a time to {btnStatusAction.Text.ToLower()}.";
                lblMessage.CssClass = "form-error-msg";
                lblMessage.Visible = true;
                return;
            }

            int selectedId = selectedIds[0];

            if (btnStatusAction.Text == "Activate Account")
            {
                
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "UPDATE dbo.Student SET IsActive = 1 WHERE StudentID = @StudentID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StudentID", selectedId);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                lblMessage.Text = "Student account activated successfully.";
                lblMessage.CssClass = "form-success-msg";
                lblMessage.Visible = true;
                BindStudentGrid();
            }
            else
            {
                
                lblMessage.Visible = false;
                Response.Redirect($"DeactivateStudent.aspx?id={selectedId}");
            }
        }

        protected void btnViewProgress_Click1(object sender, EventArgs e)
        {
            List<int> selectedIds = GetSelectedStudentIDs();

            if (selectedIds.Count == 0)
            {
                lblMessage.Text = "Please select a student to view their progress.";
                lblMessage.CssClass = "form-error-msg";
                lblMessage.Visible = true;
            }
            else if (selectedIds.Count > 1)
            {
                lblMessage.Text = "Please select only ONE student at a time to view progress.";
                lblMessage.CssClass = "form-error-msg";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;
                int selectedId = selectedIds[0];
                Response.Redirect($"ViewStudentProgress.aspx?id={selectedId}");
            }
        }

        protected void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            List<int> selectedIds = GetSelectedStudentIDs();

            if (selectedIds.Count == 0)
            {
                lblMessage.Text = "Please select a student to delete.";
                lblMessage.CssClass = "form-error-msg";
                lblMessage.Visible = true;
            }
            else if (selectedIds.Count > 1)
            {
                lblMessage.Text = "Please select only ONE student at a time to delete.";
                lblMessage.CssClass = "form-error-msg";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;
                int selectedId = selectedIds[0];
                Response.Redirect($"DeleteStudent.aspx?id={selectedId}");
            }
        }
    }
}