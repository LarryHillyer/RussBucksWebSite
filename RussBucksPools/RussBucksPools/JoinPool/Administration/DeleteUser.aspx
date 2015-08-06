<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DeleteUser.aspx.vb" Inherits="RussBucksPools.DeleteUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Enter Pool"></asp:Label>
    <br />
    <asp:TextBox ID="PoolName1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="InvalidPool1" runat="server" ></asp:Label>
    <br />
    <br />
    <p>Enter User Handle:</p>
    <p><asp:TextBox ID="UserHandle1" runat="server"></asp:TextBox></p>
    <asp:Label ID="InvalidHandle1" runat="server" ></asp:Label>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideContent" runat="server">

</asp:Content>
