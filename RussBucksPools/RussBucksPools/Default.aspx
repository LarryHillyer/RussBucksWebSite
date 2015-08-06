<%@ Page Title="Welcome" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="RussBucksPools._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: Title%></h1>
    <h2>The Offical RussBucks Pools Web Site</h2>
    <p class="lead">We are all about sports. If you think you can pick winners and losers, then these pools are for you.</p>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Administrative" Visible="false"></asp:Label>
    <br />
    <asp:Label ID="Label4" runat="server" visible="false"></asp:Label>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Enter Pool Commisioner Email Address:  " Visible="false"></asp:Label>
    <asp:TextBox ID="PoolCommisioner1" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Enter Commisioner Code:  " Visible="false"></asp:Label>
    <asp:TextBox ID="CommisionerCode1" runat="server" Visible="false"></asp:TextBox>
    <br /><br />
    <asp:Button ID="Submit1" runat="server" Text="Submit" OnClick="Submit1_Click" Visible="false"/>

</asp:Content>
