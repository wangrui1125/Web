<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyQuery.aspx.cs" Inherits="MyQuery.Web.Tmp.ListQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息查询</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/mygridview.js" type="text/javascript"></script>
    <script src="../Js/calendar.js" type="text/javascript"></script>
    <script src="../Js/helptip.js" type="text/javascript"></script>
</head>
<body onload="showWait(false)">
    <script type="text/javascript">
        function showQuery(objImg) {
            var objQuery = document.getElementById("trQuery");
            if (objQuery) {
                if (objQuery.style.display == "none") {
                    objQuery.style.display = "";
                    objImg.src = "../Img/r.gif";
                    objImg.alt = '<%=GetMessage("hide") %>';
                }
                else {
                    objQuery.style.display = "none";
                    objImg.src = "../Img/r_d.gif";
                    objImg.alt = '<%=GetMessage("show") %>';
                }
            }
        }    
    </script>
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("150","150")%>
    <form id="form1" runat="server" onsubmit="return showWait(true)">
    <div id="divQuery" runat="server" style="z-index: 1; position: absolute; top: -2px;
        right: 5px; cursor: pointer;">
        <img src="../Img/r.gif" alt='<%=GetMessage("hide") %>' border="0" onclick="showQuery(this)" />
    </div>
    <table border="0" class="searchblock" id="tblSearch" runat="server">
        <tr id="trToolTip" runat="server">
            <td>
                &nbsp;&nbsp;
                <asp:Label ID="lblDef" runat="server"></asp:Label>
            </td>
            <td align="center">
                <input type="button" class="input_button" value='<%=GetMessage("subject") %>' id="btnSubject" runat="server" />
            </td>
        </tr>
        <tr id="trQuery" runat="server">
            <td>
                <sy:MyInputs ID="MyInputs1" runat="server" EnableViewState="false" />
            </td>
            <td align="center">
                <asp:Button ID="btnQuery" runat="server" Text="查 询" CssClass="input_button" OnClick="btnQuery_Click" />
            </td>
        </tr>
    </table>
    <sy:MyBtns ID="MyBtns1" runat="server" CssClass="buttonblock" OnButtonCommand="MyBtns1_ButtonCommand" />
    <sy:MyGridView ID="MyGridView1" runat="server" OnPageIndexChanged="MyGridView1_PageIndexChanged"
        OnRowCommand="MyGridView1_RowCommand" OnSorting="MyGridView1_Sorting" CssClass="list" />
    <div class="notes" id="divNotes" runat="server">
    </div>
    <sy:MyBtns ID="MyBtns2" runat="server" CssClass="buttonblock" OnButtonCommand="MyBtns1_ButtonCommand" />
    </form>
</body>
</html>
