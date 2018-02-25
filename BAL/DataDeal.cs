using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyQuery.DAL;
using MyQuery.Utils;
using MyQuery.MyControl;

namespace MyQuery.BAL
{
    /// <summary>
    /// 封装Dao数据库操作处理、简化页面控件与数据库交互处理
    /// by 贾世义 2009-1-28
    /// </summary>
    public class DataDeal
    {
        private Dao dao = null;
        private bool IsSysLog = false;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataDeal()
        {
            dao = new Dao();
            IsSysLog = WebHelper.GetIsSysLog();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        public DataDeal(DBType dbtype)
        {
            dao = new Dao(dbtype);
            IsSysLog = WebHelper.GetIsSysLog();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbtype">数据库类型</param>
        /// <param name="connectionString">连接字符串</param>
        public DataDeal(DBType dbtype, string connectionString)
        {
            dao = new Dao(dbtype, connectionString);
            IsSysLog = WebHelper.GetIsSysLog();
        }
        #endregion
        /// <summary>
        /// 返回数据库类型
        /// </summary>
        public DBType Dbtype
        {
            get { return dao.Dbtype; }
        }

        #region 封装Dao数据库操作处理
        /// <summary>
        /// 根据输入的SQL语句得到DataSet对象
        /// </summary>
        /// <param name="sql">SQL语句 多条用;分割</param>
        /// <param name="paramters">参数集合</param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, params IDataParameter[] paramters)
        {
            return dao.GetDataSet(sql, paramters);
        }
        /// <summary>
        /// 根据MySqlParameters对象得到DataSet对象，适用于带参数语句
        /// </summary>
        /// <param name="parameters">自定义Sql及参数列表 </param>
        /// <returns></returns>
        public DataSet GetDataSet(MySqlParameters parameters)
        {
            return dao.GetDataSet(parameters, true);
        }
        /// <summary>
        /// 根据输入的SQL语句获得DataTable
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramters">参数集合</param>
        /// <returns></returns>
        public DataTable GetTable(string sql, params IDataParameter[] paramters)
        {
            return dao.GetDataTable(sql, paramters);
        }
        /// <summary>
        /// 根据MySqlParameters对象得到DataTable，适用于带参数语句
        /// </summary>
        /// <param name="parameters">自定义Sql及参数列表</param>
        /// <returns></returns>
        public DataTable GetTable(MySqlParameters parameters)
        {
            return dao.GetDataTable(parameters, true);
        }
        /// <summary>
        /// 按SQL语句获得指定DataReader对象(一定记得在使用完DataReader后关闭)
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramters">参数集合</param>
        /// <returns>DataReader对象</returns>
        public IDataReader GetDataReader(string sql, params IDataParameter[] paramters)
        {
            return dao.GetDataReader(sql, paramters);
        }
        /// <summary>
        /// 根据MySqlParameters对象得到DataReader对象(一定记得在使用完DataReader后关闭)
        /// </summary>
        /// <param name="parameters">自定义Sql及参数列表</param>
        /// <returns>DataReader对象</returns>
        public IDataReader GetDataReader(MySqlParameters parameters)
        {
            return dao.GetDataReader(parameters, true);
        }
        /// <summary>
        /// 根据输入SQL执行查询，并返回查询结果集中的第一行，第一列的值,忽略其他列和行
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramters">参数集合</param>
        /// <returns>返回第一行第一列的值 空为空格</returns>
        public string GetScalar(string sql, params IDataParameter[] paramters)
        {
            return dao.GetScalar(sql, paramters);
        }
        /// <summary>
        /// 根据MySqlParameters对象执行查询，并返回查询结果集中的第一行，第一列的值,忽略其他列和行
        /// </summary>
        /// <param name="parameters">自定义Sql及参数列表</param>
        /// <returns></returns>
        public string GetScalar(MySqlParameters parameters)
        {
            return dao.GetScalar(parameters, true);
        }
        /// <summary>
        /// 根据传入的SQL在数据库中执行SQl操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paramters">参数集合</param>
        /// <returns></returns>
        public int SqlExecute(string sql, params IDataParameter[] paramters)
        {
            int result = dao.SqlExecute(sql, paramters);
            if (IsSysLog && result > 0)
            {
                new LogDeal().Write("执行操作语句:" + sql);
            }
            return result;
        }
        /// <summary>
        /// 根据MySqlParameters对象在数据库中执行SQl操作
        /// </summary>
        /// <param name="parameters">自定义Sql及参数列表</param>
        /// <returns></returns>
        public int SqlExecute(MySqlParameters parameters)
        {
            int result = dao.SqlExecute(parameters, true);
            if (IsSysLog && result > 0)
            {
                new LogDeal().Write("执行操作语句:" + parameters.GetSql(dao.Dbtype, false));
            }
            return result;
        }
        /// <summary>
        /// 执行一条Sql语句是否出错（insert/update/delete）不再抛出异常
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns>是否出错(判断数据提交是否出错) true表示出现错误</returns>
        public bool SqlExecuteFail(string sql)
        {
            bool isRun = false;
            try
            {
                isRun = dao.SqlExecute(sql) > 0;
            }
            catch
            {
                isRun = false;
            }
            if (IsSysLog && isRun)
            {
                new LogDeal().Write("执行操作语句:" + sql);
            }
            return !isRun;
        }
        /// <summary>
        /// 根据MySqlParameters对象执行一条Sql语句是否出错（insert/update/delete）不再抛出异常
        /// </summary>
        /// <param name="parameters">自定义Sql及参数列表</param>
        /// <returns>是否出错(判断数据提交是否出错) true表示出现错误</returns>
        public bool SqlExecuteFail(MySqlParameters parameters)
        {
            bool isRun = false;
            try
            {
                isRun = dao.SqlExecute(parameters, true) > 0;
            }
            catch
            {
                isRun = false;
            }
            if (IsSysLog && isRun)
            {
                new LogDeal().Write("执行操作语句:" + parameters.GetSql(dao.Dbtype, false));
            }
            return !isRun;
        }
        /// <summary>
        /// 事物批量执行SQL
        /// </summary>
        /// <param name="sqls">SQL语句集合,空语句忽略</param>
        /// <returns></returns>
        public bool SqlExecute(List<string> sqls)
        {
            bool result = dao.SqlExecute(sqls);
            if (IsSysLog && result)
            {
                string sql = "";
                foreach (string s in sqls)
                {
                    if (!String.IsNullOrEmpty(s))
                    {
                        sql += s + ";";
                    }
                }
                new LogDeal().Write("执行操作语句:" + sql);
            }
            return result;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">自定义Sql参数列表 null表示无参数</param>
        /// <returns></returns>
        public string ProcedureExecute(string procName, MySqlParameters parameters)
        {
            if (parameters == null)
            {
                return dao.ProcedureExecute(procName);
            }
            else
            {
                return dao.ProcedureExecute(procName, parameters.GetDataParamters(dao.Dbtype));
            }
        }
        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">自定义Sql参数列表 null表示无参数</param>
        /// <returns></returns>
        public DataSet GetDataSetProcedure(string procName, MySqlParameters parameters)
        {
            if (parameters == null)
            {
                return dao.GetDataSetProcedure(procName);
            }
            else
            {
                return dao.GetDataSetProcedure(procName, parameters.GetDataParamters(dao.Dbtype));
            }
        }
        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">自定义Sql参数列表 null表示无参数</param>
        /// <returns>不存在时为null</returns>
        public DataTable GetDataTableProcedure(string procName, MySqlParameters parameters)
        {
            if (parameters == null)
            {
                return dao.GetDataTableProcedure(procName);
            }
            else
            {
                return dao.GetDataTableProcedure(procName, parameters.GetDataParamters(dao.Dbtype));
            }
        }

        /// <summary>
        /// 获得DataTable
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">字段 多个用,分割</param>
        /// <param name="where">条件表达式(参数用{0}比表示) 不包含where</param>
        /// <param name="value">替换表达式的参数值</param>
        /// <param name="count">获取数量 小于1表示不限制</param>
        /// <returns></returns>
        public DataTable GetTable(string table, string field, string where, string value, int count)
        {
            MySqlParameters mySql = new MySqlParameters(null);
            mySql.Add("p", value, where);
            mySql.Sql = SqlHelper.GetCountSql(dao.Dbtype, "select " + field + " from " + table, count);
            return dao.GetDataTable(mySql, true);
        }
        /// <summary>
        /// 获得DataTable
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="field">字段 多个用,分割</param>
        /// <param name="endSql">from后表达式 可包含join或where或group by或order by 或空</param>
        /// <param name="count">获取数量 小于1表示不限制</param>
        /// <returns></returns>
        public DataTable GetTable(string table, string field, string endSql, int count)
        {
            string sql = "select " + field + " from " + table;
            if (!String.IsNullOrEmpty(endSql))
            {
                sql += endSql;
            }
            return dao.GetDataTable(SqlHelper.GetCountSql(dao.Dbtype, sql, count));
        }

        /// <summary>
        /// 开始执行事务(在事务提交前为一个完整的事务,即使多次开启也是同一事务)
        /// </summary>
        public void TransactionBegin()
        {
            dao.TransactionBegin();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns>是否成功</returns>
        public bool TransactionCommit()
        {
            return dao.TransactionCommit();
        }
        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <returns>是否成功</returns>
        public bool TransactionRollback()
        {
            return dao.TransactionRollback();
        }
        #endregion

        #region SQL库及控件操作（包括列表控件绑定、初始化列表控件等）
        /// <summary>
        /// 读出详细信息赋给相应控件,控件包括Label,TextBox,CheckBox,RichTextBox,UserCtrls,ListControl
        /// </summary>
        /// <param name="selFields">选择字段数组,可以加表名及运算，比如：string[] selFields={"title","content","Derivation","convert(varchar(10),Dates,20) as Dates"};</param>
        /// <param name="ctrls">控件数组与字段数组按顺序一一对应，比如：Control[] ctrls={txtTitle,txtContent,txtDerivation,cdrDates};</param>
        /// <param name="parameters">自定义Sql及参数列表 SQL语句只要表名及条件语句（包含from）</param>
        /// <returns>有无记录</returns>
        public bool Detail(string[] selFields, Control[] ctrls, MySqlParameters parameters)
        {
            bool isRun = false;
            IDataReader dr = GetDataReader(parameters);
            if (dr != null)
            {
                if (dr.Read())
                {
                    isRun = true;
                    for (int i = 0; i < selFields.Length && i < ctrls.Length; i++)
                    {
                        setCtrlValue(ctrls[i], dr[i]);
                    }
                }
                dr.Close();
            }
            return isRun;
        }
        /// <summary>
        /// 读出详细信息赋给相应控件,控件包括Label,TextBox,CheckBox,RichTextBox,UserCtrls,ListControl
        /// </summary>
        /// <param name="selFields">选择字段数组,可以加表名及运算，比如：string[] selFields={"title","content","Derivation","convert(varchar(10),Dates,20) as Dates"};</param>
        /// <param name="ctrls">控件数组与字段数组按顺序一一对应，比如：Control[] ctrls={txtTitle,txtContent,txtDerivation,cdrDates};</param>
        /// <param name="tblNameWhere">SQL语句只要表名及条件语句（不包含from）</param>
        /// <returns>有无记录</returns>
        public bool Detail(string[] selFields, Control[] ctrls, string tblNameWhere)
        {
            bool isRun = false;
            IDataReader dr = dao.GetDataReader("select " + SqlHelper.GetSelect(selFields) + " from " + tblNameWhere);
            if (dr != null)
            {
                if (dr.Read())
                {
                    isRun = true;
                    for (int i = 0; i < selFields.Length && i < ctrls.Length; i++)
                    {
                        setCtrlValue(ctrls[i], dr[i]);
                    }
                }
                dr.Close();
            }
            return isRun;
        }
        /// <summary>
        /// 从查询中读出字段数组详细信息赋给预先定义好的变量数组
        /// </summary>
        /// <param name="selFields">选择字段数组,可以加表名及运算，比如：string[] selFields={"title","content","Derivation","convert(varchar(10),Dates,20) as Dates"};</param>
        /// <param name="vars">变量数组与字段按顺序一一对应,string[] refVariables={"",""}变量数组的个数与字段数相同</param>
        /// <param name="tblNameWhere">SQL语句只要表名及条件语句（不包含from）</param>
        /// <returns>有无记录</returns>
        public bool DetailVar(string[] selFields, string[] vars, string tblNameWhere)
        {
            bool isRun = false;
            IDataReader dr = dao.GetDataReader("select " + SqlHelper.GetSelect(selFields) + " from " + tblNameWhere);
            if (dr != null)
            {
                if (dr.Read())
                {
                    isRun = true;
                    for (int i = 0; i < selFields.Length && i < vars.Length; i++)
                    {
                        if (Convert.IsDBNull(dr[i]))
                        {
                            vars[i] = "";
                        }
                        else
                        {
                            vars[i] = dr[i].ToString();
                        }
                    }
                }
                dr.Close();
            }
            return isRun;
        }
        /// <summary>
        /// 绑定Repeater控件不分页
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="rpt">控件id</param>
        public void BindCtrl(string sql, Repeater rpt)
        {
            DataTable dt = dao.GetDataTable(sql);
            if (dt != null)
            {
                rpt.DataSource = dt;
                rpt.DataBind();
            }
        }
        /// <summary>
        /// 绑定DropDownList，CheckBoxList，RadioButtonList，ListBox控件
        /// </summary>
        /// <param name="sql">SQL语句（至少需1个字段 0列值 1列显示 2列=="1"选择 3列=="1"加(*)标记）</param>
        /// <param name="listCtrl">DropDownList，CheckBoxList，RadioButtonList，ListBox控件id值</param>
        /// <param name="isAll">有否“请选择”</param>
        public void BindListCtrl(string sql, ListControl listCtrl, bool isAll)
        {
            WebHelper.BindListCtrl(dao.GetDataTable(sql), listCtrl, isAll);
        }
        /// <summary>
        /// 绑定DropDownList，CheckBoxList，RadioButtonList，ListBox控件
        /// </summary>
        /// <param name="id">代码ID</param>
        /// <param name="listCtrl">DropDownList，CheckBoxList，RadioButtonList，ListBox控件id值</param>
        /// <param name="isAll">有否“全部”</param>
        public void BindCodeList(string id, ListControl listCtrl, bool isAll)
        {
            WebHelper.BindListCtrl(GetCodeTable(id), listCtrl, isAll);
        }
        /// <summary>
        /// 获得字典数据
        /// </summary>
        /// <param name="id">代码ID</param>
        /// <returns></returns>
        public DataTable GetCodeTable(string id)
        {
            MySqlParameters mySql = new MySqlParameters("f_code");
            mySql.EditSqlMode = SqlMode.Select;
            mySql.Add("code");
            mySql.Add("name");
            mySql.Add("id", id, "iflag=1 and id={0}");
            mySql.SqlEnd = " order by sn";
            return GetTable(mySql);
        }
        /// <summary>
        /// 判断数据是否存在表中 提供的MySqlParameters 需指定name和条件
        /// </summary>
        /// <param name="mySql"></param>
        /// <returns></returns>
        public bool IsExistInTable(MySqlParameters mySql)
        {
            mySql.EditSqlMode = SqlMode.Select;
            mySql.Add("count(*)");
            return !"0".Equals(GetScalar(mySql));
        }
        /// <summary>
        /// 部门列表绑定DropDownList控件
        /// </summary>
        /// <param name="parentID">parentID</param>
        /// <param name="listCtrl">DropDownList</param>
        /// <param name="isAll">有否“全部”</param>
        public void BindDepartList(string parentID, ListControl listCtrl, bool isAll)
        {
            string sql = "select id,name from s_dep where iflag=1";
            if (!String.IsNullOrEmpty(parentID))
            {
                sql += " and parentid='" + parentID + "'";
            }
            sql += " order by parentid,sn";
            BindListCtrl(sql, listCtrl, isAll);
        }

        /// <summary>
        /// 给指定控件赋值(Label,TextBox,CheckBox,RichTextBox,UserCtrls,ListControl,HypeLink)
        /// </summary>
        /// <param name="ctrl">控件ID</param>
        /// <param name="obj">值</param>
        private void setCtrlValue(Control ctrl, object obj)
        {
            if (ctrl is TextBox)
            {
                ((TextBox)ctrl).Text = obj.ToString();
            }
            else if (ctrl is MyDate)
            {
                ((MyDate)ctrl).Text = obj.ToString();
            }
            else if (ctrl is CheckBox)
            {
                ((CheckBox)ctrl).Checked = Constants.TRUE_ID.Equals(obj.ToString());
            }
            else if (ctrl is Label)
            {
                if (DataHelper.IsNullOrEmpty(obj))
                {
                    ((Label)ctrl).Text = "&nbsp;";
                }
                else
                {
                    ((Label)ctrl).Text = obj.ToString();
                }
            }
            else if (ctrl is ListControl)
            {
                WebHelper.SetSelCtrl(obj.ToString(), (ListControl)ctrl);
            }
            else if (ctrl is HyperLink)
            {
                string s = obj.ToString();
                int p = s.IndexOf(" ");
                if (p > 0)
                {
                    ((HyperLink)ctrl).NavigateUrl = s.Substring(0, p);
                    ((HyperLink)ctrl).Text = s.Substring(p + 1);
                }
                else
                {
                    ((HyperLink)ctrl).Text = s;
                }
            }
            else if (ctrl is Image)
            {
                if (obj.ToString() != "")
                {
                    ((Image)ctrl).ImageUrl = @obj.ToString();
                    ctrl.Visible = true;
                }
            }
        }
        /// <summary>
        /// 获得社区名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns>名称</returns>
        public string GetComName(string id)
        {
            return dao.GetScalar("select des from m_com where id='" + id + "'");
        }
        #endregion


    }
}
