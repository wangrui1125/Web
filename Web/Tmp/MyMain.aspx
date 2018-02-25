<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyMain.aspx.cs" Inherits="MyQuery.Web.Tmp.MyMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>弹出窗口</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript">
        function getFrameHeight(height) {
            document.getElementById("iframeMain").height = Math.max(parseInt(document.documentElement.clientHeight, 10) - 30, height);
        }
    </script>
</head>
<body scroll="no" style="overflow: hidden;" onload="getFrameHeight(500)">
    <form id="form1" runat="server">
    <sy:MyTabPage ID="MyTabPage1" runat="server" EnableViewState="false" CssClass="tab"
        Target="iframeMain">
    </sy:MyTabPage>
    <div style="z-index:99; position:absolute; top:2px; right:5px;" id="divButtons" runat="server"></div>
    <iframe id="iframeMain" name="iframeMain" scrolling="auto" frameborder="0" src="<%=Url %>"
        width="100%" height="500"></iframe>
    </form>
</body>
</html>
