<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CityAddEdit.aspx.cs" Inherits="AdminPanel_City_CityAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>City Add Edit Page</h2>
        </div>
    </div>
    <div class="container">
        <div class="col-md-12"><br />
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
            <asp:ValidationSummary ID="vsCityTable" runat="server" ForeColor="Red" />
            <table style="margin:10px;">
                <tr class="tableHight">
                    <td class="col-md-4">
                        Select Country
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:DropDownList ID="ddlCountryID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td class="col-md-3">
                        <asp:RequiredFieldValidator ID="rfvCountryName" runat="server" ErrorMessage="Please Select Country" Display="None" ControlToValidate="ddlCountryID" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-4">
                        Select State
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:DropDownList ID="ddlStateID" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="-1">Select State</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="col-md-3">
                        <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ErrorMessage="Please Select State" Display="None" ControlToValidate="ddlStateID" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-4">
                        Enter City Name
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtCityName" Placeholder="Enter City Name" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </td>
                    <td class="col-md-3">
                        <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ControlToValidate="txtCityName" Display="None" ErrorMessage="Please Enter City Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-4">
                        Enter STD Code
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtStdCode" Placeholder="Enter STD Code" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="col-md-3">
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-4">
                        Enter PinCode
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtPinCode" Placeholder="Enter Pincode" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                    </td>
                    <td class="col-md-3">
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-4">
                    </td>
                    <td class="col-md-1"></td>
                    <td colspan="3" class="col-md-7">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" />
                    &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

