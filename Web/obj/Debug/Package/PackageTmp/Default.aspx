<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MyQuery.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>科小觅管理平台</title>
    <link href="CSS/menu.css" rel="stylesheet" type="text/css" />
    <script src="JS/menu.js" type="text/javascript"></script>
    <script src="Js/popupWindow.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onload = function () {
            DynarchMenu.setup("menu", { scrolling: true, electric: 800, timeout: 200 });
            showTime();
            getFrameHeight();
        } 
        function getFrameHeight() {
            var ifm = document.getElementById("iframeContent");
            if (ifm) {
                ifm.height = (parseInt(document.documentElement.clientHeight, 10) - 95) + "px";
            }
        }
       function showTime() {
            <%if (CurrentUser!=null){ %>
            var userid = "<%=CurrentUser.Id %>";
            var waitingTasks = AjaxGet("WebService.asmx", "GetWFWaiting", "userid=" + userid);
            if (parseFloat(waitingTasks) > 0) {
                var waitCount = parseInt(waitingTasks.split(".")[0], 10);
                var doCount = parseInt(waitingTasks.split(".")[1], 10);
                waitingTasks = "";
                if (waitCount > 0) {
                    waitingTasks = "<a href='Tmp/MyDetail.aspx?n=detailWFWaitting&i=0&title=0' target='iframeContent'>待办[" + waitCount + "]条</a><br/>";
                }
                if (doCount > 0) {
                    waitingTasks += "<a href='Tmp/MyDetail.aspx?n=detailWFWaitting&i=1&title=0' target='iframeContent'>在办[" + doCount + "]条</a>";
                }
            }
            else {
                waitingTasks = "";
            }
            popupWin(waitingTasks, "任务提醒", 100, 60);
            setTimeout("showTime()", 30000);
<%} %>
        }

    </script>
    <meta http-equiv="Window-target" content="_top" />
</head>
<body scroll="no" style="overflow:hidden;" onresize="getFrameHeight()">
    <form id="form1" runat="server">
    <!--head-->
    <div style="height: 55px; padding-left: 5px; padding-right: 5px; position: relative;
        background: url(Img/wrapBack.jpg) no-repeat right;">
        <div style="float: left;color:White;font-size:26px;font-weight:bold;padding-left:15px;padding-top:20px;">
            科小觅管理平台
        </div>
        <dl style="text-align: center; margin-top: 15px; width: 260px; line-height: 1.5em;
            color: #666; float: right;">
            <dt>
                <%=welcome %>&nbsp;&nbsp;<a href="Sys/LogOut.aspx" target="_self">安全退出</a></dt>
        </dl>
    </div>
    <!--menu-->
    <div id="menuHolder" style="padding-left: 5px; padding-right: 5px; height: 24px;
        padding-top: 2px; vertical-align: top; background-image: url(img/nav_bg.gif)"
        runat="server">
    </div>
    <!--mian-->
        <iframe id="iframeContent" name="iframeContent" scrolling="auto" frameborder="0"
            src="<%=Url %>"  width="100%" height="500"
        style="margin-left: 5px; margin-right: 5px;"></iframe>
    </form>
</body>
</html>
