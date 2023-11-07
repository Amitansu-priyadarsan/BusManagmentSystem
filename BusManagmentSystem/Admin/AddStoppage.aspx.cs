using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BusManagementSystem.Models.CommonFn;

namespace BusManagmentSystem.Admin
{
    public partial class AddStoppageMaster : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStoppages();
            }
        }

        private void GetStoppages()
        {
            DataTable dt = fn.Fetch("SELECT StopId, PickupPoint, DropPoint FROM StoppageMaster;");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                string formattedDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                string query = "INSERT INTO StoppageMaster (PickupPoint, DropPoint, CreatedBy, CreatedDate) " +
                 "VALUES ('" + txtPickupPoint.Text.Trim() + "', '" + txtDropPoint.Text.Trim() + "', '" + (string)Session["Username"].ToString() + "', '" + formattedDate + "')";


                fn.Query(query);
                lblMsg.Text = "Stoppage Inserted Successfully!";
                lblMsg.CssClass = "alert alert-success";
                txtPickupPoint.Text = string.Empty;
                txtDropPoint.Text = string.Empty;
                GetStoppages();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }
       




        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetStoppages();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetStoppages();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetStoppages();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int stoppageId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["StopId"]);
                string pickupPoint = (row.FindControl("txtPickupPointEdit") as TextBox).Text;
                string dropPoint = (row.FindControl("txtDropPointEdit") as TextBox).Text;
                fn.Query($"UPDATE StoppageMaster SET PickupPoint = '{pickupPoint}', DropPoint = '{dropPoint}' WHERE StopId = {stoppageId}");

                lblMsg.Text = "Stoppage updated successfully";
                GridView1.EditIndex = -1;
                GetStoppages();
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
                int stoppageId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["StopId"]);
                fn.Query($"DELETE FROM StoppageMaster WHERE StopId = {stoppageId}");

                lblMsg.Text = "Stoppage deleted successfully";
                GetStoppages();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
