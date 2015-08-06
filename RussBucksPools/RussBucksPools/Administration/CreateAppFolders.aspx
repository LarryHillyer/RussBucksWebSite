<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreateAppFolders.aspx.vb" Inherits="RussBucksPools.CreateAppFolders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label3" runat="server" Text="Root Folder: "></asp:Label>
    <asp:TextBox ID="RootFolder1" runat="server" Width="763px"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="Test Driver Root Folder: "></asp:Label>
    <asp:TextBox ID="TestDriverRootFolder1" runat="server" Width="691px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Submit" Onclick="Button1_Click"/>
    <br />

</asp:Content>
