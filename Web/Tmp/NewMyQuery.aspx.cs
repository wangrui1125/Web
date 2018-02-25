using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Xml;
using MyQuery.MyControl;
using MyQuery.Work;
using MyQuery.Utils;

namespace MyQuery.Web.Tmp
{
    public partial class NewMyQuery : BaseExpand
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(QueryString["fn"]))
            {
                name = QueryString["fn"];
            }
            Template.SetGridViewStyle(MyGridView1);
            FromXml.SetGridViewPage(myQuery, MyGridView1);
            //获得查询及展示的列属性与集合
            FromXml.SetGridViewColumn(myQuery, MyGridView1, dataFrom);
            if (!Page.IsPostBack && IsAdmin())
            {
                if (!WebHelper.GetPageName(Page).Equals(name))
                {
                    type.SelectedIndex = -1;
                    foreach (ListItem item in type.Items)
                    {
                        if (name.StartsWith(item.Value, StringComparison.CurrentCultureIgnoreCase))
                        {
                            item.Selected = true;
                            type.Enabled = false;
                        }
                    }
                }
                WebHelper.SetControlAttributes(btnGet, new TextBoxVal[] { tablename });
            }
        }

        protected void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dataFrom.GetDataTable("select top 1 * from " + tablename.Text);
                if (dt != null)
                {
                    DataTable ds = new DataTable();
                    ds.Columns.Add("name");
                    ds.Columns.Add("des");
                    ds.Columns.Add("isselect");
                    ds.Columns.Add("iswhere");
                    foreach (DataColumn dc in dt.Columns)
                    {
                        DataRow dr = ds.NewRow();
                        dr["name"] = dc.ColumnName;
                        dr["des"] = dc.Caption;
                        ds.Rows.Add(dr);
                    }
                    MyGridView1.DataSource = ds;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("表" + tablename.Text, ex);
                RedirectError("获取表" + tablename.Text + "字段失败。原因:" + ex.Message);
                return;
            }
            WebHelper.SetControlAttributes(btnSave, new TextBoxVal[] { tablename, title });
            MyGridView1.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //创建一个空的XML文档
            XmlDocument doc = XmlHelper.GetXmlDocument(true);
            XmlNode myquery = XmlHelper.GetNode(doc.DocumentElement, "myquery");
            XmlHelper.SetAttribute(myquery, "title", title.Text);
            XmlNode node = XmlHelper.SetNode(myquery, "from");
            XmlHelper.SetAttribute(node, "type", "table");
            XmlHelper.SetNode(node, "table", tablename.Text, "title", title.Text);
            node = XmlHelper.SetNode(myquery, "select");
            if (type.SelectedValue.Equals("edit") || type.SelectedValue.Equals("detail"))
            {
                XmlHelper.SetAttribute(myquery, "pagesize", "0");
                XmlNode colgroup = XmlHelper.SetNode(node, "colgroup");
                XmlHelper.SetNode(colgroup, "col", null, "width", "100px");
                XmlHelper.SetNode(colgroup, "col");
            }
            else
            {
                if (type.SelectedValue.Equals("list"))
                {
                    XmlHelper.SetAttribute(myquery, "isfirstload", "true");
                }
            }
            DataTable dt = (DataTable)MyGridView1.DataSource;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string keyValue = dr["name"].ToString();
                    if (!String.IsNullOrEmpty(Page.Request.Form["isselect" + keyValue]))
                    {
                        XmlNode column = XmlHelper.SetNode(node, "column");
                        XmlHelper.SetAttribute(column, "name", keyValue);
                        XmlHelper.SetAttribute(column, "title", Page.Request.Form["des" + keyValue]);
                        if (type.SelectedValue.Equals("list") || type.SelectedValue.Equals("query"))
                        {
                            XmlHelper.SetInnerText(column, tablename.Text + "." + keyValue);
                        }
                    }
                }
            }
            if (type.SelectedValue.Equals("list") || type.SelectedValue.Equals("query"))
            {
                node = XmlHelper.SetNode(myquery, "where");
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string keyValue = dr["name"].ToString();
                        if (!String.IsNullOrEmpty(Page.Request.Form["iswhere" + keyValue]))
                        {
                            XmlNode column = XmlHelper.SetNode(node, "column");
                            XmlHelper.SetAttribute(column, "name", keyValue);
                            XmlHelper.SetAttribute(column, "title", Page.Request.Form["des" + keyValue]);
                            XmlHelper.SetInnerText(column, "and " + tablename.Text + "." + keyValue + "={0}");
                        }
                    }
                }
            }
            if (WebHelper.GetPageName(Page).Equals(name))
            {
                name = type.SelectedValue + tablename.Text;
            }
            try
            {
                doc.Save(WebHelper.GetMyXmlPath() + "\\query\\" + name + Constants.XML_SUFFIX);
            }
            catch (Exception ex)
            {
                Logger.Error("配置保存失败", ex);
                RedirectError("配置保存失败。原因:" + ex.Message);
                return;
            }
            if (String.IsNullOrEmpty(QueryString["fn"]))
            {
                Redirect("../Tmp/EditMyQuery.aspx?" + Constants.MYQUERY_NAME + "=" + name);
            }
            else
            {
                Redirect("../WorkFlow/EditMyTask.aspx?" + Constants.MYQUERY_NAME + "=" + QueryString["fn"]);
            }
        }

        protected void MyGridView1_CellDataBound(object sender, MyBoundEventArgs e)
        {
            if (e.RowType == DataControlRowType.DataRow)
            {
                if (e.Column.Name.Equals("des"))
                {
                    HtmlGenericControl txt = WebHelper.GetHtmlInput(e.Column.Name + e.KeyValue, e.Column.Value.ToString(), "100%", null);
                    txt.Attributes.Add("title", "请录入字段描述");
                    e.Cell.Controls.Clear();
                    e.Cell.Controls.Add(txt);
                }
                else if (e.Column.Name.Equals("isselect"))
                {
                    HtmlGenericControl chk = WebHelper.GetHtmlCheckBox(e.Column.Name + e.KeyValue, e.Column.Value.ToString(), null);
                    chk.Attributes.Add("title", "选中则作为展示或编辑列");
                    e.Cell.Controls.Clear();
                    e.Cell.Controls.Add(chk);

                }
                else if (e.Column.Name.Equals("iswhere"))
                {
                    HtmlGenericControl chk = WebHelper.GetHtmlCheckBox(e.Column.Name + e.KeyValue, e.Column.Value.ToString(), null);
                    chk.Attributes.Add("title", "选中则作为查询条件列（编辑或详述无效）");
                    e.Cell.Controls.Clear();
                    e.Cell.Controls.Add(chk);
                }
            }
        }
    }
}