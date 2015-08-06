<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="TestDriver.aspx.vb" Inherits="RussBucksPools.TestDriver" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Administration/CreateSuperUser.aspx">Create Super User</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Administration/CreatePoolAdministrator.aspx">Create Pool Administrator</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/Administration/DeletePoolParameters.aspx">Delete Pool Parameters List</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/Administration/CreatePoolParameters.aspx">Create Pool Parameters</asp:HyperLink> 

    <br/>
    <br />
    <asp:Label ID="Label1" runat="server" Text="To Install To New Directory"></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Administration/DeleteAppFolders.aspx">1. Delete App Folders</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Administration/CreateAppFolders.aspx">2. Add App Folders</asp:HyperLink> 
    <br />
    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Administration/CreateScheduleFileList.aspx">3. Create Schedule File List</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Administration/CreateFTPFiles.aspx">4. Create FTP Files</asp:HyperLink>
    <br /><br />
    <asp:Label ID="Label2" runat="server" Text="Save Database Tables To XML"></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Administration/WriteMembershipTable.aspx">Save Members</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Administration/BarPoolList.aspx">Save Bar Pools</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Administration/PoolAdministratorList.aspx">Save Pool Administrators</asp:HyperLink>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Save XML Files To Databases"></asp:Label>
    <br />
    <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Administration/LoadMembersTable.aspx">Load Members Table</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/Administration/LoadBarPools.aspx">Load Bar Pools</asp:HyperLink>
    <br />
    <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/Administration/LoadPoolAdminstrators.aspx">Load Pool Adminstrators</asp:HyperLink>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Delete Database" OnClick="Button1_Click"/>
</asp:Content>
