using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using static BusManagementSystem.Models.CommonFn;

namespace BusManagmentSystem
{
    public partial class LoginForm1 : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlog_Click(object sender, EventArgs e)
        {
            try
            {
                string username = inputUsername.Value.Trim();
                string password = inputPassword.Value.Trim();


                DataTable dt = fn.Fetch("SELECT * FROM [User] WHERE Username = '" + username + "' AND Password = '" + password + "'");

                if (dt.Rows.Count > 0)
                {
                    Session["Username"] = username;
                    Response.Redirect("Admin/AdminHome.aspx");
                }
                else
                {
                    lb1Msg.Text = "Login Failed!!";
                    lb1Msg.ForeColor = System.Drawing.Color.Red;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
