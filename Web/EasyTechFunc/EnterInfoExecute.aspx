<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterInfoExecute.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.EnterInfoExecute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head  id="Head1" runat="server">
    <title>我的企业（执行的）</title>
     
</head>
<body  style="background:white">
<form id="form2" method=post runat="server" encType="multipart/form-data">
 
 
 
 

<asp:Panel ID="Query1" runat="server">
 <iframe id="iframeContent" width="100%" height="500px" name="iframeContent" scrolling="auto" frameborder="0"
        src="<%=Url %>">
         </iframe>

</asp:Panel>


  
   <div>
       

        </div>
    </form>
    
</body>
</html>
