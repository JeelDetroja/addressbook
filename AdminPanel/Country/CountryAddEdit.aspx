<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryAddEdit.aspx.cs" Inherits="AdminPanel_Country_CountryAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Country Add Edit Page</h2>
        </div>
    </div>
    <div class="container">
        <div class="col-md-12"><br />
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" Text=""></asp:Label>
            <asp:ValidationSummary ID="vsCountryTable" ForeColor="Red" runat="server" />
            <table style="margin:10px;">
                <tr class="tableHight">
                    <td class="col-md-4">
                        Enter Country Name
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtCountryName" runat="server" Placeholder="Enter Country Name" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </td>
                    <td class="col-md-3">
                        <asp:RequiredFieldValidator ID="rfvCountryName" runat="server" ControlToValidate="txtCountryName" Display="None" ErrorMessage="Please Enter Country Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-4">
                        Enter Country Code
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtCountryCode" runat="server" Placeholder="Enter Country Code" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="col-md-3">
                    </td>
                </tr>
                 <tr class="tableHight">
                    <td class="col-md-4">
                    </td>
                     <td class="col-md-1"></td>
                    <td colspan="2" class="col-md-7">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" />
                    &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

