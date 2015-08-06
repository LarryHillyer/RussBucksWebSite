<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Season End1.aspx.vb" Inherits="RussBucksPools.Season_End" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Season End   Final Results</h2>

    <br />
    <br />

    <div id ="Contenders1" style=" text-align: center; vertical-align: middle;">

        <asp:Table ID="Contenders2" runat="server" GridLines="None" CssClass="Contenders2" CellPadding="-1" CellSpacing="-1">
            <asp:TableHeaderRow Height="60px">
                <asp:TableHeaderCell Id ="ContendersName"     Width="120px">Contender's Name</asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="WashingtonImage"    Width="15px"><asp:Image ID="WashingtonImage1"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MiamiImage"         Width="15px"><asp:Image ID="MiamiImage1"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ColoradoImage"      Width="15px"><asp:Image ID="ColoradoImage1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ArizonaImage"       Width="15px"><asp:Image ID="ArizonaImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanFranciscoImage"  Width="15px"><asp:Image ID="SanFranciscoImage1"       runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanDiegoImage"      Width="15px"><asp:Image ID="SanDiegoImage1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PittsburgImage"     Width="15px"><asp:Image ID="PittsburgImage1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="CincinnatiImage"    Width="15px"><asp:Image ID="CincinnatiImage1"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TorontoImage"       Width="15px"><asp:Image ID="TorontoImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYYankeesImage"     Width="15px"><asp:Image ID="NYYankeesImage1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BostonImage"        Width="15px"><asp:Image ID="BostonImage1"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TampaBayImage"      Width="15px"><asp:Image ID="TampaBayImage1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="AtlantaImage"       Width="15px"><asp:Image ID="AtlantaImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PhiladelphiaImage"       Width="15px"><asp:Image ID="PhiladelphiaImage1"  runat ="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoWhiteSoxImage"  Width="15px"><asp:Image ID="ChicagoWhiteSoxImage1" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="DetroitImage"       Width="15px"><asp:Image ID="DetroitImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="KansasCityImage"    Width="15px"><asp:Image ID="KansasCityImage1"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ClevelandImage"     Width="15px"><asp:Image ID="ClevelandImage1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MilwaukeeImage"     Width="15px"><asp:Image ID="MilwaukeeImage1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LADodgersImage"     Width="15px"><asp:Image ID="LADodgersImage1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MinnesotaImage"     Width="15px"><asp:Image ID="MinnesotaImage1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="OaklandImage"       Width="15px"><asp:Image ID="OaklandImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="HoustonImage"       Width="15px"><asp:Image ID="HoustonImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TexasImage"         Width="15px"><asp:Image ID="TexasImage1"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="STLouisImage"       Width="15px"><asp:Image ID="STLouisImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoCubsImage"   Width="15px"><asp:Image ID="ChicagoCubsImage1"        runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LAAngelsImage"      Width="15px"><asp:Image ID="LAAngelsImage1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SeattleImage"       Width="15px"><asp:Image ID="SeattleImage1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYMetsImage"        Width="15px"><asp:Image ID="NYMetsImage1"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BaltimoreImage"     Width="15px"><asp:Image ID="BaltimoreImage1"          runat="server" /></asp:TableHeaderCell>
            </asp:TableHeaderRow>

            <asp:TableHeaderRow Height="15px">
                <asp:TableHeaderCell Id ="ContendersName1"     Width="120px"></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="WashingtonIcon"    Width="15px"><asp:Image ID="WashingtonIcon1"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MiamiIcon"         Width="15px"><asp:Image ID="MiamiIcon1"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ColoradoIcon"      Width="15px"><asp:Image ID="ColoradoIcon1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ArizonaIcon"       Width="15px"><asp:Image ID="ArizonaIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanFranciscoIcon"  Width="15px"><asp:Image ID="SanFranciscoIcon1"       runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SanDiegoIcon"      Width="15px"><asp:Image ID="SanDiegoIcon1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PittsburgIcon"     Width="15px"><asp:Image ID="PittsburgIcon1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="CincinnatiIcon"    Width="15px"><asp:Image ID="CincinnatiIcon1"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TorontoIcon"       Width="15px"><asp:Image ID="TorontoIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYYankeesIcon"     Width="15px"><asp:Image ID="NYYankeesIcon1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BostonIcon"        Width="15px"><asp:Image ID="BostonIcon1"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TampaBayIcon"      Width="15px"><asp:Image ID="TampaBayIcon1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="AtlantaIcon"       Width="15px"><asp:Image ID="AtlantaIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="PhiladelphiaIcon"     Width="15px"><asp:Image ID="PhiladelphiaIcon1"    runat ="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoWhiteSoxIcon"  Width="15px"><asp:Image ID="ChicagoWhiteSoxIcon1" runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="DetroitIcon"       Width="15px"><asp:Image ID="DetroitIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="KansasCityIcon"    Width="15px"><asp:Image ID="KansasCityIcon1"         runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ClevelandIcon"     Width="15px"><asp:Image ID="ClevelandIcon1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MilwaukeeIcon"     Width="15px"><asp:Image ID="MilwaukeeIcon1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LADodgersIcon"     Width="15px"><asp:Image ID="LADodgersIcon1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="MinnesotaIcon"     Width="15px"><asp:Image ID="MinnesotaIcon1"          runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="OaklandIcon"       Width="15px"><asp:Image ID="OaklandIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="HoustonIcon"       Width="15px"><asp:Image ID="HoustonIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="TexasIcon"         Width="15px"><asp:Image ID="TexasIcon1"              runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="STLouisIcon"       Width="15px"><asp:Image ID="STLouisIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="ChicagoCubsIcon"   Width="15px"><asp:Image ID="ChicagoCubsIcon1"        runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="LAAngelsIcon"      Width="15px"><asp:Image ID="LAAngelsIcon1"           runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="SeattleIcon"       Width="15px"><asp:Image ID="SeattleIcon1"            runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="NYMetsIcon"        Width="15px"><asp:Image ID="NYMetsIcon1"             runat="server" /></asp:TableHeaderCell>
                <asp:TableHeaderCell ID ="BaltimoreIcon"     Width="15px"><asp:Image ID="BaltimoreIcon1"          runat="server" /></asp:TableHeaderCell>
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
        .LoserTable1 {  border-collapse: collapse; }    
        .LoserTable1 > tbody > tr > th, 
        .LoserTable1 > tbody > tr > td { border: 1px ridge black; padding: 3px;text-align:center; font-size:9px; }
        .Contenders2 {  border-collapse: collapse; }    
        .Contenders2 > tbody > tr > th, 
        .Contenders2 > tbody > tr > td { border: 1px ridge black; max-width:23px;text-align:center;font-size:9px; }
        .UserName1 { min-width:120px;}
    </style>

</asp:Content>
