<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceEdit.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.ServiceEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 242px;
            text-align: center;
            height: 15px;
        }
        .style10
        {
            height: 49px;
            width: 237px;
            text-align: left;
        }
        #TextArea1
        {
            width: 229px;
            height: 161px;
            margin-left: 0px;
        }
        .style14
        {
            width: 55px;
            text-align: left;
            height: 15px;
        }
        .style15
        {
            height: 158px;
            width: 55px;
            text-align: left;
        }
        </style>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function TextArea1_onclick() {

        }

        function Button5_onclick() {
        }

// ]]>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 443px; height: 42px">
    
        编辑服务单：</div>
    <table style="width: 71%; height: 482px;" class="style1" frame="box" 
        rules="all">
        <tr>
            <td class="style14">
                服务单号：<asp:Label ID="Label3" runat="server"></asp:Label>
            </td>
            <td class="style10">
                服务单类型：<asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style15">
                分配服务单给某个小觅：<br /> 
    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" 
        Height="30px" Width="120px" style="font-size: large">
    </asp:DropDownList>
    <br />
                <br />
                支付金额：<asp:TextBox ID="TextBox1" runat="server" Width="65px" Height="30px"></asp:TextBox>
    <br />
                <br />
    <asp:Button ID="Button1" runat="server" Text="确定" onclick="Button1_Click" />
            </td>
        </tr>
        </table>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
