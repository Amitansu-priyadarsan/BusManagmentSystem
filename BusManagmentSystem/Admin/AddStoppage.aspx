<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AddStoppageMaster.aspx.cs" Inherits="BusManagmentSystem.Admin.AddStoppageMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container bg-image">
        <div class="container p-md-4 p-sm-4">
            <div>
                <asp:Label ID="lblMsg" runat="server" CssClass="alert"></asp:Label>
            </div>
            <h3 class="text-center">Stoppage</h3>

            <div class="row mb-3">
                <div class="col-md-6">
                    <label for="txtPickupPoint">Pickup Point</label>
                    <asp:TextBox ID="txtPickupPoint" runat="server" CssClass="form-control" placeholder="Enter Pickup Point"></asp:TextBox>
                </div>
                <div class="col-md-6">
                    <label for="txtDropPoint">Drop Point</label>
                    <asp:TextBox ID="txtDropPoint" runat="server" CssClass="form-control" placeholder="Enter Drop Point"></asp:TextBox>
                </div>
            </div>
            <div class="row mb-3">
                <div class="col-md-3 col-md-offset-2">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" Text="Add Stoppage" OnClick="btnAdd_Click" />
                </div>
            </div>

            <div class="row mb-3">
                <div class="col-md-12">
                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-bordered" DataKeyNames="StopId" AutoGenerateColumns="False"
                        EmptyDataText="No Record To Display" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                        OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="StopId" HeaderText="StopId" ReadOnly="True">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Pickup Point">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPickupPointEdit" runat="server" Text='<%# Eval("PickupPoint") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPickupPoint" runat="server" Text='<%# Eval("PickupPoint") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Drop Point">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDropPointEdit" runat="server" Text='<%# Eval("DropPoint") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDropPoint" runat="server" Text='<%# Eval("DropPoint") %>'></asp:Label>
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
