<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="EditMark.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.EditMark" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标签信息编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        #form1
        {
            height: 557px;
        }
        .input_button
        {
            width: 59px;
            height: 22px;
        }
        .style1
        {
            font-size: xx-large;
        }
        .style2
        {
            font-size: x-large;
        }
    </style>
</head>
<!-- 以上是head，下面是页面的实体部分 -->
<body>
    <form id="form1" method="post" runat="server">
    <!-- form是表单，用于向服务器传送数据 -->
    <!-- div是行 -->
       <div class="editblock">
        <table cellspacing="0" cellpadding="0" border="0">
            <div>  
            <span class="style1">需求等级标签:</span><br />
            <span class="style2">目前等级：</span><asp:Literal ID="Literal2" runat="server"></asp:Literal><br />
            <asp:DropDownList ID="TechLevel" runat="server" 
              onselectedindexchanged="TechLevel_SelectedIndexChanged" Height="26px" 
              Width="635px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            </div>
           
        </table>
      </div>
       <div class="operation" style="text-align: left">
            <asp:Button ID="Button1" runat="server" CssClass="input_button" onclick="Button1_Click" Text="提交" 
                />
            &nbsp;&nbsp;<input type="button" value="返 回" class="input_button" onclick="window.returnValue=true;window.close()" />&nbsp;&nbsp;&nbsp;
            </div>
       <div>
       <tr></tr>
       <tr></tr>
       <tr></tr>
       <tr></tr>
       </div>





   

    <div class="editblock">
        <table cellspacing="0" cellpadding="0" border="0">
            <!-- table是表格，后面是可选参数 -->
            <colgroup>
                <col style="width: 80px;" />
                <col style="width: 180px;" />
                <col style="width: 80px;" />
                <col />
            </colgroup>
        <div>
        <span class="style1">需求分类标签：</Span><span class="style2"><br />
        已有标签</span><span class="style1">: 
        </span>&nbsp;<asp:Literal ID="Literal1" runat="server"></asp:Literal>       
        </div>


            <tr>
                <td>
                    关键词
                </td>
                <td>
                    <asp:TextBox ID="txtKeywords" runat="server" Width="99%" TextMode="MultiLine" Rows="8" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    内容描述
                </td>
                <td>
                    <asp:TextBox ID="txtNotes" runat="server" Width="99%" TextMode="MultiLine" Rows="8" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    标签体系
                </td>
                <td>
                    <asp:DropDownList ID="MarkColor" runat="server" Width="150px">
                    </asp:DropDownList>
                </td>
                <td>
                    标签类别
                </td>
                <td>
                    <asp:DropDownList ID="CategoryID" AutoPostBack="true" runat="server" Width="300px"
                        OnSelectedIndexChanged="CategoryID_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:CheckBoxList ID="Marks" runat="server" RepeatLayout="Table" 
                        RepeatColumns="5" AutoPostBack="True">
                    </asp:CheckBoxList>
                </td>
            </tr>
        </table>
    </div>
    <div class="operation" style="text-align: left">
        <asp:Button ID="btnSave" runat="server" Text="提 交" CssClass="input_button" OnClick="btnSave_Click" />
        <asp:Button ID="Button2" runat="server" Text="清 除" CssClass="input_button" OnClick="btnSave_Clickc" />
        &nbsp;&nbsp;<input type="button" value="返 回" class="input_button" onclick="window.returnValue=true;window.close()" />
    </div>
   

     
    </form>
</body>
</html>
