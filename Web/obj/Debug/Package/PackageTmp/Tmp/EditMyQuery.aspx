<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="EditMyQuery.aspx.cs"
    Inherits="MyQuery.Web.Tmp.EditMyQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>配置编辑</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../Js/ClientValidate.js" type="text/javascript"></script>
    <script src="../Js/helptip.js" type="text/javascript"></script>
    <script src="../Js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showDiv(id) {
            var divs = $(".editblock");
            for (var i = 0; i < divs.length; i++) {
                if (parseInt(divs[i].id, 10) == id) {
                    divs[i].style.display = "";
                }
                else {
                    divs[i].style.display = "none";
                }
            }
        }
        function showBody(img, tBodyId) {
            var tBody = document.getElementById(tBodyId);
            if (tBody.style.display == "none") {
                img.src = "../Img/r.gif";
                tBody.style.display = "";
            }
            else {
                img.src = "../Img/r_d.gif";
                tBody.style.display = "none";
            }
        }
    </script>
</head>
<body onload="showWait(false)" oncontextmenu="return false">
    <%=MyQuery.Utils.WebHelper.GetWaitDiv("150","150")%>
    <form id="form1" runat="server">
    <sy:MyTabPage ID="MyTabPage1" runat="server" EnableViewState="false" CssClass="tab">
        <asp:ListItem Value="javascript:showDiv(0);">XML配置</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(1);">公共配置</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(2);">基本配置</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(3);">数据来源</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(4);">展示数据</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(5);">查询条件</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(6);">处理按钮</asp:ListItem>
        <asp:ListItem Value="javascript:showDiv(7);">分析图形</asp:ListItem>
    </sy:MyTabPage>
    <div id="0" class="editblock" style="display: ">
        &nbsp;&nbsp;<strong>页卡配置项</strong>：（除公共配置外其它为选中页卡的配置）&nbsp;&nbsp;
        <asp:Button ID="myquery_Add" runat="server" EnableViewState="False" Text="新增页卡" ToolTip="查询模板仅支持一个页卡"
            OnClick="myquery_Add_Click" /><br />
        <asp:RadioButtonList ID="rblMyQuery" runat="server" RepeatLayout="Flow" RepeatColumns="5"
            AutoPostBack="true" OnSelectedIndexChanged="rblMyQuery_SelectedIndexChanged">
        </asp:RadioButtonList>
        <br />
        &nbsp;&nbsp;<strong>全部配置内容</strong>：（适合熟悉XML的，可直接编辑）&nbsp;&nbsp;<br />
        <asp:TextBox ID="txt0" runat="server" EnableViewState="False" TextMode="MultiLine"
            Rows="20" Width="98%" ToolTip="属性设置注意，表达逻辑的属性的值必须是true或false，其它值则按照程序默认的逻辑值处理，数值型的属性必须指定正确的数值，否则按照程序默认值处理，可选择的字符串是必须是说明中列出的英文字符"></asp:TextBox><br />
        &nbsp;&nbsp;<a href="down.aspx?f=\query\<%=name%>.xml" target="_self"><strong>下载本XML配置</strong></a>
        &nbsp;&nbsp;<a href="down.aspx?f=\tpl\<%=name%>.htm" target="_self"><strong>下载本Html模板</strong></a>
        &nbsp;&nbsp;<a href="EditHtml.aspx?n=<%=name%>" target="_self"><strong>转Html模板编辑页面</strong></a>
        &nbsp;&nbsp;<a href="down.aspx?f=\Demo.xml" target="_self"><strong>下载XML配置详细说明及示例文档</strong></a>
    </div>
    <div id="1" class="editblock" style="display: none">
        &nbsp;&nbsp;<strong>公共按钮</strong>：&nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'配置应用多个TAB页卡时所有页卡的公共按钮');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="dlButtons_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlButtons_Add_Click" /><br />
        <asp:DataList ID="dlButtons" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <img src="../Img/r_d.gif" style="cursor: pointer;" align="absbottom" onclick="showBody(this, 'tbody<%#Eval("name")%>')" /><%#Eval("title")%>(<%#Eval("name")%>)配置
                        &nbsp;&nbsp;<asp:Button ID="dlButtons_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlButtons_Del_Click" />
                    </td>
                </tr>
                <tbody id='tbody<%#Eval("name")%>' style="display: none">
                    <tr>
                        <td>
                            按钮名称:<asp:TextBox ID="dlButtons_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 按钮的名称 必须唯一否则前面的会被覆盖" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            按钮标题:<asp:TextBox ID="dlButtons_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 按钮的显示标题"></asp:TextBox>
                        </td>
                        <td>
                            按钮样式:<asp:TextBox ID="dlButtons_cssclass" runat="server" EnableViewState="False"
                                Text='<%#Eval("cssclass")%>' Width="100px" CssClass="input_text" ToolTip="按钮的样式class"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            处理类型:<asp:DropDownList ID="dlButtons_type" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("type")%>' Width="80px">
                                <asp:ListItem Value="dialog">打开模式窗体</asp:ListItem>
                                <asp:ListItem Value="redirect">页面重定向</asp:ListItem>
                                <asp:ListItem Value="open">打开新页面</asp:ListItem>
                                <asp:ListItem Value="deal">自定义处理</asp:ListItem>
                                <asp:ListItem Value="toexcel">导出到excel</asp:ListItem>
                                <asp:ListItem Value="runsql">执行语句</asp:ListItem>
                                <asp:ListItem Value="doscript">执行脚本</asp:ListItem>
                                <asp:ListItem Value="save">编辑的保存</asp:ListItem>
                                <asp:ListItem Value="reset">编辑的重置</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'处理类型说明：&lt;br&gt;打开模式窗体，当新窗体返回true时父窗体自动刷新；&lt;br&gt;页面重定向，直接进入配置的页面；&lt;br&gt;打开新页面，如果父窗体需要刷新open的新页面需要写parent.href=parent.href脚本；&lt;br&gt;自定义后台处理使用，值表达式为实现了IButtonDeal接口的完整类名；&lt;br&gt;导出到excel，直接将datatsource数据导出到excel（只能配一个） &lt;br&gt;&nbsp;&nbsp;注意 直接导出不需额外配置时值为空&lt;br&gt;&nbsp;&nbsp;针对先提供一个选择配置列的页面，根据选中的项再进行导出excel时配置一个open按钮 &lt;br&gt;&nbsp;&nbsp;值配置为Tmp/MyToExcel.aspx?s=1&n=导出excel配置文件（不含.xml后缀）&lt;br&gt;&nbsp;&nbsp;如果n的参数与当前配置同名则按照当前配置提供选择和导出 否则需配置f=当前配置文件（不含.xml后缀）并保证新配置能接收此配置生成的条件&lt;br&gt;&nbsp;&nbsp;对于直接使用的excel输出模板的定义redirect的按钮 值为Tmp/MyToExcel.aspx?n=导出excel配置文件（不含.xml后缀）&lt;br&gt;&nbsp;&nbsp;如果n的参数与当前配置同名则功能与toexcel相同 使用MyToExcel.aspx的导出优先使用Session中对应查询的条件 不存在时按照默认条件处理；&lt;br&gt;runsql 执行语句（只能配一个） 注意 值需要配置为sql节&lt;br&gt;&nbsp;&nbsp;sql title=“无满足条件的执行语句时提示” select判断语句，语句中{0}将被替换为选中行的id注意多选值用,隔开/sql&lt;br&gt;&nbsp;&nbsp;sql value=“” title=“执行成功后提示信息”当值等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开/sql(可以为多个)&lt;br&gt;&nbsp;&nbsp;sql title=“执行成功后提示信息”&gt;当值未设置或值不等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开&lt;/sql(可以为多个) &lt;br&gt;执行脚本值为需要执行的脚本&lt;br&gt;编辑保存（只能配一个）编辑模板的保存处理按钮&lt;br&gt;重置按钮（只能配一个）编辑模板的重置处理按钮');"
                                src="../Img/shm.gif" />
                        </td>
                        <td rowspan="4">
                            <b>按钮配置值表达式：</b><br />
                            <asp:TextBox ID="dlButtons_value" TextMode="MultiLine" Rows="5" runat="server" EnableViewState="False"
                                Text='<%#Eval("value")%>' Width="250px" CssClass="input_text" ToolTip='打开模式窗体、页面重定向、打开新页面时为导航到的页面；deal时为实现了IButtonDeal接口的完整类名；doscript时为javascript脚本；runsql时为值需要配置为<sql title="无满足条件的执行语句时提示">select判断语句，语句中{0}将被替换为选中行的id注意多选值用,隔开</sql><sql value="" title="执行成功后提示信息">当值等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开</sql>(可以为多个)<sql title="执行成功后提示信息">当值未设置或值不等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开</sql>(可以为多个)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            功能代码:<asp:TextBox ID="dlButtons_functioncode" runat="server" EnableViewState="False"
                                Text='<%#Eval("functioncode")%>' Width="100px" CssClass="input_text" ToolTip="按钮的功能代码 空为不限制 需结合判断是否授权给了用户的SQL配置"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            需要选中:<asp:DropDownList ID="dlButtons_isselectedrow" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("isselectedrow")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'当按钮需要先选中行时选是&lt;br&gt;选择是时按钮配置值必须包含替换参数{0}，&lt;br&gt;程序将{0}参数的值由客户端选中行获得的选中行的ID&lt;br&gt;（行的ID有keycolumnnames获得，多个用,分割，多选时多个ID用;分割）');"
                                src="../Img/shm.gif" />
                        </td>
                        <td>
                            窗口宽度:<asp:TextBox ID="dlButtons_windowwidth" runat="server" EnableViewState="False"
                                Text='<%#Eval("windowwidth")%>' Width="100px" CssClass="input_num" ToolTip="打开新窗口宽度 空为不设置"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            窗口高度:<asp:TextBox ID="dlButtons_windowheight" runat="server" EnableViewState="False"
                                Text='<%#Eval("windowheight")%>' Width="100px" CssClass="input_num" ToolTip="打开新窗口高度 空为不设置"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            确认提示:<asp:TextBox ID="dlButtons_confirminfo" runat="server" EnableViewState="False"
                                Text='<%#Eval("confirminfo")%>' Width="100px" CssClass="input_text" ToolTip="点击按钮给出的确认信息 空为不设置"></asp:TextBox>
                        </td>
                        <td>
                            参数集合:<asp:TextBox ID="dlButtons_parameters" runat="server" EnableViewState="False"
                                Text='<%#Eval("parameters")%>' Width="100px" CssClass="input_text" ToolTip="参数name集合 可选设置 需要的参数name,值从Request.QueryString中获取 多于一个用,分割"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            按钮宽度:<asp:TextBox ID="dlButtons_width" runat="server" EnableViewState="False" Text='<%#Eval("width")%>'
                                Width="100px" CssClass="input_num" ToolTip="按钮宽度 空为不设置" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            按钮图片:<asp:TextBox ID="dlButtons_imgsrc" runat="server" EnableViewState="False" Text='<%#Eval("imgsrc")%>'
                                Width="100px" CssClass="input_text" ToolTip="提供按钮图片URL 空为不设置" onkeypress="return validateCode2(this, getkeyCode(event));"
                                onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            参数name:<asp:TextBox ID="dlButtons_parameter" runat="server" EnableViewState="False"
                                Text='<%#Eval("parameter")%>' Width="100px" CssClass="input_text" ToolTip="参数name 可选设置 需要的参数name 值从Request.QueryString中获取"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            参数的值:<asp:TextBox ID="dlButtons_parametervalue" runat="server" EnableViewState="False"
                                Text='<%#Eval("parametervalue")%>' Width="100px" CssClass="input_text" ToolTip="提供对应参数name的值（可匹配多种情况时值用,分割），与当parameter配合使用，当获取的值与配置的值匹配时按钮才加入"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            页卡索引:<asp:TextBox ID="dlButtons_tabindex" runat="server" EnableViewState="False"
                                Text='<%#Eval("tabindex")%>' Width="100px" CssClass="input_text" ToolTip="dialog按钮返回的页卡索引，仅多页卡时使用"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            下一页卡:<asp:DropDownList ID="dlButtons_isnexttab" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("isnexttab")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'成功后是否前往下一个TAB 仅多个TAB页卡时有效');"
                                src="../Img/shm.gif" />
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>判断是否授权给了用户的SQL</strong>：（当配置了功能代码时需要）<br />
        <asp:TextBox ID="functionsql" runat="server" EnableViewState="False" TextMode="MultiLine"
            Rows="2" Width="98%" ToolTip="此处只配置from及其后部分(语句中可用#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID,{1}将有functioncode值替换)如果没有配置在从用户缓存的功能列表中判断"></asp:TextBox>
        &nbsp;&nbsp;<strong>公共脚本</strong>：（本配置页面需要的公共javascript脚本）<br />
        <asp:TextBox ID="javascript" runat="server" EnableViewState="False" TextMode="MultiLine"
            Rows="3" Width="98%" ToolTip="定义了需要的公共脚本 #USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID"></asp:TextBox>
        &nbsp;&nbsp;<strong>公共引用脚本文件</strong>：（本配置页面需要引用的公共javascript脚本文件）
        <asp:Button ID="dlScriptsrc_Add" runat="server" EnableViewState="False" Text="新增"
            OnClick="dlScriptsrc_Add_Click" /><br />
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <asp:DataList ID="dlScriptsrc" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    RepeatColumns="4">
                    <ItemTemplate>
                        <td>
                            <asp:TextBox ID="dlScriptsrc_value" runat="server" EnableViewState="False" Text='<%#Eval("value")%>'
                                Width="200px" CssClass="input_text" ToolTip="引用脚本文件地址（可以是包含http的完整地址或在存在与js文件下的文件名）"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                            &nbsp;&nbsp;
                            <asp:Button ID="dlScriptsrc_Del" runat="server" Text="移除" ToolTip='<%#Eval("index")%>'
                                OnClick="dlScriptsrc_Del_Click" />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </tr>
        </table>
    </div>
    <div id="2" class="editblock" style="display: none">
        &nbsp;&nbsp;<strong>本配置基本属性</strong>：&nbsp;&nbsp;
        <asp:Button ID="myquery_Del" runat="server" EnableViewState="False" Text="删除本页卡"
            ToolTip="仅一个页卡时不能删除 将直接删除本页卡所有配置" OnClick="myquery_Del_Click" /><br />
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    标题
                </th>
                <td colspan="5">
                    <asp:TextBox ID="title" runat="server" EnableViewState="False" ToolTip="请输入显示的标题"
                        CssClass="input_text" Width="98%"></asp:TextBox>
                </td>
                <td colspan="2" rowspan="4">
                    <b>标题SQL：</b><br />
                    <asp:TextBox ID="titleSql" runat="server" EnableViewState="False" TextMode="MultiLine"
                        Rows="5" ToolTip="输入替换标题{0}的SQL语句 （只取一行一列，语句中可用#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID） 获取的值替换标题中的{0} 注意详述不支持"
                        CssClass="input_text" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    功能代号
                </th>
                <td>
                    <asp:TextBox ID="functioncode" runat="server" EnableViewState="False" MaxLength="20"
                        ToolTip="本配置将根据功能代号判断是否授权 需结合判断是否授权给了用户的SQL配置" CssClass="input_text" Width="100px"
                        onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                        ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    标题参数
                </th>
                <td colspan="3">
                    <asp:TextBox ID="titleParameters" runat="server" EnableViewState="False" ToolTip="可选设置 需要的参数name,值从Request.QueryString中获取 多于一个用,分割 获取的值对应替换SQL中的{n}"
                        CssClass="input_text" Width="200px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    进入加载
                </th>
                <td>
                    <asp:DropDownList ID="isfirstload" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（列表或详述时有效）是否首次打开本查询时加载并展示默认条件的数据 详述时此项要配成是，否则无法显示列表数据');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    是否分页
                </th>
                <td>
                    <asp:DropDownList ID="allowpaging" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（列表时有效）列表数据是否进行分页');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    分页记录数
                </th>
                <td>
                    <asp:TextBox ID="pagesize" runat="server" EnableViewState="False" MaxLength="6" ToolTip="（列表时有效）列表数据每页记录数 不设置则取Web.Config中配置的值 详述的单条信息展示时必须设置为0"
                        CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    是否排序
                </th>
                <td>
                    <asp:DropDownList ID="allowsorting" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,' （列表时有效）行标题是否支持点击后重新排序');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    显示记录数
                </th>
                <td>
                    <asp:DropDownList ID="pagervisible" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,' （列表时有效）无分页或只有一页时是否显示记录数 否则不显示');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    处理方式
                </th>
                <td>
                    <asp:DropDownList ID="islogicdeal" runat="server" EnableViewState="False" Width="80px">
                        <asp:ListItem Value="false" Selected="True">物理处理</asp:ListItem>
                        <asp:ListItem Value="true">逻辑处理</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'处理方式说明：（列表时有效）&lt;br&gt;物理处理是按照分页仅获取当前页数据，对应的数据库需支持分页语句（如Sql2005、oracle、db2、informix）&lt;br&gt;逻辑处理 获取所有满足条件数据， 来源数据库Sql2000和access或不来自数据库时等只能设置逻辑处理');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
            <tr>
                <th>
                    缓存数据
                </th>
                <td>
                    <asp:DropDownList ID="issavedata" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（列表时有效）列表数据是否缓存 当数据量不大或来源效率较低时可以设置为是，使只在查询时获取数据');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    列表高度
                </th>
                <td>
                    <asp:TextBox ID="list_height" runat="server" EnableViewState="False" Width="25px"
                        CssClass="input_text" ToolTip="可选属性 设置列表显示高度" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    <asp:DropDownList ID="height_unit" runat="server" EnableViewState="False" Width="50px">
                        <asp:ListItem Value="px" Selected="True">像素</asp:ListItem>
                        <asp:ListItem Value="%">百分比</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    列表宽度
                </th>
                <td>
                    <asp:TextBox ID="list_width" runat="server" EnableViewState="False" Width="25px"
                        CssClass="input_text" ToolTip="可选属性 设置列表显示宽度" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    <asp:DropDownList ID="width_unit" runat="server" EnableViewState="False" Width="50px">
                        <asp:ListItem Value="px" Selected="True">像素</asp:ListItem>
                        <asp:ListItem Value="%">百分比</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    固定列数
                </th>
                <td>
                    <asp:TextBox ID="fixedcols" runat="server" EnableViewState="False" Width="25px" CssClass="input_text"
                        ToolTip="可选属性 设置从左侧起固定列的数量 列表显示宽度后设置有效" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    计算行可见
                </th>
                <td>
                    <asp:DropDownList ID="footervisible" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（列表时有效）页脚计算列是否可见 当列配置了汇总等计算时需要设置为是');"
                        src="../Img/shm.gif" />
                </td>
                <th>备注说明</th>
                <td colspan="5">
                    <asp:TextBox ID="notes" runat="server" TextMode="MultiLine" Rows="2" EnableViewState="False"
                        ToolTip="请输入显示的备注说明" CssClass="input_text" Width="98%"></asp:TextBox>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>编辑增加需执行的SQL语句</strong>（编辑配置有效）：<asp:Button ID="dlUpdatesqls_Add"
            runat="server" EnableViewState="False" Text="新增" OnClick="dlUpdatesqls_Add_Click" />&nbsp;&nbsp;编辑表的主键ID是否为自增:
        <asp:DropDownList ID="isautoid" runat="server" EnableViewState="False">
            <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
            <asp:ListItem Value="true">是</asp:ListItem>
        </asp:DropDownList>
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（编辑配置有效）编辑表的主键ID是否为自增 &lt;br&gt;如果使用自增ID，下面的sql必须按照ismodify=false和true分别写sql&lt;br&gt;&nbsp;&nbsp;false时语句中使用#ID#代替获得的自增主健ID&lt;br&gt;&nbsp;&nbsp;true时id在paranames中引用 语句中用对应的{n}代替&lt;br&gt;不是用自增id 时 主健可配置在columnnames中 语句中统一用对应的#id#代替');"
            src="../Img/shm.gif" /><br />
        <asp:DataList ID="dlUpdatesqls" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td>
                        执行条件:<asp:DropDownList ID="dlUpdatesqls_ismodify" runat="server" EnableViewState="False"
                            SelectedValue='<%#Eval("ismodify")%>'>
                            <asp:ListItem Value="all">提交时执行</asp:ListItem>
                            <asp:ListItem Value="true">修改时执行</asp:ListItem>
                            <asp:ListItem Value="false">新增时执行</asp:ListItem>
                            <asp:ListItem Value="pagefirstload">页面初始化时执行</asp:ListItem>
                        </asp:DropDownList>
                        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,' （编辑配置有效）提交时执行为点击保存后即执行；&lt;br&gt;修改时执行为修改更新数据时才执行；&lt;br&gt;新增时执行为新增数据时执行；&lt;br&gt;页面初始化时执行为页面初始化完成时执行');"
                            src="../Img/shm.gif" />
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="dlUpdatesqls_Del" runat="server" Text="移除" ToolTip='<%#Eval("index")%>'
                            OnClick="dlUpdatesqls_Del_Click" />&nbsp;&nbsp;
                    </td>
                    <td rowspan="2">
                        SQL表达式:<br />
                        <asp:TextBox ID="dlUpdatesqls_value" TextMode="MultiLine" Rows="3" runat="server"
                            EnableViewState="False" Text='<%#Eval("value")%>' Width="500px" CssClass="input_text"
                            ToolTip="为一条insert 或 update语句(语句中可以写入#ID#代替获得的自增主健ID只有在isautoid=true并且ismodify=false是时才有效  #USERID#来代替登录用户的ID)  注意 #name# 将被实际name获得得值代替 字符串是需要加上对应的''"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        字段name集合:<asp:TextBox ID="dlUpdatesqls_columnnames" runat="server" EnableViewState="False"
                            Text='<%#Eval("columnnames")%>' Width="150px" CssClass="input_text" ToolTip="需要匹配值SQL语句中#name#从对应字段取值的name集合 多个用,分割 与定义字段的name一致注意大小写一致 语句中用#对应的name#代替"
                            onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                            ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    </td>
                    <td>
                        参数name集合:<asp:TextBox ID="dlUpdatesqls_paranames" runat="server" EnableViewState="False"
                            Text='<%#Eval("paranames")%>' Width="150px" CssClass="input_text" ToolTip="需要匹配值SQL语句中#name#从Request.QueryString中取值的name集合 多个用,分割 必须为URL传入参数的name 语句中用#对应的name#代替"
                            onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                            ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="3" class="editblock" style="display:none; ">
        &nbsp;&nbsp;<strong>数据源基本属性</strong>：配置数据的来源<br />
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="100px" />
                <col width="200px" />
                <col width="100px" />
                <col width="200px" />
                <col width="100px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    来源类型
                </th>
                <td>
                    <asp:DropDownList ID="type" runat="server" EnableViewState="False" Width="180px">
                        <asp:ListItem Value="table" Selected="True">数据来自表</asp:ListItem>
                        <asp:ListItem Value="procedure">数据来自存储过程</asp:ListItem>
                        <asp:ListItem Value="xls">数据来自Excel文件</asp:ListItem>
                        <asp:ListItem Value="xml">数据来自xml文件</asp:ListItem>
                        <asp:ListItem Value="txt">数据来自txt文件</asp:ListItem>
                        <asp:ListItem Value="urlxml">数据来自Url链接</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'数据来源类型：&lt;br&gt;数据来自表时需要定义数据库类型、连接字符串和数据表节；&lt;br&gt;数据来自存储过程时需要定义数据库类型、连接字符串和存储过程名；&lt;br&gt;数据来自Excel文件时文件为Excel普通格式和xml格式（首行必须是标题行），文件的绝对路径（必须在web服务器所在机器上）多个文件用,分割；&lt;br&gt;数据来自xml文件时文件为xml文件的绝对路径（必须在web服务器所在机器上）多个文件用,分割；&lt;br&gt;数据来自txt文件时文件为txt（首行必须是标题行），文件的绝对路径（必须在web服务器所在机器上）多个文件用,分割；&lt;br&gt;数据来自Url链接时需要配置提交方式、链接参数集合和文件链接（必须返回xml数据）');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    数据库类型
                </th>
                <td>
                    <asp:DropDownList ID="dbtype" runat="server" EnableViewState="False" Width="205px">
                        <asp:ListItem Value="" Selected="True">默认web.config配置</asp:ListItem>
                        <asp:ListItem Value="sql2005">SQL Server2005及以上数据库</asp:ListItem>
                        <asp:ListItem Value="sql2000">SQL Server2000及以下数据库</asp:ListItem>
                        <asp:ListItem Value="oracle">Oracle数据库</asp:ListItem>
                        <asp:ListItem Value="oledb">OleDB连接数据库</asp:ListItem>
                        <asp:ListItem Value="odbcmysql">MySql数据库</asp:ListItem>
                        <asp:ListItem Value="odbcdb2">DB2数据库</asp:ListItem>
                        <asp:ListItem Value="odbcinformix">Informix数据库</asp:ListItem>
                        <asp:ListItem Value="odbc">ODBC数据源</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    连接字符串<br />
                    文件链接
                </th>
                <td>
                    <asp:TextBox ID="connectionstring" runat="server" EnableViewState="False" ToolTip="可选设置 当数据来自表或存储过程时设置数据库连接字符串 空则取web.config中对应CurrentDBType的配置； 当数据来自xml、Excel或文本文件时设置文件的绝对路径（必须在web服务器所在机器上），多个文件用,分割； 当数据来自Url链接（必须返回xml数据）时设置为http页面、WebServise （注意Methodname是区分大小写的）或 注意：当配置了链接参数集合或查询条件时将以name=value提交，需要url访问地址的支持"
                        CssClass="input_text" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    存储过程名<br />
                    首行是标题
                </th>
                <td>
                    <asp:TextBox ID="procedurename" runat="server" EnableViewState="False" ToolTip="当调用存储过程时必须配置为存储过程名；当数据来源于Excel或Txt文件时true表示首行为标题行 false时表示无标题行 空则默认true首行为标题行"
                        CssClass="input_text" Width="200px" onkeypress="return validateCode(this, getkeyCode(event));"
                        onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    提交方式
                </th>
                <td>
                    <asp:DropDownList ID="method" runat="server" EnableViewState="False" Width="205px">
                        <asp:ListItem Value="" Selected="True">无</asp:ListItem>
                        <asp:ListItem Value="POST">from表单提交</asp:ListItem>
                        <asp:ListItem Value="GET">Url参数提交</asp:ListItem>
                        <asp:ListItem Value="STOR">ftp默认</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    链接参数
                </th>
                <td>
                    <asp:TextBox ID="parameters" runat="server" EnableViewState="False" ToolTip="需要提供的链接参数集合 可选属性 必须为name=value的格式，多个用,分割 当获取数据需要的用户帐户与密码信息时格式id={0},pwd={1} id和pwd可根据实际调整名称 {0}为当前用户帐户{1}当前用户密码；当数据来源于txt文本时可设置数据字段间隔字符"
                        CssClass="input_text" Width="200px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>数据来自表时需要的表配置</strong>：（仅数据来源表时需要）<asp:Button ID="dlTables_Add"
            runat="server" EnableViewState="False" Text="新增" OnClick="dlTables_Add_Click" /><br />
        <asp:DataList ID="dlTables" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td>
                        表或连接表达式:<asp:TextBox ID="dlTables_value" runat="server" EnableViewState="False" Text='<%#Eval("value")%>'
                            Width="650px" CssClass="input_text" ToolTip="第一个必须为主表的表名或子查询语句并且#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID 可以起别名 如 userinfo u；以后的必须是连接表达式支持子查询  如 departmentinfo d on u.depid=d.depid"></asp:TextBox>
                    </td>
                    <td>
                        描述:<asp:TextBox ID="dlTables_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                            Width="100px" CssClass="input_text" ToolTip="表描述 可选属性"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="dlTables_Del" runat="server" Text="移除" ToolTip='<%#Eval("index")%>'
                            OnClick="dlTables_Del_Click" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>数据来自表时需要的分组配置</strong>：（仅数据来源表时需要）<br />
        <asp:TextBox ID="group" runat="server" EnableViewState="False" ToolTip="分组设置（实质是SQL的group by部分，不包含group by 可包含having） 注意：当展示列中指定的列表达式使用了聚合函数时需要"
            CssClass="input_text" Width="98%"></asp:TextBox>
        &nbsp;&nbsp;<strong>加载数据时排序配置</strong>：（数据来源表时并且物理处理时必须设置）<br />
        <asp:TextBox ID="order" runat="server" EnableViewState="False" ToolTip="排序设置（实质是SQL的order by部分，不包含order by） 注意：数据来源表时并且物理处理时必须设置 当有分组设置时 排序的字段必须在分组设置中包括或为聚会函数"
            CssClass="input_text" Width="98%"></asp:TextBox>
        &nbsp;&nbsp;<strong>SQL配置</strong>：（仅html模板数据、指定工作流下一步人员或扩展语句获取数据时需要）
        <asp:Button ID="dlSqls_Add" runat="server" EnableViewState="False" Text="新增" OnClick="dlSqls_Add_Click" /><br />
        <asp:DataList ID="dlSqls" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td>
                        名称:<asp:TextBox ID="dlSqls_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                            Width="100px" CssClass="input_text" ToolTip="必须设置 作为内部处理的唯一的表名，如扩展语句获取数据则必须与扩展语句的列name集合名称一致" onkeypress="return validateCode(this, getkeyCode(event));"
                            onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        &nbsp;<asp:Button ID="dlSqls_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlSqls_Del_Click" />
                    </td>
                    <td rowspan="3">
                        SQL表达式(完整SQL语句)<br />
                        <asp:TextBox ID="dlSqls_value" runat="server" EnableViewState="False" TextMode="MultiLine"
                            Text='<%#Eval("value")%>' Rows="3" Width="98%" ToolTip="完整SQL语句(语句中可用#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID,{n}将有参数获取的QueryString的值替换)"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        参数name集合:<asp:TextBox ID="dlSqls_paranames" runat="server" EnableViewState="False"
                            Text='<%#Eval("paranames")%>' Width="150px" CssClass="input_text" ToolTip="需要匹配值SQL语句中对应顺序从Request.QueryString中取值的name集合 多个用,分割 用于工作流编辑时paranames中可配置model（获得当前模型id）,task（获得当前任务id）,（process获得当前流程id）,instance（获得当前任务实例id）"
                            onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                            ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        描述:<asp:TextBox ID="dlSqls_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                            Width="150px" CssClass="input_text" ToolTip="表描述 可选属性"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Button ID="dlTables_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlTables_Del_Click" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="4" class="editblock" style="display: none">
        &nbsp;&nbsp;<strong>展示数据基本属性</strong>：（仅列表时生效）<br />
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    主键字段
                </th>
                <td>
                    <asp:TextBox ID="keycolumnnames" runat="server" EnableViewState="False" ToolTip="主键字段对应的name集合 可选设置 主键的值将作为tr的id（当需要对选中的行进行处理时必须设置） 必须是下面字段对应的name 多于一个用,分割 获取的值也用,分割"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    是否多选
                </th>
                <td>
                    <asp:DropDownList ID="ismultiselect" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否允许多选 当是时第一列为复选框&lt;br&gt;选中行的主键多个用;分割&lt;br&gt;值可从MyGridView控件名_selectid(界面隐藏控件)中获取&lt;br&gt;按钮区配置了isselectedrow=true的将选中的行的ID替换{0}参数');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    是否单选
                </th>
                <td>
                    <asp:DropDownList ID="issingleselect" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否显示单选按钮 当是时第一列为单选框&lt;br&gt;注意如果设置了允许多选 此项设置失效');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    显示序号
                </th>
                <td>
                    <asp:DropDownList ID="isshowrownum" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否显示序号 当是时第一列为序号 与是否多选重合时 第一列为复选框和序号');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
            <tr>
                <th>
                    行合并列
                </th>
                <td colspan="3">
                    <asp:TextBox ID="mergecolumns" runat="server" EnableViewState="False" ToolTip="可选设置 必须是字段对应的name 多于一个用,分割 设置后按照第一个设置的列判断行数据是否相同,相同的将合并指定的列的数据行"
                        CssClass="input_text" Width="300px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    点击排序
                </th>
                <td>
                    <asp:DropDownList ID="sortdirection" runat="server" EnableViewState="False" Width="80px">
                        <asp:ListItem Value="ascending" Selected="True">升序</asp:ListItem>
                        <asp:ListItem Value="descending">降序</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'第一次点击列标题排序时使用的排序方式');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    交叉表数据
                </th>
                <td>
                    <asp:DropDownList ID="iscrosstable" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否为行列交叉表数据即行、列、值三个字段的表 处理时将第二列的值转化为列 仅对数据源为表的有效');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
            <tr>
                <th>
                    分组主键列
                </th>
                <td>
                    <asp:TextBox ID="parentcolumnname" runat="server" EnableViewState="False" ToolTip="可选设置 必须是展示字段中对应的name（数据集中此列值不能重复）"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    子行上级列
                </th>
                <td>
                    <asp:TextBox ID="childcolumnname" runat="server" EnableViewState="False" ToolTip="分组数据的子行对应的上级列 配合分组主键列使用 必须是展示字段中对应的name"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    关系展示列
                </th>
                <td>
                    <asp:TextBox ID="parentchilddisplay" runat="server" EnableViewState="False" ToolTip="可选设置 父子关系展示所在列 配合分组主键列使用 必须是展示字段中对应的name"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    展开子节点
                </th>
                <td>
                    <asp:DropDownList ID="isallopenchilds" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否展开子节点 配合分组设置');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>展示列摆放设置</strong>：（列表时无效） &nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（查询列表时忽略此节按照实际的column配置获取，注意需成对出现 第一个为展示标题 第二个展示为控件）');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="dlSelectCol_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlSelectCol_Add_Click" /><br />
        <asp:DataList ID="dlSelectCol" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
            RepeatColumns="4">
            <ItemTemplate>
                宽度:<asp:TextBox ID="dlSelectCol_width" runat="server" EnableViewState="False" Text='<%#Eval("width")%>'
                    Width="25px" CssClass="input_text" ToolTip="可选属性 设置显示宽度" onkeypress="return validateInt(this, getkeyCode(event));"
                    onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                <asp:DropDownList ID="dlSelectCol_unit" runat="server" EnableViewState="False" SelectedValue='<%#Eval("unit")%>'
                    Width="50px">
                    <asp:ListItem Value="px" Selected="True">像素</asp:ListItem>
                    <asp:ListItem Value="%">百分比</asp:ListItem>
                </asp:DropDownList>
                &nbsp;样式
                <asp:TextBox ID="dlSelectCol_style" runat="server" EnableViewState="False" Text='<%#Eval("style")%>'
                    Width="50px" CssClass="input_text" ToolTip="可选设置 样式属性 如white-space:nowrap;" onkeypress="return validateCode(this, getkeyCode(event));"
                    onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                &nbsp;
                <asp:Button ID="dlSelectCol_Del" runat="server" Text="移除" Width="30px" ToolTip='<%#Eval("index")%>'
                    OnClick="dlSelectCol_Del_Click" />&nbsp;
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>展示列配置</strong>：&nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'配置应用列表或导出时展示的列及编辑或详述时展示的列');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="dlSelectColumn_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlSelectColumn_Add_Click" /><br />
        <asp:DataList ID="dlSelectColumn" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <img src="../Img/r_d.gif" style="cursor: pointer;" align="absbottom" onclick="showBody(this, 'Select<%#Eval("name")%>')" /><%#Eval("title")%>(<%#Eval("name")%>)配置
                        &nbsp;&nbsp;
                        <asp:Button ID="dlSelectColumn_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlSelectColumn_Del_Click" />
                    </td>
                </tr>
                <tbody id='Select<%#Eval("name")%>' style="display: none">
                    <tr>
                        <td>
                            本列名称:<asp:TextBox ID="dlSelectColumn_name" runat="server" EnableViewState="False"
                                Text='<%#Eval("name")%>' Width="100px" CssClass="input_text" ToolTip="必须设置 作为内部处理的唯一的列名 否则前面的会被覆盖"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            本列标题:<asp:TextBox ID="dlSelectColumn_title" runat="server" EnableViewState="False"
                                Text='<%#Eval("title")%>' Width="100px" CssClass="input_text" ToolTip="必须设置 作为列标题展示"></asp:TextBox>
                        </td>
                        <td>
                            控件样式:<asp:TextBox ID="dlSelectColumn_cssclass" runat="server" EnableViewState="False"
                                Text='<%#Eval("cssclass")%>' Width="100px" CssClass="input_text" ToolTip="样式class 可选设置 当展示为控件时为控件的class否则为当前格即td的样式"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            处理类型:<asp:DropDownList ID="dlSelectColumn_type" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("type")%>' Width="80px">
                                <asp:ListItem Value="">默认文本</asp:ListItem>
                                <asp:ListItem Value="text">文本</asp:ListItem>
                                <asp:ListItem Value="select">下拉列表</asp:ListItem>
                                <asp:ListItem Value="link">链接</asp:ListItem>
                                <asp:ListItem Value="image">展示图片</asp:ListItem>
                                <asp:ListItem Value="calcscript">脚本计算列</asp:ListItem>
                                <asp:ListItem Value="calccs">程序计算列</asp:ListItem>
                                <asp:ListItem Value="button">按钮</asp:ListItem>
                                <asp:ListItem Value="linkbutton">链接按钮</asp:ListItem>
                                <asp:ListItem Value="imagebutton">图片按钮</asp:ListItem>
                                <asp:ListItem Value="formula">Excel公式</asp:ListItem>
                                <asp:ListItem Value="excelout">仅Excel导出</asp:ListItem>
                                <asp:ListItem Value="chart">分析图形</asp:ListItem>
                                <asp:ListItem Value="auto">自动生成</asp:ListItem>
                                <asp:ListItem Value="textint">整数文本框</asp:ListItem>
                                <asp:ListItem Value="textdouble2">两位小数文本框</asp:ListItem>
                                <asp:ListItem Value="textdouble">数值文本框</asp:ListItem>
                                <asp:ListItem Value="textdate">日期文本框</asp:ListItem>
                                <asp:ListItem Value="texttime">日期时间文本框</asp:ListItem>
                                <asp:ListItem Value="checkbox">复选框</asp:ListItem>
                                <asp:ListItem Value="textajax">输入选择文本框</asp:ListItem>
                                <asp:ListItem Value="textarea">多行文本编辑</asp:ListItem>
                                <asp:ListItem Value="detail">仅显示文本</asp:ListItem>
                                <asp:ListItem Value="selectint">下拉整数</asp:ListItem>
                                <asp:ListItem Value="selectdouble">下拉数值</asp:ListItem>
                                <asp:ListItem Value="selectdate">下拉日期</asp:ListItem>
                                <asp:ListItem Value="composite">合并列</asp:ListItem>
                                <asp:ListItem Value="fileinput">上传文件</asp:ListItem>
                                <asp:ListItem Value="textnowrap">不换行文本</asp:ListItem>
                                <asp:ListItem Value="title">显示标题文字</asp:ListItem>
                                <asp:ListItem Value="getvalue">作为获取值字段</asp:ListItem>
                                <asp:ListItem Value="hidden">作为隐藏域字段</asp:ListItem>
                                <asp:ListItem Value="singleselect">弹出单选列表</asp:ListItem>
                                <asp:ListItem Value="multiselect">弹出多选列表</asp:ListItem>
                                <asp:ListItem Value="iframe">嵌入链接</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'文本：就是将值表达式作为获取数据列 并展示出来 当编辑时为文本编辑框 当详述时为展示文本；&lt;br&gt;下拉列表：（通用）当编辑时为下拉列表 其它是按照列表的配置将值替换为对应的文本 需要配置&lt;br&gt;&nbsp;&nbsp;下拉数据配置为sql:SQL语句 第一字段为值 第二字段为显示；xml:xml文件完整名称；url:http或WebServise 将form节配置的默认参数和当前的Request.QueryString作为参数；也可直接用字符串指定下拉列表数据 如0,男;1,女；&lt;br&gt;链接：（非编辑）需要配置 链接格式串、引用的字段name集合（对应链接格式串中{n}）、导航、值（作为链接显示的内容）；&lt;br&gt;图片：（非编辑）需要配置 链接格式串、引用的字段name集合（当有值时作为title提示）、值（替换链接格式串中的{0}）；&lt;br&gt;脚本计算列：（非编辑）利用脚本计算的列 尽量使用脚本函数 需配置 引用的字段作为值表达式中对应{n}替换内容、值为符合MSScriptControl.Eval方法的返回值的脚本；&lt;br&gt;程序计算列：（非编辑）值为实现IColumnCalc的类名，必须包含完整的命名空间；&lt;br&gt;按钮：需要配置 标题（作为按钮展示文本）、处理（作为为实现了IButtonDeal接口的完整类名）、提示信息（作为为执行按钮前的确认信息 无则忽略）、值作为按钮的参数值；&lt;br&gt;链接按钮：标题（作为按钮展示文本）、处理（作为为实现了IButtonDeal接口的完整类名）、提示信息（作为为执行按钮前的确认信息 无则忽略）、值作为按钮的参数值；&lt;br&gt;图片按钮：标题（作为按钮展示文本）、链接格式串（作为图片地址）、处理（作为为实现了IButtonDeal接口的完整类名）、提示信息（作为为执行按钮前的确认信息 无则忽略）、值作为按钮的参数值；&lt;br&gt;计算公式：（仅Excel导出，此项页面不展示）值为Excel定义公式 格式=R[n]C[n] R垂直 C水平 n相对本格的偏移量 R负数为向上偏移 正数向下偏移 C负数为向左偏移 正数向右偏移 0时省略[0] 如=R[-1]C[-1]+R[-1]C 则表示为本格的上方偏左一个和正上方一格的和 最好的方法是在Excel中定义一个公式另存为xml格式，再用记事本打开可查看公式；&lt;br&gt;仅Excel导出：（仅Excel导出，此项页面不展示）值表达式作为获取数据列（同文本）；&lt;br&gt;分析图形：（非编辑，详述的单条信息展示有效）其名称必须与分析图形节中的名称相同 否则不能展示分析图形；&lt;br&gt;自动生成：（仅编辑）当配置了默认值时按照默认值处理 否则按照数据库自增处理 (oracle的序列时写法：select sequenceName.NEXTVAL from dual)；&lt;br&gt;整数文本框：（非列表）展示为只能输入整数的文本框；&lt;br&gt;两位小数文本框：（非列表）展示为只能输入两位小数的数值文本框；&lt;br&gt;数值文本框：（非列表）展示为输入数值文本框；&lt;br&gt;日期文本框：（非列表）展示为日期选择输入文本框；&lt;br&gt;日期时间文本框：（非列表）展示为日期时间选择输入文本框；&lt;br&gt;复选框：（非列表）展示为复选框；&lt;br&gt;输入选择文本框：（非列表）展示为输入出现下拉选择的文本框；&lt;br&gt;多行文本编辑（非列表）展示为多行文本编辑框 需设置下拉数据作为行数；&lt;br&gt;编辑时仅显示文本：（仅编辑）仅显示值，值表达式作为获取数据列（同文本）；&lt;br&gt;下拉整数：（非列表）展示为下拉列表，值必须能转换为整数 需要配置同下拉列表；&lt;br&gt;下拉数值：（非列表）展示为下拉列表，值必须能转换为数值 需要配置同下拉列表&lt;br&gt;下拉日期：（非列表）展示为下拉列表，值必须能转换为日期 需要配置同下拉列表；&lt;br&gt;合并列：值必须为子列的配置 列表时将展示为合并列标题；&lt;br&gt;上传文件（仅编辑）展示为可上传文件文件的控件；&lt;br&gt;不换行文本 仅列表时增加nowrap属性 其它同文本；&lt;br&gt;显示标题文字：仅将标题文字作为输出显示；&lt;br&gt;作为获取值字段，需配置引用数据和数据格式')"
                                src="../Img/shm.gif" />
                        </td>
                        <td rowspan="4">
                            <b>展示列配置值表达式：</b><br />
                            <asp:TextBox ID="dlSelectColumn_exception" TextMode="MultiLine" Rows="5" runat="server"
                                EnableViewState="False" Text='<%#Eval("exception")%>' Width="250px" CssClass="input_text"
                                ToolTip='数据源来源表或存储过程且当列表或详述或导出Excel时配置为数据库获取的字段名或子查询 防止冲突建议加上表名（格式:表名或别名.字段名）；当链接时作为值链接显示的内容；当图片时值替换图片链接串中的{0}；当脚本计算列时为符合MSScriptControl.Eval方法的返回值的脚本；当程序计算列时值为实现IColumnCalc的类名，必须包含完整的命名空间；当按钮时值作为按钮的参数值；当Excel公式时值为Excel定义公式 格式=R[n]C[n] R垂直 C水平 n相对本格的偏移量 R负数为向上偏移 正数向下偏移 C负数为向左偏移 正数向右偏移 0时省略[0] 如=R[-1]C[-1]+R[-1]C 则表示为本格的上方偏左一个和正上方一格的和 最好的方法是在Excel中定义一个公式另存为xml格式，再用记事本打开可查看公式；当处理类型为合并列时值必须为子列的配置；当编辑时值可以配置为验证，如：<validates><validate type="校验类型[isnotnull,非空验证;regex,正则表达式;js,javascript验证;isexit,是否存在验证;servervalidate,后台验证]" title="错误时提示信息" scope="校验范围[all,全部;edit,仅修改;add,仅增加]"></validate></validates>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            数据格式:<asp:TextBox ID="dlSelectColumn_dataformat" runat="server" EnableViewState="False"
                                Text='<%#Eval("dataformat")%>' Width="100px" CssClass="input_text" ToolTip="可选设置 建议数字指定格式 支持String.Format的复合格式 如{0:D}日期、{0:G}常规通用、{0:P2}百分数(2位小数)、{0:C0}货币(0位小数)、{0:F2}固定(2位小数)、{0:N}千分位数字、{0:E}科学记数法（指数）、{0:M}自定义为展示多行文本、{0:ROOT}自定义为加上web根路径、{0:ID}自定义为获取name[id]中id值、{0:NAME}自定义为获取name[id]中name值、{0:HTML}自定义去除html标签 注意当处理类型为下列列表时此项配置不空时为下拉列表的第一项为值,显示 如：,全部或者_,无"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            指定宽度:<asp:TextBox ID="dlSelectColumn_width" runat="server" EnableViewState="False"
                                Text='<%#Eval("width")%>' Width="30px" CssClass="input_text" ToolTip="可选属性 设置显示宽度"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                            <asp:DropDownList ID="dlSelectColumn_unit" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("unit")%>'>
                                <asp:ListItem Value="px" Selected="True">像素</asp:ListItem>
                                <asp:ListItem Value="%">百分比</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            是否可见:<asp:DropDownList ID="dlSelectColumn_visible" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("visible")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否可见：是时展示 列表当指定否时获取值但不展示，一般作为其它引用数据；&lt;br&gt;编辑时设置为否且有值时隐藏行（注意同行的其它也不可见了），一般作为不展示的提交数据');"
                                src="../Img/shm.gif" />
                        </td>
                        <td>
                            是否排序:<asp:DropDownList ID="dlSelectColumn_issort" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("issort")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'列表时时表示是否排序 当指定否时本列不能排序 注意：当数据不是来源于表时要实现排序必须设置为缓存数据；&lt;br&gt;导出Excel时表示是否默认选择；&lt;br&gt;工作流表示此字段写入流程名称');"
                                src="../Img/shm.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            排序字段:<asp:TextBox ID="dlSelectColumn_sort" runat="server" EnableViewState="False"
                                Text='<%#Eval("sort")%>' Width="100px" CssClass="input_text" ToolTip="仅列表时有效 可选设置 当逻辑处理时展示列名称为排序表达式；物理处理时必须为原始字段表达式作为排序表达式"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            链接格式:<asp:TextBox ID="dlSelectColumn_urlformatstring" runat="server" EnableViewState="False"
                                Text='<%#Eval("urlformatstring")%>' Width="100px" CssClass="input_text" ToolTip="下拉列表时设置生成列表数据的依据(其数据源同from节设置)sql:SQL语句 第一字段为值 第二字段为显示（语句支持url参数即Request.QueryString取值或session取值，{n}必须和usefields中的参数name对应，语句中可用#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID）  当使用Url参数时usefields配置URL参数name对应，多个用,分割;xml:xml文件完整名称;url:http或WebServise 将form节配置的默认参数和当前的Request.QueryString作为参数;也可直接用字符串指定下拉列表数据 如0,男;1,女；当链接时设置链接格式串必须设置本项目路径从根目录开始 其它用http开头；当图片或图片按钮时设置图片链接格式串 必须设置 本项目路径从根目录开始 其它用http开头；输入选择文本框时设置使用的WEB服务Path 必须设置 本项目路径从根目录指定 其它用http开头；当多行文本编辑设置为定义行数（默认10）；上传文件时设置为文件下载链接（如：Editor/Down.aspx?t=file&f={0}）；其它类型此项不设置"></asp:TextBox>
                        </td>
                        <td>
                            引用数据:<asp:TextBox ID="dlSelectColumn_usefields" runat="server" EnableViewState="False"
                                Text='<%#Eval("usefields")%>' Width="100px" CssClass="input_text" ToolTip="当链接格式串时设置为替换对应{n}的字段名称 多于一个用,分割；当列表数据时设置为替换对应{n}的URL参数name或Session的name；当图片时为图片的title信息；当输入选择文本框时设置为WEB服务的ServiceMethod（服务方法）；上传文件时设置为允许的文件后缀，空或不设置则不限制 多个时使用,分割 如txt,doc"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            导航处理:<asp:TextBox ID="dlSelectColumn_target" runat="server" EnableViewState="False"
                                Text='<%#Eval("target")%>' Width="100px" CssClass="input_text" ToolTip="当链接时设置为_self（本页）、_blank（打开新页,此项为默认值）_parent（父窗口）_top（最顶层窗口）或frame的名称；当按钮时设置为实现了IButtonDeal接口的完整类名"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            计算类型:<asp:TextBox ID="dlSelectColumn_calctype" runat="server" EnableViewState="False"
                                Text='<%#Eval("calctype")%>' Width="100px" CssClass="input_text" ToolTip="脚本列计算类型：none（无计算）、sum（求和，数据源中存在的列如物理分页则为当前页数据合计）、avg（平均，数据源中存在的列如物理分页则为当前页数据平均）、totalsum（求总和）、totalavg（总平均）、totalcount（总计数），自定义处理时为实现IFooterCalc的类名，必须包含完整的命名空间"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            合并列数:<asp:TextBox ID="dlSelectColumn_colspan" runat="server" EnableViewState="False"
                                Text='<%#Eval("colspan")%>' MaxLength="6" ToolTip="（非列表时有效）展示的控件合并列数" CssClass="input_text"
                                Width="100px" onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            合并行数:<asp:TextBox ID="dlSelectColumn_rowspan" runat="server" EnableViewState="False"
                                Text='<%#Eval("rowspan")%>' MaxLength="6" ToolTip="（非列表时有效）标题及控件合并行数 一行内仅支持一个列配置此项"
                                CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            最大长度:<asp:TextBox ID="dlSelectColumn_maxlength" runat="server" EnableViewState="False"
                                Text='<%#Eval("maxlength")%>' MaxLength="6" ToolTip="（编辑时有效）编辑框时可输入的最大长度；当输入选择文本框时为回发的最小字符数 建议为2；上传文件时设置为允许的文件大小，单位M 0或不设置则不控制附件大小"
                                CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            功能代号：<asp:TextBox ID="dlSelectColumn_functioncode" runat="server" EnableViewState="False"
                                MaxLength="20" ToolTip="本配置将根据功能代号判断是否授权 需结合判断是否授权给了用户的SQL配置" Text='<%#Eval("functioncode")%>'
                                CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td colspan="3">
                            备注说明：<asp:TextBox ID="dlSelectColumn_notes" runat="server" EnableViewState="False"
                                ToolTip="当列表和详述时作为标题格鼠标悬停时的提示信息 编辑控件时会在其后增加一个帮助按钮 点击可查看本提示" Text='<%#Eval("notes")%>'
                                CssClass="input_text" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            列默认值:<asp:TextBox ID="dlSelectColumn_value" runat="server" EnableViewState="False"
                                Text='<%#Eval("value")%>' Width="150px" CssClass="input_text" ToolTip="可选配置 直接值或框架支持的字符串替换值（useraccount当前用户账号; username当前用户姓名; userorg当前用户部门id; querystring,name获取Request.QueryString[name]的值; form,name获取Request.Form[name]的值; session,name获取Session[name]的值; getdate,n 获取当前偏移n天的日期(,0可省略); getweekfromtodate,n获得指定偏移周数的周的开始至当天(,0可省略); getweekfromtoend,n获得指定偏移周数的周的开始至周末(,0可省略); getmonthfromtodate,n 获得当月指定偏移月数的月开始至当日(,0可省略);getmonthfromtoend,n 获得当月指定偏移月数的月开始至当月底(,0可省略);getyearfromtodate,n 获得当年指定偏移年数的年开始至当日(,0可省略);getyearfromtoend,n 获得当年指定偏移年数的年开始至当年底(,0可省略); select语句 将获得select语句的第一行第一列的值） 或配置为实现了IDefaultCalc接口的类名(参数) 类名必须包含完整的命名空间（包含.认为为类）"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>展示列需要的公共脚本</strong>：（展示列配置需要的公共javascript脚本）<br />
        <asp:TextBox ID="selectjavascript" runat="server" EnableViewState="False" TextMode="MultiLine"
            Rows="3" Width="98%" ToolTip="定义了展示列需要的公共javascript脚本 #USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID"></asp:TextBox>
    </div>
    <div id="5" class="editblock" style="display: none">
        &nbsp;&nbsp;<strong>条件配置基本属性</strong>：<br />
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="80px" />
                <col width="80px" />
                <col width="100px" />
                <col width="80px" />
                <col width="80px" />
                <col width="80px" />
                <col width="80px" />
                <col width="80px" />
                <col width="80px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    使用参数
                </th>
                <td>
                    <asp:DropDownList ID="isparameter" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否使用参数 &lt;brgt;是时使用参数传条件值（仅数据来源于数据库时有效，Procedure时必须为是）&lt;brgt;否时将条件值替换到条件字符串中（数据来源非数据库时使用）');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    显示默认条件
                </th>
                <td>
                    <asp:DropDownList ID="isshowappend" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,' （列表时有效）是否显示默认条件 &lt;brgt;是时将默认条件配置的标题显示在列表的上方显示默认条件行 &lt;brgt;否时不显示');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    启用缓存
                </th>
                <td>
                    <asp:DropDownList ID="issession" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,' （列表时有效）是否使用session缓存查询的条件&lt;brgt;是时使用session缓存的条件 并在首次进入时加载session缓存的条件&lt;brgt;否时不使用缓存条件（建议不使用）');"
                        src="../Img/shm.gif" />
                </td>
                <th>
                    查询按钮
                </th>
                <td>
                    <asp:DropDownList ID="isquerybutton" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">在按钮右侧</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">在条件右侧</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>
                    查询方案
                </th>
                <td>
                    <asp:DropDownList ID="isquerysubject" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">无</asp:ListItem>
                        <asp:ListItem Value="true">使用</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>默认条件配置</strong>：&nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'定义非界面条件默认条件 一般定义为数据权限等 此条件优先与页面条件');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="dlAppends_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlAppends_Add_Click" /><br />
        <asp:DataList ID="dlAppends" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <img src="../Img/r_d.gif" style="cursor: pointer;" align="absbottom" onclick="showBody(this, 'Append<%#Eval("name")%>')" /><%#Eval("title")%>(<%#Eval("name")%>)配置
                        &nbsp;&nbsp;<asp:Button ID="dlAppends_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlAppends_Del_Click" />
                    </td>
                </tr>
                <tbody id='Append<%#Eval("name")%>' style="display: none">
                    <tr>
                        <td>
                            条件类型:<asp:DropDownList ID="dlAppends_type" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("type")%>'>
                                <asp:ListItem Value="default">直接条件</asp:ListItem>
                                <asp:ListItem Value="sql">自定义语句条件</asp:ListItem>
                                <asp:ListItem Value="querystring">URL导航条件</asp:ListItem>
                                <asp:ListItem Value="sys">系统定义</asp:ListItem>
                                <asp:ListItem Value="querystring||sys">导航或系统条件</asp:ListItem>
                                <asp:ListItem Value="session">Session会话条件</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'条件类型：&lt;br&gt;直接条件 此时条件表达式可以为直接的用于where的SQL条件表达式，如设置了参数name 则需要默认值作为值，替换条件表达式中{0}；&lt;br&gt;自定义语句条件时条件表达式配置为sql判断语句（获取的第一行第一列的值 当值与下面value配置的when相同时返回对应的配置条件 必须要配置一个when= 的值对应获取不到值时的value 语句省略则从缓存中取用户的数据权限） value when=值 title=默认条件的提示 where=返回的条件值（#USERID#来代替登录用户的ID #USERDEPID#来代替登录用户的部门ID）返回的条件表达式/value；&lt;br&gt;URL导航条件 此时参数name必须设置 值从Request.QueryString中获取 空时取默认值；&lt;br&gt;系统定义 默认值为实现了IWhereCalc接口的类名(参数) 必须包含完整的命名空间；&lt;br&gt;导航或系统条件 优先从导航中获取值，空时再使用系统定义条件；&lt;br&gt;Session会话条件 参数name必须设置 值从Session中获取 支持从Session对象中返回字段值 如user.Id 空则从默认值获取');"
                                src="../Img/shm.gif" />
                        </td>
                        <td>
                            参数name:<asp:TextBox ID="dlAppends_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 作为内部处理的唯一的名称 否则前面的会被覆盖 当条件从导航或Session中获取时同时也是获取值的name"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td rowspan="3">
                            <b>条件表达式</b>:<br />
                            <asp:TextBox ID="dlAppends_exception" runat="server" EnableViewState="False" TextMode="MultiLine"
                                Rows="3" Text='<%#Eval("exception")%>' Width="500px" CssClass="input_text" ToolTip='必须设置 条件表达式 要包含此条件连接的方式and(并且条件)or(或者条件)，当有值时需要{0}作为替换表达式 注意当系统定义时可为and或or；当自定义语句条件时条件表达式配置为<sql>判断语句（获取的第一行第一列的值 当值与下面value配置的when相同时返回对应的配置条件 必须要配置一个when=""的值对应获取不到值时的value）</sql> <value when="值" title="默认条件的提示" where="返回的条件值（#USERID#来代替登录用户的ID #USERDEPID#来代替登录用户的部门ID）">返回的条件表达式</value>'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            条件描述:<asp:TextBox ID="dlAppends_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                                Width="100px" CssClass="input_text" ToolTip="可选设置 作为显示的默认条件值含义的提示 可以使用{0}作为值的替换符"></asp:TextBox>
                        </td>
                        <td>
                            默认值:<asp:TextBox ID="dlAppends_value" runat="server" EnableViewState="False" Text='<%#Eval("value")%>'
                                Width="120px" CssClass="input_text" ToolTip="可选设置 当系统定义条件时此为实现了IWhereCalc接口的类名(参数) 必须包含完整的命名空间；其它情况下此为默认值 直接值或框架支持的字符串替换值（useraccount当前用户账号; username当前用户姓名; userorg当前用户部门id; querystring,name获取Request.QueryString[name]的值; form,name获取Request.Form[name]的值; session,name获取Session[name]的值; getdate,n 获取当前偏移n天的日期(,0可省略); getweekfromtodate,n获得指定偏移周数的周的开始至当天(,0可省略); getweekfromtoend,n获得指定偏移周数的周的开始至周末(,0可省略); getmonthfromtodate,n 获得当月指定偏移月数的月开始至当日(,0可省略);getmonthfromtoend,n 获得当月指定偏移月数的月开始至当月底(,0可省略);getyearfromtodate,n 获得当年指定偏移年数的年开始至当日(,0可省略);getyearfromtoend,n 获得当年指定偏移年数的年开始至当年底(,0可省略); select语句 将获得select语句的第一行第一列的值） 或配置为实现了IDefaultCalc接口的类名(参数) 类名必须包含完整的命名空间（包含.认为为类）"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            值&nbsp;&nbsp;类&nbsp;&nbsp;型:<asp:DropDownList ID="dlAppends_datatype" runat="server"
                                EnableViewState="False" SelectedValue='<%#Eval("datatype")%>'>
                                <asp:ListItem Value="string">字符串</asp:ListItem>
                                <asp:ListItem Value="int">整数</asp:ListItem>
                                <asp:ListItem Value="double">数值</asp:ListItem>
                                <asp:ListItem Value="date">日期</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'值类型：当数据库对值的类型严格时必须正确设置获取到值的类型如Oracle数据库 建议设置防止出现自动的转换错误&lt;br&gt;字符串 注意使用参数时不能加单引号 不使用参数而是直接值时必须加单引号；&lt;br&gt;整数 将获取的值字符串转化为整数值；&lt;br&gt;数值 将获取的值字符串转化为数值（带小数时使用）；&lt;br&gt;日期 将获取的值字符串转化为日期；');"
                                src="../Img/shm.gif" />
                        </td>
                        <td>
                            功能代号：<asp:TextBox ID="dlAppends_functioncode" runat="server" EnableViewState="False"
                                MaxLength="20" ToolTip="本配置将根据功能代号判断是否授权 需结合判断是否授权给了用户的SQL配置" Text='<%#Eval("functioncode")%>'
                                CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>界面条件摆放设置</strong>： &nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'（界面条件摆放设置，注意需成对出现 第一个为展示标题 第二个展示为控件）');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="dlWhereCols_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlWhereCols_Add_Click" /><br />
        <asp:DataList ID="dlWhereCols" runat="server" RepeatLayout="Table" RepeatDirection="Horizontal"
            RepeatColumns="4">
            <ItemTemplate>
                宽度:<asp:TextBox ID="dlWhereCols_width" runat="server" EnableViewState="False" Text='<%#Eval("width")%>'
                    Width="25px" CssClass="input_text" ToolTip="可选属性 设置显示宽度" onkeypress="return validateInt(this, getkeyCode(event));"
                    onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                <asp:DropDownList ID="dlWhereCols_unit" runat="server" EnableViewState="False" SelectedValue='<%#Eval("unit")%>'
                    Width="50px">
                    <asp:ListItem Value="px" Selected="True">像素</asp:ListItem>
                    <asp:ListItem Value="%">百分比</asp:ListItem>
                </asp:DropDownList>
                &nbsp;样式
                <asp:TextBox ID="dlWhereCols_style" runat="server" EnableViewState="False" Text='<%#Eval("style")%>'
                    Width="50px" CssClass="input_text" ToolTip="可选设置 样式属性 如white-space:nowrap;" onkeypress="return validateCode(this, getkeyCode(event));"
                    onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                &nbsp;
                <asp:Button ID="dlWhereCols_Del" runat="server" Text="移除" Width="30px" ToolTip='<%#Eval("index")%>'
                    OnClick="dlWhereCols_Del_Click" />&nbsp;
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>界面条件配置</strong>：（仅列表时有效）&nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'配置应用列表界面展示的条件（展示为标题 输入条件控件');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="dlWheres_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlWheres_Add_Click" /><br />
        <asp:DataList ID="dlWheres" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <img src="../Img/r_d.gif" style="cursor: pointer;" align="absbottom" onclick="showBody(this, 'Where<%#Eval("name")%>')" /><%#Eval("title")%>(<%#Eval("name")%>)配置
                        &nbsp;&nbsp;
                        <asp:Button ID="dlWheres_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlWheres_Del_Click" />
                    </td>
                </tr>
                <tbody id='Where<%#Eval("name")%>' style="display: none">
                    <tr>
                        <td>
                            参数name:<asp:TextBox ID="dlWheres_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 作为内部处理的唯一的条件参数名 否则前面的会被覆盖"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            条件标题:<asp:TextBox ID="dlWheres_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 作为条件标题展示"></asp:TextBox>
                        </td>
                        <td>
                            处理类型:<asp:DropDownList ID="dlWheres_type" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("type")%>' Width="80px">
                                <asp:ListItem Value="">默认文本</asp:ListItem>
                                <asp:ListItem Value="text">文本</asp:ListItem>
                                <asp:ListItem Value="textint">整数文本框</asp:ListItem>
                                <asp:ListItem Value="textdouble2">两位小数文本框</asp:ListItem>
                                <asp:ListItem Value="textdouble">数值文本框</asp:ListItem>
                                <asp:ListItem Value="textdate">日期文本框</asp:ListItem>
                                <asp:ListItem Value="texttime">日期时间文本框</asp:ListItem>
                                <asp:ListItem Value="checkbox">复选框</asp:ListItem>
                                <asp:ListItem Value="textajax">输入选择文本框</asp:ListItem>
                                <asp:ListItem Value="textarea">多行文本编辑</asp:ListItem>
                                <asp:ListItem Value="fromto">起止文本框</asp:ListItem>
                                <asp:ListItem Value="fromtoint">起止整数</asp:ListItem>
                                <asp:ListItem Value="fromtodate">起止日期</asp:ListItem>
                                <asp:ListItem Value="fromtodouble">起止数值</asp:ListItem>
                                <asp:ListItem Value="select">下拉列表</asp:ListItem>
                                <asp:ListItem Value="selectint">下拉整数</asp:ListItem>
                                <asp:ListItem Value="selectdouble">下拉数值</asp:ListItem>
                                <asp:ListItem Value="selectdate">下拉日期</asp:ListItem>
                                <asp:ListItem Value="fromtoselectint">整数起止列表</asp:ListItem>
                                <asp:ListItem Value="fromtoselectdouble">数值起止列表</asp:ListItem>
                                <asp:ListItem Value="singleselect">弹出单选列表</asp:ListItem>
                                <asp:ListItem Value="multiselect">弹出多选列表</asp:ListItem>
                                <asp:ListItem Value="fieldwhere">自定义条件</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'文本：展示为文本编辑框；&lt;br&gt;下拉列表：展示为下拉列表 需要配置&lt;br&gt;&nbsp;&nbsp;列表数据配置为sql:SQL语句 第一字段为值 第二字段为显示；xml:xml文件完整名称；url:http或WebServise 将form节配置的默认参数和当前的Request.QueryString作为参数；也可直接用字符串指定下拉列表数据 如0,男;1,女；&lt;br&gt;整数文本框：展示为只能输入整数的文本框；&lt;br&gt;两位小数文本框：展示为只能输入两位小数的数值文本框；&lt;br&gt;数值文本框：展示为输入数值文本框；&lt;br&gt;日期文本框：展示为日期选择输入文本框；&lt;br&gt;日期时间文本框：展示为日期时间选择输入文本框；&lt;br&gt;复选框：展示为复选框；&lt;br&gt;输入选择文本框：展示为输入出现下拉选择的文本框；&lt;br&gt;多行文本编辑展示为多行文本编辑框 需设置列表数据作为行数；&lt;br&gt;起止整数：展示为开始和截止两个整数编辑框；&lt;br&gt;起止日期：展示为开始和截止两个日期编辑框；&lt;br&gt;起止数值：展示为开始截止两个数值编辑框；&lt;br&gt;下拉整数：展示为下拉列表，值必须能转换为整数 需要配置同下拉列表；&lt;br&gt;下拉数值：展示为下拉列表，值必须能转换为数值 需要配置同下拉列表&lt;br&gt;下拉日期：展示为下拉列表，值必须能转换为日期 需要配置同下拉列表；&lt;br&gt;整数起止列表：展示为下拉列表，列表的单个值必须能转换为整数 需要配置同下拉列表，列表的两个值用~分割；&lt;br&gt;数值起止列表：展示为下拉列表，列表的单个值必须能转换为数值 需要配置同下拉列表，列表的两个值用~分割');"
                                src="../Img/shm.gif" />
                        </td>
                        <td rowspan="4">
                            <b>条件表达式：</b><br />
                            <asp:TextBox ID="dlWheres_exception" TextMode="MultiLine" Rows="5" runat="server"
                                EnableViewState="False" Text='<%#Eval("exception")%>' Width="250px" CssClass="input_text"
                                ToolTip="必须设置 条件表达式 其中{0}将被控件值替换 当选择了起止的两个控件条件时{1}将被第二个控件值替换" onkeypress="return validateCode(this, getkeyCode(event));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            列表选项:<asp:TextBox ID="dlWheres_dataformat" runat="server" EnableViewState="False"
                                Text='<%#Eval("dataformat")%>' Width="100px" CssClass="input_text" ToolTip="可选设置 下列列表时此项配置不空时为下拉列表的第一项为值,显示 如：,全部或者_,无"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            指定宽度:<asp:TextBox ID="dlWheres_width" runat="server" EnableViewState="False" Text='<%#Eval("width")%>'
                                Width="30px" CssClass="input_text" ToolTip="可选属性 设置显示宽度" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                            <asp:DropDownList ID="dlWheres_unit" runat="server" EnableViewState="False" SelectedValue='<%#Eval("unit")%>'>
                                <asp:ListItem Value="px" Selected="True">像素</asp:ListItem>
                                <asp:ListItem Value="%">百分比</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            控件样式:<asp:TextBox ID="dlWheres_cssclass" runat="server" EnableViewState="False" Text='<%#Eval("cssclass")%>'
                                Width="100px" CssClass="input_text" ToolTip="样式class 可选设置 当展示为控件时为控件的class" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            下拉数据:<asp:TextBox ID="dlWheres_urlformatstring" runat="server" EnableViewState="False"
                                Text='<%#Eval("urlformatstring")%>' Width="100px" CssClass="input_text" ToolTip="下拉列表时设置生成列表数据的依据(其数据源同from节设置)sql:SQL语句 第一字段为值 第二字段为显示（语句支持url参数即Request.QueryString取值或session取值，{n}必须和usefields中的参数name对应，语句中可用#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID）;xml:xml文件完整名称;url:http或WebServise 将form节配置的默认参数和当前的Request.QueryString作为参数;也可直接用字符串指定下拉列表数据 如0,男;1,女；输入选择文本框时设置使用的WEB服务Path 必须设置 本项目路径从根目录指定 其它用http开头；当多行文本编辑设置为定义行数（默认10）；其它类型此项不设置"></asp:TextBox>
                        </td>
                        <td>
                            引用数据:<asp:TextBox ID="dlWheres_usefields" runat="server" EnableViewState="False"
                                Text='<%#Eval("usefields")%>' Width="100px" CssClass="input_text" ToolTip="当下拉数据时设置为替换对应{n}的URL参数name或Session的name  多于一个用,分割；当输入选择文本框时设置为WEB服务的ServiceMethod（服务方法）"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            模糊处理:<asp:DropDownList ID="dlWheres_target" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("target")%>' Width="80px">
                                <asp:ListItem Value="none">无</asp:ListItem>
                                <asp:ListItem Value="right">在值后面加%</asp:ListItem>
                                <asp:ListItem Value="left">在值前面加%</asp:ListItem>
                                <asp:ListItem Value="all">在值两面及空格加%</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            合并列数:<asp:TextBox ID="dlWheres_colspan" runat="server" EnableViewState="False" Text='<%#Eval("colspan")%>'
                                MaxLength="6" ToolTip="展示的控件合并列数" CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            合并行数:<asp:TextBox ID="dlWheres_rowspan" runat="server" EnableViewState="False" Text='<%#Eval("rowspan")%>'
                                MaxLength="6" ToolTip="标题及控件合并行数 一行内仅支持一个列配置此项" CssClass="input_text" Width="100px"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            最大长度:<asp:TextBox ID="dlWheres_maxlength" runat="server" EnableViewState="False"
                                Text='<%#Eval("maxlength")%>' MaxLength="6" ToolTip="编辑框时可输入的最大长度 当输入选择文本框时为回发的最小字符数 建议为2"
                                CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            功能代号：<asp:TextBox ID="dlWheres_functioncode" runat="server" EnableViewState="False"
                                MaxLength="20" ToolTip="本配置将根据功能代号判断是否授权 需结合判断是否授权给了用户的SQL配置" Text='<%#Eval("functioncode")%>'
                                CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            备注说明：<asp:TextBox ID="dlWheres_notes" runat="server" EnableViewState="False" ToolTip="在条件控件后增加一个帮助按钮 点击可查看本提示"
                                Text='<%#Eval("notes")%>' CssClass="input_text" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            默&nbsp;&nbsp;认&nbsp;&nbsp;值:<asp:TextBox ID="dlWheres_value" runat="server" EnableViewState="False"
                                Text='<%#Eval("value")%>' Width="150px" CssClass="input_text" ToolTip="可选配置 直接值或框架支持的字符串替换值（useraccount当前用户账号; username当前用户姓名; userorg当前用户部门id; querystring,name获取Request.QueryString[name]的值; form,name获取Request.Form[name]的值; session,name获取Session[name]的值; getdate,n 获取当前偏移n天的日期(,0可省略); getweekfromtodate,n获得指定偏移周数的周的开始至当天(,0可省略); getweekfromtoend,n获得指定偏移周数的周的开始至周末(,0可省略); getmonthfromtodate,n 获得当月指定偏移月数的月开始至当日(,0可省略);getmonthfromtoend,n 获得当月指定偏移月数的月开始至当月底(,0可省略);getyearfromtodate,n 获得当年指定偏移年数的年开始至当日(,0可省略);getyearfromtoend,n 获得当年指定偏移年数的年开始至当年底(,0可省略); select语句 将获得select语句的第一行第一列的值） 或配置为实现了IDefaultCalc接口的类名(参数) 类名必须包含完整的命名空间（包含.认为为类）"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="6" class="editblock" style="display: none">
        &nbsp;&nbsp;<strong>处理按钮配置</strong>：&nbsp;&nbsp;
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'配置在本页卡的处理按钮 编辑的保存和重置按钮用于编辑模板 其它用于列表展示');"
            src="../Img/shm.gif" />
        <br />
        按钮区域位置：<asp:DropDownList ID="position" runat="server" EnableViewState="False">
            <asp:ListItem Value="all">上下都有</asp:ListItem>
            <asp:ListItem Value="top">列表上方</asp:ListItem>
            <asp:ListItem Value="bottom">列表下方</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;按钮权限处理类：<asp:TextBox ID="ifunctioncalc" runat="server" EnableViewState="False"
            Width="150px" CssClass="input_text" ToolTip="可选设置 根据按钮的功能代号  若配置了类 则按照其类反射 获得是否有此功能 如没有配置类 则按照是否授权给了用户的SQL判断是否有此功能。均未配置时按钮默认展示 类为实现了IFunctionCalc接口的类 类名必须包含完整的命名空间 建议使用判断是否授权给了用户的SQL"
            onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
            ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
        按钮区提示：<asp:TextBox ID="buttonsnotes" runat="server" EnableViewState="False" ToolTip="在按钮区右侧增加一个帮助按钮 点击可查看本提示" CssClass="input_text" Width="200px"></asp:TextBox>
        &nbsp;&nbsp;<asp:Button ID="dlButton_Add" runat="server" EnableViewState="False"
            Text="新增" OnClick="dlButton_Add_Click" /><br />
        <asp:DataList ID="dlButton" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <img src="../Img/r_d.gif" style="cursor: pointer;" align="absbottom" onclick="showBody(this, 'Button<%#Eval("name")%>')" /><%#Eval("title")%>(<%#Eval("name")%>)配置
                        &nbsp;&nbsp;<asp:Button ID="dlButton_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlButton_Del_Click" />
                    </td>
                </tr>
                <tbody id='Button<%#Eval("name")%>' style="display: none">
                    <tr>
                        <td>
                            按钮名称:<asp:TextBox ID="dlButton_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 按钮的名称 必须唯一否则前面的会被覆盖" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            按钮标题:<asp:TextBox ID="dlButton_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 按钮的显示标题"></asp:TextBox>
                        </td>
                        <td>
                            按钮样式:<asp:TextBox ID="dlButton_cssclass" runat="server" EnableViewState="False" Text='<%#Eval("cssclass")%>'
                                Width="100px" CssClass="input_text" ToolTip="按钮的样式class" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            处理类型:<asp:DropDownList ID="dlButton_type" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("type")%>' Width="80px">
                                <asp:ListItem Value="dialog">打开模式窗体</asp:ListItem>
                                <asp:ListItem Value="redirect">页面重定向</asp:ListItem>
                                <asp:ListItem Value="open">打开新页面</asp:ListItem>
                                <asp:ListItem Value="deal">自定义处理</asp:ListItem>
                                <asp:ListItem Value="toexcel">导出到excel</asp:ListItem>
                                <asp:ListItem Value="runsql">执行语句</asp:ListItem>
                                <asp:ListItem Value="doscript">执行脚本</asp:ListItem>
                                <asp:ListItem Value="save">编辑的保存</asp:ListItem>
                                <asp:ListItem Value="reset">编辑的重置</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'处理类型说明：&lt;br&gt;打开模式窗体，当新窗体返回true时父窗体自动刷新；&lt;br&gt;页面重定向，直接进入配置的页面；&lt;br&gt;打开新页面，如果父窗体需要刷新open的新页面需要写parent.href=parent.href脚本；&lt;br&gt;自定义后台处理使用，值表达式为实现了IButtonDeal接口的完整类名；&lt;br&gt;导出到excel，直接将datatsource数据导出到excel（只能配一个） &lt;br&gt;&nbsp;&nbsp;注意 直接导出不需额外配置时值为空&lt;br&gt;&nbsp;&nbsp;针对先提供一个选择配置列的页面，根据选中的项再进行导出excel时配置一个open按钮 &lt;br&gt;&nbsp;&nbsp;值配置为Tmp/MyToExcel.aspx?s=1&n=导出excel配置文件（不含.xml后缀）&lt;br&gt;&nbsp;&nbsp;如果n的参数与当前配置同名则按照当前配置提供选择和导出 否则需配置f=当前配置文件（不含.xml后缀）并保证新配置能接收此配置生成的条件&lt;br&gt;&nbsp;&nbsp;对于直接使用的excel输出模板的定义redirect的按钮 值为Tmp/MyToExcel.aspx?n=导出excel配置文件（不含.xml后缀）&lt;br&gt;&nbsp;&nbsp;如果n的参数与当前配置同名则功能与toexcel相同 使用MyToExcel.aspx的导出优先使用Session中对应查询的条件 不存在时按照默认条件处理；&lt;br&gt;runsql 执行语句（主要用于删除） 注意 值需要配置为sql节&lt;br&gt;&nbsp;&nbsp;sql title=“无满足条件的执行语句时提示” select判断语句，语句中{0}将被替换为选中行的id注意多选值用,隔开/sql&lt;br&gt;&nbsp;&nbsp;sql value=“” title=“执行成功后提示信息”当值等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开/sql(可以为多个)&lt;br&gt;&nbsp;&nbsp;sql title=“执行成功后提示信息”&gt;当值未设置或值不等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开&lt;/sql(可以为多个) &lt;br&gt;执行脚本值为需要执行的脚本&lt;br&gt;编辑保存（只能配一个）编辑模板的保存处理按钮&lt;br&gt;重置按钮（只能配一个）编辑模板的重置处理按钮');"
                                src="../Img/shm.gif" />
                        </td>
                        <td rowspan="4">
                            <b>按钮配置值表达式：</b><br />
                            <asp:TextBox ID="dlButton_value" TextMode="MultiLine" Rows="5" runat="server" EnableViewState="False"
                                Text='<%#Eval("value")%>' Width="250px" CssClass="input_text" ToolTip='打开模式窗体、页面重定向、打开新页面时为导航到的页面；deal时为实现了IButtonDeal接口的完整类名；doscript时为javascript脚本；runsql时为值需要配置为<sql title="无满足条件的执行语句时提示">select判断语句，语句中{0}将被替换为选中行的id注意多选值用,隔开</sql><sql value="" title="执行成功后提示信息">当值等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开</sql>(可以为多个)<sql title="执行成功后提示信息">当值未设置或值不等于判断语句获取的第一行第一列的值时执行语句，语句中{0}将被替换为选中行的id注意多选值用,隔开</sql>(可以为多个)'></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            功能代码:<asp:TextBox ID="dlButton_functioncode" runat="server" EnableViewState="False"
                                Text='<%#Eval("functioncode")%>' Width="100px" CssClass="input_text" ToolTip="按钮的功能代码 空为不限制 需结合判断是否授权给了用户的SQL配置"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            需要选中:<asp:DropDownList ID="dlButton_isselectedrow" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("isselectedrow")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'当按钮需要先选中行时选是&lt;br&gt;选择是时按钮配置值必须包含替换参数{0}，&lt;br&gt;程序将{0}参数的值由客户端选中行获得的选中行的ID&lt;br&gt;（行的ID有keycolumnnames获得，多个用,分割，多选时多个ID用;分割）');"
                                src="../Img/shm.gif" />
                        </td>
                        <td>
                            窗口宽度:<asp:TextBox ID="dlButton_windowwidth" runat="server" EnableViewState="False"
                                Text='<%#Eval("windowwidth")%>' Width="100px" CssClass="input_num" ToolTip="打开新窗口宽度 空为不设置"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            窗口高度:<asp:TextBox ID="dlButton_windowheight" runat="server" EnableViewState="False"
                                Text='<%#Eval("windowheight")%>' Width="100px" CssClass="input_num" ToolTip="打开新窗口高度 空为不设置"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            确认提示:<asp:TextBox ID="dlButton_confirminfo" runat="server" EnableViewState="False"
                                Text='<%#Eval("confirminfo")%>' Width="100px" CssClass="input_text" ToolTip="点击按钮给出的确认信息 空为不设置"></asp:TextBox>
                        </td>
                        <td>
                            参数集合:<asp:TextBox ID="dlButton_parameters" runat="server" EnableViewState="False"
                                Text='<%#Eval("parameters")%>' Width="100px" CssClass="input_text" ToolTip="参数name集合 可选设置 需要的参数name,值从Request.QueryString中获取 多于一个用,分割"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            按钮宽度:<asp:TextBox ID="dlButton_width" runat="server" EnableViewState="False" Text='<%#Eval("width")%>'
                                Width="100px" CssClass="input_num" ToolTip="按钮宽度 空为不设置" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            按钮图片:<asp:TextBox ID="dlButton_imgsrc" runat="server" EnableViewState="False" Text='<%#Eval("imgsrc")%>'
                                Width="100px" CssClass="input_text" ToolTip="提供按钮图片URL 空为不设置" onkeypress="return validateCode2(this, getkeyCode(event));"
                                onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            参数name:<asp:TextBox ID="dlButton_parameter" runat="server" EnableViewState="False"
                                Text='<%#Eval("parameter")%>' Width="100px" CssClass="input_text" ToolTip="参数name 可选设置 需要的参数name 值从Request.QueryString中获取"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            参数的值:<asp:TextBox ID="dlButton_parametervalue" runat="server" EnableViewState="False"
                                Text='<%#Eval("parametervalue")%>' Width="100px" CssClass="input_text" ToolTip="提供对应参数name的值（可匹配多种情况时值用,分割），与当parameter配合使用，当获取的值与配置的值匹配时按钮才加入"
                                onkeypress="return validateCode(this, getkeyCode(event));" onpaste="return validateCode(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            页卡索引:<asp:TextBox ID="dlButton_tabindex" runat="server" EnableViewState="False"
                                Text='<%#Eval("tabindex")%>' Width="100px" CssClass="input_text" ToolTip="dialog按钮返回的页卡索引，仅多页卡时使用"
                                onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            下一页卡:<asp:DropDownList ID="dlButton_isnexttab" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("isnexttab")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'成功后是否前往下一个TAB 仅多个TAB页卡时有效');"
                                src="../Img/shm.gif" />
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:DataList>
        &nbsp;&nbsp;<strong>列表增加自定义数据行</strong>：（列表时有效）<asp:Button ID="dlAddrows_Add" runat="server"
            EnableViewState="False" Text="新增" OnClick="dlAddrows_Add_Click" /><br />
        <asp:DataList ID="dlAddrows" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td>
                        名称:<asp:TextBox ID="dlAddrows_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                            Width="200px" CssClass="input_text" ToolTip="自定义数据行的名称 必须唯一否则前面的会被覆盖" onkeypress="return validateCode(this, getkeyCode(event));"
                            onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    </td>
                    <td>
                        行处理表达式:<asp:TextBox ID="dlAddrows_value" runat="server" EnableViewState="False" Text='<%#Eval("value")%>'
                            Width="500px" CssClass="input_text" ToolTip="实现了IRowCalc接口的类的完整名称(参数)" onkeypress="return validateCode2(this, getkeyCode(event));"
                            onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                    </td>
                    <td width="50px">
                        <asp:Button ID="dlAddrows_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlAddrows_Del_Click" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div id="7" class="editblock" style="display: none">
        &nbsp;&nbsp;<strong>分析图形基本配置</strong>：（仅列表和详述有效）<br />
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col width="100px" />
                <col />
            </colgroup>
            <tr>
                <th>
                    名称
                </th>
                <td>
                    <asp:TextBox ID="chart_name" runat="server" EnableViewState="False" ToolTip="详述时必须与配置存放chart的列名称相同 否则不能展示分析图形 当名称为空时表示没有分析图形配置"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                        onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    宽度
                </th>
                <td>
                    <asp:TextBox ID="chart_width" runat="server" EnableViewState="False" MaxLength="6"
                        ToolTip="必须设置分析图形展示的宽度（单位像素px）" CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    高度
                </th>
                <td>
                    <asp:TextBox ID="chart_height" runat="server" EnableViewState="False" MaxLength="6"
                        ToolTip="必须设置分析图形展示的高度（单位像素px）" CssClass="input_text" Width="100px" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <th>
                    样式
                </th>
                <td>
                    <asp:DropDownList ID="chart_template" runat="server" EnableViewState="False">
                        <asp:ListItem Value="skyblue" Selected="True">天蓝</asp:ListItem>
                        <asp:ListItem Value="warmtones">柔和</asp:ListItem>
                        <asp:ListItem Value="whitewmoke">白色</asp:ListItem>
                        <asp:ListItem Value="">无</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'如需改动样式请到本系统XML目录的Data目录修改对应的模板文件；&lt;br&gt;如在本系统XML目录的Data目录中增加了其它样式增加在此选项中即可');"
                        src="../Img/shm.gif" />
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>图形标题</strong>：（可设置多个标题 第一个为主标题）
        <asp:Button ID="dlTitles_Add" runat="server" EnableViewState="False" Text="新增" OnClick="dlTitles_Add_Click" /><br />
        <table cellpadding="0" cellspacing="0" border="0">
            <tr>
                <asp:DataList ID="dlTitles" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    RepeatColumns="4">
                    <ItemTemplate>
                        <td>
                            <asp:TextBox ID="dlTitles_value" runat="server" EnableViewState="False" Text='<%#Eval("value")%>'
                                Width="150px" CssClass="input_text" ToolTip="标题文本 不能为空"></asp:TextBox>
                            <asp:DropDownList ID="dlTitles_docking" runat="server" EnableViewState="False" SelectedValue='<%#Eval("docking")%>'>
                                <asp:ListItem Value="top">放在顶部</asp:ListItem>
                                <asp:ListItem Value="bottom">放在底部</asp:ListItem>
                                <asp:ListItem Value="left">放在左侧</asp:ListItem>
                                <asp:ListItem Value="right">放在右侧</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;<asp:Button ID="dlTitles_Del" runat="server" Text="移除" ToolTip='<%#Eval("index")%>'
                                OnClick="dlTitles_Del_Click" />
                        </td>
                    </ItemTemplate>
                </asp:DataList>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>分析图形图像区域配置</strong>：（仅列表和详述有效）<br />
        <table cellpadding="0" cellspacing="0" border="0">
            <colgroup>
                <col width="180px" />
                <col width="180px" />
                <col width="180px" />
                <col width="180px" />
                <col />
            </colgroup>
            <tr>
                <td>
                    区域名称:<asp:TextBox ID="area_name" runat="server" EnableViewState="False" ToolTip="名称 必须设置"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                        onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    数据来源:<asp:DropDownList ID="area_isdatatable" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">数据表</asp:ListItem>
                        <asp:ListItem Value="true">其它</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'当来源数据表时（使用自定义的数据源或当期配置获取的数据是表） 仅支持DataTable中的数据 &lt;br&gt;&nbsp;&nbsp;如不指定系列 则将横坐标字段列作为横坐标 其余列各作为一个图的序列，指定系列按照指定的列生成图的序列；&lt;br&gt;当选择其它数据源时（从列配置中直接获得数据） 如指定了rows属性则只按照指定的行索引，否则取全部数据生成图 支持IList和计算列');"
                        src="../Img/shm.gif" />
                </td>
                <td>
                    指定行:<asp:TextBox ID="area_rows" runat="server" EnableViewState="False" ToolTip="指定的数据行 搭配数据来源-其它使用 指定行的索引号 多于一个用,分割  无则处理所有行"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    横坐标:<asp:TextBox ID="area_xfield" runat="server" EnableViewState="False" ToolTip="横坐标对应字段名称 必须设置 并且必须对应展示列名称或在自定义的数据源中存在"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                        onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td rowspan="6">
                    <strong>数据源表达式</strong>：<br />
                    <asp:TextBox ID="area_datasource" runat="server" EnableViewState="False" ToolTip="数据源表达式：必须以以下特定字符开始(其数据源同from节设置) 返回的DataTable必须包含xfield指定的列其它的均作为数据展示。sql:SQL语句 （语句支持url参数即Request.QueryString取值或session取值，{n}必须和需要和数据来源使用参数集合中匹配值SQL语句中{n}的参数的name对应，语句中可用#USERID#来代替登录用户的ID,#USERDEPID#来代替登录用户部门的ID）；xml:xml文件完整名称；url:http或WebServise 将form节配置的默认参数和当前的Request.QueryString作为参数"
                        CssClass="input_text" Width="99%" TextMode="MultiLine" Rows="4"></asp:TextBox>
                    <br />
                    数据使用参数集合:<br />
                    <asp:TextBox ID="datasource_paranames" runat="server" EnableViewState="False" ToolTip="需要匹配值SQL语句中{n}的Url参数的name 多于一个用,分开"
                        CssClass="input_text" Width="99%" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    横值类型:<asp:DropDownList ID="area_xvaluetype" runat="server" EnableViewState="False"
                        Width="80px">
                        <asp:ListItem Value="auto">默认处理</asp:ListItem>
                        <asp:ListItem Value="date">日期</asp:ListItem>
                        <asp:ListItem Value="datetime">日期时间</asp:ListItem>
                        <asp:ListItem Value="datetimeoffset">日期偏移量</asp:ListItem>
                        <asp:ListItem Value="double">数值</asp:ListItem>
                        <asp:ListItem Value="int32">32位整数</asp:ListItem>
                        <asp:ListItem Value="int64">64位整数</asp:ListItem>
                        <asp:ListItem Value="string">字符串</asp:ListItem>
                        <asp:ListItem Value="time">时间</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'横(x)轴展示的数据类型');"
                        src="../Img/shm.gif" />
                </td>
                <td>
                    横轴格式:<asp:TextBox ID="area_xdataformat" runat="server" EnableViewState="False" ToolTip="横轴(x)轴数据显示格式 可选设置 建议数字指定格式 支持String.Format的复合格式 如{0:D}日期、{0:G}常规通用、{0:P2}百分数(2位小数)、{0:C0}货币(0位小数)、{0:F2}固定(2位小数)、{0:N}千分位数字、{0:E}科学记数法（指数）"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    纵值类型:<asp:DropDownList ID="area_yvaluetype" runat="server" EnableViewState="False"
                        Width="80px">
                        <asp:ListItem Value="auto">默认处理</asp:ListItem>
                        <asp:ListItem Value="date">日期</asp:ListItem>
                        <asp:ListItem Value="datetime">日期时间</asp:ListItem>
                        <asp:ListItem Value="datetimeoffset">日期偏移量</asp:ListItem>
                        <asp:ListItem Value="double">数值</asp:ListItem>
                        <asp:ListItem Value="int32">32位整数</asp:ListItem>
                        <asp:ListItem Value="int64">64位整数</asp:ListItem>
                        <asp:ListItem Value="string">字符串</asp:ListItem>
                        <asp:ListItem Value="time">时间</asp:ListItem>
                    </asp:DropDownList>
                    <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'纵(y)轴展示的数据类型');"
                        src="../Img/shm.gif" />
                </td>
                <td>
                    纵轴格式:<asp:TextBox ID="area_ydataformat" runat="server" EnableViewState="False" ToolTip="纵轴(y)轴数据显示格式 可选设置 建议数字指定格式 支持String.Format的复合格式 如{0:D}日期、{0:G}常规通用、{0:P2}百分数(2位小数)、{0:C0}货币(0位小数)、{0:F2}固定(2位小数)、{0:N}千分位数字、{0:E}科学记数法（指数）"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode2(this, getkeyCode(event));"
                        onpaste="return validateCode2(this, window.clipboardData.getData('Text'));" ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    3D效果:<asp:DropDownList ID="area_isshow3d" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                        <asp:ListItem Value="true">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    聚簇3D:<asp:DropDownList ID="area_isclustered" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    直角坐标:<asp:DropDownList ID="area_isrightangleaxes" runat="server" EnableViewState="False">
                        <asp:ListItem Value="false">否</asp:ListItem>
                        <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    光照模式:<asp:DropDownList ID="area_lightstyle" runat="server" EnableViewState="False">
                        <asp:ListItem Value="None" Selected="True">无</asp:ListItem>
                        <asp:ListItem Value="Simplistic">简单化的照明</asp:ListItem>
                        <asp:ListItem Value="Realistic">写实照明样式</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    X轴角度:<asp:TextBox ID="area_rotation" runat="server" EnableViewState="False" ToolTip="转动X轴角度 3D时有效 可选设置 >180度为反方向旋转"
                        CssClass="input_text" Width="100px" MaxLength="3" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    Y轴角度:<asp:TextBox ID="area_inclination" runat="server" EnableViewState="False" ToolTip="转动Y轴角度 3D时有效 可选设置 >90度为反方向旋转"
                        CssClass="input_text" Width="100px" MaxLength="3" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    点深度:<asp:TextBox ID="area_pointdepth" runat="server" EnableViewState="False" ToolTip="数据点深度 3D时有效 可选设置 不建议设置值 让系统自动处理"
                        CssClass="input_text" Width="100px" MaxLength="3" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    间隔距离:<asp:TextBox ID="area_pointgapdepth" runat="server" EnableViewState="False"
                        ToolTip="序列间隔的距离 3D时有效 可选设置 不建议设置值 让系统自动处理" CssClass="input_text" Width="100px"
                        MaxLength="3" onkeypress="return validateInt(this, getkeyCode(event));" onpaste="return validateInt(this, window.clipboardData.getData('Text'));"
                        ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    横轴步长:<asp:TextBox ID="area_xinterval" runat="server" EnableViewState="False" ToolTip="横(x)轴步长 可选设置 不建议设置值 让系统自动处理"
                        CssClass="input_text" Width="100px" MaxLength="8" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    纵轴步长:<asp:TextBox ID="area_yinterval" runat="server" EnableViewState="False" ToolTip="纵(y)轴步长 可选设置 不建议设置值 让系统自动处理"
                        CssClass="input_text" Width="100px" MaxLength="8" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    横最小值:<asp:TextBox ID="area_xminimum" runat="server" EnableViewState="False" ToolTip="横(x)轴最小值 可选设置 不建议设置值 让系统自动处理"
                        CssClass="input_text" Width="100px" MaxLength="8" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td>
                    纵最小值:<asp:TextBox ID="area_yminimum" runat="server" EnableViewState="False" ToolTip="纵(y)轴最小值 可选设置 不建议设置值 让系统自动处理"
                        CssClass="input_text" Width="100px" MaxLength="8" onkeypress="return validateInt(this, getkeyCode(event));"
                        onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    图例名称:<asp:TextBox ID="legend_name" runat="server" EnableViewState="False" ToolTip="图例名称 不设置表示不显示图例"
                        CssClass="input_text" Width="100px" onkeypress="return validateCode(this, getkeyCode(event));"
                        onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                </td>
                <td colspan="2">
                    图例标题:<asp:TextBox ID="legend_title" runat="server" EnableViewState="False" Width="200px"
                        CssClass="input_text" ToolTip="可选设置 无时不显示图例标题"></asp:TextBox>
                </td>
                <td>
                    图例位置:<asp:DropDownList ID="legend_docking" runat="server" EnableViewState="False">
                        <asp:ListItem Value="top">放在顶部</asp:ListItem>
                        <asp:ListItem Value="bottom">放在底部</asp:ListItem>
                        <asp:ListItem Value="left">放在左侧</asp:ListItem>
                        <asp:ListItem Value="right">放在右侧</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;<strong>图像生成的系列配置</strong>：&nbsp;&nbsp;
        <asp:Button ID="dlSeries_Add" runat="server" EnableViewState="False" Text="新增" OnClick="dlSeries_Add_Click" /><br />
        <asp:DataList ID="dlSeries" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
            RepeatColumns="1">
            <ItemTemplate>
                <tr>
                    <td colspan="10">
                        <img src="../Img/r_d.gif" style="cursor: pointer;" align="absbottom" onclick="showBody(this, 'Serie<%#Eval("name")%>')" /><%#Eval("title")%>(<%#Eval("name")%>)配置
                        &nbsp;&nbsp;<asp:Button ID="dlSeries_Del" runat="server" Text="移除" ToolTip='<%#Eval("name")%>'
                            OnClick="dlSeries_Del_Click" />
                    </td>
                </tr>
                <tbody id='Serie<%#Eval("name")%>' style="display: none">
                    <tr>
                        <td>
                            系列名称:<asp:TextBox ID="dlSeries_name" runat="server" EnableViewState="False" Text='<%#Eval("name")%>'
                                Width="100px" CssClass="input_text" ToolTip="必须设置 作为内部处理的唯一的名 否则前面的会被覆盖" onkeypress="return validateCode(this, getkeyCode(event));"
                                onpaste="return validateCode(this, window.clipboardData.getData('Text'));" ondrop="return validateCode(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            图例标题:<asp:TextBox ID="dlSeries_title" runat="server" EnableViewState="False" Text='<%#Eval("title")%>'
                                Width="100px" CssClass="input_text" ToolTip="可选设置 当设置了图例时作为图例展示"></asp:TextBox>
                        </td>
                        <td>
                            成图类型:<asp:DropDownList ID="dlSeries_charttype" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("charttype")%>' Width="80px">
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
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'类型为MSChart支持的所有类型 翻译可能不准确请参照MSChart的解释，并不是所有的类型都能够融合在一个图形中，请参照MSChart的解释；常用类型为colum柱图 line线图 point点图 pie饼图');"
                                src="../Img/shm.gif" />
                        </td>
                        <td rowspan="3">
                            <b>值表达式：</b><br />
                            <asp:TextBox ID="dlSeries_value" TextMode="MultiLine" Rows="3" runat="server" EnableViewState="False"
                                Text='<%#Eval("value")%>' Width="250px" CssClass="input_text" ToolTip="可选设置 值表达式 当数据来源于展示的数据时必须设置为对应select节的column的名称，当多个YValue时可以用,分割；当获取其它数据时必须设置为对应datasource中数据列的name，当多个YValue时可以用,分割；空取name的值"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            显示格式:<asp:TextBox ID="dlSeries_dataformat" runat="server" EnableViewState="False"
                                Text='<%#Eval("dataformat")%>' Width="100px" CssClass="input_text" ToolTip="可选设置 建议数字指定格式 支持String.Format的复合格式 如{0:D}日期、{0:G}常规通用、{0:P2}百分数(2位小数)、{0:C0}货币(0位小数)、{0:F2}固定(2位小数)、{0:N}千分位数字、{0:E}科学记数法（指数） 最好与上面区域对应轴的格式一致"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            图形显示值:<asp:DropDownList ID="dlSeries_isvalueshownaslabel" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("isvalueshownaslabel")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'是否在图形上显示值');"
                                src="../Img/shm.gif" />
                        </td>
                        <td>
                            停留显示值:<asp:DropDownList ID="dlSeries_istooltip" runat="server" EnableViewState="False"
                                SelectedValue='<%#Eval("istooltip")%>'>
                                <asp:ListItem Value="false">否</asp:ListItem>
                                <asp:ListItem Value="true">是</asp:ListItem>
                            </asp:DropDownList>
                            <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'鼠标停留在图形值位置是否在图形上显示值');"
                                src="../Img/shm.gif" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            链接字串:<asp:TextBox ID="dlSeries_urlformatstring" runat="server" EnableViewState="False"
                                Text='<%#Eval("urlformatstring")%>' Width="100px" CssClass="input_text" ToolTip="当图形链接其它页面使用 本项目路径从根目录开始 其它用http开头"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            引用数据:<asp:TextBox ID="dlSeries_usefields" runat="server" EnableViewState="False"
                                Text='<%#Eval("usefields")%>' Width="100px" CssClass="input_text" ToolTip="参数替换符时使用的字段名（必须设置为对应select节的column的名称或自定义数据源的列）且与之对应 多于一个用,分割"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            图形颜色:<asp:TextBox ID="dlSeries_color" runat="server" EnableViewState="False" Text='<%#Eval("color")%>'
                                Width="100px" CssClass="input_text" ToolTip="不设置则自动设置颜色 格式为：255,0,0 表示红色" onclick="var arr=openDialogByArguments('Color.htm',this.value,300,280);if (arr) {this.value=arr;this.style.color='rgb('+arr+')';}"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        <td>
                            定制属性:<asp:TextBox ID="dlSeries_customproperty" runat="server" EnableViewState="False"
                                Text='<%#Eval("customproperty")%>' Width="100px" CssClass="input_text" ToolTip="格式：属性=值 多于一个用,分割 必须为有效的MSChart序列的属性和值"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            标记点大小:<asp:TextBox ID="dlSeries_markersize" runat="server" EnableViewState="False"
                                Text='<%#Eval("markersize")%>' ToolTip="标记点大小 默认0 则不显示标记" CssClass="input_text"
                                Width="100px" MaxLength="2" onkeypress="return validateInt(this, getkeyCode(event));"
                                onpaste="return validateInt(this, window.clipboardData.getData('Text'));" ondrop="return validateInt(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                        标记点样式:<asp:DropDownList ID="dlSeries_markerstyle" runat="server" EnableViewState="False"
                            SelectedValue='<%#Eval("markerstyle")%>'>
                            <asp:ListItem Value="None">无标记点</asp:ListItem>
                            <asp:ListItem Value="Square">方形</asp:ListItem>
                            <asp:ListItem Value="Circle">圆形</asp:ListItem>
                            <asp:ListItem Value="Diamond">菱形</asp:ListItem>
                            <asp:ListItem Value="Triangle">三角</asp:ListItem>
                            <asp:ListItem Value="Cross">十字星</asp:ListItem>
                            <asp:ListItem Value="Star4">4角</asp:ListItem>
                            <asp:ListItem Value="Star5">五角</asp:ListItem>
                            <asp:ListItem Value="Star6">六角</asp:ListItem>
                            <asp:ListItem Value="Star10">十角</asp:ListItem>
                        </asp:DropDownList>
                        <td>
                            图形颜色:<asp:TextBox ID="dlSeries_markercolor" runat="server" EnableViewState="False"
                                Text='<%#Eval("markercolor")%>' Width="100px" CssClass="input_text" ToolTip="不设置则自动设置颜色 格式为：255,0,0 表示红色"
                                onclick="var arr=openDialogByArguments('Color.htm',this.value,300,280);if (arr) {this.value=arr;this.style.color='rgb('+arr+')';}"
                                onkeypress="return validateCode2(this, getkeyCode(event));" onpaste="return validateCode2(this, window.clipboardData.getData('Text'));"
                                ondrop="return validateCode2(this, event.dataTransfer.getData('Text'));"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="operation">
        <input type="image" style="border-width: 0px; cursor: help;" onclick="return showHelpTip(event,'按钮说明：&lt;br&gt;本页配置应用:应用本页编辑框中的XML配置或应用本页编辑结果到XML中 必须通过应用来暂存配置；&lt;br&gt;&nbsp;&nbsp;注意：页面上的【新增】、【移除】按钮将刷新导致未应用的配置未暂存&lt;br&gt;提交:将编辑配置结果提交保存 注意先将确认的配置页应用后再提交；&lt;br&gt;恢复原值:放弃修改恢复为服务器保存的配置；');"
            src="../Img/shm.gif" />
        &nbsp;&nbsp;<asp:Button ID="btnSave" runat="server" Text="本页配置应用" CssClass="input_button"
            OnClick="btnSave_Click" />
        &nbsp;&nbsp;<asp:Button ID="btnSubmit" runat="server" Text="提 交" CssClass="input_button"
            OnClick="btnSubmit_Click" />
        &nbsp;&nbsp;<asp:Button ID="btnRestore" runat="server" Text="恢复原值" CssClass="input_button"
            OnClick="btnRestore_Click" />
    </div>
    </form>
</body>
</html>
