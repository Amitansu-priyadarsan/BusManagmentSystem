<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="GenerateIDcard.aspx.cs" Inherits="BusManagmentSystem.Admin.GenerateIDcard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Add your CSS styles here -->
    <style type="text/css">
        body {
            background-color: #f4f4f4;
            font-family: Arial, sans-serif;
        }

        .center-content {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }

        .box {
            border: 1px solid #ccc;
            width: 400px;
            padding: 18px;
            border-radius: 5px;
            background-color: #fff;
        }

        .input-box {
            margin: 10px 0;
            padding: 8px;
            width: 100%;
            box-sizing: border-box;
        }

        .label {
            font-weight: bold;
        }

        .button {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 20px;
            margin: 10px 0;
            cursor: pointer;
        }

            .button:hover {
                background-color: #0056b3;
            }

        #additionalFields {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
        }

            #additionalFields label,
            #additionalFields .input-box {
                width: calc(50% - 10px);
            }

        /* Style for the "Generate" text on the button */
        .button:after {
            content: "Generate";
        }

        .popup {
            display: none;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index: 1000;
            background-color: white;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        }

        .id-card-label {
            background-color: #007bff;
            color: #fff;
            padding: 10px;
            border-radius: 5px;
            font-weight: bold;
        }
        #btnSubmit {
    float: left;
}

#btnCancel {
    float: right;
    color: indianred;
    background-color: red; 
}

        #lblErrorMessage {
            margin-top: 10px;
            text-align: center;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="center-content">
                <div class="box">
                    <div>
                        <label for="txtEmployeeCode" class="label">Employee Code:</label>
                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="input-box" placeholder="Enter Employee Code" AutoPostBack="true" OnTextChanged="txtEmployeeCode_TextChanged"></asp:TextBox>
                        <asp:HiddenField ID="hdnEmployeeExists" runat="server" Value="0" />
                    </div>
                    <asp:Panel ID="additionalFields" runat="server" Visible="false">
                        <label for="txtName" class="label">Name:</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="input-box" placeholder="Employee Name"></asp:TextBox>

                        <label for="txtDesignation" class="label">Designation:</label>
                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="input-box" placeholder="Employee Designation"></asp:TextBox>


                        <label for="txtDepartment" class="label">Department:</label>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="input-box" placeholder="Employee Department"></asp:TextBox>

                        <label for="txtBusType" class="label">Bus Type:</label>
                        <asp:TextBox ID="txtBusType" runat="server" CssClass="input-box" placeholder="Bus Type"></asp:TextBox>

                        <label for="txtDateOfIssue" class="label">Date of Issue:</label>
                        <asp:TextBox ID="txtDateOfIssue" runat="server" CssClass="input-box" placeholder="Date of Issue"></asp:TextBox>

                        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="red" Visible="false"></asp:Label>
                        <br />
                         
                        <asp:Button ID="btnSubmit" runat="server" Text="Generate" CssClass="button" OnClick="btnSubmit_Click" />

                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />
                      

                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
