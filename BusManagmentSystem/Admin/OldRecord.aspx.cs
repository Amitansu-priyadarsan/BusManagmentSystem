using System;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;

namespace BusManagmentSystem.Admin
{
    public partial class OldRecord : Page
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
            int selectedMonth = int.Parse(ddlMonth.SelectedValue);
            int selectedYear = int.Parse(ddlYear.SelectedValue);

            // Fetch data from the TransactionTable based on selectedMonth and selectedYear
            DataTable transactions = FetchTransactions(selectedMonth, selectedYear);

            // Bind the retrieved data to the GridView
            GridView1.DataSource = transactions;
            GridView1.DataBind();
        }
        private DataTable FetchTransactions(int month, int year)
        {
            // Use the provided connection string
            string connectionString = "Data Source=JSLJRDPC1472; Initial Catalog=BusManagment; User Id=sa; Password=anujchess12";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Modify the SQL query to select the relevant columns that exist in your table
                string query = "SELECT EmployeeId, EmployeeName, CreatedDate, DeductionAmount, Month, Year FROM TransactionTable WHERE Month = @Month AND Year = @Year";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Month", month);
                    cmd.Parameters.AddWithValue("@Year", year);

                    connection.Open();

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}

