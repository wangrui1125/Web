<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDetail1.aspx.cs" Inherits="MyQuery.Web.Tmp.MyDetail1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>信息详述</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/mygridview.js" type="text/javascript"></script>
    <script src="../Js/calendar.js" type="text/javascript"></script>
    <script src="../Js/helptip.js" type="text/javascript"></script>
</head>
<body onload="showWait(false)">
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("150","150")%>
    <form id="form1" runat="server">
    
    <div class="tab" id="divTab" runat="server">
    </div>
    <hr style="width: 100%; height: 0px; border: 0px;" runat="server" id="hrFirefox"
        visible="false" />
    <sy:MyBtns ID="MyBtns1" runat="server" CssClass="operation" OnButtonCommand="MyBtns1_ButtonCommand" />
    <asp:PlaceHolder ID="phContent" runat="server"></asp:PlaceHolder>
    <div class="notes" id="divNotes" runat="server">
    </div>
    <sy:MyBtns ID="MyBtns2" runat="server" CssClass="operation" OnButtonCommand="MyBtns1_ButtonCommand" />
    </form>
</body>
</html>