<%@ Page Title="My Pools" Language="vb" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="MyPools.aspx.vb" Inherits="RussBucksPools.MyPools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            
            <div class="container1">
            <h2><%:Page.Title%></h2>

            <div id="MyPoolMenu" style="text-align:center">

              <asp:ListView id="myLoserPool" runat="server"  ItemType="RussBucksPools.MyPool1" SelectMethod="GetMyPools1" >

                <ItemTemplate>
                    <b style="font-size:large; font-style:normal">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Cssclass="mLP1" Text=<%#: Item.PoolName%>><%#: Item.PoolName%></asp:LinkButton>
                        <!--<a id="loserPoolId" href=<%#: Item.PageURL%> class="mLP1"><%#: Item.PoolName%></a>-->
                    </b>
                </ItemTemplate>

                <ItemSeparatorTemplate>   |   </ItemSeparatorTemplate>

            </asp:ListView>

            <br />
            <br />

          </div>
        </div>

    <style type="text/css">
        .container1 {text-align: center; border: ridge 2px blue; padding: 10px; color: #0000FF;}
        .mLP1 {text-align:center;border:ridge 2px blue;padding:2px;color:blue;font:bold 9px arial;}
        .mPP1 {text-align:center;border:ridge 2px blue;padding:2px;color:blue;font:bold 9px arial;}
    </style>
</asp:Content>
