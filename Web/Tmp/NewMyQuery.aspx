<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewMyQuery.aspx.cs" Inherits="MyQuery.Web.Tmp.NewMyQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建配置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/ClientValidate.js" type="text/javascript"></script>
    <script src="../Js/helptip.js" type="text/javascript"></script>
    <script src="../Js/mygridview.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="editblock">
        主表名：
        <asp:TextBoxVal ID="tablename" runat="server" EnableViewState="False" ToolTip="配置的主表名称，将根据此表的字段形成配置"
            ClientValidate="r_nchar" CssClass="input_text" Width="200px"></asp:TextBoxVal>
        &nbsp;&nbsp;
        <asp:Button ID="btnGet" runat="server" Text="获得字段列表" OnClick="btnGet_Click" />
    </div>
    <div class="list">
        <sy:MyGridView ID="MyGridView1" runat="server" OnCellDataBound="MyGridView1_CellDataBound" />
    </div>
    <div class="operation">
        配置类型：
        <asp:DropDownList ID="type" runat="server" EnableViewState="False">
            <asp:ListItem Value="list" Selected="True">列表</asp:ListItem>
            <asp:ListItem Value="query">查询</asp:ListItem>
            <asp:ListItem Value="edit">编辑</asp:ListItem>
            <asp:ListItem Value="detail">详述</asp:ListItem>
        </asp:DropDownList>
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'类型：&lt;br&gt;列表时形成管理列表配置，且配置名以list开头；&lt;br&gt;查询时形成查询列表配置，且配置名以query开头；&lt;br&gt;编辑时形成编辑配置，且配置名以edit开头；&lt;br&gt;详述时形成详述配置，且配置名以detail开头');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp; 标题：<asp:TextBoxVal ID="title" runat="server" Width="300px" ClientValidate="r_" ToolTip="请输入配置的标题"></asp:TextBoxVal>&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="生成配置" OnClick="btnSave_Click" CssClass="input_button" />&nbsp;&nbsp;
    </div>
    </form>
</body>
</html>
