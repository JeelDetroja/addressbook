<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogInPage.aspx.cs" Inherits="User_LogInPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous" />
    <link href="../../Content/css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../../Content/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Content/js/bootstrap.bundle.min.js"></script>
</head>
<body>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js" integrity="sha256-9/aliU8dGd2tb6OSsuzixeV4y/faTqgFtohetphbbj0=" crossorigin="anonymous"></script>
    <form id="form1" runat="server">
    <div>
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <h1>Multi User Address Book Login</h1>
                <br /><br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
                    </div>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <br /><br />
                <div class="row">
                    <div class="col-md-4">
                        Enter User Name
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtUserName" Placeholder="Enter Username" CssClass="form-control" />
                    </div>
                </div>
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" Display="None" ErrorMessage="Enter User Name"></asp:RequiredFieldValidator>
                <br />
                <div class="row">
                    <div class="col-md-4">
                        Enter Password
                    </div>
                    <div class="col-md-1">
                        :
                    </div>
                    <div class="col-md-7">
                        <asp:TextBox runat="server" ID="txtPassword"  Placeholder="Enter Password" TextMode="Password" CssClass="form-control" />
                    </div>
                </div>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" Display="None" ErrorMessage="Enter Password"></asp:RequiredFieldValidator>
                <br />
                <div class="row">
                    <div class="col-md-5"></div>
                    <div class="col-md-7">
                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary btn-sm" Text="Login" OnClick="btnSave_Click" />
                        <br />
                        <br />
                        Not have an Account -&nbsp;
                        <asp:LinkButton ID="lbRegister" runat="server" OnClick="lbRegister_Click" CausesValidation="False">Register</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
