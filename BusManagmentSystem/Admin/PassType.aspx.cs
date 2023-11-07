using static BusManagementSystem.Models.CommonFn;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagmentSystem.Admin
{
    public partial class AddPass : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPasses();
            }
        }

        private void GetPasses()
        {
            DataTable dt = fn.Fetch("SELECT PassId, PassName FROM Pass;");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fetch("Select * from Pass where PassName = '" + txtPass.Text.Trim() + "' ");
                if (dt.Rows.Count == 0)
                {
                    DateTime currentDate = DateTime.Now;
                    string formattedDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = "Insert into Pass(PassName, CreatedBy, CreatedDate) values( '" + txtPass.Text.Trim() + "', '" + (string)Session["Username"].ToString() + "', '" + formattedDate + "')";
                    fn.Query(query);
                    lblMsg.Text = "Pass Inserted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    txtPass.Text = string.Empty;
                    GetPasses();
                }
                else
                {
                    lblMsg.Text = "Entered Pass Already Exists!";
                    lblMsg.CssClass = "alert alert-danger ";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetPasses();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetPasses();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetPasses();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int passId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["PassId"]);
                string passName = (row.FindControl("txtPassEdit") as TextBox).Text;
                fn.Query($"UPDATE Pass SET PassName = '{passName}' WHERE PassId = {passId}");

                lblMsg.Text = "Pass updated successfully";
                GridView1.EditIndex = -1;
                GetPasses();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int passId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["PassId"]);
                fn.Query($"DELETE FROM Pass WHERE PassId = {passId}");

                lblMsg.Text = "Pass deleted successfully";
                GetPasses();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
