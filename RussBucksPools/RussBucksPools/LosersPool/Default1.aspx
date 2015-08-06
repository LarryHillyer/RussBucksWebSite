<%@ Page Title="Loser Pool" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Default1.aspx.vb" Inherits="RussBucksPools._Default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
      <div>
        <hgroup>
          <h2><%:Page.Title + "  - " + CStr(Session("dayNumber"))%></h2>
          <h3>Don't Let Jumpin Janet</h3>
          <h3>Call You A Loser!!!</h3>
        </hgroup>
        <br />
        <div id="MyOptions" class="currentdata1" >
          <asp:label runat="server" CssClass="cC1" >Current Choices</asp:label><br /><br />
          <asp:ListView ID="enterUserData" runat="server" ItemType="RussbucksPools.LosersPool.Models.User" 
              SelectMethod="GetMyOptions" >
            <ItemTemplate>
             <div >
              <b>
                <a Id="enterUserData1" href="../LosersPool/WeeklyLoserPoolUserEntry.aspx" class="eUD1" >
                <%#: Item.OptionState%></a>
              </b>
             </div>
            </ItemTemplate> 
          </asp:ListView>

          <asp:ListView ID="scoringUpdate" runat="server" ItemType="RussbucksPools.LosersPool.Models.User" 
              SelectMethod="GetMyOptions" >
            <ItemTemplate>
              <div >
              <b>
                <a Id="scoringUpdate11" href="../LosersPool/WeeklyScoringUpdate.aspx" Class="eUD1" >
                <%#: Item.OptionState%></a>
              </b>
              </div>
            </ItemTemplate>
          </asp:ListView>
          <br />
          <b>
          <a id="standingsDisplay1" href="../LosersPool/DisplayStandings.aspx" class="eUD1"> Display Standings</a>
          </b>
          <br />
        </div>
        </div>

          <br />
          <b>Previous Results</b>
          <br />
          <asp:ListView ID="completedDays" runat="server" ItemType="RussbucksPools.DayCompleted" SelectMethod="completedDays1_GetData">
              <ItemTemplate >
                  
                  <b>
                    <a id="completedDays" href="Default.aspx?Id=<%#: Item.DayId%>" class="cD1"><%#: CStr(Item.DayId)%></a>
                  </b>
                 
              </ItemTemplate>
              <ItemSeparatorTemplate> | </ItemSeparatorTemplate>
          </asp:ListView>      
   
    <style type="text/css">
        .currentdata1 {
            text-align: center;
            max-width: 120px;
            border: ridge 2px blue;
            padding: 10px;
            color: #0000FF;
        }
        .eUD1 { text-align:center;border:ridge 2px blue;
                padding:2px;color:blue;font:bold 9px arial;
                }
        .cC1 {text-align:center;font:bold 9px arial;margin-bottom:5px;color:blue}
        .cD1 {text-align:center;font:bold 9px arial;color:blue}
    </style>
</asp:Content>
