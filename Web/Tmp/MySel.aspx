<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySel.aspx.cs" Inherits="MyQuery.Web.Tmp.MySel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>弹出选择对话框</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<body>
    <form id="form1" runat="server">
    <iframe id="iframeContent" name="iframeContent" scrolling="auto" frameborder="0"
        src="MyQuery.aspx?n=<%=sParams %>" style="width: 100%; height: 590px;"></iframe>
    </form>
</body>
</html>
