<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactAddEdit.aspx.cs" Inherits="AdminPanel_Contact_ContactAddEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Contact Add Edit Page</h2>
        </div>
    </div>
    <div class="container">
        <div class="col-md-12"><br />
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
            <asp:ValidationSummary ID="vsContactTable" ForeColor="Red" runat="server" />
            <table style="margin:10px;">
                <tr class="tableHight">
                    <td class="col-md-1" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Select Country
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:DropDownList ID="ddlCountryID" runat="server" CssClass="select2me form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountryID_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvCountryName" runat="server" ErrorMessage="Please Select Country" Display="None" ControlToValidate="ddlCountryID" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1 requiredfild" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Select State
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:DropDownList ID="ddlStateID" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStateID_SelectedIndexChanged">
                            <asp:ListItem Value="-1">Select State</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvStateName" runat="server" ErrorMessage="Please Select State" Display="None" ControlToValidate="ddlStateID" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1 requiredfild">
                        *
                    </td>
                    <td class="col-md-4">
                        Select City
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:DropDownList ID="ddlCityID" runat="server" AutoPostBack="True">
                            <asp:ListItem Value="-1">Select City</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvCityName" runat="server" ErrorMessage="Please Select City" Display="None" ControlToValidate="ddlCityID" ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Select Contact Category
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td colspan="2" class="col-md-6">                      
                        <%--<asp:CheckBoxList ID="cblContactCategory" runat="server" CssClass="checkbox-inline" Font-Bold="False" Font-Overline="False" Font-Size="9pt" RepeatDirection="Horizontal">
                        </asp:CheckBoxList>--%>
                        <asp:CheckBoxList ID="cblContactCategory" runat="server" CssClass="p-5" RepeatDirection="Horizontal"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Enter Contact Name
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtContactName" runat="server" Placeholder="Enter Contact Name" CssClass="form-control" MaxLength="250"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" Display="None" ErrorMessage="Please Enter Contact Name" ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Enter Contact No.
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtContactNo" runat="server" Placeholder="Enter Contact No" CssClass="form-control" MaxLength="250"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" ControlToValidate="txtContactNo" Display="None" ErrorMessage="Please Enter Contact No." ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                        Enter WhatsApp No.
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtWhatsAppNo" runat="server" Placeholder="Enter Whatsapp No" CssClass="form-control" MaxLength="250"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                        Enter Birth Date
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtBirthDate" runat="server" Placeholder="Enter Birth Date" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </td>
                    <td class="col-md-2">   
                        <asp:CompareValidator ID="cvBirthDate" runat="server" ControlToValidate="txtBirthDate" ErrorMessage="Enter Valid Birth Date" ForeColor="Red" Operator="DataTypeCheck" Type="Date" Display="None"></asp:CompareValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Enter Email
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter Email" CssClass="form-control" MaxLength="250"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" Display="None" ErrorMessage="Please Enter Email" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                        Enter Age
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtAge" runat="server" Placeholder="Enter Age" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                        <asp:CompareValidator ID="cvAge" runat="server" ErrorMessage="Enter Valid Age" ControlToValidate="txtAge" Operator="DataTypeCheck" Type="Integer" ForeColor="Red" Display="None"></asp:CompareValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1" style="color:red; text-align:right;">
                        *
                    </td>
                    <td class="col-md-4">
                        Enter Address
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <textarea id="txtareaAddress" runat="server" cols="25" rows="4" maxlength="500" ></textarea>
                    </td>
                    <td class="col-md-2">
                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtareaAddress" Display="None" ErrorMessage="Please Enter Address" ForeColor="Red" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                        Enter Blood Group
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtBloodGroup" runat="server" Placeholder="Enter Blood Group" CssClass="form-control" MaxLength="50"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                        Enter Facebook ID
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtFacebookID" runat="server" Placeholder="Enter FacebookID" CssClass="form-control" MaxLength="250"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                        Enter LinkedIN ID
                    </td>
                    <td class="col-md-1">
                        :
                    </td>
                    <td class="col-md-4">
                        <asp:TextBox ID="txtLinkedINID" runat="server" Placeholder="Enter LinkedIN-ID" CssClass="form-control" MaxLength="250"></asp:TextBox>
                    </td>
                    <td class="col-md-2">
                    </td>
                </tr>
                <tr class="tableHight">
                    <td class="col-md-1">
                        
                    </td>
                    <td class="col-md-4">
                    </td>
                    <td class="col-md-1">
                    </td>
                    <td colspan="2" class="col-md-6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" />
                    &nbsp;
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" Text="Cancel" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

