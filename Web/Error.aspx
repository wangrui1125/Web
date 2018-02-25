<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Error.aspx.cs"
    Inherits="MyQuery.Web.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>错误提示</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/Css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin: 20px auto 1px auto; text-align: center; width: 90%;">
        <asp:Image ID="Image1" AlternateText="双击查看具体原因" runat="server" />
        <br />
        <br />
        <asp:Label ID="lblErr" runat="server">您无权访问此功能，请稍候再试</asp:Label>
        <br />
        <asp:TextBox ID="txtErr" runat="server" TextMode="MultiLine" Rows="5" Width="99%"
            ReadOnly="true" Style="display: none;"></asp:TextBox>
    </div>
    </form>
</body>
</html>
