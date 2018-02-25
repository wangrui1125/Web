<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatTest.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.WeChatTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #iframeContent
        {
            height: 476px;
        }
        </style>
</head>
<body>
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("50", "50")%>
    <form id="form1" runat="server">
    <div style="height: 35px; width: 900px;">            
        <asp:Panel ID="Panel1" runat="server">
            <asp:Label ID="Label1" runat="server" Text="Label" Height="30px" style="text-align: center"></asp:Label>

            <asp:DropDownList ID="DropDownList1" 
            runat="server" Height="30px" style="text-align: center">
            </asp:DropDownList>

            <asp:Button ID="Button1" runat="server" Height="30px" onclick="Button1_Click" 
                Text="提交" Width="80px" />

            <asp:Button ID="Button2" runat="server" Height="30px" onclick="Button2_Click" 
                Text="返回" Width="80px" />
        </asp:Panel>
    &nbsp;</div>
    <div>
   <iframe id="iframeContent" name="iframeContent" scrolling="auto" frameborder="0"
        src="../Tmp/MyDetail1.aspx?n=detailTechRequire<%=GetUrlParams()%>"
        width="100%"></iframe>
    </div>
    </form>
</body>
</html>
