<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AddDesignation.aspx.cs" Inherits="BusManagmentSystem.Admin.AddDesignation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background-image:url('../Image/bg4.PNG'); width:100%; height:720px; background-repeat: no-repeat; background-size: cover; background-attachment:fixed; ">
        <div class="container p-md-4 p-sm-4 ">
            <div>
                <asp:Label ID="lblMsg" runat="server" ></asp:Label>
            </div>
            <h3 class="text-center"> DESIGNATION </h3>

            <div class="row mb-3 mr-1g-5 ml-1g-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtClass">Designation Name</label>
                    <asp:TextBox ID="txtClass" runat="server" CssClass="form-control" placeholder="Enter Designation Name" required></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3 mr-1g-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Add Designation" OnClick="btnAdd_Click" />
                </div>
            </div>
            
        <div class="row mb-3 mr-1g-5 ml-lg-5">
            <div class="col-md-6">
             <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="DesignationId" AutoGenerateColumns="False"
    EmptyDataText="No Record To Display" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
    OnRowUpdating="GridView1_RowUpdating" >
    <Columns>
        <asp:BoundField DataField="DesignationId" HeaderText="DesignationId" ReadOnly="True">
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Designation Name">
            <EditItemTemplate>
                <asp:TextBox ID="txtClassEdit" runat="server" Text='<%# Eval("DesignationName") %>' CssClass="form-control"></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
                <asp:Label ID="lblclassName" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" />
    </Columns>
    <HeaderStyle BackColor="#5558C9" ForeColor="White"/>
</asp:GridView>

                </div>
            </div>

        </div>
    </div>

</asp:Content>
