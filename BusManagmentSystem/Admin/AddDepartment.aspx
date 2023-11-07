<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AddDepartment.aspx.cs" Inherits="BusManagmentSystem.Admin.AddDepartment" %>
<!-- This line specifies the page's properties and code-behind file. -->

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<!-- This is a placeholder for the page's head content (metadata). -->
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image:url('../Image/bg4.PNG'); width:100%; height:720px; background-repeat: no-repeat; background-size: cover; background-attachment:fixed; ">
         <!-- Styling for the background of the page. -->

        <div class="container p-md-4 p-sm-4 ">
               <!-- A container for page content. -->
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h3 class="text-center">DEPARTMENT</h3>

            <div class="row mb-3 mr-1g-5 ml-1g-5 mt-md-5">
                <div class="col-md-6">
                    <label for="txtDepartment">Department Name</label>
                    <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control" placeholder="Enter Department Name" required></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3 mr-1g-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" BackColor="#5558C9" Text="Add Department" OnClick="btnAdd_Click" />
                </div>
            </div>

            <div class="row mb-3 mr-1g-5 ml-lg-5">
                           <!-- A row for displaying the department data in a GridView. -->
                <div class="col-md-6">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="DepartmentId" AutoGenerateColumns="False"
                        EmptyDataText="No Record To Display" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating">
                         <!-- GridView for displaying department data. -->

                        <Columns>
                            <asp:BoundField DataField="DepartmentId" HeaderText="DepartmentId" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Department Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDepartmentEdit" runat="server" Text='<%# Eval("DepartmentName") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label>
                                      <!-- Label for displaying department name. -->
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" />
                             <!-- CommandField for edit operations. -->
                        </Columns>
                        <HeaderStyle BackColor="#5558C9" ForeColor="White" />
                          <!-- Styling for the GridView header. -->
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
