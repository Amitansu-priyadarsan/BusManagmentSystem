using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Globalization;
using System.Web.UI.WebControls;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace BusManagmentSystem.Admin
{
    public partial class MonthlyPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate the month DropDownList
                for (int i = 1; i <= 12; i++)
                {
                    ddlMonth.Items.Add(new ListItem(DateTimeFormatInfo.CurrentInfo.GetMonthName(i), i.ToString()));
                }

                // Populate the year DropDownList (adjust the range as needed)
                int currentYear = DateTime.Now.Year;
                for (int i = currentYear; i <= currentYear + 10000; i++)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle the submission of the form and filter data
            BindEmployeeData();
        }

        private void BindEmployeeData()
        {
            // Get the connection string from the web.config file
            string connectionString = ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Construct the query based on the selected month and year
                string selectedMonth = ddlMonth.SelectedValue;
                string selectedYear = ddlYear.SelectedValue;

                // The modified query retrieves data for the selected month and all previous months
                string queryString = @"
                   SELECT 
    e.[EmployeeId],  -- Employee ID
    [EmpCode], 
    [EmployeeName], 
    d.DesignationName AS [Designation],  -- Display Designation Name
    dp.DepartmentName AS [Department],  -- Display Department Name
    [Contact], 
    [SurrenderDate], 
    spmPickup.PickupPoint AS [PickupLocation],  -- Display Pickup Location
    spmDrop.DropPoint AS [DropLocation],  -- Display Drop Location
    e.[CreatedDate],
    e.bus_type AS [BUS TYPE], 
    dc.[Charge] AS [BusFees],  -- Display Bus Fees
    [LeaveDate]
FROM [BusManagment].[dbo].[Employee] e
LEFT JOIN [dbo].[Designation] d ON d.DesignationId = e.Designation
LEFT JOIN [dbo].[Department] dp ON dp.DepartmentId = e.Department
LEFT JOIN [dbo].[Pass] p ON CAST(p.PassId AS VARCHAR) = e.bus_type 
LEFT JOIN [dbo].[DesignationCharges] dc ON dc.[DesignationId] = e.BusFees
LEFT JOIN [dbo].[StoppageMaster] spmPickup ON spmPickup.StopId = e.PickupPoint
LEFT JOIN [dbo].[StoppageMaster] spmDrop ON spmDrop.StopId = e.DropPoint
WHERE e.[IsActive] = 1

                    AND (YEAR(e.[CreatedDate]) < @SelectedYear
                        OR (YEAR(e.[CreatedDate]) = @SelectedYear AND MONTH(e.[CreatedDate]) <= @SelectedMonth));";

                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.Parameters.AddWithValue("@SelectedMonth", selectedMonth);
                    command.Parameters.AddWithValue("@SelectedYear", selectedYear);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Bind the data to a GridView control (assuming you have one on your page).
                    GridView1.DataSource = reader;
                    GridView1.DataBind();
                    reader.Close();
                }
            }
        }

        protected void btnMonthlyReport_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("EmployeeData");

                // Fetch data from the database based on the selected month and year
                string connectionString = ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Construct the SQL query based on the selected month and year
                    string selectedMonth = ddlMonth.SelectedValue;
                    string selectedYear = ddlYear.SelectedValue;
                    string queryString = @"
                      SELECT 
    e.[EmployeeId],  -- Employee ID
    [EmpCode], 
    [EmployeeName], 
    d.DesignationName AS [Designation],  -- Display Designation Name
    dp.DepartmentName AS [Department],  -- Display Department Name
    [Contact], 
    [SurrenderDate], 
    spmPickup.PickupPoint AS [PickupLocation],  -- Display Pickup Location
    spmDrop.DropPoint AS [DropLocation],  -- Display Drop Location
    e.[CreatedDate],
    e.bus_type AS [BUS TYPE], 
    dc.[Charge] AS [BusFees],  -- Display Bus Fees
    [LeaveDate]
FROM [BusManagment].[dbo].[Employee] e
LEFT JOIN [dbo].[Designation] d ON d.DesignationId = e.Designation
LEFT JOIN [dbo].[Department] dp ON dp.DepartmentId = e.Department
LEFT JOIN [dbo].[Pass] p ON CAST(p.PassId AS VARCHAR) = e.bus_type 
LEFT JOIN [dbo].[DesignationCharges] dc ON dc.[DesignationId] = e.BusFees
LEFT JOIN [dbo].[StoppageMaster] spmPickup ON spmPickup.StopId = e.PickupPoint
LEFT JOIN [dbo].[StoppageMaster] spmDrop ON spmDrop.StopId = e.DropPoint
WHERE e.[IsActive] = 1

                        AND (YEAR(e.[CreatedDate]) < @SelectedYear
                            OR (YEAR(e.[CreatedDate]) = @SelectedYear AND MONTH(e.[CreatedDate]) <= @SelectedMonth));";

                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedMonth", selectedMonth);
                        command.Parameters.AddWithValue("@SelectedYear", selectedYear);

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

                        // Write data with formatting and calculate deduction
                        while (reader.Read())
                        {
                            col = 1;
                            row++;
                            int columnCount = reader.FieldCount;

                            for (int i = 0; i < columnCount; i++)
                            {
                                // Code omitted for brevity
                            }

                            // Calculate deduction and add it to the "deduction" column
                            double deduction = CalculateDeduction(reader);
                            worksheet.Cells[row, col].Value = deduction;

                            // Add conditional formatting for deduction values
                            using (ExcelRange deductionCell = worksheet.Cells[row, col, row, col])
                            {
                                deductionCell.Style.Font.Color.SetColor(deduction < 0.0 ? Color.Red : Color.Green);
                            }
                            if (deduction > 0)
                            {
                                InsertDeductionRecords(connection, reader, selectedMonth, selectedYear);


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
        private void InsertDeductionRecords(SqlConnection connection, SqlDataReader reader, string selectedMonth, string selectedYear)
        {
            while (reader.Read())
            {
                double deductionAmount = CalculateDeduction(reader); // Rename the variable to "deductionAmount"
                int EmpCode = reader.GetInt32(reader.GetOrdinal("EmployeeId"));

                // Adjust the SQL statement based on your table structure
                string insertQuery = "INSERT INTO TransactionTable (EmployeeId, EmployeeName, CreatedDate, DeductionAmount, Month, Year) VALUES (@EmployeeId, @EmployeeName, @CreatedDate, @DeductionAmount, @Month, @Year)";

                using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@EmployeeId", EmpCode);
                    insertCommand.Parameters.AddWithValue("@EmployeeName", reader["EmployeeName"]);
                    insertCommand.Parameters.AddWithValue("@CreatedDate", reader["CreatedDate"]);
                    insertCommand.Parameters.AddWithValue("@DeductionAmount", deductionAmount); // Use the renamed variable
                    insertCommand.Parameters.AddWithValue("@Month", selectedMonth);
                    insertCommand.Parameters.AddWithValue("@Year", selectedYear);

                    // Execute the insert command
                    insertCommand.ExecuteNonQuery();
                   
                }
            }
            reader.Close();
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
                        return busFees;
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

    }
}
