<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelChartType.aspx.cs" Inherits="MyQuery.Web.Tmp.SelChartType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择图形类型</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript">
        function changeImg(src) {
            var obj = document.getElementById("imgChart");
            if (obj) {
                obj.src = "ChartType/2D" + src + ".png";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="text-align: center; margin: 5px auto 0px auto;">
        <asp:DropDownList ID="dlCharttype" runat="server" Width="200px" onchange="changeImg(this.value)">
            <asp:ListItem Value="area">area区域范围</asp:ListItem>
            <asp:ListItem Value="bar">bar条棒</asp:ListItem>
            <asp:ListItem Value="boxplot">boxplot箱点</asp:ListItem>
            <asp:ListItem Value="bubble">bubble泡泡</asp:ListItem>
            <asp:ListItem Value="candlestick">candlestick烛台</asp:ListItem>
            <asp:ListItem Value="column">column列</asp:ListItem>
            <asp:ListItem Value="doughnut">doughnut圆环</asp:ListItem>
            <asp:ListItem Value="fastline">fastline快线</asp:ListItem>
            <asp:ListItem Value="fastpoint">fastpoint快点</asp:ListItem>
            <asp:ListItem Value="kagi">kagi升降</asp:ListItem>
            <asp:ListItem Value="funnel">funnel漏斗</asp:ListItem>
            <asp:ListItem Value="line">line线</asp:ListItem>
            <asp:ListItem Value="pie">pie饼</asp:ListItem>
            <asp:ListItem Value="point">point点</asp:ListItem>
            <asp:ListItem Value="pointandfigure">pointandfigure点绘</asp:ListItem>
            <asp:ListItem Value="polar">polar极线</asp:ListItem>
            <asp:ListItem Value="pyramid">pyramid金字塔</asp:ListItem>
            <asp:ListItem Value="radar">radar雷达</asp:ListItem>
            <asp:ListItem Value="range">range范围</asp:ListItem>
            <asp:ListItem Value="rangebar">rangebar范围条棒</asp:ListItem>
            <asp:ListItem Value="rangecolumn">rangecolumn范围柱</asp:ListItem>
            <asp:ListItem Value="renko">renko浮动</asp:ListItem>
            <asp:ListItem Value="spline">spline齿条</asp:ListItem>
            <asp:ListItem Value="splinearea">splinearea齿条区域</asp:ListItem>
            <asp:ListItem Value="splinerange">splinerange齿条范围</asp:ListItem>
            <asp:ListItem Value="stackedarea">stackedarea艳丽区域</asp:ListItem>
            <asp:ListItem Value="stackedarea100">stackedarea100艳丽区域100</asp:ListItem>
            <asp:ListItem Value="stackedbar">stackedbar艳丽条棒</asp:ListItem>
            <asp:ListItem Value="stackedbar100">stackedbar100艳丽条棒100</asp:ListItem>
            <asp:ListItem Value="stackedcolumn">stackedcolumn艳丽柱</asp:ListItem>
            <asp:ListItem Value="stackedcolumn100">stackedcolumn100艳丽柱100</asp:ListItem>
            <asp:ListItem Value="stepline">stepline直线条</asp:ListItem>
            <asp:ListItem Value="stock">stock股票</asp:ListItem>
            <asp:ListItem Value="threelinebreak">threelinebreak分割线</asp:ListItem>
        </asp:DropDownList>
        <br />
        <img id="imgChart" src="ChartType/2D<%=dlCharttype.SelectedValue %>.png" width="200px"
            height="150px" alt="选中类型图片预览" />
        <br />
    </div>
    <div class="notes">
        注意：要根据数据情况选择合适的图形类型，否则可能出错或图形不正确
    </div>
    <div class="operation">
        <input type="button" value="确 定" class="input_button" onclick="window.returnValue=document.getElementById('dlCharttype').value;window.close();" />
        &nbsp;&nbsp;<input type="button" value="取 消" class="input_button" onclick="window.close()" />
    </div>
    </form>
</body>
</html>
