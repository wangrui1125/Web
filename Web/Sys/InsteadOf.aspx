<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsteadOf.aspx.cs" Inherits="MyQuery.Web.Sys.InsteadOf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>代替操作</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/helptip.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="edttitle" style="padding-top: 15px;">
        代替操作</div>
    <div style="text-align: center;">
        <table class="editblock" align="center" cellspacing="0" cellpadding="2" border="0"
            style="width: 350px">
            <colgroup>
                <col style="width: 120px;" />
                <col style="width: 200px;" />
            </colgroup>
            <tr>
                <th>
                    帐号：
                </th>
                <td>
                    <asp:TextBox ID="txtID" Width="160px" runat="server" MaxLength="20" CssClass="input_text"></asp:TextBox>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'帐号：必须正确的输入在职人员的账号');"
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
