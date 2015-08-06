<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="WeeklyLoserPoolUserEntry1.aspx.vb" Inherits="RussBucksPools.WeeklyLoserPoolUserEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" >

    <div class="container1">    

    
       <h2><%: CStr(Session("dayNumber"))%></h2><br />
       <asp:Label ID ="loser1" Text="You are a Loser!!!" runat="server"></asp:Label><br />
       <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" CssClass="button1"/>
       <asp:Button ID="Button2" runat="server" Text="Submit" onclick="Button2_Click" CssClass="button1"/>
       <br />
       <br />

  <div class="container2 clearfix">
         <div id="GameElement1" style=" text-align: center; ">
    <asp:Table ID="GameTable1" runat="server" GridLines="None" CssClass="GameTable gT1" CellPadding="-1" CellSpacing="-1" >
        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber1" ColumnSpan="2" style="text-align:center; " Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber2" ColumnSpan="2" style="text-align:center; " Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber3" ColumnSpan="2" style="text-align:center"   Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber4" ColumnSpan="2" style="text-align:center"   Width="30px" ></asp:TableHeaderCell>
        
        </asp:TableHeaderRow>
        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber1Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber2Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber3Status" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber4Status" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow Height="60px">
            <asp:TableHeaderCell ID="HomeTeam1Image" style="text-align:center"  Width="15px" ><asp:Image ID="HomeTeam1Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam1Image" style="text-align:center"  Width="15px" ><asp:Image ID="AwayTeam1Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam2Image" style="text-align:center"  Width="15px" ><asp:Image ID="HomeTeam2Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam2Image" style="text-align:center"  Width="15px" ><asp:Image ID="AwayTeam2Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam3Image" style="text-align:center; " Width="15px"  ><asp:Image ID="HomeTeam3Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam3Image" style="text-align:center; " Width="15px"  ><asp:Image ID="AwayTeam3Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam4Image" style="text-align:center; " Width="15px"  ><asp:Image ID="HomeTeam4Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam4Image" style="text-align:center; " Width="15px"  ><asp:Image ID="AwayTeam4Image1" runat="server" /></asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell ID="HomeTeam1Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam1Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam1Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam1Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam2Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam2Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam2Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam2Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam3Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam3Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam3Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam3Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam4Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam4Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam4Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam4Icon1" runat="server" /></asp:TableHeaderCell>
      
        </asp:TableHeaderRow>

        <asp:TableRow Height="15px">
            <asp:TableCell ID="HomePick1" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game1HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick1" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game1AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick2" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game2HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick2" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game2AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick3" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game3HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick3" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game3AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick4" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game4HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick4" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game4AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
        
        </asp:TableRow>
    </asp:Table>
      
    <asp:Table ID ="GameTable2" runat="server"  GridLines="None" CssClass="GameTable gT2" CellPadding="-1" CellSpacing="-1" >

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber5" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber6" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber7" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber8" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber5Status" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber6Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber7Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber8Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="60px">
            <asp:TableHeaderCell ID="HomeTeam5Image" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam5Image1"  runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam5Image" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam5Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam6Image" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam6Image1"  runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam6Image" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam6Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam7Image" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam7Image1"  runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam7Image" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam7Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam8Image" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam8Image1"  runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam8Image" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam8Image1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell ID="HomeTeam5Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam5Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam5Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam5Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam6Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam6Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam6Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam6Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam7Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam7Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam7Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam7Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam8Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam8Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam8Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam8Icon1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableRow Height="15px">
            <asp:TableCell ID="HomePick5" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game5HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick5" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game5AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick6" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game6HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick6" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game6AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick7" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game7HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick7" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game7AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick8" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game8HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick8" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game8AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>

        </asp:TableRow>
    </asp:Table>
      
    <asp:Table ID ="GameTable3" runat="server"  GridLines="None" CssClass="GameTable gT3" CellPadding="-1" CellSpacing="-1" >

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber9"  ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber10" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber11" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber12" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber9Status" ColumnSpan="2" style="text-align:center" Width="30px"   ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber10Status" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber11Status" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber12Status" ColumnSpan="2" style="text-align:center" Width="30px"  ></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="60px">
            <asp:TableHeaderCell ID="HomeTeam9Image" style="text-align:center"  Width="15px"  ><asp:Image ID="HomeTeam9Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam9Image" style="text-align:center"  Width="15px" ><asp:Image ID="AwayTeam9Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam10Image" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam10Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam10Image" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam10Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam11Image" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam11Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam11Image" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam11Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam12Image" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam12Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam12Image" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam12Image1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell ID="HomeTeam9Icon" style="text-align:center" Width="15px"  ><asp:Image ID="HomeTeam9Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam9Icon" style="text-align:center" Width="15px"  ><asp:Image ID="AwayTeam9Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam10Icon" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam10Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam10Icon" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam10Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam11Icon" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam11Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam11Icon" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam11Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam12Icon" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam12Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam12Icon" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam12Icon1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableRow Height="15px">
            <asp:TableCell ID="HomePick9" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game9HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick9" style="text-align:center" Width="15px"  ><asp:RadioButton ID="Game9AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick10" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game10HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick10" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game10AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick11" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game11HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick11" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game11AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick12" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game12HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick12" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game12AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
        
        </asp:TableRow>
    </asp:Table>
      
    <asp:Table ID ="GameTable4" runat="server"  GridLines="None" CssClass="GameTable gT4" CellPadding="-1" CellSpacing="-1" >

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber13" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber14" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber15" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell Id="GameNumber13Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber14Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>
            <asp:TableHeaderCell Id="GameNumber15Status" ColumnSpan="2" style="text-align:center" Width="30px" ></asp:TableHeaderCell>

        </asp:TableHeaderRow>

        <asp:TableHeaderRow Height="60px">
            <asp:TableHeaderCell ID="HomeTeam13Image" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam13Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam13Image" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam13Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam14Image" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam14Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam14Image" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam14Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam15Image" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam15Image1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam15Image" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam15Image1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableHeaderRow Height="15px">
            <asp:TableHeaderCell ID="HomeTeam13Icon" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam13Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam13Icon" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam13Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam14Icon" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam14Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam14Icon" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam14Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="HomeTeam15Icon" style="text-align:center" Width="15px" ><asp:Image ID="HomeTeam15Icon1" runat="server" /></asp:TableHeaderCell>
            <asp:TableHeaderCell ID="AwayTeam15Icon" style="text-align:center" Width="15px" ><asp:Image ID="AwayTeam15Icon1" runat="server" /></asp:TableHeaderCell>

        </asp:TableHeaderRow>
        <asp:TableRow Height="15px">
            <asp:TableCell ID="HomePick13" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game13HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick13" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game13AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick14" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game14HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick14" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game14AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="HomePick15" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game15HomeRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>
            <asp:TableCell ID="AwayPick15" style="text-align:center" Width="15px" ><asp:RadioButton ID="Game15AwayRadio" Visible="false" runat="server" GroupName="PPick" /></asp:TableCell>

        </asp:TableRow>
    </asp:Table>

    </div>

    <div class ="sidebar">
      <h4>JJ's Box</h4>
         <div class="JJVideo1">
           <iframe  class="video1" src="https://www.youtube.com/embed/Z49zpEWgUA4"></iframe>
         </div>
      </div>
    </div>
   </div>

   <div class="container2">
       <asp:Button ID="Button3" runat="server" Text="Submit" onclick="Button1_Click" CssClass="button1"/>
       <asp:Button ID="Button4" runat="server" Text="Submit" onclick="Button2_Click" CssClass="button1"/>
   </div>

    <style type="text/css" >
        .GameTable {  border-collapse: collapse; }    
        .GameTable > tbody> tr > th, 
        .GameTable > tbody> tr > td { border: 1px ridge black; max-width:23px;font-size:9px }

        .container1 {margin:5px;padding:9px; min-height:275px;}
        .container2 {margin:5px;padding:9px; clear:both}

        .gT1 {float:left}
        .gT2 {float:left}
        .gT3 {float:left}
        .gT4 {float:left}

     .clearfix::before {content: " ";display: table;}
     .clearfix::after {clear:both;}
     .clearfix {*zoom:1;}

     .sidebar {margin:5px;padding:3px; float:left; text-align:center; }
     .video1 {margin:5px;border:2px ridge blue;padding:3px;height: 100px;width: 175px;}

    </style>

    <br />

</asp:Content>


<asp:Content ContentPlaceHolderID="SideContent" runat="server">
</asp:Content>
