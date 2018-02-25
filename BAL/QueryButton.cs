using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using MyQuery.MyControl;
using MyQuery.DAL;
using MyQuery.Utils;
using System.IO;
using MyQuery.Work;
using System.Data;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对查询模板按钮接口的实现 
    /// 注意 为了记录按钮操作日志 请使用DataDeal 而不直接实用Dao
    /// by 贾世义 2009-1-27
    /// </summary>
    public class QueryButton : IButtonDeal
    {
        private string _name = null;
        /// <summary>
        /// 对应查询的name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        private MyButton _button = null;
        /// <summary>
        /// 对应的按钮对象
        /// </summary>
        public MyButton Button
        {
            get
            {
                return _button;
            }
            set
            {
                _button = value;
            }
        }
        private string _id = null;
        /// <summary>
        /// 处理的选中行的ID 行的ID有keycolumnnames获得（多个用,分割）多选时多个ID用;分割
        /// </summary>
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private MyColumns _columns = null;
        /// <summary>
        /// 处理的列集合
        /// </summary>
        public MyColumns Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }
        private NameValueCollection _queryString = null;
        /// <summary>
        /// 按钮所在页面的Page.Request.QueryString
        /// </summary>
        public NameValueCollection QueryString
        {
            get
            {
                return _queryString;
            }
            set
            {
                _queryString = value;
            }
        }
        private MyUser _User = null;
        /// <summary>
        /// 当前用户
        /// </summary>
        public MyUser User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
            }
        }
        /// <summary>
        /// 返回运行的脚本则执行之 空或空字符串则忽略
        /// 注意当case多于10个时建议再仿照此类另建一个实现类，配置中配成新实现的类名即可
        /// </summary>
        /// <returns></returns>
        public string Deal()
        {
            string result = null;
            Dao dao = new Dao();
            switch (Name.ToLower())
            {
                case "listuser":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!"admin".Equals(Id) && "0".Equals(dao.GetScalar("select count(*) from loginfo where userid='" + Id + "'")))
                        {
                            if (dao.SqlExecute("delete from s_user where id='" + Id + "';delete from S_UserTransfer where userid='" + Id + "'") > 0)
                            {
                                result = "alert('选择记录删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录删除失败，请稍后重试');";
                            }
                        }
                        else
                        {
                            if (dao.SqlExecute("update s_user set Iflag='0' where id='" + Id + "'") > 0)
                            {
                                result = "alert('选择记录停用成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录停用失败，请稍后重试');";
                            }
                        }
                    }
                    break;
                case "listdep":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if ("0".Equals(dao.GetScalar("select count(*) from s_user where depid='" + Id + "'")))
                        {
                            if (dao.SqlExecute("delete from s_dep where id='" + Id + "'") > 0)
                            {
                                result = "alert('选择记录删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录删除失败，请稍后重试');";
                            }
                        }
                        else
                        {
                            if (dao.SqlExecute("update s_dep set Iflag=0 where id='" + Id + "'") > 0)
                            {
                                result = "alert('选择记录停用成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录停用失败，请稍后重试');";
                            }
                        }
                    }
                    break;
                case "listfun":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if ("0".Equals(dao.GetScalar("select count(*) from s_rolefun where funid=" + Id)))
                        {
                            if (dao.SqlExecute("delete from s_fun where id=" + Id) > 0)
                            {
                                result = "alert('选择记录删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录删除失败，请稍后重试');";
                            }
                        }
                        else
                        {
                            if (dao.SqlExecute("update s_fun set Iflag=0 where id=" + Id) > 0)
                            {
                                result = "alert('选择记录停用成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录停用失败，请稍后重试');";
                            }
                        }
                    }
                    break;
                case "listrole":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if ("0".Equals(dao.GetScalar("select count(*) from s_rolefun where roleid=" + Id)))
                        {
                            if (dao.SqlExecute("delete from s_role where id=" + Id) > 0)
                            {
                                result = "alert('选择记录删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录删除失败，请稍后重试');";
                            }
                        }
                        else
                        {
                            if (dao.SqlExecute("update s_role set Iflag=0 where id=" + Id) > 0)
                            {
                                result = "alert('选择记录停用成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录停用失败，请稍后重试');";
                            }
                        }
                    }
                    break;
                case "listcodelib":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if ("0".Equals(dao.GetScalar("select count(*) from f_code where id='" + Id + "'")))
                        {
                            if (dao.SqlExecute("delete from f_code where id='" + Id + "';delete from f_codelib where id='" + Id + "'") > 0)
                            {
                                result = "alert('选择记录删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录删除失败，请稍后重试');";
                            }
                        }
                        else
                        {
                            if (dao.SqlExecute("update f_codelib set iflag=1 where id='" + Id + "'") > 0)
                            {
                                result = "alert('选择记录停用成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录停用失败，请稍后重试');";
                            }
                        }
                    }
                    break;
                case "listcode":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (dao.SqlExecute("update f_code set iflag=0 where code='" + Id + "' and id='" + QueryString["id"] + "'") > 0)
                        {
                            result = "alert('选择记录停用成功！');";
                        }
                        else
                        {
                            result = "alert('选择记录停用失败，请稍后重试');";
                        }
                    }
                    break;
                case "listinfo":
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if ("0".Equals(dao.GetScalar("select count(*) from infonews where fid=" + Id)))
                        {
                            if (dao.SqlExecute("delete from f_info where id=" + Id) > 0)
                            {
                                result = "alert('选择记录删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录删除失败，请稍后重试');";
                            }
                        }
                        else
                        {
                            if (dao.SqlExecute("update f_info set Iflag=0 where id=" + Id) > 0)
                            {
                                result = "alert('选择记录停用成功！');";
                            }
                            else
                            {
                                result = "alert('选择记录停用失败，请稍后重试');";
                            }
                        }
                    }
                    break;
                case "listhtmltemplate":
                    if ("btnhtml".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        string url = WebHelper.GetAppConfig("GenerateStaticHtml_Url");
                        if (!String.IsNullOrEmpty(url))
                        {
                            if (!url.EndsWith("/"))
                            {
                                url += "/";
                            }
                        }
                        if (MyUser.IsAdmin(_User))
                        {
                            //循环生成所有城市的
                            DataTable dt = dao.GetDataTable("select distinct depid from HtmlTemplate");
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                result = "alert('没有页面需要生成')";
                            }
                            else
                            {
                                result = "";
                                string msg = "";
                                foreach (DataRow dr in dt.Rows)
                                {
                                    msg = getHtmls(url, dr[0].ToString());
                                    result += dr[0].ToString() + "、";
                                }
                                result = "alert('" + result + WebHelper.GetSafeScript(msg) + "')";
                            }
                        }
                        else
                        {
                            result = "alert('" + WebHelper.GetSafeScript(getHtmls(url, _User.DepID)) + "');";
                        }
                    }
                    else if (MyUser.IsAdmin(_User))
                    {
                        if ("btnfind".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                        {
                            int amount = 0;
                            string rootPath = WebHelper.GetMyXmlPath() + "\\tpl";
                            if (Directory.Exists(rootPath))
                            {
                                //获得其下所有.aspx页面
                                DirectoryInfo dir = new DirectoryInfo(rootPath);
                                foreach (FileInfo f in dir.GetFiles("*" + Constants.HTML_SUFFIX))
                                {
                                    MySqlParameters mySql = HtmlGenerate.GetHtmlTemplate(dao, f.Name, _User.Id);
                                    if (mySql != null)
                                    {
                                        amount += dao.SqlExecute(mySql, true);
                                    }
                                }
                            }
                            result = "alert('发现并成功增加" + amount + "个！');";
                        }
                        else if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                        {
                            string filename = WebHelper.GetMyXmlPath() + "\\tpl\\" + Id + Constants.HTML_SUFFIX;
                            File.Delete(filename);
                            File.Delete(filename.Replace("\\tpl\\", "\\query\\").Replace(Constants.HTML_SUFFIX, Constants.XML_SUFFIX));
                            if (dao.SqlExecute("delete from HtmlTemplate where HtmlUrl='" + Id + "'") > 0)
                            {
                                result = "alert('选择HTML模板删除成功！');";
                            }
                            else
                            {
                                result = "alert('选择HTML模板删除失败，请稍后重试');";
                            }
                        }
                    }
                    else
                    {
                        result = "alert('您无权执行此操作！');";
                    }
                    break;
                case "listinfonews":
                    result = "";
                    string ids = SqlHelper.GetSqlInWhere(Id);
                    if ("html".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (dao.SqlExecute("update infonews set iflag=1 where id " + ids) > 0)
                        {
                            result += "选择记录审核成功！";
                        }
                        else
                        {
                            result += "选择记录审核失败";
                        }
                    }
                    else
                    {
                        //非生成则先作废
                        if (dao.SqlExecute("update infonews set iflag=4 where id " + ids) > 0)
                        {
                            if ("cencel".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                result = "选择记录作废成功！";
                            }
                        }
                        else if ("cencel".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                        {
                            result = "选择记录作废失败！";
                        }
                    }
                    //生成
                    string url1 = WebHelper.GetAppConfig("GenerateStaticHtml_Url");
                    if (String.IsNullOrEmpty(url1))
                    {
                        result += HtmlGenerate.GetHtmls(QueryString["fid"], null, "id " + ids, _User);
                    }
                    else
                    {
                        if (!url1.EndsWith("/"))
                        {
                            url1 += "/";
                        }
                        result += WebHelper.WSInvokeMethod(url1 + "WebService.asmx", "WWW", "GetHtml", new object[] { QueryString["fid"], null, "id " + ids, _User.Id }).ToString();
                    }
                    if ("delete".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        List<String> sqls = new List<string>();
                        sqls.Add("delete from InfoAdds where infoid " + ids);
                        sqls.Add("delete from InfoBrowsers where infoid " + ids);
                        sqls.Add("delete from InfoDeals where infoid " + ids);
                        sqls.Add("delete from InfoKeys where infoid " + ids);
                        sqls.Add("update wf_instance set instancestatus=4,notes='信息删除' where processid in (select process from InfoNews where process is not null and id " + ids + ")");
                        sqls.Add("update wf_process set processstatuss=4,notes='信息删除' where processid in (select process from InfoNews where process is not null and id " + ids + ")");
                        sqls.Add("delete from infonews where id " + ids);
                        if (dao.SqlExecute(sqls))
                        {
                            result += "选择记录删除成功！";
                        }
                        else
                        {
                            result += "选择记录删除失败，请稍后重试";
                        }
                    }

                    if (result.Length > 0)
                    {
                        result = "alert('" + result + "');";
                    }
                    break;
                case "querylogimport":
                    if ("deldata".Equals(Button.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        string[] tables = DataHelper.GetStrings(Id);
                        if (tables == null || tables.Length <= 1)
                        {
                            result = "alert('配置错误，请稍后重试');";
                        }
                        else
                        {
                            List<String> sqls = new List<string>();
                            for (int i = 1; i < tables.Length; i++)
                            {
                                sqls.Add("delete from " + tables[i] + " where ImportId=" + tables[0]);
                            }
                            sqls.Add("update LogImport set upcount=0 where id=" + tables[0]);

                            if (dao.SqlExecute(sqls))
                            {
                                result = "alert('删除本次导入数据成功！');";
                            }
                            else
                            {
                                result = "alert('删除本次导入数据失败，请稍后重试');";
                            }
                        }
                    }
                    break;
            }
            return result;
        }
        /// <summary>
        /// 批量生成静态网页
        /// </summary>
        /// <param name="url"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        private string getHtmls(string url, string city)
        {
            if (String.IsNullOrEmpty(url))
            {
                return HtmlGenerate.GetHtmls(city, _queryString);
            }
            else
            {
                return WebHelper.WSInvokeMethod(url + "WebService.asmx", "MyQuery.Web", "GetHtmls", new object[] { city }).ToString();
            }
        }

    }
}
