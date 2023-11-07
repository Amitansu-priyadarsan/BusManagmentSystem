using BusManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static BusManagementSystem.Models.CommonFn;
namespace BusManagmentSystem.Admin
{
    public partial class AdminHome : System.Web.UI.Page
    {
        Commonfnx fn = new Commonfnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PassCount();
                JSLPass();
                StarlingPass();
                SpecialPass();
                EmployeeCount();
            }
        }


        void EmployeeCount()
        {
            DataTable dt = fn.Fetch("SELECT COUNT(*) FROM Employee");
            Session["Total_Employee"] = dt.Rows[0][0];
        }
        void PassCount()
        {
            DataTable dt = fn.Fetch("SELECT COUNT(*) FROM Employee WHERE SurrenderDate IS NULL AND LeaveDate IS NULL");
            Session["Total_Pass"] = dt.Rows[0][0];
        }

        void JSLPass()
        {
            DataTable dt = fn.Fetch("Select Count(*) FROM Employee WHERE bus_type = 'JSL PASS'AND SurrenderDate IS NULL AND LeaveDate IS NULL");
            Session["JSL_Pass"] = dt.Rows[0][0];
        }

        void StarlingPass()
        {
            DataTable dt = fn.Fetch("Select Count(*) FROM Employee WHERE bus_type = 'STERLING'AND SurrenderDate IS NULL AND LeaveDate IS NULL");
            Session["Starling_Pass"] = dt.Rows[0][0];
        }

        void SpecialPass()
        {
            DataTable dt = fn.Fetch("Select Count(*) FROM Employee WHERE bus_type = 'Special Pass'AND SurrenderDate IS NULL AND LeaveDate IS NULL");
            Session["Special_Pass"] = dt.Rows[0][0];
        }
    }
}