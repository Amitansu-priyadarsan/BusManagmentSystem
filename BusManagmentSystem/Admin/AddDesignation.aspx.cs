using static BusManagementSystem.Models.CommonFn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace BusManagmentSystem.Admin
{
    public partial class AddDesignation : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDesignation();
            }
        }

        private void GetDesignation()
        {
            DataTable dt = fn.Fetch("SELECT DesignationId, DesignationName FROM Designation;");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fetch("Select * from Designation where DesignationName = '" + txtClass.Text.Trim() + "' ");
                if (dt.Rows.Count == 0)
                {
                    DateTime currentdate = DateTime.Now;
                    string fdate = currentdate.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = "Insert into Designation(DesignationName, CreatedBy, CreatedDate) values( '" + txtClass.Text.Trim() + "', '" + (string)Session["Username"].ToString() + "', '" + fdate + "')";
                    fn.Query(query);
                    lblMsg.Text = "Inserted Succesfully!";
                    lblMsg.CssClass = "alert alert-success";
                    txtClass.Text = string.Empty;
                    GetDesignation();
                }
                else
                {
                    lblMsg.Text = "Entered Designation Already Existed!";
                    lblMsg.CssClass = "alert alert-danger ";

                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GetDesignation();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetDesignation();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDesignation(); 
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int cId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["DesignationId"]);
                string DesignationName = (row.FindControl("txtClassEdit") as TextBox).Text;
                fn.Query($"UPDATE Designation SET DesignationName = '{DesignationName}' WHERE DesignationId = {cId}");

                lblMsg.Text = "Class updated successfully";
                GridView1.EditIndex = -1;
                GetDesignation();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }

        }

    }
}

