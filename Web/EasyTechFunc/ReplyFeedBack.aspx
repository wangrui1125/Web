<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyFeedBack.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.ReplyFeedBack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    
        <br />
    
    </div>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="TextBox1" runat="server" ontextchanged="TextBox1_TextChanged" 
        TextMode="MultiLine" Height="27px" Width="428px"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="回复" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="关闭页面" Width="56px" />
    <br />
    </form>
</body>
</html>
