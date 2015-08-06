<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InactivePool.aspx.vb" Inherits="RussBucksPools.InactivePool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Pool Is not Currently Active!!!!"></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Return" OnClick="Button1_Click"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
