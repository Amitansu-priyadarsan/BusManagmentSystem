<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneateReport.aspx.cs" Inherits="BusManagmentSystem.Admin.GeneateReport" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Generate Employee Report</title>
    <style>
        /* GridView styles */
        .gridview {
            border-collapse: collapse;
            width: 100%;
            margin: 20px 0;
        }

        .gridview th, .gridview td {
            border: 1px solid #ddd;
            text-align: left;
            padding: 8px;
        }

        .gridview th {
            background-color: #f2f2f2;
        }

        /* Style the "Back" button */
        .custom-button-back {
            background-color: red;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            float: left; /* Align to the left */
        }

        /* Style the "Generate Report" button */
        .custom-button-generate {
            background-color: #007BFF;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            float: right; /* Align to the right */
        }
     .custom-button-back {
        background-color: #007bff;
        color: #fff;
        border: none;
        padding: 10px 20px;
        margin-right: 10px; /* Add margin to the right for spacing */
    }

    .custom-button-generate {
        background-color: #007bff;
        color: #fff;
        border: none;
        padding: 10px 20px;
    }

    .custom-button-space {
        width: 10px; /* Adjust the width to control the spacing */
        display: inline-block;
    }
        /* Style the button on hover */
        .custom-button:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <asp:Button ID="Button1" runat="server" Text="Back" OnClick="Button1_Click" CssClass="custom-button-back" Width="73px" />
<asp:Button ID="btnGetMonthlyReport" runat="server" Text="Generate Report" OnClick="btnGetMonthlyReport_Click" CssClass="custom-button-generate" />

<!-- Add a non-breaking space ( ) to create spacing between the buttons -->
&nbsp;

<asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
<asp:TextBox ID="txtSearch" runat="server" Width="140px"></asp:TextBox>
<asp:Button ID="btnSearch" runat="server"  BorderColor="#1E90FF" ForeColor="#1E90FF" BackColor="#FFFFFF" Text="Search" OnClick="btnSearch_Click1" />


            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="gridview"></asp:GridView>
        </div>
    </form>
</body>
</html>
