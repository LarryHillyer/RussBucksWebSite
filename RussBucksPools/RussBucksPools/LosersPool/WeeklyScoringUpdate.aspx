<%@ Page Title="Scoring Update" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WeeklyScoringUpdate.aspx.vb" Inherits="RussBucksPools.WeeklyScoringUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
       <h2><%:Page.Title + "  - " + CStr(Session("poolAlias")) + " - " + CStr(Session("TimePeriod"))%></h2><br />
    <br />
  <div id="GameElement1" >
    <asp:Table ID="TeamsByGameTable1" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
        <asp:TableHeaderRow Height="15px" CssClass="r1">
            <asp:TableHeaderCell ID="Nothing1"  Width="120px" CssClass="UserName1"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber1" ColumnSpan="2"  Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber2" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber3" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber4" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber5" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber6" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber7" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber8" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber9" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber10" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber11" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber12" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber13" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber14" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber15" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber16" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableHeaderRow Height="15px" CssClass="r1">
            <asp:TableHeaderCell ID="Status1"  Width="120px" CssClass="UserName1"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber1Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber2Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber3Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber4Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber5Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber6Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber7Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber8Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber9Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber10Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber11Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber12Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber13Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber14Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber15Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber16Status" ColumnSpan="2"  Width="30px"></asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableHeaderRow Height="60px" CssClass="r1">
            <asp:TableHeaderCell ID="Nothing1A" Width="120px" CssClass="UserName1"></asp:TableHeaderCell> 
            <asp:TableHeaderCell ID="HomeTeam1Image"  Width="15px"><asp:Image ID="HomeTeam1Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam1Image"  Width="15px"><asp:Image ID="AwayTeam1Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam2Image"  Width="15px"><asp:Image ID="HomeTeam2Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam2Image"  Width="15px"><asp:Image ID="AwayTeam2Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam3Image"  Width="15px"><asp:Image ID="HomeTeam3Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam3Image"  Width="15px"><asp:Image ID="AwayTeam3Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam4Image"  Width="15px"><asp:Image ID="HomeTeam4Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam4Image"  Width="15px"><asp:Image ID="AwayTeam4Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam5Image"  Width="15px"><asp:Image ID="HomeTeam5Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam5Image"  Width="15px"><asp:Image ID="AwayTeam5Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam6Image"  Width="15px"><asp:Image ID="HomeTeam6Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam6Image"  Width="15px"><asp:Image ID="AwayTeam6Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam7Image"  Width="15px"><asp:Image ID="HomeTeam7Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam7Image"  Width="15px"><asp:Image ID="AwayTeam7Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam8Image"  Width="15px"><asp:Image ID="HomeTeam8Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam8Image"  Width="15px"><asp:Image ID="AwayTeam8Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam9Image"  Width="15px"><asp:Image ID="HomeTeam9Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam9Image"  Width="15px"><asp:Image ID="AwayTeam9Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam10Image"  Width="15px"><asp:Image ID="HomeTeam10Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam10Image"  Width="15px"><asp:Image ID="AwayTeam10Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam11Image"  Width="15px"><asp:Image ID="HomeTeam11Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam11Image"  Width="15px"><asp:Image ID="AwayTeam11Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam12Image"  Width="15px"><asp:Image ID="HomeTeam12Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam12Image"  Width="15px"><asp:Image ID="AwayTeam12Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam13Image"  Width="15px"><asp:Image ID="HomeTeam13Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam13Image"  Width="15px"><asp:Image ID="AwayTeam13Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam14Image"  Width="15px"><asp:Image ID="HomeTeam14Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam14Image"  Width="15px"><asp:Image ID="AwayTeam14Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam15Image"  Width="15px"><asp:Image ID="HomeTeam15Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam15Image"  Width="15px"><asp:Image ID="AwayTeam15Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam16Image"  Width="15px"><asp:Image ID="HomeTeam16Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam16Image"  Width="15px"><asp:Image ID="AwayTeam16Image1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableHeaderRow CssClass="IconRow" >
            <asp:TableHeaderCell ID="Nothing1C" Width="120px" CssClass="UserName1"></asp:TableHeaderCell> 
            <asp:TableHeaderCell ID="HomeTeam1Icon"  Width="15px"><asp:Image ID="HomeTeam1Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam1Icon"  Width="15px"><asp:Image ID="AwayTeam1Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam2Icon"  Width="15px"><asp:Image ID="HomeTeam2Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam2Icon"  Width="15px"><asp:Image ID="AwayTeam2Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam3Icon"  Width="15px"><asp:Image ID="HomeTeam3Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam3Icon"  Width="15px"><asp:Image ID="AwayTeam3Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam4Icon"  Width="15px"><asp:Image ID="HomeTeam4Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam4Icon"  Width="15px"><asp:Image ID="AwayTeam4Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam5Icon"  Width="15px"><asp:Image ID="HomeTeam5Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam5Icon"  Width="15px"><asp:Image ID="AwayTeam5Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam6Icon"  Width="15px"><asp:Image ID="HomeTeam6Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam6Icon"  Width="15px"><asp:Image ID="AwayTeam6Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam7Icon"  Width="15px"><asp:Image ID="HomeTeam7Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam7Icon"  Width="15px"><asp:Image ID="AwayTeam7Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam8Icon"  Width="15px"><asp:Image ID="HomeTeam8Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam8Icon"  Width="15px"><asp:Image ID="AwayTeam8Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam9Icon"  Width="15px"><asp:Image ID="HomeTeam9Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam9Icon"  Width="15px"><asp:Image ID="AwayTeam9Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam10Icon"  Width="15px"><asp:Image ID="HomeTeam10Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam10Icon"  Width="15px"><asp:Image ID="AwayTeam10Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam11Icon"  Width="15px"><asp:Image ID="HomeTeam11Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam11Icon"  Width="15px"><asp:Image ID="AwayTeam11Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam12Icon"  Width="15px"><asp:Image ID="HomeTeam12Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam12Icon"  Width="15px"><asp:Image ID="AwayTeam12Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam13Icon"  Width="15px"><asp:Image ID="HomeTeam13Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam13Icon"  Width="15px"><asp:Image ID="AwayTeam13Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam14Icon"  Width="15px"><asp:Image ID="HomeTeam14Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam14Icon"  Width="15px"><asp:Image ID="AwayTeam14Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam15Icon"  Width="15px"><asp:Image ID="HomeTeam15Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam15Icon"  Width="15px"><asp:Image ID="AwayTeam15Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam16Icon"  Width="15px"><asp:Image ID="HomeTeam16Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam16Icon"  Width="15px"><asp:Image ID="AwayTeam16Icon1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableRow Height="15px" CssClass="r1">
            <asp:TableCell ID="Nothing1B" Width="120px" CssClass="UserName1"></asp:TableCell>
            <asp:TableCell ID="HomeScore1"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore1"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore2"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore2"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore3"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore3"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore4"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore4"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore5"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore5"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore6"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore6"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore7"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore7"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore8"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore8"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore9"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore9"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore10"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore10"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore11"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore11"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore12"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore12"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore13"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore13"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore14"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore14"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore15"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore15"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="HomeScore16"  Width="15px"></asp:TableCell>
            <asp:TableCell ID="AwayScore16"  Width="15px"></asp:TableCell>

        </asp:TableRow>

    </asp:Table>
 </div>
    <br />
 <asp:Button ID="Button1" runat="server" Height="35px" Text="Return" Width="120px"/>
<style type="text/css" >
        .Contenders2 {  border-collapse: collapse; }
        .Contenders2 > tbody > .r1 > th,   
        .Contenders2 > tbody > .r1 > td { border: 1px ridge black; max-width:23px ; text-align:center;font-size:7px;  }
        .Contenders2 > tbody > .IconRow { max-height:15px}
        .Contenders2 > tbody > .IconRow > th {border: 1px ridge black; max-width:23px;text-align:center;font-size:7px;}
        .Contenders2 > tbody > .dRow1 > td {border: 1px ridge black;  font-size:7px;text-align:center;}
        .UserName1 {border: 1px ridge black; min-width:120px; font-size:7px;text-align:center;}
        
</style>
  
</asp:Content>
