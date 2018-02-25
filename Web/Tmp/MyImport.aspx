<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyImport.aspx.cs" Inherits="MyQuery.Web.Tmp.MyImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入文件数据</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/helptip.js" type="text/javascript"></script>
</head>
<body onload="showWait(false)">
    <script type="text/javascript">
        function check() {
            var isOk = true;
            var errMsg = "";
            var inputFile = document.getElementById("fileImport");
            var typeFile = document.getElementById("type");
            if (inputFile && typeFile) {
                if (typeFile.options[typeFile.selectedIndex].value == "xls") {
                    isOk = inputFile.value.search(/[.xls|.xlsx|.xlsm|.xlsb|.xml]$/i) != -1;
                    errMsg = '<%=GetMessage("xls") %>';
                }
                else if (typeFile.options[typeFile.selectedIndex].value == "txt") {
                    isOk = inputFile.value.search(/[.txt|.csv]$/i) != -1;
                    errMsg = '<%=GetMessage("txt") %>';
                }
                else if (typeFile.options[typeFile.selectedIndex].value == "xml") {
                    isOk = inputFile.value.search(/.xml$/i) != -1;
                    errMsg = '<%=GetMessage("xml") %>';
                }
            }
            if (isOk) {
                showWait(true);
                return true;
            }
            else {
                alert(errMsg);
                return false;
            }
        }
    </script>
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("150","150")%>
    <form id="form1" runat="server">
    <div class="editblock">
        <table border="0" cellpadding="0" cellspacing="0">
            <colgroup>
                <col width="100px" />
                <col width="200px" />
                <col width="100px" />
                <col width="100px" />
                <col width="80px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    <asp:Label ID="lbltitle" runat="server" Text="导入数据"></asp:Label>
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddlTitle" runat="server" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'<%=GetMessage("tiptitle") %>');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    <asp:Label ID="lbltype" runat="server" Text="文件类型"></asp:Label>
                </th>
                <td>
                    <asp:DropDownList ID="type" runat="server" AutoPostBack="true" Width="180px" OnSelectedIndexChanged="type_SelectedIndexChanged">
                        <asp:ListItem Value="xls">数据来自Excel文件</asp:ListItem>
                        <asp:ListItem Value="xml">数据来自xml文件</asp:ListItem>
                        <asp:ListItem Value="txt">数据来自txt文件</asp:ListItem>
                        <asp:ListItem Value="urlxml">数据来自Url链接</asp:ListItem>
                        <asp:ListItem Value="table">数据来自表或视图</asp:ListItem>
                        <asp:ListItem Value="procedure">数据来自存储过程</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'<%=GetMessage("tiptype") %>')"
                        src="../Img/shm.gif" />
                </td>
            </tr>
            <tr id="trDatabase" runat="server" visible="false">
                <th>
                    <asp:Label ID="lbldbtype" runat="server" Text="数据库类型"></asp:Label>
                </th>
                <td>
                    <asp:DropDownList ID="dbtype" runat="server" EnableViewState="False" Width="200px">
                        <asp:ListItem Value="" Selected="True">默认web.config配置</asp:ListItem>
                        <asp:ListItem Value="sql2005">SQL Server2005及以上数据库</asp:ListItem>
                        <asp:ListItem Value="sql2000">SQL Server2000及以下数据库</asp:ListItem>
                        <asp:ListItem Value="oracle">Oracle数据库</asp:ListItem>
                        <asp:ListItem Value="oledb">OleDB连接数据库</asp:ListItem>
                        <asp:ListItem Value="odbcdb2">DB2数据库</asp:ListItem>
                        <asp:ListItem Value="odbcinformix">Informix数据库</asp:ListItem>
                        <asp:ListItem Value="odbc">ODBC数据源</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    <asp:Label ID="lblconnectionstring" runat="server" Text="连接字符串<br />或文件链接"></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="connectionstring" runat="server" EnableViewState="False" ToolTip="可选设置 当数据来自表或存储过程时设置数据库连接字符串； 当数据来自xml、Excel或文本文件时请选择文件； 当数据来自Url链接（必须返回xml数据）时设置为http或WebServise （注意Methodname是区分大小写的）或ftp 注意：当配置了链接参数集合或查询条件时将以name=value提交，需要url访问地址的支持"
                        CssClass="input_text" Width="100px"></asp:TextBox>
                </td>
                <th>
                    <asp:Label ID="lblprocedurename" runat="server" Text="数据源<br />存储过程名"></asp:Label>
                </th>
                <td>
                    <asp:TextBox ID="procedurename" runat="server" EnableViewState="False" ToolTip="当数据来源表时必须配置为表名，可连接多个表或子查询，可以加上where及条件；当调用存储过程时必须配置存储过程名，可带参数值；当数据来源于Excel或Txt文件时true表示首行为标题行 false时表示无标题行 空则默认true首行为标题行"
                        CssClass="input_text" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblmethod" runat="server" Text="提交方式"></asp:Label>
                </th>
                <td>
                    <asp:DropDownList ID="method" runat="server" EnableViewState="False" Width="200px">
                        <asp:ListItem Value="" Selected="True">无</asp:ListItem>
                        <asp:ListItem Value="POST">from表单提交</asp:ListItem>
                        <asp:ListItem Value="GET">Url参数提交</asp:ListItem>
                        <asp:ListItem Value="STOR">ftp默认</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th colspan="2">
                    <asp:Label ID="lblparameters" runat="server" Text="链接参数或间隔字符"></asp:Label>
                </th>
                <td colspan="2">
                    <asp:TextBox ID="parameters" runat="server" EnableViewState="False" ToolTip="需要提供的链接参数集合 可选属性 必须为name=value的格式，多个用,分割 当获取数据需要的用户帐户与密码信息时格式id={0},pwd={1} id和pwd可根据实际调整名称 {0}为当前用户帐户{1}当前用户密码；当数据来源于txt文本时可设置数据字段间隔字符"
                        CssClass="input_text" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <asp:Label ID="lblfileimport" runat="server" Text="请选择文件"></asp:Label>
                </th>
                <td colspan="3">
                    <asp:FileUpload ID="fileImport" runat="server" Width="95%" />
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="IsFirstTitle" runat="server" Text="文件的首行为标题行" Checked="true" />
                </td>
            </tr>
        </table>
    </div>
    <div class="operation">
        <asp:Button ID="btnAdmin" runat="server" Visible="false" Text="设计" CausesValidation="false" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSubmit" runat="server" Text="提 交" CssClass="input_button" OnClick="btnSubmit_Click" />
    </div>
    <div style="text-align: left; padding-left: 10px;">
        <b><span style="color: Red">
            <%=GetMessage("errorspan")%></span></b>
        <ul id="ulContent" runat="server" style="list-style-type: decimal">
        </ul>
    </div>
    <div style="text-align: left; padding-left: 10px; font-weight:bold; " id="divErrFile" runat="server"
        visible="false">
    </div>
    </form>
</body>
</html>
