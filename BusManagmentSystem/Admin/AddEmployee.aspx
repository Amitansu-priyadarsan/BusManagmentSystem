<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMst.Master" AutoEventWireup="true" CodeBehind="AddEmployee.aspx.cs" Inherits="BusManagmentSystem.Admin.AddEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container  overflow:hidden;" startt="left">
        <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#mymodal">
            Add Employee
        </button>

        <div class="modal fade  " id="mymodal" role="dialog ">
            <div class="modal-dialog modal-lg;">
                <div class="modal-content  ">
                    <div class="modal-header  ov">
                        <h4 class="modal-title">Add Employee</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">

                        <div class="form-group">
                            <label for="txtName" class="control-label col-md-3">Name</label>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtIssuedDate" class="control-label col-md-3">Issued Date</label>
                            <div class="col-md-9">
                                <input type="date" id="txtIssuedDate" class="form-control" placeholder="Issued Date" runat="server">
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtEmployeeCode" class="control-label col-md-3">Employee Code</label>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtEmployeeCode" CssClass="form-control" placeholder="Employee Code" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtContactNo" class="control-label col-md-3">Contact No</label>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtContactNo" CssClass="form-control" placeholder="Contact No" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="ddlDepartment" class="control-label col-md-3">Department</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlDepartment" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepartment" InitialValue="Select Department" runat="server" ErrorMessage="Department is Required" ControlToValidate="ddlDepartment" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label for="ddldesignation">Designation Name</label>
                            <asp:DropDownList ID="ddldesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDesignation" InitialValue="Select Designation" runat="server" ErrorMessage="Designation is Required" ControlToValidate="ddldesignation" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <label for="ddlPickupPoint" class="control-label col-md-3">Pickup Point</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlPickupPoint" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPickupPoint" InitialValue="Select PickupPoint" runat="server" ErrorMessage="PickupPoint is Required" ControlToValidate="ddlPickupPoint" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="ddlDropPoint" class="control-label col-md-3">Drop Point</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="ddlDropPoint" CssClass="form-control" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDropPoint" InitialValue="Select DropPoint" runat="server" ErrorMessage="DropPoint is Required" ControlToValidate="ddlDropPoint" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtSurrenderDate" class="control-label col-md-3">Surrender Date</label>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtSurrenderDate" runat="server" CssClass="form-control" TextMode="Date" placeholder="Surrender Date"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtLeaveDate" class="control-label col-md-3">Leave Date</label>
                            <div class="col-md-9">
                                <asp:TextBox ID="txtLeaveDate" runat="server" CssClass="form-control" TextMode="Date" placeholder="Leave Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label for="ddlCharges">Charges</label>
                            <asp:DropDownList ID="ddlCharges" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorCharges" InitialValue="Select Charges" runat="server" ErrorMessage="Charges is Required" ControlToValidate="ddlCharges" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <label for="ddlBusType">Bus Type</label>
                            <asp:DropDownList ID="ddlBusType" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="Select BusType" runat="server" ErrorMessage="BusType is Required" ControlToValidate="ddlBusType" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnAddEmployee" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAddEmployee_Click" />
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>

                </div>
            </div>
        </div>
        <div style="width: 990px;">
            <div style="float: right; margin-right: 10px;">
                <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
                <asp:TextBox ID="txtSearch" runat="server" Width="140px"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" BorderColor="#1E90FF" ForeColor="#1E90FF" BackColor="#FFFFFF" Text="Search" OnClick="btnSearch_Click" />

            </div>
            <div style="height: 100% !important; width: 100% !important; overflow: auto !important; text-align: center !important;" class="my-bootstrap-class">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True" EmptyDataText="No Record To Display">
                </asp:GridView>
            </div>

        </div>
</asp:Content>

