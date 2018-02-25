using System;
using System.Collections.Generic;
using System.Text;
using MyQuery.MyControl;
using MyQuery.DAL;
using MyQuery.Utils;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对查询模板按钮的function计算接口的实现 
    /// by 贾世义 2009-1-28
    /// </summary>
    public class QueryFunction : IFunctionCalc
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
        private string _functionCode = null;
        /// <summary>
        /// 按钮对应的FunctionCode
        /// </summary>
        public string FunctionCode
        {
            get
            {
                return _functionCode;
            }
            set
            {
                _functionCode = value;
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
        /// 返回是否有此功能
        /// </summary>
        /// <returns></returns>
        public bool HasFunction()
        {
            if (_User == null)
            {
                return false;
            }
            else if (_User.IsAdmin())
            {
                return true;
            }
            else if (String.IsNullOrEmpty(_User.Functions))
            {
                bool result = false;
                MySqlParameters mySql = new MySqlParameters(SqlMode.Select);
                mySql.Sql = @"select count(*) from s_rolefun rf  
                 inner join s_roleuser ur on ur.roleid=rf.roleid";
                mySql.Add("uid", _User.Id, "ur.uid={0}");
                mySql.Add("funid", _functionCode, " and rf.funid={0}");

                try
                {
                    Dao dao = new Dao();
                    result = !"0".Equals(dao.GetScalar(mySql, true));
                }
                catch
                {
                    result = false;
                }
                return result;
            }
            else
            {
                return DataHelper.InStrings(_User.Functions, _functionCode);
            }
        }

    }
}
