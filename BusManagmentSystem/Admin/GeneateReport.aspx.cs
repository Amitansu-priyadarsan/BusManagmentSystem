using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace BusManagmentSystem.Admin
{
    public partial class GeneateReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployeeData();
            }
        }

        protected void btnGetMonthlyReport_Click(object sender, EventArgs e)
        {
            // Implement your logic to generate the Excel report.
            GenerateExcelReport();
        }

        private void BindEmployeeData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string queryString = @"
          
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
    e.BusFees -- Add this line to include the BusFees column
    
FROM [BusManagment].[dbo].[Employee] e
LEFT JOIN [dbo].[Designation] d ON d.DesignationId = e.Designation
LEFT JOIN [dbo].[Department] dp ON dp.DepartmentId = e.Department
LEFT JOIN [dbo].[Pass] p ON CAST(p.PassId AS VARCHAR) = e.bus_type 
LEFT JOIN [dbo].[DesignationCharges] dc ON dc.[DesignationId] = e.BusFees
LEFT JOIN [dbo].[StoppageMaster] spmPickup ON spmPickup.StopId = e.PickupPoint
LEFT JOIN [dbo].[StoppageMaster] spmDrop ON spmDrop.StopId = e.DropPoint
WHERE e.[IsActive] = 1;";

                // Assuming your table name is "employee"
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Bind the data to a GridView control (assuming you have one on your page).
                    GridView1.DataSource = reader;
                    GridView1.DataBind();
                }
            }
        }

        private void GenerateExcelReport()
        {
            DateTime currentDate = DateTime.Now; 
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("EmployeeData");

                // Fetch data from the database (replace with your own logic)
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryString = "SELECT * FROM employee";  // Assuming your table name is "employee"
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        int col = 1;
                        int row = 1;

                        // Write column headers with formatting
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            worksheet.Cells[row, col].Value = reader.GetName(i);

                            // Add formatting for headers
                            using (ExcelRange headerCell = worksheet.Cells[row, col, row, col])
                            {
                                headerCell.Style.Font.Bold = true;
                                headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                headerCell.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                headerCell.Style.Font.Color.SetColor(Color.White);
                            }

                            col++;
                        }

                        // Add a "deduction" column header
                        worksheet.Cells[row, col].Value = "Deduction";

                        // Add formatting for the "Deduction" header
                        using (ExcelRange headerCell = worksheet.Cells[row, col, row, col])
                        {
                            headerCell.Style.Font.Bold = true;
                            headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            headerCell.Style.Fill.BackgroundColor.SetColor(Color.Gray);
                            headerCell.Style.Font.Color.SetColor(Color.White);
                        }

                        col++;

                        // Write data with formatting
                        while (reader.Read())
                        {
                            col = 1;
                            row++;

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (!reader.IsDBNull(i))
                                {
                                    if (reader.GetDataTypeName(i) == "date")
                                    {
                                        // Handle date formatting here
                                        DateTime dateValue = reader.GetDateTime(i);
                                        worksheet.Cells[row, col].Value = dateValue.ToString("yyyy-MM-dd"); // Adjust the format as needed
                                    }
                                    else
                                    {
                                        worksheet.Cells[row, col].Value = reader[i];
                                    }
                                }
                                else
                                {
                                    // Handle null values if necessary
                                    worksheet.Cells[row, col].Value = "N/A"; // Or any other representation you prefer
                                }

                                col++;
                            }

                            // Calculate deduction and add it to the "deduction" column
                            double deduction = CalculateDeduction(reader);
                            worksheet.Cells[row, col].Value = deduction;

                            // Add conditional formatting for deduction values
                            using (ExcelRange deductionCell = worksheet.Cells[row, col, row, col])
                            {
                                deductionCell.Style.Font.Color.SetColor(deduction < 0.0 ? Color.Red : Color.Green);
                            }
                        }
                    }
                }

                // Save the Excel file
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment; filename=EmployeeData.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(excelPackage.GetAsByteArray());
                Response.End();
            }
        }

        private double CalculateDeduction(SqlDataReader reader)
        {
            // Get relevant data from the reader
            DateTime issuanceDate = reader.IsDBNull(reader.GetOrdinal("CreatedDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("CreatedDate"));
            DateTime returnDate = reader.IsDBNull(reader.GetOrdinal("LeaveDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("LeaveDate"));
            DateTime surrenderDate = reader.IsDBNull(reader.GetOrdinal("SurrenderDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("SurrenderDate"));

            double busFees = 0.0;

            // Check if the "BusFees" column is not NULL
            int busFeesColumnIndex = reader.GetOrdinal("BusFees");
            if (!reader.IsDBNull(busFeesColumnIndex))
            {
                // Try to get the "BusFees" as a double
                if (double.TryParse(reader[busFeesColumnIndex].ToString(), out busFees))
                {
                    // Check for surrender
                    if (surrenderDate != DateTime.MinValue)
                    {
                        // Surrendered, so deduction is 0
                        return 0.0;
                    }

                    // Check if there's no leave or surrender date
                    if (returnDate == DateTime.MinValue && surrenderDate == DateTime.MinValue)
                    {
                        // No leave or surrender, use BusFees as the deduction
                        return busFees;
                    }
                   

                    // Your deduction calculation logic here
                    if (issuanceDate.Day <= 25 && returnDate.Day >= 5)
                    {
                        // No deduction, full payment
                        return 0.0;
                    }
                    else
                    {
                        // Calculate deduction based on your criteria
                        // Example: Deduct 50% for any other cases
                        return busFees ;
                    }

                }
                DateTime nextMonth25 = issuanceDate.AddMonths(1);
                nextMonth25 = new DateTime(nextMonth25.Year, nextMonth25.Month, 25);


                if (issuanceDate.Day <= 25)
                {
                    // Issued on or before the 25th, so money should be calculated for the next month
                    if (returnDate >= nextMonth25)
                    {
                        // After the 25th of the following month, money should be calculated
                        return busFees;
                    }
                    else
                    {
                        // Within the first month, no money is calculated
                        return 0.0;
                    }
                }
                else
                {
                    // Issued after the 25th, so money should be calculated for the next month
                    return busFees;
                }
            }

          
            // Handle other cases, including NULL values or non-numeric data
            return 0.0; // You can adjust this as needed
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminHome.aspx");
        }

       

        protected void btnSearch_Click1(object sender, EventArgs e)
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

    
    


