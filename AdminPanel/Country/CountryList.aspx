<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CountryList.aspx.cs" Inherits="AdminPanel_Country_CountryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Country List</h2>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <br /><br />
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="hlAddCountry" runat="server" Text="Add New Country" CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Country/Add"></asp:HyperLink>
                </div>
            </div><br />
            <div class="container">
                <div class="table-responsive col-md-12">
                    <asp:GridView ID="gvCountry" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCountry_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Are you Sure you want to Delete record ?')" Text="Delete" CommandName="DeleteRecord" CommandArgument='<%# Eval("CountryID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <%--<asp:HyperLink ID="hlEdit" runat="server" Text="Edit" NavigateUrl='<%# "~/AdminPanel/Country/Edit/" + Eval("CountryID").ToString().Trim() %>'></asp:HyperLink>--%>
                                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-warning btn-sm" Text="Edit" CommandName="EditRecord" CommandArgument='<%# Eval("CountryID").ToString().Trim() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="CountryID" HeaderText="ID" />--%>
                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                            <asp:BoundField DataField="CountryCode" HeaderText="Country-Code" />
                            <%--<asp:BoundField DataField="CreationDate" HeaderText="Creation-Date" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="Modification-Date" />--%>
                        </Columns>
                    </asp:GridView>
                </div>  
            </div>
        </div>
    </div>
</asp:Content>

