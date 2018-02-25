<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditHtml.aspx.cs" Inherits="MyQuery.Web.Tmp.EditHtml"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HTML模板编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Pragma" content="no-cache" />
    <script src="../Js/calendar.js" type="text/javascript"></script>
    <script src="../Js/ClientValidate.js" type="text/javascript"></script>
</head>
<body onload="showWait(false)">
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("150","150")%>
    <form id="frmEdit" method="post" runat="server" onsubmit="return showWait(true)">
    <div class="editblock">
        <table cellspacing="0" cellpadding="0" border="0" width="1000px">
            <colgroup>
                <col width="150px" />
                <col width="200px" />
                <col width="150px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    HTML模板文件
                </th>
                <td>
                    <asp:TextBoxVal ID="HtmlUrl" runat="server" MaxLength="100" Width="200px" ClientValidate="r_nchar"
                        ToolTip="HTML模板文件不能为空" Enabled="false"></asp:TextBoxVal>
                </td>
                <th>
                    预览对应Url
                </th>
                <td>
                    <asp:TextBoxVal ID="AspxUrl" runat="server" MaxLength="100" Width="500px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"
                        ToolTip="预览对应Url文件（空则程序自动获取）"></asp:TextBoxVal>
                </td>
            </tr>
            <tr>
                <th>
                    归属单位
                </th>
                <td>
                    <asp:DropDownList ID="DepId" runat="server" Width="205px">
                    </asp:DropDownList>
                </td>
                <th>
                    描述
                </th>
                <td>
                    <asp:TextBoxVal ID="txtTitle" runat="server" MaxLength="100" Width="500px" ClientValidate="r_"
                        ToolTip="描述不能为空"></asp:TextBoxVal>
                </td>
            </tr>
        </table>
        <fieldset>
            <legend>HTML模板编辑</legend>
            <asp:TextBox ID="Content" runat="server" EnableViewState="False" TextMode="MultiLine"
                Rows="10" Width="1000px" ToolTip="模板编辑注意：#为自定义标签，其它均为html标准标签，网站模板的生成文件在WWW\html下（打印模板默认路径为Tmp）"></asp:TextBox>
        </fieldset>
        <fieldset>
            <legend>XML数据配置</legend>
            <asp:TextBox ID="txtXml" runat="server" EnableViewState="False" TextMode="MultiLine"
                Rows="10" Width="1000px" ToolTip="属性设置注意，表达逻辑的属性的值必须是true或false，其它值则按照程序默认的逻辑值处理，数值型的属性必须指定正确的数值，否则按照程序默认值处理，可选择的字符串是必须是说明中列出的英文字符"></asp:TextBox>
        </fieldset>
        <div class="operation">
            <asp:Button ID="btnSubmit" runat="server" Text="提  交" OnClick="btnSubmit_Click" CssClass="input_button" />
        </div>
        <div>
            &nbsp;&nbsp;<a href="down.aspx?f=\tpl\<%=name%>.htm" target="_self"><strong>下载本Html模板</strong></a>
            &nbsp;&nbsp;<a href="down.aspx?f=\query\<%=name%>.xml" target="_self"><strong>下载本XML配置</strong></a>
            &nbsp;&nbsp;<a href="EditMyQuery.aspx?n=<%=name%>" target="_self"><strong>转设计XML配置界面</strong></a>
            &nbsp;&nbsp;<a href="down.aspx?f=\Demo.xml" target="_self"><strong>下载XML配置详细说明及示例文档</strong></a>
        </div>
        <fieldset id="divUp" runat="server" visible="false" style="width: 1024px">
            <legend>HTML模板上传</legend>HTML模板文件:<input id="fileHtml" type="file" runat="server" size="35" />
            &nbsp;&nbsp;Xml数据配置：<input id="fileXml" type="file" runat="server" size="35" />
            &nbsp;&nbsp;<asp:Button ID="btnUp" runat="server" Text="上传模板" CssClass="input_button"
                OnClick="btnUp_Click" Visible="false" />
        </fieldset>
    </div>
    </form>
</body>
</html>
