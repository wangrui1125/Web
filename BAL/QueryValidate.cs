using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyQuery.MyControl;
using MyQuery.DAL;
using MyQuery.Utils;
using System.Web;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对编辑模板服务器校验接口的实现 
    /// by jiashiyi 2010-05-18
    /// </summary>
    public class QueryValidate : IValidateCalc
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
        private MyColumn _column = null;
        /// <summary>
        /// 当前处理的列
        /// </summary>
        public MyColumn Column
        {
            get
            {
                return _column;
            }
            set
            {
                _column = value;
            }
        }
        private MyColumns _dataColumns = null;
        /// <summary>
        /// 当前处理的数据列的集合
        /// </summary>
        public MyColumns DataColumns
        {
            get
            {
                return _dataColumns;
            }
            set
            {
                _dataColumns = value;
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
        /// 返回未通过验证的提示信息 通过则返回null
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            string result = null;
            switch (_name.ToLower())
            {
                case "editcode":
                    if ("0".Equals(new Dao().GetScalar("select count(*) from f_code where id='" + DataColumns["id"].Value + "' and code='" + Column.Value + "'")))
                    {
                        result = null;
                    }
                    else
                    {
                        result = "编号已经存在不能重复";
                    }
                    break;
                case "editdep":
                case "editwfgroupuser":
                    string userid = null;
                    if (Column.Value != null)
                    {
                        userid = DataHelper.GetIDFromBracket(Column.Value.ToString());
                    }
                    if (String.IsNullOrEmpty(userid))
                    {
                        result = null;
                    }
                    else
                    {
                        Dao dao = new Dao();
                        if ("1".Equals(dao.GetScalar("select count(*) from s_user where id='" + userid + "' and iflag='" + Constants.TRUE_ID + "'")))
                        {
                            if ("editwfgroupuser".Equals(_name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                int id = DataHelper.GetIntValue(DataColumns["id"].Value, 0);
                                if ("0".Equals(dao.GetScalar(String.Format("select count(*) from wf_groupuser where gid={0} and uid='{1}' {2}", DataColumns["gid"].Value, DataColumns["uid"].Value, id == 0 ? "" : String.Format("and id!={0}", id)))))
                                {
                                    result = null;
                                }
                                else
                                {
                                    result = "此成员已经存在，不能重复录入";
                                }
                            }
                            else
                            {
                                result = null;
                            }
                        }
                        else
                        {
                            result = "选择人员必须为在职人员，正确格式为账户或姓名[账户]";
                        }
                    }
                    break;
                case "editmark":
                    string kid = null;
                    if (Column.Value != null)
                    {
                        kid = DataHelper.GetIDFromBracket(Column.Value.ToString());
                    }
                    if (String.IsNullOrEmpty(kid))
                    {
                        result = null;
                    }
                    else
                    {
                        Dao dao = new Dao();
                        if ("0".Equals(dao.GetScalar("select count(*) from d_mark where markid='" + kid + "'")))
                        {
                            result = "输入选择标签必须为系统定义标签，正确格式为描述[编号]";
                        }
                    }
                    break;
                case "edittechrequire":
                    string eid = null;
                    if (Column.Value != null)
                    {
                        eid = DataHelper.GetIDFromBracket(Column.Value.ToString());
                    }
                    if (String.IsNullOrEmpty(eid))
                    {
                        result = null;
                    }
                    else
                    {
                        Dao dao = new Dao();
                        if ("0".Equals(dao.GetScalar("select count(*) from Enterprise where Enp_ID=" + eid)))
                        {
                            result = "输入选择企业，正确格式为名称[编号]";
                        }
                    }
                    break;
            }
            return result;
        }

    }
}
