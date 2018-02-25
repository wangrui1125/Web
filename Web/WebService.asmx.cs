using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using MyQuery.Utils;
using MyQuery.Work;

namespace MyQuery.Web
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 获得用户名 匹配帐户和姓名
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetUserNameList(string prefixText, int count)
        {
            try
            {
                MySqlParameters mySql = new MySqlParameters("s_user");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " name+'['+id+']'");
                mySql.Add("iflag", Constants.TRUE_ID, "and iflag={0}");
                mySql.Add("id", "%" + prefixText + "%", "and (id like {0} or name like {0})");
                
                
                
                /*
                string depid=this.Context.Request.QueryString["depid"];
                if (!String.IsNullOrEmpty(depid))
                {
                    mySql.Add("depid", depid + '%', "and depid like {0}");
                }*/
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得用户错误", ex);
                return null;
            }
        }
        [WebMethod]
        public string[] GetConsultUserList(string prefixText, int count)
        {
            try
            {
                MySqlParameters mySql = new MySqlParameters("s_user");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " name+'['+id+']'");
                mySql.Add("iflag", Constants.TRUE_ID, "and iflag={0} and role=2");
                mySql.Add("id", "%" + prefixText + "%", "and (id like {0} or name like {0})");
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得用户错误", ex);
                return null;
            }
        }
        [WebMethod]
        public string[] GetManagerUserList(string prefixText, int count)
        {
            try
            {
                MySqlParameters mySql = new MySqlParameters("s_user");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " name+'['+id+']'");
                mySql.Add("iflag", Constants.TRUE_ID, "and iflag={0} and (role=4 or role=6)");
                mySql.Add("id", "%" + prefixText + "%", "and (id like {0} or name like {0})");
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得用户错误", ex);
                return null;
            }
        }
        /// <summary>
        /// 获得标签 匹配代码和名称
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetMarkList(string prefixText, int count)
        {
            try
            {
                MySqlParameters mySql = new MySqlParameters("D_Mark");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " MarkDes+'['+MarkID+']'");
                mySql.Add("id", "%" + prefixText + "%", "(MarkDes like {0} or MarkID like {0})");
                mySql.SqlEnd = "order by MarkDes";
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得标签错误", ex);
                return null;
            }
        }
        /// <summary>
        /// 获得企业 匹配代码和名称
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetEnpList(string prefixText, int count)
        {
            try
            {
                MySqlParameters mySql = new MySqlParameters("Enterprise");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " Enp_Name+'['+cast(Enp_ID as varchar)+']'");
                mySql.Add("id", "%" + prefixText + "%", "Enp_Name like {0}");
                mySql.SqlEnd = "order by Enp_Name";
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得企业错误", ex);
                return null;
            }
        }
        [WebMethod]
        public string[] GetEnpList1(string prefixText, int count)
        {
            try
            {
                //MySqlParameters mySql = new MySqlParameters("Enterprise");
                //mySql.EditSqlMode = SqlMode.Select;
                //mySql.Add("top " + count + " Enp_Name+'['+cast(Enp_ID as varchar)+']'");
                //mySql.Add("id", "%" + prefixText + "%", "Enp_Name like {0}");
                //mySql.SqlEnd = "order by Enp_Name";
                MySqlParameters mySql = new MySqlParameters("Researcher");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " Name+'['+cast(ID as varchar)+']'");
                mySql.Add("id", "%" + prefixText + "%", "Name like {0}");
                mySql.SqlEnd = "order by Name";
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得企业错误", ex);
                return null;
            }
        }
        [WebMethod]
        public string[] GetResearcherList(string prefixText, int count)
        {
            try
            {
                MySqlParameters mySql = new MySqlParameters("Researcher");
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("top " + count + " Name+'['+cast(ID as varchar)+']'");
                mySql.Add("id", "%" + prefixText + "%", "Name like {0}");
                mySql.SqlEnd = "order by Name";
                return DataHelper.GetDataFirstCol(new DataFrom().GetDataTable(mySql));
          
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得企业错误", ex);
                return null;
            }
        }
        /// <summary>
        /// 获得用户和部门信息
        /// </summary>
        /// <returns></returns>
        /// <param name="name"></param>
        [WebMethod]
        public DataSet GetUserDepartInfo(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                name = "%";
            }
            return new DataFrom().GetDataSet("select * from s_dep where name like '" + name + "';select * from s_user");
        }
        /// <summary>
        /// 获得等待任务条数
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [WebMethod]
        public string GetWFWaiting(string userid)
        {
            string result = "0";
            try
            {
                DataTable dt = new DataFrom().GetDataTable(@"select count(*) as sumCount
 ,sum(case when wf_instance.instancestatus=0 then 1 else 0 end) as waitCount
 ,sum(case when wf_instance.instancestatus=1 then 1 else 0 end) as doCount
 from wf_instance 
 inner join wf_process on wf_process.processid=wf_instance.processid
 where (wf_instance.instancestatus=0 or wf_instance.instancestatus=1) and wf_process.processstatus=0 and wf_instance.userid='" + userid + "'");
                if (dt != null && dt.Rows.Count == 1 && Convert.ToInt32(dt.Rows[0]["sumCount"]) > 0)
                {
                    result = dt.Rows[0]["waitCount"].ToString() + "." + dt.Rows[0]["doCount"].ToString();
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("获得等待任务条数错误", ex);
            }
            return result;
        }
        /// <summary>
        /// ajax更新
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="key">主键</param>
        /// <param name="keyvalue">主键值</param>
        /// <returns></returns>
        [WebMethod]
        public string UpdateData(string table, string field, string value, string key, string keyvalue)
        {
            MySqlParameters mySql = new MySqlParameters(table);
            mySql.EditSqlMode = SqlMode.Update;
            mySql.Add(field, DataHelper.GetIDFromBracket(value));
            if (key.IndexOf(Constants.MY_SPLIT) == -1)
            {
                mySql.Add(key, keyvalue, key + "={0}");
            }
            else
            {
                string[] keys = DataHelper.GetStrings(key);
                string[] keyvalues = DataHelper.GetStrings(keyvalue);
                if (keys.Length == keyvalues.Length)
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        mySql.Add(keys[i], keyvalues[i], keys[i] + "={0}");
                    }
                }
                else
                {
                    return "主键值不对应不能修改";
                }
            }
            try
            {
                return new DataFrom().SqlExecute(mySql).ToString();
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("执行更新错误" + SqlHelper.GetSql(mySql, DBType.Sql2005), ex);
                return LanguageResource.Current.PUBLIC_DealError;
            }
        }
    }
}
