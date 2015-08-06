<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="JoinPlayoffPool.aspx.vb" Inherits="RussBucksPools.JoinPlayoffPool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="joinPlayoffPool1">
      <h2 class="pageTitle1">Enter User Name</h2>
      <br />
      <asp:TextBox ID="UserNameTextBox" runat="server" ></asp:TextBox>
      <br />
      <br />
      <asp:LinkButton ID="JoinPlayoffPoolBtn" runat="server"  OnClick="JoinLoserPoolBtn_Click" CssClass="jPPB1" >Submit</asp:LinkButton>
      <br />
    </div>
    <br />
    <asp:Label ID="JoinError" runat="server" EnableViewState="true"></asp:Label>
    <style type="text/css">
        .joinPlayoffPool1 {text-align: center; max-width: 120px; border: ridge 2px blue; padding: 10px; color: #0000FF;}
        .pageTitle1 {color:blue;}
        .PPB1 {text-align:center;border:ridge 2px blue;padding:2px;color:blue;font:bold 9px arial;}
    </style>
</asp:Content>
