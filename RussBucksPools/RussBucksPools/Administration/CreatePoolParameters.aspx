<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CreatePoolParameters.aspx.vb" Inherits="RussBucksPools.PoolParameters" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <asp:Label ID="Label1" runat="server" Text="Sport: "></asp:Label>
    <asp:TextBox ID="Sport1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label4" runat="server" Text="Time Period Name: "></asp:Label>
    <asp:TextBox ID="TimePeriodName1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Time Period Increment: "></asp:Label>
    <asp:TextBox ID="TimeIncrement1" runat="server" Width="16px"></asp:TextBox>
    <br />
    <asp:Label ID="Label5" runat="server" Text="Season Start Date: "></asp:Label>
    <asp:TextBox ID="SeasonStartDate1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label6" runat="server" Text="Season Start Time: "></asp:Label>
    <asp:TextBox ID="SeasonStartTime1" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label7" runat="server" Text="Season End Date: "></asp:Label>
    <asp:TextBox ID="SeasonEndDate1" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Submit" Onclick="Button1_Click"/>
    <br />
 
</asp:Content>
