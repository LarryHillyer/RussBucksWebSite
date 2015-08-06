<%@ Page Title="Season End" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SeasonEnd.aspx.vb" Inherits="RussBucksPools.Season_End" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%:Page.Title + "  - " + CStr(Session("poolAlias"))%></h2><br />
    <br />

    <div id ="Contenders1" style=" text-align: center; vertical-align: middle;">

        <asp:Table ID="Contenders4" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
            <asp:TableHeaderRow Height="60px">
                <asp:TableHeaderCell Id ="ContendersName" Width="120px">Contender's Name</asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team1Label"     Width="15px"><asp:Image ID="Team1Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team2Label"     Width="15px"><asp:Image ID="Team2Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team3Label"     Width="15px"><asp:Image ID="Team3Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team4Label"     Width="15px"><asp:Image ID="Team4Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team5Label"     Width="15px"><asp:Image ID="Team5Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team6Label"     Width="15px"><asp:Image ID="Team6Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team7Label"     Width="15px"><asp:Image ID="Team7Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team8Label"     Width="15px"><asp:Image ID="Team8Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team9Label"     Width="15px"><asp:Image ID="Team9Label2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team10Label"    Width="15px"><asp:Image ID="Team10Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team11Label"    Width="15px"><asp:Image ID="Team11Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team12Label"    Width="15px"><asp:Image ID="Team12Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team13Label"    Width="15px"><asp:Image ID="Team13Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team14Label"    Width="15px"><asp:Image ID="Team14Label2"  runat ="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team15Label"    Width="15px"><asp:Image ID="Team15Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team16Label"    Width="15px"><asp:Image ID="Team16Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team17Label"    Width="15px"><asp:Image ID="Team17Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team18Label"    Width="15px"><asp:Image ID="Team18Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team19Label"    Width="15px"><asp:Image ID="Team19Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team20Label"    Width="15px"><asp:Image ID="Team20Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team21Label"    Width="15px"><asp:Image ID="Team21Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team22Label"    Width="15px"><asp:Image ID="Team22Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team23Label"    Width="15px"><asp:Image ID="Team23Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team24Label"    Width="15px"><asp:Image ID="Team24Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team25Label"    Width="15px"><asp:Image ID="Team25Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team26Label"    Width="15px"><asp:Image ID="Team26Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team27Label"    Width="15px"><asp:Image ID="Team27Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team28Label"    Width="15px"><asp:Image ID="Team28Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team29Label"    Width="15px"><asp:Image ID="Team29Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team30Label"    Width="15px"><asp:Image ID="Team30Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team31Label"    Width="15px"><asp:Image ID="Team31Label2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team32Label"    Width="15px"><asp:Image ID="Team32Label2"  runat="server" /></asp:TableHeaderCell>
            
            </asp:TableHeaderRow>

            <asp:TableHeaderRow Height="15px">
                <asp:TableHeaderCell Id ="ContendersName1" Width="120px"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team1Icon"       Width="15px"><asp:Image ID="Team1Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team2Icon"       Width="15px"><asp:Image ID="Team2Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team3Icon"       Width="15px"><asp:Image ID="Team3Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team4Icon"       Width="15px"><asp:Image ID="Team4Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team5Icon"       Width="15px"><asp:Image ID="Team5Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team6Icon"       Width="15px"><asp:Image ID="Team6Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team7Icon"       Width="15px"><asp:Image ID="Team7Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team8Icon"       Width="15px"><asp:Image ID="Team8Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team9Icon"       Width="15px"><asp:Image ID="Team9Icon2"   runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team10Icon"      Width="15px"><asp:Image ID="Team10Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team11Icon"      Width="15px"><asp:Image ID="Team11Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team12Icon"      Width="15px"><asp:Image ID="Team12Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team13Icon"      Width="15px"><asp:Image ID="Team13Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team14Icon"      Width="15px"><asp:Image ID="Team14Icon2"  runat ="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team15Icon"      Width="15px"><asp:Image ID="Team15Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team16Icon"      Width="15px"><asp:Image ID="Team16Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team17Icon"      Width="15px"><asp:Image ID="Team17Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team18Icon"      Width="15px"><asp:Image ID="Team18Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team19Icon"      Width="15px"><asp:Image ID="Team19Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team20Icon"      Width="15px"><asp:Image ID="Team20Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team21Icon"      Width="15px"><asp:Image ID="Team21Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team22Icon"      Width="15px"><asp:Image ID="Team22Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team23Icon"      Width="15px"><asp:Image ID="Team23Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team24Icon"      Width="15px"><asp:Image ID="Team24Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team25Icon"      Width="15px"><asp:Image ID="Team25Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team26Icon"      Width="15px"><asp:Image ID="Team26Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team27Icon"      Width="15px"><asp:Image ID="Team27Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team28Icon"      Width="15px"><asp:Image ID="Team28Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team29Icon"      Width="15px"><asp:Image ID="Team29Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team30Icon"      Width="15px"><asp:Image ID="Team30Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team31Icon"      Width="15px"><asp:Image ID="Team31Icon2"  runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="Team32Icon"      Width="15px"><asp:Image ID="Team32Icon2"  runat="server" /></asp:TableHeaderCell>            
            </asp:TableHeaderRow>

        </asp:Table>
    </div>
    <br />
    <asp:Table Id="LoserTable1" runat="server"  GridLines="None" CssClass="LoserTable1" CellPadding="-1" CellSpacing="-1">
        <asp:TableHeaderRow >
            <asp:TableHeaderCell ID="LosersName"    >The Losers</asp:TableHeaderCell>
            <asp:TableHeaderCell Id="TimePeriod1"        >Day</asp:TableHeaderCell>
            <asp:TableHeaderCell ID="LosingPick1"   >Losing Pick</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Height="35px" Text="Return" Width="120px"/>

    <style type="text/css" >
        .LoserTable1 {  border-collapse: collapse; }    
        .LoserTable1 > tbody > tr > th, 
        .LoserTable1 > tbody > tr > td { border: 1px ridge black; padding: 3px;text-align:center; font-size:9px; }
        .Contenders2 {  border-collapse: collapse; }    
        .Contenders2 > tbody > tr > th, 
        .Contenders2 > tbody > tr > td { border: 1px ridge black; max-width:23px;text-align:center;font-size:9px; }
        .UserName1 { min-width:120px;}
    </style>

</asp:Content>
