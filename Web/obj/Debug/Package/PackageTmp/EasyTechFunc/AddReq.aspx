<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddReq.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.AddReq" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>科技需求导入</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/helptip.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
</head>
<!-- 以上是head，下面是页面的实体部分 -->
<body>
    <form id="form1" method=post runat="server" encType="multipart/form-data">
<!-- form是表单，用于向服务器传送数据 -->
    <div class="edttitle" style="padding-top: 15px;"> 
        科技需求导入</div>
<!-- div是行 -->
    <div style="text-align: center;">
        <table id = "TableAddReq" class="editblock" align="center" cellspacing="0" cellpadding="2" border="0"
            style="width: 600px" runat="Server">
            <!-- editblock
        <!-- table是表格，后面是可选参数 -->
            <colgroup>
                <col style="width: 300px;" />
                <col style="width: 300px;" />
            </colgroup>
            <tr>
                <td>
                    原始标签
                </td>
                <td>
                    对应标签
                </td>
            </tr>          
        </table>
        <br />
          <div>
         <input ID=fileId type=file runat="Server">
         </div>
        <div align="center">
            <asp:Button ID="btnSubmit" runat="server" Text="读取文件" CssClass="input_button" OnClick="btnSubmit_Click" />
        </div>
        <div align="center">
            <FONT>表格的插入方式（有表格时生效）</FONT>
            <select name="tableinsertstyle" id="tableStyle" runat="server">
                <option value="1">段落方式</option>
                <option value="2">表格方式：第一行是标签</option>
                <option value="3">表格方式：第一列是标签</option>
            </select>
            <table><tr>
            <td> 
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Img/bg0.jpg"  /></td>
            <td><asp:Image ID="Image2" runat="server" ImageUrl="~/Img/bg1.jpg" /></td>
            <td><asp:Image ID="Image3" runat="server" ImageUrl="~/Img/bg2.jpg" /></td>
            </tr></table>
        </div>
        <!-- myinfo说明：myinfo和myinfo2：重复的技术需求和企业
            myinfo3：成功插入的数目统计
            myinfo4：其他的奇怪错误
            myinfo5：标签未匹配相关错误
         -->
        <div>
            <strong id = "myinfo" runat = "server" visible = "false">434</strong>
        </div>
        <div>
            <strong id = "myinfo2" runat = "server" visible = "false">434</strong>
        </div>
        <div>
            <textarea id="myinfo5" cols="100" rows="20" runat="Server" visible="false"></textarea>  
        </div>
        <div>
            <strong id = "myinfo3" runat = "server" visible = "false">434</strong>
        </div>
        <div>
            <strong id = "myinfo4" runat = "server" visible = "false">434</strong>
        </div>
    </div>
    </form>
</body>
</html>
