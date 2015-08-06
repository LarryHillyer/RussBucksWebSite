<%@ Page Title="Loser Pool" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LoserPoolHome.aspx.vb" Inherits="RussBucksPools.LoserPoolHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
      <div>
        <hgroup>
          <h2><%:Page.Title + "  - " + CStr(Session("poolAlias")) + " - " + CStr(Session("TimePeriod"))%></h2>
          <h3>Don't Let Jumpin Janet</h3>
          <h3>Call You A Loser!!!</h3>
        </hgroup>
        <br />
        <div id="MyOptions" class="currentdata1" >
          <asp:label runat="server" CssClass="cC1" >Current Choices</asp:label><br /><br />
          <asp:ListView ID="enterUserData" runat="server" ItemType="RussBucksPools.MyOption" 
              SelectMethod="GetMyOptions"  >
            <ItemTemplate>
             <div >
              <b>
                <a Id="enterUserData1" href=<%#: Item.PageURL%> class="eUD1" >
                <%#: Item.TimePeriod%></a>
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
          <asp:ListView ID="completedTimePeriods" runat="server" ItemType="RussBucksPools.TimePeriodCompleted" SelectMethod="completedTimePeriods2_GetData">
              <ItemTemplate >                  
                  <b>
                    <a id="completedTimePeriods1" href="LoserPoolHome.aspx?Id=<%#: Item.TimePeriod%>" class="cD1"><%#: CStr(Item.TimePeriod)%></a>
                  </b>                
              </ItemTemplate>
              <ItemSeparatorTemplate> | </ItemSeparatorTemplate>
          </asp:ListView> 

      <br />
      <asp:Label ID="Administration1" runat="server" Text="Adminstration" Visible="false"></asp:Label>
      <br />
      

      <asp:HyperLink ID="ChangeUserPick1" runat="server" NavigateUrl="~/LosersPool/Administration/ChangeUserPick.aspx" Visible="false">Change User Pick</asp:HyperLink>

      <br />
         
   
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
