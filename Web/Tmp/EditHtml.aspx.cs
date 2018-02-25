using System;
using System.Collections;
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
using System.Text.RegularExpressions;
using MyQuery.Utils;
using MyQuery.Work;
using MyQuery.MyControl;

namespace MyQuery.Web.Tmp
{
    [AspNetHostingPermission(System.Security.Permissions.SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
    public partial class EditHtml : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            name = QueryString[Constants.MYQUERY_NAME];
            if (!Page.IsPostBack)
            {
                WebHelper.SetControlAttributes(btnSubmit, new TextBoxVal[] { txtTitle, HtmlUrl, AspxUrl });
                if (CurrentUser.IsAdmin())//仅管理员才能操作
                {
                    HtmlUrl.Enabled = true;
                    DepId.Enabled = true;
                    divUp.Visible = true;
                    btnUp.Visible = true;
                    WebHelper.SetAttributesOfFile(fileHtml);
                    WebHelper.SetAttributesOfFile(fileXml);
                }
                else
                {
                    HtmlUrl.Enabled = false;
                    DepId.Enabled = false;
                    divUp.Visible = false;
                    btnUp.Visible = false;
                }
                try
                {
                    DataFrom dataFrom = new DataFrom();
                    dataFrom.BindListCtrl(String.Format("select id,case when parentid='0' then '' else '  ' end+name from f_city where iflag=1 and (parentid='{0}' or id='{0}') order by parentid,id", CurrentUser.DepID), DepId, CurrentUser.IsAdmin());
                    if (!String.IsNullOrEmpty(name))
                    {
                        HtmlUrl.Text = name;
                        DataTable dt = dataFrom.GetDataTable("select Title,AspxUrl,depid from HtmlTemplate where HtmlUrl='" + name + "'");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            txtTitle.Text = dt.Rows[0]["Title"].ToString();
                            AspxUrl.Text = dt.Rows[0]["AspxUrl"].ToString();
                            WebHelper.SetSelCtrl(dt.Rows[0]["depid"].ToString(), DepId);
                        }
                        Content.Text = TxtHelper.GetString(WebHelper.GetMyXmlPath() + "\\tpl\\" + name + Constants.HTML_SUFFIX);

                        XmlDocument doc = FromXml.GetXml(name);
                        if (doc == null)
                        {
                            txtXml.Text = XmlHelper.GetXmlDocument(true).DocumentElement.OuterXml;
                        }
                        else
                        {
                            txtXml.Text = XmlHelper.GetFormatXml(doc);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("初始化失败", ex);
                    RedirectError("初始化失败，请稍候再试。原因:" + ex.Message);
                    return;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Content.Text) || Content.Text.Length < 10)
            {
                Alert("内容不能为空并且必须10个字以上！");
                return;
            }
            XmlDocument doc = null;
            if (txtXml.Text.Length > 0)
            {
                string msg = null;
                doc = new XmlDocument();
                try
                {
                    doc.LoadXml(txtXml.Text);
                    if (!XmlHelper.HasChildNode(doc.DocumentElement, "myquery")
                        || !XmlHelper.HasChildNode(doc.DocumentElement, "myquery/sql"))
                    {
                        msg = "XML配置文件不符合系统规范";
                    }
                }
                catch (Exception ex)
                {
                    msg = "xml语法错误:" + ex.Message;
                }
                if (!String.IsNullOrEmpty(msg))
                {
                    Alert(msg);
                    return;
                }
            }
            bool isOk = false;
            MySqlParameters mySql = new MySqlParameters("HtmlTemplate");
            if (String.IsNullOrEmpty(name))
            {
                mySql.EditSqlMode = SqlMode.Insert;
            }
            else
            {
                mySql.EditSqlMode = SqlMode.Update;
                mySql.Add("whereHtmlUrl", name, "HtmlUrl={0}");
            }
            string htmlUrl = HtmlUrl.Text;
            if (!htmlUrl.EndsWith(DepId.SelectedValue))
            {
                htmlUrl += DepId.SelectedValue;
            }
            mySql.Add("HtmlUrl", htmlUrl);
            string aspxUrl = AspxUrl.Text;
            if (String.IsNullOrEmpty(aspxUrl))
            {
                aspxUrl = HtmlGenerate.GetAspxUrl(htmlUrl);
            }
            mySql.Add("AspxUrl", aspxUrl);
            mySql.Add("Title", txtTitle.Text);
            mySql.Add("depid", DepId.SelectedValue);
            mySql.Add("optime", DateTime.Now);
            mySql.Add("userid", CurrentUser.Id);
            DataFrom data = new DataFrom();
            try
            {
                data.SqlExecute(mySql);
                TxtHelper.WriteToFile(WebHelper.GetMyXmlPath() + "\\tpl\\" + name + Constants.HTML_SUFFIX, Content.Text, true);
                if (doc != null)
                {
                    doc.Save(WebHelper.GetMyXmlPath() + "\\query\\" + name + Constants.XML_SUFFIX);
                }
                isOk = true;
            }
            catch (Exception ex)
            {
                Logger.Error(SqlHelper.GetSql(mySql, data.Dbtype), ex);
                RedirectError("提交失败，请稍候再试。原因:" + ex.Message);
                return;
            }
            if (isOk)
            {
                Redirect("Tmp/MyQuery.aspx?" + Constants.MYQUERY_NAME + "=listHtmlTemplate&sy=1");
            }
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            if (fileHtml.Value.Length > 0)
            {
                int p = fileHtml.Value.IndexOf("\\web\\", StringComparison.CurrentCultureIgnoreCase);
                string fileName = Path.GetFileName(fileHtml.PostedFile.FileName);
                if (p == -1)
                {
                    if (fileName.EndsWith(Constants.HTML_SUFFIX, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //上传htm模板
                        string rootPath = WebHelper.GetMyXmlPath() + "\\tpl\\";
                        try
                        {
                            fileHtml.PostedFile.SaveAs(rootPath + fileName);
                            DataFrom data = new DataFrom();
                            MySqlParameters mySql = HtmlGenerate.GetHtmlTemplate(data, fileName, CurrentUser.Id);
                            if (mySql != null)
                            {
                                data.SqlExecute(mySql);
                            }
                            name = fileName.ToLower().Replace(Constants.HTML_SUFFIX, "");
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("上传HTML模板错误", ex);
                            RedirectError("上传HTML模板失败，请稍后重试。原因:" + ex.Message);
                            return;
                        }
                    }
                    else if (fileName.EndsWith(Constants.XML_SUFFIX, StringComparison.CurrentCultureIgnoreCase))
                    {
                        string rootPath = WebHelper.GetMyXmlPath() + "\\query\\";
                        try
                        {
                            fileHtml.PostedFile.SaveAs(rootPath + fileName);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error("上传XML配置错误", ex);
                            RedirectError("上传XML配置失败，请稍后重试。原因:" + ex.Message);
                            return;
                        }
                    }
                    else
                    {
                        Alert("请选择HTML模板");
                        return;
                    }
                }
                else
                {
                    //上传web 目录文件
                    try
                    {
                        string rootPath = WebHelper.GetRootServerPath() + fileHtml.Value.Substring(p + 5, fileHtml.Value.LastIndexOf("\\") - p - 4);
                        fileHtml.PostedFile.SaveAs(rootPath + fileName);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("上传覆盖错误", ex);
                        RedirectError("上传覆盖失败，请稍后重试。原因:" + ex.Message);
                        return;
                    }
                }
            }
            else
            {
                Alert("请选择HTML模板");
            }
            if (fileXml.Value.Length > 0
                && fileXml.PostedFile.FileName.EndsWith(Constants.XML_SUFFIX, StringComparison.CurrentCultureIgnoreCase)
                && !String.IsNullOrEmpty(name))
            {
                string rootPath = WebHelper.GetMyXmlPath() + "\\query\\";
                try
                {
                    fileXml.PostedFile.SaveAs(rootPath + name + Constants.XML_SUFFIX);
                }
                catch (Exception ex)
                {
                    Logger.Error("上传XML配置错误", ex);
                    RedirectError("上传XML配置失败，请稍后重试。原因:" + ex.Message);
                    return;
                }
            }
            if (!String.IsNullOrEmpty(name))
            {
                Redirect("Tmp/EditHtml.aspx?" + Constants.MYQUERY_NAME + "=" + name + "&sy=1");
            }
        }

    }
}
