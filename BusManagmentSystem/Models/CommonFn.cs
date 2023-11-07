using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BusManagementSystem.Models
{
    public class CommonFn
    {
        
        public class Commonfnx
        {
            // Create a SqlConnection using the connection string from the configuration file.
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["BusCS"].ConnectionString);

            public void Query(string query, SqlParameter[] parameters = null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                // Execute a SQL query that doesn't return a result (e.g., INSERT, UPDATE, DELETE).
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    cmd.ExecuteNonQuery();
                }
                // Close the connection
                con.Close();
            }
            // Execute a SQL query and return the result as a DataTable.
            public DataTable Fetch(string query, SqlParameter[] parameters = null)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    // Fill the DataTable with the result of the query.
                    return dt;
                }
            }
        }
    }
}
