<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="JoinLoserPool.aspx.vb" Inherits="RussBucksPools.JoinLoserPool" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="joinLoserPool1">
      <h2 class="pageTitle1">Enter User Name</h2>
      <br />
      <asp:TextBox ID="UserNameTextBox" runat="server" ></asp:TextBox>
      <br />
      <br />
      <asp:LinkButton ID="JoinLoserPoolBtn" runat="server" OnClick="JoinLoserPoolBtn_Click" CssClass="jLPB1" >Submit</asp:LinkButton>
      <br />
    </div>
    <br />
    <asp:Label ID="JoinError" runat="server" EnableViewState="true"></asp:Label>

    <style type="text/css">
        .joinLoserPool1 {text-align: center;  border: ridge 2px blue; padding: 10px; color: #0000FF;}
        .pageTitle1 {color:blue;}
        .jLPB1 {text-align:center;border:ridge 2px blue;padding:2px;color:blue;font:bold 9px arial;}

    </style>
</asp:Content>
