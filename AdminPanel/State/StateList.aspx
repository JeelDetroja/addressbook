﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AdminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphhead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h2>State List</h2>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
            <br />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:HyperLink ID="hlAddState" runat="server" Text="Add New State" CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/State/Add"></asp:HyperLink>
                </div>
            </div><br />
            <div class="container">
                <div class="table-responsive col-md-12">
                    <asp:GridView ID="gvState" runat="server" AutoGenerateColumns="false" OnRowCommand="gvState_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger btn-sm" Text="Delete" CommandName="DeleteRecord" OnClientClick="return confirm('Are you Sure you want to Delete record ?')" CommandArgument='<%# Eval("StateID").ToString() %>'   />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <%--<asp:HyperLink ID="hlEdit" runat="server" Text="Edit" NavigateUrl='<%# "~/AdminPanel/State/Edit/" + Eval("StateID").ToString().Trim() %>'></asp:HyperLink>--%>
                                    <asp:Button runat="server" ID="btnEdit" CssClass="btn btn-warning btn-sm" Text="Edit" CommandName="EditRecord" CommandArgument='<%# Eval("StateID").ToString().Trim() %>' />                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="StateID" HeaderText="ID" />--%>
                            <asp:BoundField DataField="CountryName" HeaderText="Country" />
                            <asp:BoundField DataField="StateName" HeaderText="State" />
                            <asp:BoundField DataField="StateCode" HeaderText="State-Code" />
                            <%--<asp:BoundField DataField="CreationDate" HeaderText="Creation-Date" />
                            <asp:BoundField DataField="ModificationDate" HeaderText="Modification-Date" />--%>
                        </Columns>
                    </asp:GridView>
                </div>   
            </div>
        </div>
    </div>
</asp:Content>

