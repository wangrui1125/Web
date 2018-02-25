using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Text;
using MyQuery.Work;
using MyQuery.Utils;
using MyQuery.MyControl;
using System.Drawing;
using System.Xml.XPath;

namespace MyQuery.Web.Tmp
{
    public partial class EditMyQuery : BasePage
    {
        private XmlDocument doc = null;//当前处理的配置

        #region 初始化数据与展示
        protected void Page_Load(object sender, EventArgs e)
        {
            name = QueryString[Constants.MYQUERY_NAME];
            doc = new XmlDocument();
            if (!Page.IsPostBack && IsAdmin())
            {
                firstLoad();
            }
        }
        /// <summary>
        /// 首次加载使用
        /// </summary>
        private void firstLoad()
        {
            string fileName = WebHelper.GetMyXmlPath() + "\\query\\" + name + Constants.XML_SUFFIX;
            if (File.Exists(fileName))
            {
                try
                {
                    doc.Load(fileName);
                    if (XmlHelper.Exists(doc.DocumentElement, "myquery"))
                    {
                        //开始加载xml
                        txt0.Text = XmlHelper.GetFormatXml(doc);
                    }
                    else
                    {
                        Alert("xml错误:不是本系统的配置文件");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("读取xml错误", ex);
                    RedirectError("读取xml错误:" + ex.Message);
                    return;
                }
                initData(false);
            }
            else
            {
                //创建一个空的XML文档
                Redirect("Tmp/NewMyQuery.aspx?" + Constants.MYQUERY_NAME + "=" + name);
            }
            for (int i = 0; i < dlSeries.Items.Count; i++)
            {
                Control[] ctls = WebHelper.FindControls(dlSeries.Items[i], dlSeries.ID + "_color");
                if (ctls != null && ctls.Length > 0)
                {
                    TextBox txt = ctls[0] as TextBox;
                    if (txt != null && !String.IsNullOrEmpty(txt.Text))
                    {
                        Color? color = DataHelper.GetColor(txt.Text);
                        if (color.HasValue)
                        {
                            txt.ForeColor = color.Value;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 初始化XML配置展示
        /// </summary>
        /// <param name="isAdd">是否新增</param>
        private void initData(bool isAdd)
        {
            if (loadXml())
            {
                int index = 0;
                if (Page.IsPostBack)
                {
                    if (isAdd)
                    {
                        index = rblMyQuery.Items.Count;
                        XmlNode addNode = XmlHelper.SetNode(doc.DocumentElement, "myquery", null, "title", "页卡" + (index + 1));
                        XmlHelper.SetNode(addNode, "from");
                        XmlHelper.SetNode(addNode, "select");
                        XmlHelper.SetNode(addNode, "where");
                        txt0.Text = XmlHelper.GetFormatXml(doc);
                    }
                    else
                    {
                        index = rblMyQuery.SelectedIndex;
                    }
                }
                XmlNodeList myQuerys = XmlHelper.GetNodes(doc.DocumentElement, "myquery");
                rblMyQuery.Items.Clear();
                for (int i = 0; i < myQuerys.Count; i++)
                {
                    string text = XmlHelper.GetTitle(myQuerys[i]);
                    if (String.IsNullOrEmpty(text))
                    {
                        text = "页卡" + (i + 1);
                    }
                    else
                    {
                        text = String.Format(text, "");
                    }
                    ListItem item = new ListItem(text);
                    item.Selected = (i == index);
                    rblMyQuery.Items.Add(item);
                }
                myquery_Del.Enabled = rblMyQuery.Items.Count > 1;
                /*
                for (int i = 2; i < MyTabPage1.Items.Count; i++)
                {
                    MyTabPage1.Items[i].Text = rblMyQuery.SelectedValue + MyTabPage1.Items[i].Text.Substring(MyTabPage1.Items[i].Text.Length - 4);
                }*/
                //functionsql
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "functionsql");
                if (node != null)
                {
                    functionsql.Text = XmlHelper.GetValue(node);
                }
                //javascript
                node = XmlHelper.GetNode(doc.DocumentElement, "javascript");
                if (node != null)
                {
                    javascript.Text = XmlHelper.GetValue(node);
                }
                //buttons
                FromXml.InitButtons(dlButtons, XmlHelper.GetNodes(doc.DocumentElement, "buttons/button"), false);
                //scriptsrc
                FromXml.InitSrcipSrc(XmlHelper.GetNodes(doc.DocumentElement, "scriptsrc/src"), dlScriptsrc, false);
                //myquery
                XmlNode myQuery = myQuerys[index];
                //title
                title.Text = XmlHelper.GetTitle(myQuery);
                node = XmlHelper.GetNode(myQuery, "title");
                if (node != null)
                {
                    titleSql.Text = XmlHelper.GetValue(node);
                    titleParameters.Text = XmlHelper.GetAttribute(node, "parameters");
                }
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "islogicdeal"), islogicdeal);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "isfirstload"), isfirstload);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "allowsorting"), allowsorting);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "allowpaging"), allowpaging);
                pagesize.Text = XmlHelper.GetAttribute(myQuery, "pagesize");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "footervisible"), footervisible);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "issavedata"), issavedata);
                functioncode.Text = XmlHelper.GetAttribute(myQuery, "functioncode");
                list_height.Text = XmlHelper.GetAttribute(myQuery, "height");
                if (list_height.Text.EndsWith("%"))
                {
                    height_unit.SelectedValue = "%";
                }
                list_width.Text = XmlHelper.GetAttribute(myQuery, "width");
                if (list_width.Text.EndsWith("%"))
                {
                    width_unit.SelectedValue = "%";
                }
                fixedcols.Text = XmlHelper.GetAttribute(myQuery, "fixedcols");
                notes.Text = XmlHelper.GetAttribute(myQuery, "notes");
                //updatesqls
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(myQuery, "updatesqls", "isautoid"), isautoid);
                FromXml.InitUpdateSqls(XmlHelper.GetNodes(myQuery, "updatesqls/sql"), dlUpdatesqls, false);
                node = XmlHelper.GetNode(myQuery, "from");
                //from
                WebHelper.SetSelCtrl(XmlHelper.GetType(node), type);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "dbtype"), dbtype);
                connectionstring.Text = XmlHelper.GetAttribute(node, "connectionstring");
                procedurename.Text = XmlHelper.GetAttribute(node, "procedurename");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "method"), method);
                parameters.Text = XmlHelper.GetAttribute(node, "parameters");
                FromXml.InitFrom(XmlHelper.GetNodes(myQuery, "from/table"), dlTables, false, true);
                //group
                group.Text = XmlHelper.GetValue(myQuery, "group");
                //order
                order.Text = XmlHelper.GetValue(myQuery, "order");
                //sql
                FromXml.InitSqls(dlSqls, myQuery, false, false);
                //select
                node = XmlHelper.GetNode(myQuery, "select");
                keycolumnnames.Text = XmlHelper.GetAttribute(node, "keycolumnnames");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "ismultiselect"), ismultiselect);
                if ("true".Equals(ismultiselect.SelectedValue))
                {
                    WebHelper.SetSelCtrl("false", issingleselect);
                }
                else
                {
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "issingleselect"), issingleselect);
                }
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isshowrownum"), isshowrownum);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "sortdirection"), sortdirection);
                mergecolumns.Text = XmlHelper.GetAttribute(node, "mergecolumns");
                parentcolumnname.Text = XmlHelper.GetAttribute(node, "parentcolumnname");
                childcolumnname.Text = XmlHelper.GetAttribute(node, "childcolumnname");
                parentchilddisplay.Text = XmlHelper.GetAttribute(node, "parentchilddisplay");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isallopenchilds"), isallopenchilds);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "iscrosstable"), iscrosstable);
                //select/colgroup
                FromXml.InitColGroup(dlSelectCol, node, false);
                //select/javascript
                selectjavascript.Text = XmlHelper.GetValue(node, "javascript");
                //select/column
                FromXml.InitColumn(dlSelectColumn, node, false);
                //where
                node = XmlHelper.GetNode(myQuery, "where");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isparameter"), isparameter);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isshowappend"), isshowappend);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "issession"), issession);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isquerybutton"), isquerybutton);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isquerysubject"), isquerysubject);
                //where/append
                if (node != null)
                {
                    FromXml.InitAppend(XmlHelper.GetNodes(node, "append"), dlAppends, false);
                }
                //where/colgroup
                FromXml.InitColGroup(dlWhereCols, node, false);
                //where/column
                FromXml.InitColumn(dlWheres, node, false);
                //buttons
                node = XmlHelper.GetNode(myQuery, "buttons");
                if (node != null)
                {
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "position"), position);
                    ifunctioncalc.Text = XmlHelper.GetAttribute(node, "ifunctioncalc");
                    buttonsnotes.Text = XmlHelper.GetAttribute(node, "notes");
                    FromXml.InitButtons(dlButton, XmlHelper.GetNodes(node, "button"), false);
                }
                //addrows
                FromXml.InitAddRows(dlAddrows, XmlHelper.GetNodes(myQuery, "addrows/row"), false);
                //chart
                node = XmlHelper.GetNode(myQuery, "chart");
                if (node != null)
                {
                    chart_name.Text = XmlHelper.GetName(node);
                    chart_width.Text = XmlHelper.GetAttribute(node, "width");
                    chart_height.Text = XmlHelper.GetAttribute(node, "height");
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "template"), chart_template);
                    //chart/titles
                    FromXml.InitChartTitle(dlTitles, XmlHelper.GetNodes(node, "titles/title"), false);
                    //chart/areas
                    initChartArea(false);
                    //chart/areas/area/series
                    FromXml.InitChartSeries(dlSeries, XmlHelper.GetNode(node, "areas/area/series"), false);
                }
                if (MyTabPage1.SelectedIndex > 0)
                {
                    WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
                }
            }
        }
        /// <summary>
        /// 初始化图形区域配置展示
        /// </summary>
        /// <param name="isAdd"></param>
        private void initChartArea(bool isAdd)
        {
            XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/chart/areas/area");
            if (node != null)
            {
                area_name.Text = XmlHelper.GetName(node);
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isshow3d"), area_isshow3d);
                if ("true".Equals(area_isshow3d.SelectedValue))
                {
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isclustered"), area_isclustered);
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isrightangleaxes"), area_isrightangleaxes);
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "lightstyle"), area_lightstyle);
                    area_rotation.Text = XmlHelper.GetAttribute(node, "rotation");
                    area_inclination.Text = XmlHelper.GetAttribute(node, "inclination");
                    area_pointdepth.Text = XmlHelper.GetAttribute(node, "pointdepth");
                    area_pointgapdepth.Text = XmlHelper.GetAttribute(node, "pointgapdepth");
                }
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "xvaluetype"), area_xvaluetype);
                area_xdataformat.Text = XmlHelper.GetAttribute(node, "xdataformat");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "yvaluetype"), area_yvaluetype);
                area_ydataformat.Text = XmlHelper.GetAttribute(node, "ydataformat");
                WebHelper.SetSelCtrl(XmlHelper.GetAttribute(node, "isdatatable"), area_isdatatable);
                area_rows.Text = XmlHelper.GetAttribute(node, "rows");
                area_xfield.Text = XmlHelper.GetAttribute(node, "xfield");
                area_xinterval.Text = XmlHelper.GetAttribute(node, "xinterval");
                area_yinterval.Text = XmlHelper.GetAttribute(node, "yinterval");
                area_xminimum.Text = XmlHelper.GetAttribute(node, "xminimum");
                area_yminimum.Text = XmlHelper.GetAttribute(node, "yminimum");
                XmlNode nodeLegend = XmlHelper.GetNode(node, "legend");
                if (nodeLegend != null)
                {
                    legend_name.Text = XmlHelper.GetName(nodeLegend);
                    legend_title.Text = XmlHelper.GetAttribute(nodeLegend, "title");
                    WebHelper.SetSelCtrl(XmlHelper.GetAttribute(nodeLegend, "docking"), legend_docking);
                }
                XmlNode nodeDatasource = XmlHelper.GetNode(node, "datasource");
                if (nodeDatasource != null)
                {
                    area_datasource.Text = XmlHelper.GetValue(nodeDatasource);
                    datasource_paranames.Text = XmlHelper.GetAttribute(nodeDatasource, "paranames");
                }
            }
        }

        #endregion

        #region 应用 保存 处理
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (MyTabPage1.SelectedIndex == 0)
            {
                initData(false);
            }
            else if (loadXml())
            {
                string err = null;
                string errMsg = "";
                XmlNode node = null;
                XmlNode myQuery = getMyQuery();
                DataFrom dataFrom = new DataFrom(myQuery, CurrentUser, QueryString);
                switch (MyTabPage1.SelectedIndex)
                {
                    case 1:
                        //functionsql
                        if (String.IsNullOrEmpty(functionsql.Text))
                        {
                            XmlHelper.RemoveNode(doc.DocumentElement, "functionsql");
                        }
                        else
                        {
                            XmlHelper.SetNode(doc.DocumentElement, "functionsql", functionsql.Text);
                        }
                        //javascript
                        if (String.IsNullOrEmpty(javascript.Text))
                        {
                            XmlHelper.RemoveNode(doc.DocumentElement, "javascript");
                        }
                        else
                        {
                            XmlHelper.SetNode(doc.DocumentElement, "javascript", javascript.Text);
                        }
                        errMsg += FromXml.SetButtonsXml(dlButtons, doc.DocumentElement, dataFrom, functionsql.Text);
                        node = XmlHelper.GetNode(doc.DocumentElement, "scriptsrc");
                        if (dlScriptsrc.Items.Count == 0)
                        {
                            if (node != null)
                            {
                                doc.DocumentElement.RemoveChild(node);
                            }
                        }
                        else
                        {
                            if (node == null)
                            {
                                node = XmlHelper.SetNode(doc.DocumentElement, "scriptsrc");
                            }
                            errMsg += FromXml.SetScriptSrcXml(node, dlScriptsrc);
                        }
                        break;
                    case 2:
                        //基本属性
                        if (String.IsNullOrEmpty(title.Text))
                        {
                            errMsg += title.ToolTip + "；\n";
                        }
                        else
                        {
                            XmlHelper.SetAttribute(myQuery, "title", title.Text);
                        }
                        XmlNode titleNode = XmlHelper.GetNode(myQuery, "title");
                        if (String.IsNullOrEmpty(titleSql.Text))
                        {
                            if (titleNode != null)
                            {
                                myQuery.RemoveChild(titleNode);
                            }
                        }
                        else
                        {
                            err = FromXml.IsRightSql(String.Format(titleSql.Text, "0", "0", "0", "0", "0"), dataFrom);
                            if (!String.IsNullOrEmpty(err))
                            {
                                errMsg += titleSql.ToolTip + "；\n";
                            }
                            if (titleNode == null)
                            {
                                titleNode = XmlHelper.SetNode(myQuery, "title");
                            }
                            XmlHelper.SetInnerText(titleNode, titleSql.Text);
                            XmlHelper.SetAttribute(titleNode, "parameters", titleParameters.Text);
                        }
                        XmlHelper.SetAttribute(myQuery, "islogicdeal", islogicdeal.SelectedValue);
                        XmlHelper.SetAttribute(myQuery, "isfirstload", isfirstload.SelectedValue);
                        XmlHelper.SetAttribute(myQuery, "allowsorting", allowsorting.SelectedValue);
                        XmlHelper.SetAttribute(myQuery, "allowpaging", allowpaging.SelectedValue);
                        XmlHelper.SetAttribute(myQuery, "pagesize", pagesize.Text);
                        XmlHelper.SetAttribute(myQuery, "footervisible", footervisible.SelectedValue);
                        XmlHelper.SetAttribute(myQuery, "issavedata", issavedata.SelectedValue);
                        if (!String.IsNullOrEmpty(functioncode.Text))
                        {
                            if (String.IsNullOrEmpty(functionsql.Text))
                            {
                                errMsg += "仅当配置判断是否授权给了用户的SQL时功能代码配置才有效；\n";
                            }
                            else
                            {
                                err = FromXml.ExitsFunction(functionsql.Text, functioncode.Text, dataFrom);
                                if (!String.IsNullOrEmpty(err))
                                {
                                    errMsg += "功能代码" + functioncode.Text + err + "；\n";
                                }
                            }
                        }
                        XmlHelper.SetAttribute(myQuery, "functioncode", functioncode.Text);
                        if (String.IsNullOrEmpty(list_height.Text))
                        {
                            XmlHelper.RemoveAttribute(myQuery, "height");
                        }
                        else
                        {
                            XmlHelper.SetAttribute(myQuery, "height", list_height.Text + height_unit.SelectedValue);
                        }
                        if (String.IsNullOrEmpty(list_width.Text))
                        {
                            XmlHelper.RemoveAttribute(myQuery, "width");
                        }
                        else
                        {
                            XmlHelper.SetAttribute(myQuery, "width", list_width.Text + width_unit.SelectedValue);
                        }
                        XmlHelper.SetAttribute(myQuery, "fixedcols", fixedcols.Text);
                        XmlHelper.SetAttribute(myQuery, "notes", notes.Text);

                        node = XmlHelper.GetNode(myQuery, "updatesqls");
                        if (dlUpdatesqls.Items.Count == 0)
                        {
                            if (node != null)
                            {
                                myQuery.RemoveChild(node);
                            }
                        }
                        else
                        {
                            if (node == null)
                            {
                                node = XmlHelper.SetNode(myQuery, "updatesqls");
                            }
                            XmlHelper.SetAttribute(node, "isautoid", isautoid.SelectedValue);
                            errMsg += FromXml.SetUpdateSqlXml(dlUpdatesqls, node);
                        }
                        break;
                    case 3:
                        node = XmlHelper.GetNode(myQuery, "from");
                        if (node == null)
                        {
                            node = XmlHelper.SetNode(myQuery, "from");
                        }
                        XmlHelper.SetAttribute(node, "type", type.SelectedValue);
                        if ("table".Equals(type.SelectedValue) || "procedure".Equals(type.SelectedValue))
                        {
                            XmlHelper.SetAttribute(node, "dbtype", dbtype.SelectedValue);
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(connectionstring.Text))
                            {
                                errMsg += "当数据来自Url链接或数据来自文件时文件链接不能为空；\n";
                            }
                            else
                            {
                                if ("urlxml".Equals(type.SelectedValue))
                                {
                                    if (!connectionstring.Text.StartsWith("http://", StringComparison.CurrentCultureIgnoreCase)
                                        && !connectionstring.Text.StartsWith("ftp://", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        errMsg += "当数据来自Url链接时文件链接必须为http或ftp的访问地址；\n";
                                    }
                                }
                                else
                                {
                                    switch (type.SelectedValue.ToLower())
                                    {
                                        case "xml":
                                            if (!connectionstring.Text.EndsWith(Constants.XML_SUFFIX, StringComparison.CurrentCultureIgnoreCase))
                                            {
                                                errMsg += "当数据来自xml文件时文件必须为.xml类型文件；\n";
                                            }
                                            break;
                                        case "txt":
                                            if (!TxtHelper.IsTxtFile(connectionstring.Text))
                                            {
                                                errMsg += "当数据来自txt文件时文件必须为.txt类型文件；\n";
                                            }
                                            break;
                                        case "xls":
                                            if (!ExcelHelper.IsExcelFile(connectionstring.Text))
                                            {
                                                errMsg += "当数据来自Excel文件时文件必须为Excel的普通类型或Excel的XML文件；\n";
                                            }
                                            break;
                                    }
                                    foreach (string filename in DataHelper.GetStrings(connectionstring.Text))
                                    {
                                        if (!File.Exists(filename))
                                        {
                                            errMsg += "当数据来自文件时文件" + filename + "必须在web服务器所在机器上存在；\n";
                                        }
                                    }
                                }
                            }
                            if ("urlxml".Equals(type.SelectedValue))
                            {
                                XmlHelper.SetAttribute(node, "method", method.SelectedValue);
                                XmlHelper.SetAttribute(node, "parameters", parameters.Text);
                            }
                        }
                        XmlHelper.SetAttribute(node, "connectionstring", connectionstring.Text);
                        //table
                        string sql;
                        errMsg += FromXml.SetFromTableXml(node, dlTables, out sql);
                        //获得数据来源
                        if (!String.IsNullOrEmpty(sql))
                        {
                            try
                            {
                                dataFrom.GetScalar("select count(*) from " + sql);
                            }
                            catch (Exception ex)
                            {
                                errMsg += "测试访问表出错:" + ex.Message + "；\n";
                            }
                        }
                        if ("procedure".Equals(type.SelectedValue))
                        {
                            if (String.IsNullOrEmpty(procedurename.Text))
                            {
                                errMsg += "数据来源存储过程时，存储过程不能为空；\n";
                            }
                            else
                            {
                                try
                                {
                                    if ("0".Equals(dataFrom.GetScalar("select count(*) from sysobjects where name='" + procedurename.Text + "' and type='p'")))
                                    {
                                        errMsg += "数据来源存储过程时，检查存储过程不存在；\n";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errMsg += "测试访问出错:" + ex.Message + "；\n";
                                }
                            }
                            XmlHelper.SetAttribute(node, "procedurename", procedurename.Text);
                        }
                        else if ("urlxml".Equals(type.SelectedValue))
                        {
                            err = FromXml.IsRightUrl(connectionstring.Text, method.SelectedValue, dataFrom.GetParams());
                            if (!String.IsNullOrEmpty(err))
                            {
                                errMsg += err + "；\n";
                            }
                        }
                        else
                        {
                            foreach (string filename in DataHelper.GetStrings(connectionstring.Text))
                            {
                                DataSet ds = null;
                                try
                                {
                                    switch (type.SelectedValue.ToLower())
                                    {
                                        case "xls":
                                            ds = ExcelHelper.GetDataFromExcel(filename, !"false".Equals(procedurename.Text));
                                            break;
                                        case "xml":
                                            ds = XmlHelper.GetDataFromXml(filename);
                                            break;
                                        case "txt":
                                            char cSplit = '\t';
                                            if (!String.IsNullOrEmpty(parameters.Text))
                                            {
                                                cSplit = parameters.Text.ToCharArray()[0];
                                            }

                                            ds = TxtHelper.GetDataFromTxt(filename, "GBK", cSplit, true);
                                            break;
                                    }
                                    if (ds == null)
                                    {
                                        errMsg += "测试访问文件" + filename + "未获取到数据；\n";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errMsg += "测试访问文件" + filename + "出错:" + ex.Message + "；\n";
                                }
                            }
                        }
                        //group
                        if (String.IsNullOrEmpty(group.Text))
                        {
                            node = XmlHelper.GetNode(myQuery, "group");
                            if (node != null)
                            {
                                myQuery.RemoveChild(node);
                            }
                        }
                        else
                        {
                            err = FromXml.IsRightSql(myQuery, group.Text, true, dataFrom, QueryString["t"], type.SelectedValue, "false".Equals(issavedata.SelectedValue));
                            if (String.IsNullOrEmpty(err))
                            {
                                XmlHelper.SetNode(myQuery, "group", group.Text);
                            }
                            else
                            {
                                errMsg += "分组配置" + err + "；\n";
                            }
                        }
                        //order
                        if (String.IsNullOrEmpty(order.Text))
                        {
                            if ("table".Equals(type.SelectedValue) && "false".Equals(islogicdeal.SelectedValue))
                            {
                                errMsg += "数据来源表时并且物理处理时排序配置不能为空；\n";
                            }
                            else
                            {
                                node = XmlHelper.GetNode(myQuery, "order");
                                if (node != null)
                                {
                                    myQuery.RemoveChild(node);
                                }
                            }
                        }
                        else
                        {
                            if ("table".Equals(type.SelectedValue) && "false".Equals(islogicdeal.SelectedValue))
                            {
                                err = FromXml.IsRightSql("select count(*) from " + sql + " order by " + order.Text, dataFrom);
                            }
                            else
                            {
                                err = FromXml.ExitsColumn(myQuery, order.Text);
                            }
                            if (String.IsNullOrEmpty(err))
                            {
                                XmlHelper.SetNode(myQuery, "order", order.Text);
                            }
                            else
                            {
                                errMsg += "默认排序配置" + err + "；\n";
                            }
                        }
                        //sql
                        errMsg += FromXml.SetSqlXml(dlSqls, myQuery, dataFrom, false);
                        break;
                    case 4:
                        node = XmlHelper.GetNode(myQuery, "select");
                        if (node == null)
                        {
                            node = XmlHelper.SetNode(myQuery, "select");
                        }
                        err = FromXml.ExitsColumn(myQuery, keycolumnnames.Text);
                        if (!String.IsNullOrEmpty(err))
                        {
                            errMsg += "主键字段中" + err + "；\n";
                        }
                        XmlHelper.SetAttribute(node, "keycolumnnames", keycolumnnames.Text);
                        XmlHelper.SetAttribute(node, "ismultiselect", ismultiselect.SelectedValue);
                        if ("true".Equals(ismultiselect.SelectedValue))
                        {
                            WebHelper.SetSelCtrl("false", issingleselect);
                        }
                        XmlHelper.SetAttribute(node, "issingleselect", issingleselect.SelectedValue);

                        XmlHelper.SetAttribute(node, "isshowrownum", isshowrownum.SelectedValue);
                        err = FromXml.ExitsColumn(myQuery, mergecolumns.Text);
                        if (!String.IsNullOrEmpty(err))
                        {
                            errMsg += "行合并列中" + err + "；\n";
                        }
                        XmlHelper.SetAttribute(node, "mergecolumns", mergecolumns.Text);
                        XmlHelper.SetAttribute(node, "sortdirection", sortdirection.SelectedValue);
                        err = FromXml.ExitsColumn(myQuery, keycolumnnames.Text);
                        if (!String.IsNullOrEmpty(err))
                        {
                            errMsg += "分组主键列中" + err + "；\n";
                        }
                        XmlHelper.SetAttribute(node, "parentcolumnname", parentcolumnname.Text);
                        err = FromXml.ExitsColumn(myQuery, childcolumnname.Text);
                        if (!String.IsNullOrEmpty(err))
                        {
                            errMsg += "子行上级列中" + err + "；\n";
                        }
                        XmlHelper.SetAttribute(node, "childcolumnname", childcolumnname.Text);
                        err = FromXml.ExitsColumn(myQuery, parentchilddisplay.Text);
                        if (!String.IsNullOrEmpty(err))
                        {
                            errMsg += "关系展示列中" + err + "；\n";
                        }
                        XmlHelper.SetAttribute(node, "parentchilddisplay", parentchilddisplay.Text);
                        XmlHelper.SetAttribute(node, "isallopenchilds", isallopenchilds.SelectedValue);
                        XmlHelper.SetAttribute(node, "iscrosstable", iscrosstable.SelectedValue);
                        //select/colgroup
                        errMsg += FromXml.SetColGroupXml(dlSelectCol, node, "0".Equals(pagesize.Text));
                        //select/column
                        try
                        {
                            errMsg += FromXml.SetColumnXml(dlSelectColumn, node, dataFrom, QueryString["t"], dbtype.SelectedValue, "false".Equals(issavedata.SelectedValue));
                        }
                        catch (XPathException exx)
                        {                            
                        }
                        XmlNode nodeJavascript = XmlHelper.GetNode(node, "javascript");
                        if (String.IsNullOrEmpty(selectjavascript.Text))
                        {
                            if (nodeJavascript != null)
                            {
                                node.RemoveChild(nodeJavascript);
                            }
                        }
                        else
                        {
                            if (nodeJavascript == null)
                            {
                                nodeJavascript = XmlHelper.SetNode(node, "javascript");
                            }
                            XmlHelper.SetInnerText(nodeJavascript, selectjavascript.Text);
                        }
                        break;
                    case 5:
                        node = XmlHelper.GetNode(myQuery, "where");
                        if (node == null)
                        {
                            node = XmlHelper.SetNode(myQuery, "where");
                        }
                        XmlHelper.SetAttribute(node, "isparameter", isparameter.SelectedValue);
                        XmlHelper.SetAttribute(node, "isshowappend", isshowappend.SelectedValue);
                        XmlHelper.SetAttribute(node, "issession", issession.SelectedValue);
                        XmlHelper.SetAttribute(node, "isquerybutton", isquerybutton.SelectedValue);
                        XmlHelper.SetAttribute(node, "isquerysubject", isquerysubject.SelectedValue);

                        //append
                        errMsg += FromXml.SetAppendXml(node, dlAppends, dataFrom, QueryString["t"], dbtype.SelectedValue, "false".Equals(issavedata.SelectedValue));
                        //colgroup
                        errMsg += FromXml.SetColGroupXml(dlWhereCols, XmlHelper.GetNode(node, "colgroup"), true);
                        //column
                        errMsg += FromXml.SetColumnXml(dlWheres, node, dataFrom, QueryString["t"], dbtype.SelectedValue, "false".Equals(issavedata.SelectedValue));
                        break;
                    case 6:
                        node = XmlHelper.GetNode(myQuery, "buttons");
                        if (node == null)
                        {
                            node = XmlHelper.SetNode(myQuery, "buttons");
                        }
                        XmlHelper.SetAttribute(node, "position", position.SelectedValue);
                        if (String.IsNullOrEmpty(ifunctioncalc.Text))
                        {
                            XmlHelper.RemoveAttribute(node, "ifunctioncalc");
                        }
                        else
                        {
                            err = ReflectionDeal.IsFunctionCalc(ifunctioncalc.Text);
                            if (String.IsNullOrEmpty(err))
                            {
                                XmlHelper.SetAttribute(node, "ifunctioncalc", ifunctioncalc.Text);
                            }
                            else
                            {
                                errMsg += "按钮权限处理类" + err;
                            }
                        }
                        XmlHelper.SetAttribute(node, "notes", buttonsnotes.Text);
                        errMsg += FromXml.SetButtonsXml(dlButton, myQuery, dataFrom, functionsql.Text);
                        //addrows
                        node = XmlHelper.GetNode(myQuery, "addrows");
                        if (dlAddrows.Items.Count == 0)
                        {
                            if (node != null)
                            {
                                myQuery.RemoveChild(node);
                            }
                        }
                        else if (node == null)
                        {
                            node = XmlHelper.SetNode(myQuery, "addrows");
                        }
                        errMsg += FromXml.SetAddRowXml(node, dlAddrows);
                        break;
                    case 7:
                        node = XmlHelper.GetNode(myQuery, "chart");
                        if (String.IsNullOrEmpty(chart_name.Text))
                        {
                            if (node != null)
                            {
                                myQuery.RemoveChild(node);
                            }
                        }
                        else
                        {
                            if (node == null)
                            {
                                node = XmlHelper.SetNode(myQuery, "chart");
                            }
                            if ("0".Equals(pagesize.Text))
                            {
                                if (!XmlHelper.Exists(myQuery, "select/column[@name='" + chart_name.Text + "']"))
                                {
                                    errMsg += "详述时必须配置存放图形的列；\n";
                                }
                            }
                            XmlHelper.SetAttribute(node, "name", chart_name.Text);
                            int width = DataHelper.GetIntValue(chart_width.Text, 0);
                            if (width == 0)
                            {
                                errMsg += "图形的宽度必须设置；\n";
                            }
                            else
                            {
                                XmlHelper.SetAttribute(node, "width", chart_width.Text);
                            }
                            width = DataHelper.GetIntValue(chart_height.Text, 0);
                            if (width == 0)
                            {
                                errMsg += "图形的高度必须设置；\n";
                            }
                            else
                            {
                                XmlHelper.SetAttribute(node, "height", chart_height.Text);
                            }
                            XmlHelper.SetAttribute(node, "template", chart_template.Text);
                            //titles
                            XmlNode nodeTitles = XmlHelper.GetNode(node, "titles");
                            if (dlTitles.Items.Count == 0)
                            {
                                if (nodeTitles != null)
                                {
                                    node.RemoveChild(nodeTitles);
                                }
                            }
                            else if (nodeTitles == null)
                            {
                                nodeTitles = XmlHelper.SetNode(node, "addrows");
                            }
                            errMsg += FromXml.SetChartTitleXml(nodeTitles, dlTitles);
                            //area
                            XmlNode nodeArea = XmlHelper.GetNode(node, "areas");
                            if (nodeArea == null)
                            {
                                nodeArea = XmlHelper.SetNode(node, "areas");
                            }
                            nodeArea = XmlHelper.GetNode(nodeArea, "area");
                            if (nodeArea == null)
                            {
                                nodeArea = XmlHelper.SetNode(myQuery, "chart/areas/area");
                            }
                            XmlHelper.SetAttribute(nodeArea, "name", area_name.Text);
                            XmlHelper.SetAttribute(nodeArea, "isshow3d", area_isshow3d.SelectedValue);
                            if ("true".Equals(area_isshow3d.SelectedValue))
                            {
                                XmlHelper.SetAttribute(nodeArea, "isclustered", area_isclustered.SelectedValue);
                                XmlHelper.SetAttribute(nodeArea, "isrightangleaxes", area_isrightangleaxes.SelectedValue);
                                XmlHelper.SetAttribute(nodeArea, "lightstyle", area_lightstyle.SelectedValue);
                                XmlHelper.SetAttribute(nodeArea, "rotation", area_rotation.Text);
                                XmlHelper.SetAttribute(nodeArea, "inclination", area_inclination.Text);
                                XmlHelper.SetAttribute(nodeArea, "pointdepth", area_pointdepth.Text);
                                XmlHelper.SetAttribute(nodeArea, "pointgapdepth", area_pointgapdepth.Text);
                            }
                            else
                            {
                                XmlHelper.RemoveAttribute(nodeArea, "isclustered");
                                XmlHelper.RemoveAttribute(nodeArea, "isrightangleaxes");
                                XmlHelper.RemoveAttribute(nodeArea, "lightstyle");
                                XmlHelper.RemoveAttribute(nodeArea, "rotation");
                                XmlHelper.RemoveAttribute(nodeArea, "inclination");
                                XmlHelper.RemoveAttribute(nodeArea, "pointdepth");
                                XmlHelper.RemoveAttribute(nodeArea, "pointgapdepth");
                            }
                            XmlHelper.SetAttribute(nodeArea, "xvaluetype", area_xvaluetype.SelectedValue);
                            XmlHelper.SetAttribute(nodeArea, "xdataformat", area_xdataformat.Text);
                            XmlHelper.SetAttribute(nodeArea, "xvaluetype", area_yvaluetype.SelectedValue);
                            XmlHelper.SetAttribute(nodeArea, "ydataformat", area_ydataformat.Text);
                            XmlHelper.SetAttribute(nodeArea, "isdatatable", area_isdatatable.SelectedValue);
                            XmlHelper.SetAttribute(nodeArea, "rows", area_rows.Text);
                            if (String.IsNullOrEmpty(area_xfield.Text))
                            {
                                errMsg += "分析图形的横坐标字段不能为空；\n";
                            }
                            else
                            {
                                if (String.IsNullOrEmpty(area_datasource.Text))
                                {
                                    err = FromXml.ExitsColumn(myQuery, area_xfield.Text);
                                    if (String.IsNullOrEmpty(err))
                                    {
                                        errMsg += "非自定义数据源时分析图形的横坐标字段必须在展示列集合中配置；\n";
                                    }
                                }
                                XmlHelper.SetAttribute(nodeArea, "xfield", area_xfield.Text);
                            }
                            XmlHelper.SetAttribute(nodeArea, "xinterval", area_xinterval.Text);
                            XmlHelper.SetAttribute(nodeArea, "yinterval", area_yinterval.Text);
                            XmlHelper.SetAttribute(nodeArea, "xminimum", area_xminimum.Text);
                            XmlHelper.SetAttribute(nodeArea, "yminimum", area_yminimum.Text);
                            //legend
                            XmlNode nodeLegend = XmlHelper.GetNode(nodeArea, "legend");
                            if (String.IsNullOrEmpty(legend_name.Text))
                            {
                                if (nodeLegend != null)
                                {
                                    nodeArea.RemoveChild(nodeLegend);
                                }
                            }
                            else
                            {
                                if (nodeLegend == null)
                                {
                                    nodeLegend = XmlHelper.SetNode(nodeArea, "legend");
                                }
                                XmlHelper.SetAttribute(nodeLegend, "name", legend_name.Text);
                                XmlHelper.SetAttribute(nodeLegend, "title", legend_title.Text);
                                XmlHelper.SetAttribute(nodeLegend, "docking", legend_docking.SelectedValue);
                            }
                            //datasource
                            XmlNode nodeDatasource = XmlHelper.GetNode(nodeArea, "datasource");
                            if (String.IsNullOrEmpty(area_datasource.Text))
                            {
                                if (nodeDatasource != null)
                                {
                                    nodeArea.RemoveChild(nodeDatasource);
                                }
                            }
                            else
                            {

                                err = FromXml.IsRightColumnSelects(area_datasource.Text, datasource_paranames.Text, dataFrom);
                                if (String.IsNullOrEmpty(err))
                                {
                                    if (nodeDatasource == null)
                                    {
                                        nodeDatasource = XmlHelper.SetNode(nodeArea, "datasource", area_datasource.Text);
                                    }
                                }
                                else
                                {
                                    errMsg += area_datasource.Text + "；\n";
                                }
                                XmlHelper.SetAttribute(nodeDatasource, "paranames", datasource_paranames.Text);
                            }
                            //series
                            XmlNode nodeSeries = XmlHelper.GetNode(nodeArea, "series");
                            if (dlSeries.Items.Count == 0)
                            {
                                if (nodeSeries != null)
                                {
                                    nodeArea.RemoveChild(nodeSeries);
                                }
                            }
                            else if (nodeSeries == null)
                            {
                                nodeSeries = XmlHelper.SetNode(nodeArea, "series");
                            }
                            errMsg += FromXml.SetChartSeriasXml(nodeSeries, dlSeries, area_datasource.Text);
                        }
                        break;

                }
                if (String.IsNullOrEmpty(errMsg))
                {
                    errMsg = "";
                    txt0.Text = XmlHelper.GetFormatXml(doc);
                }
                else
                {
                    errMsg = "window.alert('" + WebHelper.GetSafeScript(errMsg) + "');";
                }
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ");" + errMsg);
            }
        }

        /// <summary>
        /// 获得当前处理配置根节点myquery
        /// </summary>
        /// <returns></returns>
        private XmlNode getMyQuery()
        {
            return XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string fileName = WebHelper.GetMyXmlPath() + "\\query\\" + name + Constants.XML_SUFFIX;
            try
            {
                doc.LoadXml(txt0.Text);
                if (XmlHelper.Exists(doc.DocumentElement, "myquery"))
                {
                    doc.Save(fileName);
                    Alert("配置保存成功，涉及配置的页面需要重新进入查看调整的结果");
                }
                else
                {
                    Alert("配置不能保存:不是本系统的配置文件");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("配置保存失败", ex);
                RedirectError("配置保存失败。原因:" + ex.Message);
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            MyTabPage1.SelectedIndex = 0;
            firstLoad();
        }

        #endregion

        #region 校验相关
        /// <summary>
        /// 校验是否有语法错误
        /// </summary>
        /// <returns></returns>
        private bool loadXml()
        {
            string err = XmlHelper.CheckXmlContent(txt0.Text);
            if (String.IsNullOrEmpty(err))
            {
                try
                {
                    doc.LoadXml(txt0.Text);
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                }
            }
            if (String.IsNullOrEmpty(err))
            {
                return true;
            }
            else
            {
                WebHelper.Alert(Page, "xml语法错误:" + err);
                return false;
            }
        }
        #endregion

        #region 其它回发按钮处理
        protected void rblMyQuery_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyTabPage1.SelectedIndex = 0;
            initData(false);
        }

        protected void myquery_Add_Click(object sender, EventArgs e)
        {
            MyTabPage1.SelectedIndex = 2;
            if (loadXml())
            {
                initData(true);
            }
        }

        protected void myquery_Del_Click(object sender, EventArgs e)
        {
            MyTabPage1.SelectedIndex = 0;
            if (loadXml())
            {
                XmlNode myquery = getMyQuery();
                doc.DocumentElement.RemoveChild(myquery);
                rblMyQuery.SelectedIndex = 0;
                txt0.Text = XmlHelper.GetFormatXml(doc);
                initData(false);
            }
        }

        protected void dlButtons_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitButtons(dlButtons, XmlHelper.GetNodes(doc.DocumentElement, "buttons/button"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlButtons_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "buttons");
                XmlHelper.RemoveNode(node, "button[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitButtons(dlButtons, XmlHelper.GetNodes(node, "button"), false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlScriptsrc_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitSrcipSrc(XmlHelper.GetNodes(doc.DocumentElement, "scriptsrc/src"), dlScriptsrc, true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlScriptsrc_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "scriptsrc");
                XmlHelper.RemoveNode(node, "src[" + (sender as Button).ToolTip + "]");
                FromXml.InitSrcipSrc(XmlHelper.GetNodes(node, "src"), dlScriptsrc, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlAddrows_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitAddRows(dlAddrows, XmlHelper.GetNodes(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/addrows/row"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlAddrows_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/addrows");
                XmlHelper.RemoveNode(node, "row[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitAddRows(dlAddrows, XmlHelper.GetNodes(node, "row"), false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlUpdatesqls_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitUpdateSqls(XmlHelper.GetNodes(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/updatesqls/sql"), dlUpdatesqls, true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlUpdatesqls_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/updatesqls");
                XmlHelper.RemoveNode(node, "sql[" + (sender as Button).ToolTip + "]");
                FromXml.InitUpdateSqls(XmlHelper.GetNodes(node, "sql"), dlUpdatesqls, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlTables_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitFrom(XmlHelper.GetNodes(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/from/table"), dlTables, true, true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlTables_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/from");
                XmlHelper.RemoveNode(node, "table[" + (sender as Button).ToolTip + "]");
                FromXml.InitFrom(XmlHelper.GetNodes(node, "table"), dlTables, false, true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlSelectCol_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitColGroup(dlSelectCol, XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/select"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlSelectCol_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/select/colgroup");
                XmlHelper.RemoveNode(node, "col[" + (sender as Button).ToolTip + "]");
                FromXml.InitColGroup(dlSelectCol, node.ParentNode, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlSelectColumn_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitColumn(dlSelectColumn, XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/select"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlSelectColumn_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/select");
                XmlHelper.RemoveNode(node, "column[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitColumn(dlSelectColumn, node, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlAppends_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitAppend(XmlHelper.GetNodes(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where/append"), dlAppends, true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlAppends_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where");
                XmlHelper.RemoveNode(node, "append[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitAppend(XmlHelper.GetNodes(node, "append"), dlAppends, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlWhereCols_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitColGroup(dlWhereCols, XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlWhereCols_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where/colgroup");
                XmlHelper.RemoveNode(node, "col[" + (sender as Button).ToolTip + "]");
                FromXml.InitColGroup(dlWhereCols, node.ParentNode, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlWheres_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitColumn(dlWheres, XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where"), false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlWheres_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where");
                XmlHelper.RemoveNode(node, "column[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitColumn(dlWheres, node, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlButton_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitButtons(dlButton, XmlHelper.GetNodes(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where/buttons/button"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlButton_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/where/buttons");
                XmlHelper.RemoveNode(node, "button[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitButtons(dlButton, XmlHelper.GetNodes(node, "button"), false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        protected void dlTitles_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitChartTitle(dlTitles, XmlHelper.GetNodes(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/chart/titles/title"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlTitles_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/chart/titles");
                XmlHelper.RemoveNode(node, "title[" + (sender as Button).ToolTip + "]");
                FromXml.InitChartTitle(dlTitles, XmlHelper.GetNodes(node, "title"), false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlSeries_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitChartSeries(dlSeries, XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/chart/areas/area/series"), true);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlSeries_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = XmlHelper.GetNode(doc.DocumentElement, "myquery[" + (rblMyQuery.SelectedIndex + 1) + "]/chart/areas/area/series");
                XmlHelper.RemoveNode(node, "serie[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitChartSeries(dlSeries, node, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlSqls_Add_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                FromXml.InitSqls(dlSqls, getMyQuery(), true, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }

        protected void dlSqls_Del_Click(object sender, EventArgs e)
        {
            if (loadXml())
            {
                XmlNode node = getMyQuery();
                XmlHelper.RemoveNode(node, "sql[@name='" + (sender as Button).ToolTip + "']");
                FromXml.InitSqls(dlSqls, node, false, false);
                WebHelper.RegisterStartupScript(Page, "showHide", "showDiv(" + MyTabPage1.SelectedIndex + ")");
            }
        }
        #endregion

    }
}
