<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="BusManagmentSystem.Admin.AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="Bus_Management_System_Css1/Home.css" rel="stylesheet" />
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet" id="bootstrap-css">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image: url('../Image/bg3.PNG'); height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container p-md-4 p-sm-4 ">
            <div class="table-responsive">
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h2 class="text-center">Home Page </h2>
            <br />
            <h5 class="text-left text-black-50">
               
                <div class="bg-warning text-dark p-2 d-inline-block ">
                    Total Issued Pass - 
        <span class="count-numbers"><% Response.Write(Session["Total_Employee"]); %></span>
                </div>
            </h5>
            <br />
            <link href="../Content/Home.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />          
<div class="container">
    <div class="row ">
    <div class="col-md-3">
      <div class="card-counter primary">
        <i class="fa fa-code-fork"></i>
        <span class="count-numbers"> <%Response.Write(Session["Total_Pass"]); %></span>
        <span class="count-name">Total Active Pass</span>
      </div>
    </div>

    <div class="col-md-3">
      <div class="card-counter danger">
        <i class="fa fa-ticket"></i>
        <span class="count-numbers"> <%Response.Write(Session["JSL_Pass"]); %></span>
        <span class="count-name">JSL pass</span>
      </div>
    </div>

    <div class="col-md-3">
      <div class="card-counter success">
        <i class="fa fa-database"></i>
        <span class="count-numbers"> <%Response.Write(Session["Starling_Pass"]); %></span>
        <span class="count-name">Starling Pass</span>
      </div>
    </div>

    <div class="col-md-3">
      <div class="card-counter info">
        <i class="fa fa-users"></i>
        <span class="count-numbers"> <%Response.Write(Session["Special_Pass"]); %> </span>
        <span class="count-name">Special Pass</span>
      </div>
    </div>
  </div>
</div>




    
</asp:Content>
