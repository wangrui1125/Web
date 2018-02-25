<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MyQuery.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>科小觅管理平台登录</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        body
        {
            font: normal 12px/20px Arial, Verdana, Lucida, Helvetica, simsun, sans-serif;
            color: #ffffff;
            background: #dddddd;
        }
        td
        {
            padding-top: 2px;
            padding-bottom: 2px;
        }
        .inp1
        {
            height: 18px;
            line-height: 18px;
            border: 1px solid #cccccc;
            padding-left: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divCenter" style="text-align: center; padding-top: 200px;">
        <table border="0" cellpadding="0" cellspacing="0" align="center" style="width: 391px;
            height: 229px; background-image: url(Img/register.jpg)">
            <tr>
                <td style="width: 50px" rowspan="5">
                </td>
                <td style="height: 80px; width: 70px; font-size: 16px; font-weight: bold; color: #ff0000;">
                    
                </td>
                <td colspan="2" style="text-align: left; font-size: 20px;">
                   科小觅管理平台
                </td>
            </tr>
            <tr>
                <td align="right">
                    <strong>帐&nbsp;&nbsp;&nbsp;&nbsp;户</strong>：
                </td>
                <td align="left" style="width:110px">
                    <asp:TextBox ID="txtUser" runat="server" CssClass="inp1" Width="80px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ErrorMessage="请输入账号" ControlToValidate="txtUser"
                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td style="width: 70px" rowspan="3" align="left">
                    <asp:ImageButton ID="imgOk" runat="server" ImageUrl="Img/enter.jpg" OnClick="imgOk_Click" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <strong>密&nbsp;&nbsp;&nbsp;&nbsp;码</strong>：
                </td>
                <td align="left">
                    <asp:TextBox ID="txtPwd" runat="server" CssClass="inp1" TextMode="Password" Width="80px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ErrorMessage="请输入密码" ControlToValidate="txtPwd"
                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" rowspan="2">
                    <strong>验证码：</strong>
                </td>
                <td align="left">
                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_self"><img src="Image.aspx" width="80" height="25" border="0" alt="看不清点击更换" /></asp:HyperLink><br />
                    <asp:TextBox ID="txtYZM" runat="server" CssClass="inp1" Width="80px"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator3" runat="server" ErrorMessage="请输入验证码" ControlToValidate="txtYZM"
                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2" valign="top">
                    <asp:CheckBox ID="ckbSave" runat="server" Checked="false" Text="自动登录(公共PC勿选此项)" BorderWidth="0" />
                </td>
            </tr>
        </table>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="False"
        ShowMessageBox="True" />
    </form>
</body>
</html>
