using System;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using static BusManagementSystem.Models.CommonFn;

namespace BusManagmentSystem.Admin
{
    public partial class DesignationFees : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetFees();
                GetDesignationFees();
            }
        }

        private void GetDesignationFees()
        {
            DataTable dt = fn.Fetch("SELECT * FROM Designation");
            ddldesignation.DataSource = dt;
            ddldesignation.DataTextField = "DesignationName";
            ddldesignation.DataValueField = "DesignationId";
            ddldesignation.DataBind();
            ddldesignation.Items.Insert(0, "Select Designation");
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string DesignationVal = ddldesignation.SelectedItem.Text;
                DataTable dt = fn.Fetch("SELECT * FROM DesignationCharges WHERE DesignationChargeId = '" + ddldesignation.SelectedItem.Value + "'");

                if (dt.Rows.Count == 0)
                {
                    DateTime currentdate = DateTime.Now;
                    string fdate = currentdate.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = "INSERT INTO DesignationCharges(DesignationChargeId, DesignationName, Charge, CreatedBy, CreatedDate) " +
                                   "VALUES('" + ddldesignation.SelectedItem.Value + "', '" + ddldesignation.SelectedItem.Text + "', " + txtFeeAmounts.Text.Trim() +
                                   ", '" + (string)Session["Username"].ToString() + "', '" + fdate + "')";
                    fn.Query(query);

                    lblMsg.Text = "Inserted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    txtFeeAmounts.Text = string.Empty;
                    GetDesignationFees();
                    GetFees();
                }
                else
                {
                    lblMsg.Text = "Entered Fees Already Exist for <b>'" + DesignationVal + "'</b>";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }

        private void GetFees()
        {
            DataTable dt = fn.Fetch(@"SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS [DesignationChargeId],  c.DesignationName, f.Charge FROM DesignationCharges f INNER JOIN Designation c ON c.DesignationId = f.DesignationChargeId");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int feesId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                TextBox txtCharge = row.FindControl("txtcharge") as TextBox; // Find the TextBox control

                if (txtCharge != null)
                {
                    string feeAmt = txtCharge.Text.Trim();
                    fn.Query($"UPDATE DesignationCharges SET Charge = {feeAmt} WHERE DesignationChargeId = {feesId}");

                    lblMsg.Text = "Row updated successfully";
                    GridView1.EditIndex = -1; // Exit edit mode
                    GetFees(); // Rebind the GridView to reflect the changes
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetFees();
        }

        protected void  GrideView1_RowCancelingEditing(object sender, GridViewCancelEditEventArgs e)
        {
            // Exit edit mode and rebind the GridView to display the original data
            GridView1.EditIndex = -1;
            GetFees();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetFees();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetFees();
        }
    }
} 
