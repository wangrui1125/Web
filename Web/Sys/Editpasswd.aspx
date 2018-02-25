<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Editpasswd.aspx.cs" Inherits="MyQuery.Web.Sys.Editpasswd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/helptip.js" type="text/javascript"></script>
    <script src="../Js/ClientValidate.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="edttitle" style="padding-top: 15px;">
        修改<asp:Label ID="lblDes" runat="server"></asp:Label>密码</div>
    <div style="text-align: center;">
        <table class="editblock" align="center" cellspacing="0" cellpadding="2" border="0"
            style="width: 350px">
            <colgroup>
                <col style="width: 120px;" />
                <col style="width: 200px;" />
            </colgroup>
            <tr>
                <th>
                    帐&nbsp;&nbsp;&nbsp;&nbsp;号：
                </th>
                <td>
                    <asp:TextBox ID="txtID" Width="160px" runat="server" MaxLength="20" Enabled="false"
                        CssClass="input_text"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    原&nbsp;密&nbsp;码：
                </th>
                <td>
                    <asp:TextBoxVal ID="txtPwdOld" Width="160px" MaxLength="20" TextMode="Password" runat="server"
                        CssClass="input_text" ClientValidate="^[0-9a-zA-Z_\-\/\.\~\@\#\$\%\^\&\*]*$"
                        ToolTip="请输入原密码"></asp:TextBoxVal>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'原密码：需要修改的原来的密码');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
            <tr>
                <th>
                    新&nbsp;密&nbsp;码：
                </th>
                <td>
                    <asp:TextBoxVal ID="txtPasswd" Width="160px" TextMode="Password" runat="server" MaxLength="20"
                        CssClass="input_text" ClientValidate="^[0-9a-zA-Z_\-\/\.\~\@\#\$\%\^\&\*]+$"
                        ToolTip="请输入字母或数字"></asp:TextBoxVal>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'新密码：建议6位以上字母或数字');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
            <tr>
                <th height="50">
                    确认密码：
                </th>
                <td>
                    <asp:TextBoxVal ID="txtPwdCheck" Width="160px" TextMode="Password" runat="server"
                        MaxLength="20" CssClass="input_text" ClientValidate="==txtPasswd" ToolTip="请再次输入上面输入的密码"></asp:TextBoxVal>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'确认密码：再次输入上面输入的密码');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
        </table>
        <br />
        <div align="center">
            <asp:Button ID="btnSubmit" runat="server" Text="提  交" CssClass="input_button" OnClick="btnSubmit_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;<input type="reset" value="取 消" class="input_button" />
        </div>
    </div>
    </form>
</body>
</html>
