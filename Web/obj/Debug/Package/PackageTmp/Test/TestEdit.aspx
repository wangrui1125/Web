<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestEdit.aspx.cs" Inherits="MyQuery.Web.Test.TestEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑举例测试信息</title>

    <script src="../Js/ClientValidate.js" type="text/javascript"></script>

    <script src="../Js/calendar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="editblock">
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="100px" />
                <col width="200px" />
                <col width="100px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    发生日期<span style="color: Red;">*</span>
                </th>
                <td>
                    <asp:TextBoxVal ID="RDate" runat="server" ClientValidate="r_date" ToolTip="请输入日期，格式:2008-08-08"></asp:TextBoxVal>
                </td>
                <th>
                    归属模块<span style="color: Red;">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="FunID" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    问题标题<span style="color: Red;">*</span>
                </th>
                <td colspan="3">
                    <asp:TextBoxVal ID="txtTitle" runat="server" MaxLength="250" Width="500px" CssClass="input_text"
                        ClientValidate="r_" ToolTip="请输入问题的标题"></asp:TextBoxVal>
                </td>
            </tr>
            <tr>
                <th>
                    问题描述&nbsp;
                </th>
                <td colspan="3">
                    <asp:TextBoxVal ID="Notes" runat="server" TextMode="MultiLine" Rows="5" Width="500px"
                        CssClass="input_text"></asp:TextBoxVal>
                </td>
            </tr>
            <tr>
                <th>
                    问题截屏&nbsp;
                </th>
                <td colspan="3">
                    <input id="FileName" type="file" style="width: 500px" runat="server" />
                    <asp:Image ID="FileImg" runat="server" Visible="false" Width="500px" ImageAlign="AbsMiddle" />
                </td>
            </tr>
            <!--tr>
                <th>
                    问题级别<span style="color: Red;">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="IClass" runat="server">
                        <asp:ListItem Value="1">严重</asp:ListItem>
                        <asp:ListItem Value="2">次要</asp:ListItem>
                        <asp:ListItem Value="3">一般</asp:ListItem>
                        <asp:ListItem Value="4">新需求</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    当前状态<span style="color: Red;">*</span>
                </th>
                <td>
                    <asp:DropDownList ID="IFlag" runat="server">
                        <asp:ListItem Value="0">发现</asp:ListItem>
                        <asp:ListItem Value="1">确认</asp:ListItem>
                        <asp:ListItem Value="2">解决</asp:ListItem>
                        <asp:ListItem Value="3">关闭</asp:ListItem>
                    </asp:DropDownList>
                </td-->
            </tr>
        </table>
    </div>
    <sy:MyInputs ID="MyInputs1" runat="server" CssClass="editblock" />
    <div class="operation">
        <asp:Button ID="btnAdmin" runat="server" Visible="false" Text="设计" CausesValidation="false" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSave" runat="server" Text="提 交" CssClass="input_button" OnClick="btnSave_Click" />
        &nbsp;&nbsp;<input type="button" value="取 消" class="input_button" onclick="window.close()" />
    </div>
    </form>
</body>
</html>
