<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Eula.aspx.vb" Inherits="RussBucksPools.Eula" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>RussBucksPools EULA Agreement</h3>
        <div id ="CodeKey1" visible="false">
        <asp:Label ID="Label1" runat="server" Text="Enter Code"></asp:Label>
        <asp:TextBox ID="CommisionerCode1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Submit1" runat="server" Text="Button" onclick="Submit1_Click"/>
        </div>
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <textarea id="TextArea1" cols="20" rows="4">RussBucksPools sole purpose is to provide licensees entertainment by providing them the ability to track their and their friends sports picks. Russbucks pools does not assume any liability for any losses by any user for any reason including any financial losses which are incurred by any user as a result of any betting made on any sporting event.</textarea>
            <asp:Button ID="Disagree1" runat="server" Text="I Disagree" Onclick="Disagree1_Click"/>
            <asp:Button ID="Agree1" runat="server" Text="I Agree" Onclick="Agree1_Click"/>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
