<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recommand.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.recommand" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background:white">
    <form id="form1" runat="server"  > <div align="center">
    
    
    <asp:TextBox ID="TextBox1" runat="server" Width ="60%" ></asp:TextBox>
   
        <asp:Button ID="Button1"
            runat="server" Text="查询" onclick="Button1_Click" /></div><div>
    <iframe id="iframe1" name="iframe1" scrolling="auto" frameborder="0"
            src="<%=Url %>"  width="27%" height="500"
        style="margin-left: 5px; margin-right: 5px;"></iframe>
     <iframe id="iframe2" name="iframe2" scrolling="auto" frameborder="0"
            src="<%=Url1 %>"  width="70%" height="500"
        style="margin-left: 5px; margin-right: 5px;"></iframe>
    </div>
    </form>
</body>
</html>
