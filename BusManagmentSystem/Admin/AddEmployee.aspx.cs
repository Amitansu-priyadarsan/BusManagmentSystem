using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BusManagementSystem.Models.CommonFn;

namespace BusManagmentSystem.Admin
{
    public partial class AddEmployee : Page
    {
        Commonfnx fn = new Commonfnx();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Populate dropdown lists on the initial page load
                GetDesignation();
                GetDepartment();
                GetPickupPoints();
                GetDropPoints();
                GetEmployees();
                GetBusTypes();
                GetBusCharges();


            }
        }

        private void GetDesignation()
        {
            DataTable dt = fn.Fetch("SELECT * FROM Designation");
            ddldesignation.DataSource = dt;
            ddldesignation.DataTextField = "DesignationName";
            ddldesignation.DataValueField = "DesignationId";
            ddldesignation.DataBind();
            ddldesignation.Items.Insert(0, new ListItem("Select Designation", "0"));
        }

        private void GetDepartment()
        {
            DataTable dt = fn.Fetch("SELECT * FROM Department");
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("Select Department", "0"));
        }

        private void GetPickupPoints()
        {
            DataTable dt = fn.Fetch("SELECT DISTINCT PickupPoint, StopId FROM StoppageMaster");
            ddlPickupPoint.DataSource = dt;
            ddlPickupPoint.DataTextField = "PickupPoint";
            ddlPickupPoint.DataValueField = "StopId";
            ddlPickupPoint.DataBind();
            ddlPickupPoint.Items.Insert(0, new ListItem("Select Pickup Point", "0"));
        }

        private void GetDropPoints()
        {
            DataTable dt = fn.Fetch("SELECT DISTINCT DropPoint, StopId FROM StoppageMaster");
            ddlDropPoint.DataSource = dt;
            ddlDropPoint.DataTextField = "DropPoint";
            ddlDropPoint.DataValueField = "StopId";
            ddlDropPoint.DataBind();
            ddlDropPoint.Items.Insert(0, new ListItem("Select Drop Point", "0"));
        }

        private void GetBusTypes()
        {
            DataTable dt = fn.Fetch("SELECT * FROM Pass");
            ddlBusType.DataSource = dt;
            ddlBusType.DataTextField = "PassName";
            ddlBusType.DataValueField = "PassId";
            ddlBusType.DataBind();
            ddlBusType.Items.Insert(0, new ListItem("Select Bus Type", "0"));
        }

        private void GetBusCharges()
        {
            DataTable dt = fn.Fetch("SELECT * FROM DesignationCharges");
            ddlCharges.DataSource = dt;
            ddlCharges.DataTextField = "Charge";
            ddlCharges.DataValueField = "DesignationChargeId";
            ddlCharges.DataBind();
            ddlCharges.Items.Insert(0, new ListItem("Select Charge Type", "0"));
        }




        private void GetEmployees()
        {
           // Define your SQL query
            string query = @"
                      SELECT 
    e.[EmployeeId], 
    [EmpCode], 
    [EmployeeName], 
    d.DesignationName, 
    dp.DepartmentName, 
    [Contact], 
    [SurrenderDate], 
    spmPickup.PickupPoint, 
    spmDrop.DropPoint, 
    e.[CreatedDate],
    e.bus_type AS [BUS TYPE], 
    e.BusFees
    
FROM [BusManagment].[dbo].[Employee] e
LEFT JOIN [dbo].[Designation] d ON d.DesignationId = e.Designation
LEFT JOIN [dbo].[Department] dp ON dp.DepartmentId = e.Department
LEFT JOIN [dbo].[Pass] p ON CAST(p.PassId AS VARCHAR) = e.bus_type 
LEFT JOIN [dbo].[DesignationCharges] dc ON dc.[DesignationId] = e.BusFees
LEFT JOIN [dbo].[StoppageMaster] spmPickup ON spmPickup.StopId = e.PickupPoint
LEFT JOIN [dbo].[StoppageMaster] spmDrop ON spmDrop.StopId = e.DropPoint
WHERE e.[IsActive] = 1;";



          //  Execute the query and retrieve data
            DataTable dt = fn.Fetch(query);

          //  Bind the data to your GridView
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
               // Get values from the form

               string EmployeeName = txtName.Text.Trim();
                string EmployeeId = txtEmployeeCode.Text.Trim();
                string Contact = txtContactNo.Text.Trim();
                DateTime? SurrenderDate = null;
                DateTime? LeaveDate = null; // Initialize LeaveDate
                DateTime tempDate;

                if (DateTime.TryParse(txtSurrenderDate.Text, out tempDate))
                {
                    SurrenderDate = tempDate;
                }

                if (DateTime.TryParse(txtLeaveDate.Text, out tempDate))
                {
                    LeaveDate = tempDate; // Set LeaveDate if a valid date is provided
                }

                string Designation = ddldesignation.SelectedValue;
                string DepartmentName = ddlDepartment.SelectedValue;
                string pickupPoint = ddlPickupPoint.SelectedValue;
                string DropPoint = ddlDropPoint.SelectedValue;
                string BusFees = ddlCharges.SelectedValue;
                string BusType = ddlBusType.SelectedItem.Text.ToString();
                string createdBy = Session["Username"].ToString();
                DateTime createdDate = DateTime.Now;


                //Check if the employee already exists based on their EmployeeId
               DataTable dt = fn.Fetch($"SELECT EmployeeId FROM Employee WHERE EmpCode = '{EmployeeId}'");

                if (dt.Rows.Count == 0)
                {
                    // Insert the new employee into the database
                    string query = "INSERT INTO Employee (EmployeeName, EmpCode, Contact, SurrenderDate, Designation, Department, bus_type, PickupPoint, DropPoint, BusFees, CreatedBy, CreatedDate) " +
      $"VALUES ('{EmployeeName}', '{EmployeeId}', '{Contact}', " +
      (SurrenderDate != null ? $"'{SurrenderDate:yyyy-MM-dd}'" : "null") + $", " +
      $"'{Designation}', '{DepartmentName}', '{BusType}', '{pickupPoint}', '{DropPoint}', '{BusFees}'," +
      $"'{createdBy}', GETDATE())";



                    fn.Query(query);

                  //  Refresh dropdown lists and grid view
                    GetDesignation();
                    GetDepartment();
                    GetPickupPoints();
                    GetDropPoints();
                    GetEmployees();
                    GetBusTypes();
                    GetBusCharges();
                }
                else
                {
                  //  Employee with the same EmployeeId already exists, handle this case
                   string errorMessage = "Employee ID already exists.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAlert", "alert('" + errorMessage + "');", true);
                    }
                }
            catch (Exception ex)
            {
              // Handle the exception, you can log or display an error message
                 //For now, let's display an alert
                string errorMessage = "An error occurred: " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAlert", "alert('" + errorMessage + "');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string searchString = txtSearch.Text.Trim();
            string query = @"
        SELECT 
            e.[EmployeeId], 
            [EmpCode], 
            [EmployeeName], 
            d.DesignationName, 
            dp.DepartmentName, 
            [Contact], 
            [SurrenderDate], 
            spmPickup.PickupPoint, 
            spmDrop.DropPoint, 
            e.bus_type AS [BUS TYPE],
            dc.Charge
        FROM [BusManagment].[dbo].[Employee] e
        LEFT JOIN [dbo].[Designation] d ON d.DesignationId = e.Designation
        LEFT JOIN [dbo].[Department] dp ON dp.DepartmentId = e.Department
        LEFT JOIN [dbo].[Pass] p ON CAST(p.PassId AS VARCHAR) = e.bus_type 
        LEFT JOIN [dbo].[DesignationCharges] dc ON dc.[DesignationId] = e.Designation
        LEFT JOIN [dbo].[StoppageMaster] spmPickup ON spmPickup.StopId = e.PickupPoint
        LEFT JOIN [dbo].[StoppageMaster] spmDrop ON spmDrop.StopId = e.DropPoint
        WHERE e.[IsActive] = 1";

            // If a search string is provided, add conditions to the query
            if (!string.IsNullOrEmpty(searchString))
            {
                query += " AND (EmployeeName LIKE @searchString OR DepartmentName LIKE @searchString OR " +
          "bus_type LIKE @searchString OR DesignationName LIKE @searchString OR EmpCode  LIKE @searchString OR " +
          " Charge  LIKE @searchString )";
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        command.Parameters.AddWithValue("@searchString", "%" + searchString + "%");
                    }

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }

    }
}
    