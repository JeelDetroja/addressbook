<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="CityList.aspx.cs" Inherits="AdminPanel_City_CityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>City List</h2>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <br /><br />
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="hlAddCity" runat="server" Text="Add New City" CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/City/Add"></asp:HyperLink>
                </div>
            </div><br />
            <div class="container">
                <div class="table-responsive col-md-12">
                    <asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="false" OnRowCommand="gvCity_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger btn-sm" Text="Delete" OnClientClick="return confirm('Are you Sure you want to Delete record ?')" CommandName="DeleteRecord" CommandArgument='<%# Eval("CityID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <%--<asp:HyperLink ID="hlEdit" runat="server" Text="Edit" NavigateUrl='<%# "~/AdminPanel/City/Edit/" + Eval("CityID").ToString().Trim() %>'></asp:HyperLink>--%>
                                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-warning btn-sm" Text="Edit" CommandName="EditRecord" CommandArgument='<%# Eval("CityID").ToString().Trim() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="CityID" HeaderText="ID" />--%>
                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="CityName" HeaderText="City" />
                            <asp:BoundField DataField="STDCode" HeaderText="STD" />
                            <asp:BoundField DataField="PinCode" HeaderText="PinCode" />
                            <%--<asp:BoundField DataField="CreationDate" HeaderText="Creation-Date" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="Modification-Date" />--%>

                        </Columns>
                    </asp:GridView>
                </div>   
            </div>
        </div>
    </div>
</asp:Content>

