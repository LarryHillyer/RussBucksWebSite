<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="JoinPool.aspx.vb" Inherits="RussBucksPools.JoinPool" %>
    
<asp:content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <div class="myPools1">
      
      <h4 class="pageTitle1">Join A Pool</h4>
      <h5 class="pageTitle1">Enter Pool Name</h5>

      <asp:TextBox ID="PoolNameTextBox" runat="server"></asp:TextBox>
      <br />
      <asp:LinkButton runat="server" OnClick="FindPool_Click" CssClass="joinPool1" >Submit</asp:LinkButton>
        <br />
        <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Admin1" runat="server" Text="Administration" Visible="false"></asp:Label>
        <br />
        <asp:Label ID="CreatePool1" runat="server" Text="Create Pool"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Select Pool Type" Visible="false"></asp:Label>
        <br />
        <asp:ListBox ID="Pools1" runat="server" Visible="false" Height="20px"></asp:ListBox>
        <br />
        <asp:Label ID="Label4" runat="server"  Visible="false" Text="Select Sport"></asp:Label>
        <br />
        <asp:ListBox ID="Sport1" runat="server" Visible="false" Height="20px"></asp:ListBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Enter Pool Name" Visible="false"></asp:Label>
        <br />
        <asp:TextBox ID="PoolAlias1" runat="server" Visible="false" ></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Visible="false" ></asp:Label>
        <br />
        <asp:Button ID="Button1" runat="server" visible="false" Text="Submit" onclick="Button1_Click"/>
        <br />
        <br />
        <asp:HyperLink ID="DeleteUser1" runat="server" NavigateUrl="~/JoinPool/Administration/DeleteUser.aspx" Visible="false">Delete User</asp:HyperLink>
        <br />

        <br />
    </div>
    <style type="text/css">
        .myPools1 {text-align: center;  border: ridge 2px blue; padding: 10px; color: #0000FF;}
        .pageTitle1 {color:blue}
        .joinPool1 {text-align:center;border:ridge 2px blue;padding:2px;color:blue;font:bold 9px arial;}
    </style>
</asp:content>
