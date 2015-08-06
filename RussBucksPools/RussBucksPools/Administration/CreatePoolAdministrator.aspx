<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreatePoolAdministrator.aspx.vb" Inherits="RussBucksPools.CreatePoolAdministrator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        Enter Pool Name:</p>
    <p>
        <asp:TextBox ID="PoolName1" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" ></asp:Label>
    </p>
    <p>
        Enter UserName</p>
    <p>
        <asp:TextBox ID="UserName1" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" ></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click"/>

    </p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
