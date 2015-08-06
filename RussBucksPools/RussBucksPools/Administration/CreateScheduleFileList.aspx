<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreateScheduleFileList.aspx.vb" Inherits="RussBucksPools.CreateScheduleFileList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Start File Number "></asp:Label>
    <asp:TextBox ID="FirstFileNumber1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="End File Number "></asp:Label>
    <asp:TextBox ID="LastFileNumber1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Pool Alias " ></asp:Label>
    <asp:TextBox ID="PoolAlias1" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click"/>
&nbsp;

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
