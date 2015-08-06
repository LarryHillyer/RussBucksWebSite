<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CompletedDayResults1.aspx.vb" Inherits="RussBucksPools.CompletedDayResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <h2><%:CStr(Session("completedDayId"))%></h2>
    <br />
  <div id="GameElement1" >
    <asp:Table ID="TeamsByGameTable1" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
        <asp:TableRow Height="15px">
            <asp:TableHeaderCell ID="Nothing1"  Width="120px"></asp:TableHeaderCell>
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

        </asp:TableRow>
        <asp:TableRow Height="15px">
            <asp:TableHeaderCell ID="Status1"  Width="120px"></asp:TableHeaderCell>
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

        </asp:TableRow>
        <asp:TableRow Height="60px">
            <asp:TableHeaderCell ID="Nothing1A" Width="120px"></asp:TableHeaderCell> 
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

        </asp:TableRow>
        <asp:TableRow CssClass="IconRow" >
            <asp:TableHeaderCell ID="Nothing1C" Width="120px"></asp:TableHeaderCell> 
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

        </asp:TableRow>

        <asp:TableRow Height="15px">
            <asp:TableCell ID="Nothing1B" Width="120px"></asp:TableCell>
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

        </asp:TableRow>

    </asp:Table>
  </div>
    <h3>Picks Available to Contenders</h3>
    <br />
    <div id ="Contenders3" style=" text-align: center; vertical-align: middle;">

        <asp:Table ID="Contenders4" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
            <asp:TableHeaderRow Height="60px">
                <asp:TableHeaderCell Id ="ContendersName"     Width="120px">Contender's Name</asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="WashingtonImage"    Width="15px"><asp:Image ID="WashingtonImage2"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MiamiImage"         Width="15px"><asp:Image ID="MiamiImage2"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ColoradoImage"      Width="15px"><asp:Image ID="ColoradoImage2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ArizonaImage"       Width="15px"><asp:Image ID="ArizonaImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanFranciscoImage"  Width="15px"><asp:Image ID="SanFranciscoImage2"       runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanDiegoImage"      Width="15px"><asp:Image ID="SanDiegoImage2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PittsburgImage"     Width="15px"><asp:Image ID="PittsburgImage2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="CincinnatiImage"    Width="15px"><asp:Image ID="CincinnatiImage2"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TorontoImage"       Width="15px"><asp:Image ID="TorontoImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYYankeesImage"     Width="15px"><asp:Image ID="NYYankeesImage2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BostonImage"        Width="15px"><asp:Image ID="BostonImage2"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TampaBayImage"      Width="15px"><asp:Image ID="TampaBayImage2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="AtlantaImage"       Width="15px"><asp:Image ID="AtlantaImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PhiladelphiaImage"       Width="15px"><asp:Image ID="PhiladelphiaImage2"  runat ="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoWhiteSoxImage"  Width="15px"><asp:Image ID="ChicagoWhiteSoxImage2" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="DetroitImage"       Width="15px"><asp:Image ID="DetroitImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="KansasCityImage"    Width="15px"><asp:Image ID="KansasCityImage2"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ClevelandImage"     Width="15px"><asp:Image ID="ClevelandImage2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MilwaukeeImage"     Width="15px"><asp:Image ID="MilwaukeeImage2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LADodgersImage"     Width="15px"><asp:Image ID="LADodgersImage2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MinnesotaImage"     Width="15px"><asp:Image ID="MinnesotaImage2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="OaklandImage"       Width="15px"><asp:Image ID="OaklandImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="HoustonImage"       Width="15px"><asp:Image ID="HoustonImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TexasImage"         Width="15px"><asp:Image ID="TexasImage2"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="STLouisImage"       Width="15px"><asp:Image ID="STLouisImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoCubsImage"   Width="15px"><asp:Image ID="ChicagoCubsImage2"        runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LAAngelsImage"      Width="15px"><asp:Image ID="LAAngelsImage2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SeattleImage"       Width="15px"><asp:Image ID="SeattleImage2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYMetsImage"        Width="15px"><asp:Image ID="NYMetsImage2"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BaltimoreImage"     Width="15px"><asp:Image ID="BaltimoreImage2"          runat="server" /></asp:TableHeaderCell>
            </asp:TableHeaderRow>

            <asp:TableHeaderRow Height="15px">
                <asp:TableHeaderCell Id ="ContendersName1"     Width="120px"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="WashingtonIcon"    Width="15px"><asp:Image ID="WashingtonIcon2"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MiamiIcon"         Width="15px"><asp:Image ID="MiamiIcon2"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ColoradoIcon"      Width="15px"><asp:Image ID="ColoradoIcon2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ArizonaIcon"       Width="15px"><asp:Image ID="ArizonaIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanFranciscoIcon"  Width="15px"><asp:Image ID="SanFranciscoIcon2"       runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanDiegoIcon"      Width="15px"><asp:Image ID="SanDiegoIcon2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PittsburgIcon"     Width="15px"><asp:Image ID="PittsburgIcon2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="CincinnatiIcon"    Width="15px"><asp:Image ID="CincinnatiIcon2"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TorontoIcon"       Width="15px"><asp:Image ID="TorontoIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYYankeesIcon"     Width="15px"><asp:Image ID="NYYankeesIcon2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BostonIcon"        Width="15px"><asp:Image ID="BostonIcon2"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TampaBayIcon"      Width="15px"><asp:Image ID="TampaBayIcon2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="AtlantaIcon"       Width="15px"><asp:Image ID="AtlantaIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PhiladelphiaIcon"     Width="15px"><asp:Image ID="PhiladelphiaIcon2"    runat ="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoWhiteSoxIcon"  Width="15px"><asp:Image ID="ChicagoWhiteSoxIcon2" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="DetroitIcon"       Width="15px"><asp:Image ID="DetroitIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="KansasCityIcon"    Width="15px"><asp:Image ID="KansasCityIcon2"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ClevelandIcon"     Width="15px"><asp:Image ID="ClevelandIcon2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MilwaukeeIcon"     Width="15px"><asp:Image ID="MilwaukeeIcon2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LADodgersIcon"     Width="15px"><asp:Image ID="LADodgersIcon2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MinnesotaIcon"     Width="15px"><asp:Image ID="MinnesotaIcon2"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="OaklandIcon"       Width="15px"><asp:Image ID="OaklandIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="HoustonIcon"       Width="15px"><asp:Image ID="HoustonIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TexasIcon"         Width="15px"><asp:Image ID="TexasIcon2"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="STLouisIcon"       Width="15px"><asp:Image ID="STLouisIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoCubsIcon"   Width="15px"><asp:Image ID="ChicagoCubsIcon2"        runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LAAngelsIcon"      Width="15px"><asp:Image ID="LAAngelsIcon2"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SeattleIcon"       Width="15px"><asp:Image ID="SeattleIcon2"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYMetsIcon"        Width="15px"><asp:Image ID="NYMetsIcon2"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BaltimoreIcon"     Width="15px"><asp:Image ID="BaltimoreIcon2"          runat="server" /></asp:TableHeaderCell>
            </asp:TableHeaderRow>

        </asp:Table>
    </div>
    <br />
    <asp:Table Id="LoserTable1" runat="server"  GridLines="None" CssClass="LoserTable1" CellPadding="-1" CellSpacing="-1">
        <asp:TableHeaderRow >
            <asp:TableHeaderCell ID="LosersName"    >The Losers</asp:TableHeaderCell>
            <asp:TableHeaderCell Id="DayId1"        >Day</asp:TableHeaderCell>
            <asp:TableHeaderCell ID="LosingPick1"   >Losing Pick</asp:TableHeaderCell>
        </asp:TableHeaderRow>
    </asp:Table>
    <br />

 <br />
 <asp:Button ID="Button1" runat="server" Height="35px" Text="Return" Width="120px"/>
<style type="text/css" >
        .Contenders2 {  border-collapse: collapse; }
        .Contenders2 > tbody > tr > th,   
        .Contenders2 > tbody > tr > td { border: 1px ridge black; max-width:23px ; text-align:center; font-size:9px;}
        .Contenders2 > tbody > .IconRow { max-height:15px}
        .LoserTable1 {  border-collapse: collapse; }    
        .LoserTable1 > tbody > tr > th, 
        .LoserTable1 > tbody > tr > td { border: 1px ridge black; padding: 3px;text-align:center;font-size:9px; }

        .UserName1 { min-width:120px;}
</style>
  
</asp:Content>
