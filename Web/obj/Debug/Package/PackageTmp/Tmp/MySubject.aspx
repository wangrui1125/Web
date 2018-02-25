<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySubject.aspx.cs" Inherits="MyQuery.Web.Tmp.MySubject" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询方案</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/mygridview.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="searchblock">
        <tr>
            <th rowspan="2" width="80">
                当前查询条件<br />形成的方案<br />(可以添加)
            </th>
            <td>
                查询方案名称：<asp:TextBox ID="txtName" runat="server" MaxLength="50" CssClass="input_text"
                    Width="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="请输入方案名称" ControlToValidate="txtName" SetFocusOnError="true" ForeColor="Red">*</asp:RequiredFieldValidator>
            </td>
            <td>
                是否私有：<asp:CheckBox ID="ckbPrivate" runat="server" Checked="false" />
            </td>
            <td style="text-align:center;width:80px"><asp:Button ID="btnAdd" runat="server" Text="添 加" CssClass="input_button" onclick="btnAdd_Click" /></td>
        </tr>
        <tr>
            <td colspan="3">
                <sy:MyInputs ID="MyInputs1" runat="server" />
            </td>
        </tr>
    </table>
    <table border="0" width="100%" class="buttonblock">
        <tr>
            <td>
                <asp:Button ID="btnUse" runat="server" Text="使 用" CssClass="input_button" onclick="btnUse_Click" 
                    CausesValidation="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnDel" runat="server" Text="删 除" CssClass="input_button" onclick="btnDel_Click" 
                    CausesValidation="False" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnClose" type="button" value="关 闭" class="input_button" onclick="window.opener=null;window.open('','_self');window.close();" />
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <div class="list">
        <sy:MyGridView ID="MyGridView1" runat="server" OnPageIndexChanged="MyGridView1_PageIndexChanged"
            OnSorting="MyGridView1_Sorting" />
    </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
