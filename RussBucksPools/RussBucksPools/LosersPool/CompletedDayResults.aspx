<%@ Page Title="Completed Results" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CompletedDayResults.aspx.vb" Inherits="RussBucksPools.CompletedTimePeriodResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <h2><%:Page.Title + "  - " + CStr(Session("poolAlias")) + " - " + CStr(Session("completedTimePeriod"))%></h2><br />
  <div id="GameElement1" >
    <asp:Table ID="TeamsByGameTable1" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
        <asp:TableRow Height="15px" CssClass="r1">
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

        </asp:TableRow>
        <asp:TableRow Height="15px" CssClass="r1">
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

        </asp:TableRow>
        <asp:TableRow Height="60px" CssClass="r1">
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

        </asp:TableRow>
        <asp:TableRow CssClass="IconRow" >
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

        </asp:TableRow>

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
    <h3>Picks Available to Contenders</h3>
    <br />
    <div id ="Contenders3" style=" text-align: center; vertical-align: middle;">

        <asp:Table ID="Contenders4" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
            <asp:TableHeaderRow Height="60px" CssClass="r1">
                <asp:TableHeaderCell Id ="ContendersName" Width="120px" CssClass="UserName1">Contender's Name</asp:TableHeaderCell>
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

            <asp:TableHeaderRow Height="15px" CssClass="IconRow">
                <asp:TableHeaderCell Id ="ContendersName1" Width="120px" CssClass="UserName1"></asp:TableHeaderCell>
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
        .Contenders2 {  border-collapse: collapse; }
        .Contenders2 > tbody > .r1 > th,   
        .Contenders2 > tbody > .r1 > td { border: 1px ridge black; max-width:23px ; text-align:center; font-size:7px;}
        .Contenders2 > tbody > .IconRow { max-height:15px}
        .Contenders2 > tbody > .IconRow > th {border: 1px ridge black; max-width:23px;text-align:center;font-size:7px;}
        .Contenders2 > tbody > .dRow1 > td {border: 1px ridge black;  font-size:7px;text-align:center;}
        .UserName1 {border: 1px ridge black; min-width:120px; font-size:7px;text-align:center;}
        .LoserTable1 {  border-collapse: collapse; }    
        .LoserTable1 > tbody > tr > th, 
        .LoserTable1 > tbody > tr > td { border: 1px ridge black; padding: 3px;text-align:center;font-size:7px; }
</style>
  
</asp:Content>
