<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegistration.aspx.cs" Inherits="User_UserRegistration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous" />
    <link href="../../Content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/css/TableStyle.css" rel="stylesheet" />
    <script src="../../Content/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js" integrity="sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=" crossorigin="anonymous"></script>
    <form id="form1" runat="server">
        <div class ="container">
            <div class="row">
                <div class="col-md-12">
                    <h2>User Registration Page</h2>
                </div>
            </div>
            <div class="container">
                <div class="col-md-12"><br />
                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" Text=""></asp:Label>
                    <asp:ValidationSummary ForeColor="Red" ID="vsUserTable" runat="server" />
                    <table style="margin:10px;">
                        <tr class="tableHight">
                            <td class="col-md-1" style="color:red; text-align:right;">
                                *
                            </td>
                            <td class="col-md-4">
                                Enter User Name</td>
                            <td class="col-md-1">
                                :
                            </td>
                            <td class="col-md-4">
                                <asp:TextBox ID="txtUserName" runat="server" Placeholder="Enter Username" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </td>
                            <td class="col-md-2">
                                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" Display="None" ErrorMessage="Please Enter User Name" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tableHight">
                            <td class="col-md-1" style="color:red; text-align:right;">
                                *
                            </td>
                            <td class="col-md-4">
                                Enter Password</td>
                            <td class="col-md-1">
                                :
                            </td>
                            <td class="col-md-4">
                                <asp:TextBox ID="txtPassword" runat="server" Placeholder="Enter Password" TextMode="Password" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </td>
                            <td class="col-md-2">
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Please Enter Password" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tableHight">
                            <td class="col-md-1" style="color:red; text-align:right;">
                                *
                            </td>
                            <td class="col-md-4">
                                Enter Display Name</td>
                            <td class="col-md-1">
                                :
                            </td>
                            <td class="col-md-4">
                                <asp:TextBox ID="txtDisplayName" runat="server" Placeholder="Enter Display Name" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </td>
                            <td class="col-md-2">
                                <asp:RequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName" Display="None" ErrorMessage="Please Enter Display Name" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tableHight">
                            <td class="col-md-1" style="color:red; text-align:right;">
                                
                            </td>
                            <td class="col-md-4">
                                Enter Email</td>
                            <td class="col-md-1">
                                :
                            </td>
                            <td class="col-md-4">
                                <asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter Email" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            </td>
                            <td class="col-md-2">
                            </td>
                        </tr>
                        <tr class="tableHight">
                            <td class="col-md-1" style="color:red; text-align:right;">
                                
                            </td>
                            <td class="col-md-4">
                                Enter Mobile No</td>
                            <td class="col-md-1">
                                :
                            </td>
                            <td class="col-md-4">
                                <asp:TextBox ID="txtMobileNo" runat="server" Placeholder="Enter Mobile No" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </td>
                            <td class="col-md-2">
                            </td>
                        </tr>
                         <tr class="tableHight">
                             <td class="col-md-1" style="color:red; text-align:right;">
                            </td>
                            <td class="col-md-4">
                            </td>
                             <td class="col-md-1"></td>
                            <td colspan="3" class="col-md-6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary btn-sm" OnClick="btnSave_Click" />
                            &nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnCancel_Click" Text="Cancel" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>       
    </form>
</body>
</html>
