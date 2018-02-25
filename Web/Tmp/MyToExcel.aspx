<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyToExcel.aspx.cs" Inherits="MyQuery.Web.Tmp.MyToExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导出Excel</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/helptip.js" type="text/javascript"></script>
    <script type="text/javascript">
    //<![CDATA[
        function checkAll(obj) {
            var checkObj = document.getElementsByTagName("input");
            for (i = 0; i < checkObj.length; i++) {
                if (checkObj(i).type == "checkbox" && checkObj(i).name != obj.name) {
                    checkObj(i).checked = obj.checked;
                }
            }
        }   
      //]]>
    </script>
</head>
<body onload="showWait(false)">
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("50","50")%>
    <form id="form1" runat="server" onsubmit="return showWait(true)">
    <sy:MyInputs ID="MyInputs1" CssClass="searchblock" runat="server" EnableViewState="false" />
    <asp:PlaceHolder ID="phContent" runat="server" Visible="false"></asp:PlaceHolder>
    <div class="operation">
        <asp:Button ID="btnAdmin" runat="server" Visible="false" Text="设计" CausesValidation="false" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="ckbAll" runat="server" Text="全选" onclick="checkAll(this)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="input_button" OnClick="btnExcel_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'请选中需要导出的列（数据项）；请确认已关闭下载工具，本导出仅支持直接保存后用Excel打开');"
            src="../Img/shm.gif" />
    </div>
    </form>
</body>
</html>
