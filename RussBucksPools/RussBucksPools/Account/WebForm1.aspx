<%@ Page Title="" Language="vb" AutoEventWireup="true"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Import Namespace="RussBucksPools" %>
<%@ Import NameSpace="RussBucksPools.LosersPool.Models"%>
<%@ Import Namespace="RussBucksPools.JoinPools.Models"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
        <title> End User License Agreement </title>

</head>

<body>
    <form runat="server">
            <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="respond" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>


    <h2>RussBucksPools Entry Code</h2>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Enter Code:  "></asp:Label>
    <asp:TextBox ID="CommisionerCode1" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Submit1" runat="server" Text="Submit" OnClick="Submit1_Click" />

    <br />
    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <asp:Label ID="Label2" runat="server" Text="Label">The sole intent of RussBucksPools is to provide entertainment its users by providing them the ability to track their picks as well as their friend to certain sporting events. RussbucksPools are not in any way liable for any outcomes to any games or any financial losses incurred by any user as a result of the outcome of the game including any betting by any user.  </asp:Label>
        <asp:Button ID="Agree1" runat="server" Text="I Agree" OnClick="Agree1_Click"/>
        <asp:Button ID="Disagree1" runat="server" Text="I Reject" OnClick="Disagree1_Click" />
    </asp:Panel>

  </form>
</body>

</html>
