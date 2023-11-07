﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="OldRecord.aspx.cs" Inherits="BusManagmentSystem.Admin.OldRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .form-group {
            display: inline-block;
            vertical-align: top;
            margin-right: 20px;
        }

        .custom-button-back {
            background-color: red;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            float: left;
        }

        .custom-button-generate {
            background-color: #007BFF;
            color: #fff;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            cursor: pointer;
            float: right;
        }

        .custom-button-back {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 20px;
            margin-right: 10px;
        }

        .custom-button-generate {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 20px;
        }

        .custom-button-space {
            width: 10px;
            display: inline-block;
        }

        .gridview {
            border-collapse: collapse;
            width: 100%;
            margin: 20px 0;
        }

        .gridview th,
        .gridview td {
            border: 1px solid #ddd;
            text-align: left;
            padding: 8px;
        }

        .gridview th {
            background-color: #f2f2f2;
        }

        .submit-button {
            background-color: #007bff;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-md-9">
        <div class="form-group">
            <asp:Label ID="lblMonth" runat="server" Text="Month:"></asp:Label>
            <asp:DropDownList ID="ddlMonth" runat="server" Height="40px" Width="200px" BackColor="WhiteSmoke"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonth" InitialValue="" runat="server" ErrorMessage="Month is Required" ControlToValidate="ddlMonth" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" Height="40px" Width="200px" BackColor="WhiteSmoke"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" InitialValue="" runat="server" ErrorMessage="Year is Required" ControlToValidate="ddlYear" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submit-button" OnClick="btnSubmit_Click" style="margin-right: 10px;" />
           
        </div>

        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="gridview"></asp:GridView>
    </div>
</asp:Content>
