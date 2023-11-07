using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BusManagmentSystem.Admin
{
    public partial class Icard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["EmployeeCode"] != null)
                {
                    string employeeCode = Request.QueryString["EmployeeCode"];
                    string employeeName = Request.QueryString["EmployeeName"];
                    string designation = Request.QueryString["Designation"];
                    string department = Request.QueryString["Department"];
                    string dateOfIssue = Request.QueryString["DateOfIssue"];

                  
                   txtEmployeeName.Text = HttpUtility.HtmlEncode(employeeName);
                    txtEmployeeCode.Text = HttpUtility.HtmlEncode(employeeCode);
                    txtDesignation.Text = HttpUtility.HtmlEncode(designation);
                    txtDepartment.Text = HttpUtility.HtmlEncode(department);
                    txtDateOfIssue.Text = HttpUtility.HtmlEncode(dateOfIssue);
                }
            }
        }

        protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
        {
         
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminHome.aspx");
        }
    }
}
