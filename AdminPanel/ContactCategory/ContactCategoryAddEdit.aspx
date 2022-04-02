<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactCategoryAddEdit.aspx.cs" Inherits="AdminPanel_ContactCategory_ContactCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Contact Category Add Edit Page</h2>
        </div>
    </div>
    <div class="container">
        <div class="col-md-12"><br />
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" Text=""></asp:Label>
            <asp:ValidationSummary ID="vsContactCategoryTable" ForeColor="Red" runat="server" />
            <table style="margin:10px;">
                <tr class="tableHight">
                    <td class="col-md-4">
                        Enter Contact Category Name
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtContactCategoryName" Placeholder="Enter Contact Category" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                    </td>
                    <td class="col-md-3">
                        <asp:RequiredFieldValidator ID="rfvContactCategoryName" runat="server" ControlToValidate="txtContactCategoryName" Display="None" ErrorMessage="Please Enter Contact Category Name" ForeColor="Red"></asp:RequiredFieldValidator>
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

