<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="DesignationFees.aspx.cs" Inherits="BusManagmentSystem.Admin.DesignationFees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="background-image: url('../Image/bg4.PNG'); width: 100%; height: 720px; background-repeat: no-repeat; background-size: cover; background-attachment: fixed;">
        <div class="container p-md-4 p-sm-4 ">
            <div>
                <asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <h3 class="text-center">Designation Fees </h3>

            <div class="row mb-3 mr-1g-5 ml-1g-5 mt-md-5">
                <div class="col-md-6">
                    <label for="ddldesignation">Designation Name</label>
                    <asp:DropDownList ID="ddldesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select Designation" runat="server" ErrorMessage="Designation is Required" ControlToValidate="ddldesignation" Display="Dynamic" ForeColor="Red" SetFocusOnError="True" ValidationGroup="Busfee">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="col-md-6">
                    <label for="txtFeeAmounts">Fees</label>
                    <asp:TextBox ID="txtFeeAmounts" runat="server" CssClass="form-control" placeholder="Enter Designation Amount" TextMode="Number" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtFeeAmounts" ValidationGroup="Busfee" runat="server" ForeColor="Red" ErrorMessage="Required ?"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row mb-3 mr-1g-5 ml-lg-5">
                <div class="col-md-3 col-md-offset-2 mb-3">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-block" CausesValidation="true" BackColor="#5558C9" Text="Add Designation" OnClick="BtnAdd_Click" ValidationGroup="Busfee" />
                </div>
            </div>

            <div class="row mb-3 mr-1g-5 ml-lg-5">
                <div class="col-md-6">

                    <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing"
                        EmptyDataText="No Record To Display" OnRowUpdating="GridView1_RowUpdating" OnPageIndexChanging="GridView1_PageIndexChanging"
                        OnRowDeleting="GridView1_RowDeleting" OnRowRowCanceling="GrideView1_RowCanceling"
                        DataKeyNames="DesignationChargeId">

                        <Columns>
                            <asp:BoundField DataField="DesignationName" HeaderText="Designation" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Charge") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtcharge" runat="server" Text='<%# Eval("Charge") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="true" />
                            <asp:CommandField ShowDeleteButton="true" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
