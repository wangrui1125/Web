<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyEdit.aspx.cs" Inherits="MyQuery.Web.Tmp.MyEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/calendar.js" type="text/javascript"></script>
    <script src="../Js/helptip.js" type="text/javascript"></script>
</head>
<body onload="showWait(false)">
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("50", "50")%>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post" onsubmit="return showWait(true)">
    <div class="tab" id="divTab" runat="server">
    </div>
    <hr style="width: 100%; height: 0px; border: 0px;" runat="server" id="hrFirefox"
        visible="false" />
    <sy:MyBtns ID="MyBtns1" runat="server" CssClass="operation" OnButtonCommand="MyBtns1_ButtonCommand" />
    <sy:MyInputs ID="MyInputs1" runat="server" CssClass="editblock" />
    <%if (IsMainSub())
      { if (String.IsNullOrEmpty(GetUrlParams())){%>
    <div class="notes" style="font-size: 12px">
        <asp:Label ID="lblNoID" runat="server" Text="请先录入相关信息，点击【提交】形成主单"></asp:Label>
        <br />
        <asp:Label ID="lblNoSub" runat="server" Text="在明细列表中【新增】录入明细，【编辑】修改选中的明细"></asp:Label>
        <br />
        <asp:Label ID="lblNext" runat="server" Text="主单和明细单完成后，点击【提交】提交当前编辑的主单和明细单并保存"></asp:Label>
    </div>
    <%} else{ %>
    <iframe id="iframeContent" name="iframeContent" scrolling="auto" frameborder="0"
        src="MyDetail.aspx?n=<%=name %>&type=sub&pagesize=<%=GetSubPageSize() %><%=GetUrlParams() %>&title=0"
        width="100%" height="1000px"></iframe>
    <%}} %>
    <sy:MyBtns ID="MyBtns2" runat="server" CssClass="operation" OnButtonCommand="MyBtns1_ButtonCommand" />
    <div class="notes" id="divNotes" runat="server">
    </div>
    </form>
</body>
</html>
