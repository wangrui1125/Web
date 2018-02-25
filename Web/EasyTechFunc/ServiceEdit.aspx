<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceEdit.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.ServiceEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 242px;
            text-align: center;
            height: 15px;
        }
        .style4
        {
            width: 242px;
            text-align: center;
            height: 158px;
        }
        .style6
        {
            height: 158px;
            width: 237px;
            text-align: center;
        }
        .style7
        {
            width: 237px;
            height: 15px;
        }
        .style8
        {
            height: 114px;
            width: 237px;
        }
        .style9
        {
            width: 242px;
            text-align: center;
            height: 49px;
        }
        .style10
        {
            height: 49px;
            width: 237px;
            text-align: center;
        }
        .style11
        {
            width: 242px;
            text-align: center;
            height: 114px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <table style="width: 71%; height: 443px;">
        <tr>
            <td class="style9">
                服务单号：<asp:Label ID="Label3" runat="server"></asp:Label>
            </td>
            <td class="style10">
                服务单类型：<asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style4">
    支付情况<br /> 
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" 
        Height="16px" Width="85px">
    </asp:DropDownList>
    <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="支付金额"></asp:Label>
                ：<asp:TextBox ID="TextBox1" runat="server" Width="65px"></asp:TextBox>
    <br />
                <br />
    <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />
            </td>
            <td class="style6">
                服务单进展情况<br /> 
    <br />
    <asp:DropDownList ID="DropDownList2" runat="server" 
        Height="16px" Width="85px">
    </asp:DropDownList>
    <br />
                <br />
    <br />
                <br />
    <asp:Button ID="Button2" runat="server" Text="确定" onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                </td>
            <td class="style7">
                </td>
        </tr>
        <tr>
            <td class="style11">
            </td>
            <td class="style8">
            </td>
        </tr>
    </table>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
