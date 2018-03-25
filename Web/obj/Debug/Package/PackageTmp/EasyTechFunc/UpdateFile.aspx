<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateFile.aspx.cs" Inherits="MyQuery.Web.EasyTechFunc.UpdateFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script language="javascript" type="text/javascript">
        window.onload = function () {
            var cbl = document.getElementById('<%= CheckBoxList1.ClientID%>')
            var inputs = cbl.getElementsByTagName("input");

            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    inputs[i].onclick = function () {                                          
                        var cbs = inputs;
                        for (var i = 0; i < cbs.length; i++) {
                            if (cbs[i].type == "checkbox" && cbs[i] != this && this.checked) {
                                cbs[i].checked = false;
                            }
                        }
                    }
                }
            }
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
            onselectedindexchanged="CheckBoxList1_SelectedIndexChanged">
        </asp:CheckBoxList>
    
    </div>
    <p>
        <asp:FileUpload ID="FileUpload1" runat="server" style="margin-top: 0px" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="上传" />
    </p>
    </form>
</body>
</html>
