﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UpdateNotReady.aspx.vb" Inherits="RussBucksPools.UpdateNotReady" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Scoring Update Not Ready!</h1>
    <h2>Please wait a minute</h2>
    <p>&nbsp;</p>
    <p>
        <asp:button ID="ReturnToLoserPool1"   runat="server"  Text="Return"  OnClick="ReturnToLoserPool1_Click"/>
    </p>
</asp:Content>
