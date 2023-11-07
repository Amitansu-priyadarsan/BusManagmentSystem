using static BusManagementSystem.Models.CommonFn;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagmentSystem.Admin
{
    public partial class AddDepartment : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDepartments();
            }
        }

        private void GetDepartments()
        {
            DataTable dt = fn.Fetch("SELECT DepartmentId, DepartmentName FROM Department;");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = fn.Fetch("Select * from Department where DepartmentName = '" + txtDepartment.Text.Trim() + "' ");
                if (dt.Rows.Count == 0)
                {
                    DateTime currentDate = DateTime.Now;
                    string formattedDate = currentDate.ToString("yyyy-MM-dd HH:mm:ss");
                    string query = "Insert into Department(DepartmentName, CreatedBy, CreatedDate) values( '" + txtDepartment.Text.Trim() + "', '" + (string)Session["Username"].ToString() + "', '" + formattedDate + "')";
                    fn.Query(query);
                    lblMsg.Text = "Department Inserted Successfully!";
                    lblMsg.CssClass = "alert alert-success";
                    txtDepartment.Text = string.Empty;
                    GetDepartments();
                }
                else
                {
                    lblMsg.Text = "Entered Department Already Exists!";
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
            GetDepartments();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GetDepartments();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GetDepartments();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int departmentId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["DepartmentId"]);
                string departmentName = (row.FindControl("txtDepartmentEdit") as TextBox).Text;
                fn.Query($"UPDATE Department SET DepartmentName = '{departmentName}' WHERE DepartmentId = {departmentId}");

                lblMsg.Text = "Department updated successfully";
                GridView1.EditIndex = -1;
                GetDepartments();
            }
            catch (Exception ex)
            {
                lblMsg.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
