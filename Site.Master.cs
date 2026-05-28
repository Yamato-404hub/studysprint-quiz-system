using System;
using System.Web;
using System.Web.UI;

namespace StudySprint
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateNavMenu();
        }

        private void UpdateNavMenu()
        {
            // Reset all placeholders
            phGuest.Visible = false;
            phStudent.Visible = false;
            phAdmin.Visible = false;

            // Toggle visibility based on role
            if (Session["Role"] != null)
            {
                string userRole = Session["Role"].ToString();

                if (userRole == "Student") phStudent.Visible = true;
                else if (userRole == "Admin") phAdmin.Visible = true;
            }
            else
            {
                phGuest.Visible = true;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session and redirect to login
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Pages/Login.aspx");
        }
    }
}