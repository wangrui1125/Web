<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateHetong.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.UpdateHetong" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript">
     function deletelog(){
     var result = window.confirm("确定删除吗");
     if (result) 
     {
         document.getElementById("<%=Button6.ClientID %>").click();
       }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server"> 
    <asp:Label ID="Label1" runat="server"></asp:Label>
      </br>    
    <input type="button" name="delete" value="删除" onclick="deletelog()" dir="ltr" 
            style="text-align: left"/>            
    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" Text="Button" 
    CssClass="cssstyle" Visible="False"/>

    <div  id="divdgr" style="Height:232px; Width:auto;OVERFLOW: auto" 
        align="center" >
    
    

             <asp:DataGrid ID="DataGrid1" runat="server" 
                 onselectedindexchanged="DataGrid1_SelectedIndexChanged" 
            Width="800px"  CellPadding="4" ForeColor="#333333" 
                 GridLines="None" onitemdatabound="DataGrid1_ItemDataBound" 
                  HorizontalAlign="Center">
                 <Columns>                                 
                    <asp:ButtonColumn CommandName="Select" Text="选择"></asp:ButtonColumn> 
                </Columns>
                 <AlternatingItemStyle BackColor="White" />
                  <EditItemStyle BackColor="#2461BF" />
                  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                  <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                  <ItemStyle BackColor="#EFF3FB" />
                  <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                  <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
             </asp:DataGrid>
    </div>
    
    <div>
    <asp:FileUpload ID="FileUpload1" runat="server" Height="18px" Width="229px" />
    <asp:Button ID="Button1" runat="server" Text="上传新文件" onclick="Button1_Click" />    
    </div>
    </form>
</body>
</html>
