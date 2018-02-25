<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRoleEdit.aspx.cs" Inherits="MyQuery.Web.Sys.UserRoleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>角色分配管理</title>
</head>
<body>
	<form id="form1" runat="server">
	<div class="editblock">
		<table cellpadding="0" cellspacing="0">
			<colgroup>
				<col style="width: 80px;" />
				<col />
			</colgroup>
			<tr>
				<th>
					所选用户
				</th>
				<td>
					<asp:Literal ID="literalUsers" runat="server"></asp:Literal>
				</td>
			</tr>
			<tr>
				<th>
					角色
				</th>
				<td nowrap>
					<asp:CheckBoxList ID="chkListRoles" RepeatLayout="Flow" RepeatDirection="Horizontal"
						RepeatColumns="3" runat="server">
					</asp:CheckBoxList>
				</td>
			</tr>
		</table>
	</div>
	<div class="operation">
		<asp:Button ID="btnSave" runat="server" Text="提 交" CssClass="input_button" OnClick="btnSave_Click" />
		<input type="button" value="取 消"  onclick="window.close()" class="input_button" />
	</div>
	</form>
</body>
</html>
