<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeChatProgressReminder.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.WeChatProgressReminder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1 {
            font-size: x-large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <span class="style1">需求进度的微信提醒：</span><br />
        
        <asp:Table ID="Table1" runat="server" Height="236px" Width="575px" 
            BackColor="White">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                <asp:Label ID="Label1" runat="server" Text="需求："></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>                
                 <asp:TableCell ID="TableCell1" runat="server">
                <asp:Label ID="Label3" runat="server" Text="需求目前状态:"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>               
            </asp:TableRow>
            <asp:TableRow runat="server" Text="请指定微信发送到达人：">
                <asp:TableCell ID="TableCell3" runat="server">
                <asp:Label ID="Label5" runat="server" Text="请指定收信人："></asp:Label>
                 <asp:DropDownList ID="DropDownList1" runat="server">
                 </asp:DropDownList>
                </asp:TableCell>            
            </asp:TableRow>
             <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell5" runat="server">
                    <asp:Label ID="Label6" runat="server" Text="请输入发送内容："></asp:Label>
                </asp:TableCell>               
            </asp:TableRow>
             <asp:TableRow ID="TableRow2" runat="server">
                <asp:TableCell ID="TableCell6" runat="server">
                   <asp:TextBox ID="TextBox1" runat="server" Height="150px" Width="300px" AutoCompleteType="None" TextMode="MultiLine" ClientIDMode="Inherit" MaxLength="150">
                   </asp:TextBox> 
                </asp:TableCell>               
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server">
                    <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click"/>
                    <asp:Button ID="Button2" runat="server" Text="返回" OnClick="Button2_Click" />
                </asp:TableCell>
            </asp:TableRow>
          
        </asp:Table>
        
    </div>
    </form>
</body>
</html>
