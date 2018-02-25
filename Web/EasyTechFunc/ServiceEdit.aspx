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
        .style6
        {
            height: 158px;
            width: 237px;
            text-align: left;
        }
        .style7
        {
            width: 237px;
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
        .style12
        {
            height: 222px;
            width: 237px;
            text-align: left;
        }
        .style13
        {
            text-align: left;
            width: 222px;
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
        .style16
        {
            height: 222px;
            width: 55px;
            text-align: left;
        }
    </style>
    <script language="javascript" type="text/javascript">
// <![CDATA[

        function TextArea1_onclick() {

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
    支付情况<br /> 
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
            <td class="style6">
                服务单进展情况<br /> 
    <br />
    <asp:DropDownList ID="DropDownList2" runat="server" 
        Height="30px" Width="120px" style="font-size: large">
    </asp:DropDownList>
    <br />
                <br />
    <br />
                <br />
    <asp:Button ID="Button2" runat="server" Text="确定" onclick="Button1_Click" />
            </td>
        </tr>
        <tr>
            <td class="style14">
                </td>
            <td class="style7">
                </td>
        </tr>
        <tr>
            <td class="style16">
                需要上传的文件：<br />
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <br />
                备注：<br />
                <asp:TextBox ID="TextBox3" runat="server" Height="125px" Width="210px"></asp:TextBox>
                <asp:Button ID="Button4" runat="server" Text="提交备注" />
            </td>
            <td class="style12">
                意见反馈记录：<br />
                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
                <div class="style13">
                    新增记录：<br />
                    <asp:Label ID="Label4" 
                    runat="server" Text="Label"></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server" Height="25px" Width="155px"></asp:TextBox>
                    <asp:Button ID="Button3" runat="server" Text="确定" />
                </div>
            </td>
        </tr>
    </table>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
