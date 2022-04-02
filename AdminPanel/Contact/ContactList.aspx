<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="ContactList.aspx.cs" Inherits="AdminPanel_Contact_ContactList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>Contact List</h2>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <br /><br />
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="hlAddContact" runat="server" Text="Add New Contact" CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Contact/Add"></asp:HyperLink>
                </div>
            </div><br />
            <div class="container">
                <div class="table-responsive col-md-12">
                    <asp:GridView ID="gvContact" runat="server" AutoGenerateColumns="false" OnRowCommand="gvContact_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger btn-sm" Text="Delete" CommandName="DeleteRecord" OnClientClick="return confirm('Are you Sure you want to Delete record ?')" CommandArgument='<%# Eval("ContactID").ToString() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <%--<asp:HyperLink ID="hlEdit" runat="server" Text="Edit" NavigateUrl='<%# "~/AdminPanel/Contact/Edit/" + Eval("ContactID").ToString().Trim() %>'></asp:HyperLink>--%>
                                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-warning btn-sm" Text="Edit" CommandName="EditRecord" CommandArgument='<%# Eval("ContactID").ToString().Trim() %>' />                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="ContactID" HeaderText="ID" />--%>
                            <asp:BoundField DataField="ContactName" HeaderText="Name" />
                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No." />
                            <asp:BoundField DataField="Category" HeaderText="Contact Category" />
                            <asp:BoundField DataField="CityName" HeaderText="City" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                            <asp:BoundField DataField="WhatsAppNo" HeaderText="WhatsApp" />
                            <asp:BoundField DataField="BirthDate" HeaderText="BirthDate" />
                            <asp:BoundField DataField="Email" HeaderText="Email" />
                            <asp:BoundField DataField="Age" HeaderText="Age" />
                            <asp:BoundField DataField="Address" HeaderText="Address" />
                            <asp:BoundField DataField="BloodGroup" HeaderText="BloodGroup" />
                            <asp:BoundField DataField="FacebookID" HeaderText="Facebook" />
                            <asp:BoundField DataField="LinkedINID" HeaderText="Linked IN" />
                            <%--<asp:BoundField DataField="CreationDate" HeaderText="Creation-Date" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="Modification-Date" />--%>

                        </Columns>
                    </asp:GridView>
                </div>   
            </div>
        </div>
    </div>
</asp:Content>

