<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="MyQuery.Web.Test._Image" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>图片查看</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center; padding-top:50px;">
        <asp:Image ID="Image1" runat="server" />
    </div>
    <div class="operation">
    <input type="button" value="返 回"  class="input_button"
            onclick="window.history.go(-1)" /></div>
    </form>
</body>
</html>
