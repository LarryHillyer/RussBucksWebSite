<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreateFTPFiles.aspx.vb" Inherits="RussBucksPools.CreateFTPFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="FTP Port Number: "></asp:Label>
    <asp:TextBox ID="PortNumber1" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
