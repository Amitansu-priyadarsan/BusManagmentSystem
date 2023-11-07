<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AddPass.aspx.cs" Inherits="BusManagmentSystem.Admin.AddPass" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container bg-image">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server" CssClass="alert"></asp:Label>
            </div>
            <h3 class="text-center">Pass</h3>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtPass">Pass Name</label>
                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" placeholder="Enter Pass Name" required></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-3 col-md-offset-2">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" Text="Add Pass" OnClick="btnAdd_Click" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-6">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="PassId" AutoGenerateColumns="False"
                        EmptyDataText="No Record To Display" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="PassId" HeaderText="PassId" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Pass Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPassEdit" runat="server" Text='<%# Eval("PassName") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPassName" runat="server" Text='<%# Eval("PassName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField CausesValidation="False" HeaderText="Operation" ShowEditButton="True" ShowDeleteButton="True" />
                        </Columns>
                        <HeaderStyle BackColor="#5558C9" ForeColor="White" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
