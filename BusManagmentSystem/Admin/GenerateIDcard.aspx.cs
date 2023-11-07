using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Xml.Linq;

namespace BusManagmentSystem.Admin
{
    public partial class GenerateIDcard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // This code should only run during the initial page load
                ResetAdditionalFields();
            }
        }


        protected void txtEmployeeCode_TextChanged(object sender, EventArgs e)
        {
            string employeeCode = txtEmployeeCode.Text.Trim();
            if (string.IsNullOrEmpty(employeeCode))
            {
                ResetAdditionalFields();
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT e.EmployeeName, d.DesignationName, dp.DepartmentName, e.bus_type AS BusType, e.CreatedDate AS DateOfIssue " +
                               "FROM Employee AS e " +
                               "LEFT JOIN Designation AS d ON e.Designation = d.DesignationId " +
                               "LEFT JOIN Department AS dp ON e.Department = dp.DepartmentId " +
                               "WHERE e.EmpCode = @EmployeeCode";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtName.Text = reader["EmployeeName"].ToString();
                        txtDesignation.Text = reader["DesignationName"].ToString();
                        txtDepartment.Text = reader["DepartmentName"].ToString();
                        txtBusType.Text = reader["BusType"].ToString();
                        txtDateOfIssue.Text = reader["DateOfIssue"].ToString();
                        hdnEmployeeExists.Value = "1";
                        lblErrorMessage.Visible = false; // Hide any previous error message
                    }
                    else
                    {
                        ResetAdditionalFields();
                        lblErrorMessage.Text = "Invalid Employee Code. Please enter a valid code.";
                        lblErrorMessage.Visible = true;
                        additionalFields.Visible = false;
                    }
                }
            }

            // Make the additionalFields Panel visible only if a valid employee code is found
            additionalFields.Visible = hdnEmployeeExists.Value == "1";
        }


        protected void ResetAdditionalFields()
        {
            txtName.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            txtBusType.Text = string.Empty;
            txtDateOfIssue.Text = string.Empty;
            hdnEmployeeExists.Value = "0";
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string busType = txtBusType.Text.ToLower(); // Convert to lowercase for case-insensitive comparison

            if (busType.Contains("special pass"))
            {
                // Redirect to Icard2 if "Special Pass" is in the bus type
                string url = "Icard2.aspx";
                SetupRedirect(url);
            }
            else if (busType.Contains("jsl pass"))
            {
                // Redirect to Icard1 if "JSL Pass" is in the bus type
                string url = "Icard1.aspx";
                SetupRedirect(url);
            }
            else if (busType.Contains("sterling"))
            {
                // Redirect to Icard3 if "Sterling Pass" is in the bus type
                string url = "Icard3.aspx";
                SetupRedirect(url);
            }
        }

        private void SetupRedirect(string url)
        {
            url += "?EmployeeCode=" + txtEmployeeCode.Text;
            url += "&EmployeeName=" + txtName.Text;
            url += "&Designation=" + txtDesignation.Text;
            url += "&Department=" + txtDepartment.Text;
            url += "&DateOfIssue=" + txtDateOfIssue.Text;

            Response.Redirect(url);
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminHome.aspx");
        }

    }
} 




