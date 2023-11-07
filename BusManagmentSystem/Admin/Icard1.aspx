<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Icard1.aspx.cs" Inherits="BusManagmentSystem.Admin.Icard1" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE- edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title></title>
    <style>
        #bus-pass {
            border: 3px solid black;
            width: 644px;
            margin: 0 auto;
            padding: 0;
            text-align: center;
            height: 255px;
            background-color: aliceblue;
        }

        #img-logo {
            width: 161px;
            margin-top: 10px;
            float: initial;
            height: 20px;
        }

        #logo-details {
            text-align: center; 
        }

        #bus-number {
            color: #0aac12;
            font-size: 14px;
            font-weight: bold;
            margin-top: 5px;
            text-align: right;
        }

        #dist-text {
            font-size: 14px;
            font-weight: bold;
            margin-top: 5px;
            height: 1px;
            width: 660px;
        }

        #pass-details {
            margin-top: 5px;
            height: 43px;
            width: 637px;
        }

        #pass-title {
            font-size: 30px;
            font-weight: bold;
            color: red;
            text-decoration: underline;
            text-decoration-color: red;
            text-underline-position: under;
          
            border: 3px solid rgba(255, 255, 255, 0.5);
        }

        #hr1 {
            border: 1px solid black;
            background-color: black;
            height: 1px;
            margin-top: 20px;
        }

        #pass-holder-info {
            float: left;
            width: 80%;
            margin: 0;
            padding: 0;
            height: 100px;
        }

        #photo-container {
            float: right;
            width: 99px;
            height: 105px;
            text-align: center;
            border-radius: 10px;
            border: 2px solid black;
            margin-right: 10px;
            margin-bottom: 10px;
            margin-left: 0;
            margin-top: 0;
        }

        .input-label {
            font-weight: bold;
            margin-right: 10px; 
        }

        .input-text {
            padding: 5px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 14px;
        }

        #signature-label {
            float: right;
            margin-right: 10px;
            margin-top: 35px;
            font-size: 16px;
            height: 13px;
            width: 197px;
        }

        @media print {
            .print-button {
                display: none;
            }
        }

        .custom-button {
            background-color: #007bff;
            color: #fff;
            padding: 10px 20px;
            border: none;
            cursor: pointer;
        }

        .form-group {
            width: 497px; 
            text-align: right;
            height: 25px;
        }

            .form-group label {
                font-size: 16px; 
            }
    </style>

</head>
<body id="buss-pass1">
    <form id="form1" runat="server">
        <div id="bus-pass">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img id="img-logo" src="../Image/logo.jpg" alt="Logo">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sl.No...............
            <p id="dist-text">KNIC,DIST.JAJPUR-755026, Odisha,India</p>
            <div id="pass-details" class="underline">
                <label id="pass-title">B U S P A S S</label>
            </div>
            <div id="pass-holder-info">
                <div class="form-group">
                    <asp:Label runat="server" Text="Employee Name" CssClass="input-label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEmployeeName" CssClass="input-text" Width="359px" Height="16px"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label runat="server" Text="Employee Code" CssClass="input-label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtEmployeeCode" CssClass="input-text" Width="359px"></asp:TextBox>

                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Designation" CssClass="input-label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDesignation" CssClass="input-text" Width="359px" Height="16px"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Department" CssClass="input-label"></asp:Label>
                    <asp:TextBox runat="server" CssClass="input-text" ID="txtDepartment" Width="359px" Height="16px"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:Label runat="server" Text="Date of Issue" CssClass="input-label"></asp:Label>
                    <asp:TextBox runat="server" ID="txtDateOfIssue" Width="359px" Height="16px" CssClass="input-text"></asp:TextBox>
                </div>
            </div>
            <div id="photo-container">
            </div>
            <div id="signature-label">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Authorized Signature
            </div>


        </div>
        <div>
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" CssClass="custom-button" />
             <asp:Button ID="btn_Back" runat="server" Text="Back" OnClick="btn_Back_Click" CssClass="custom-button" />

        </div>
    </form>


</body>
</html>
