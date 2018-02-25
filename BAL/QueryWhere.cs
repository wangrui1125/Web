using System;
using System.Collections.Generic;
using System.Text;
using MyQuery.MyControl;
using MyQuery.DAL;
using MyQuery.Utils;
using MyQuery.Work;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对查询模板append中sys类型条件计算接口的实现 
    /// by 贾世义 2009-1-28
    /// </summary>
    public class QueryWhere : IWhereCalc
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
        private string _parameter = null;
        /// <summary>
        /// 配置的参数值字符串
        /// </summary>
        public string Parameter
        {
            get
            {
                return _parameter;
            }
            set
            {
                _parameter = value;
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
        /// 返回替换条件表达式中{0}的值
        /// </summary>
        /// <returns></returns>
        public string[] GetValue()
        {
            string[] result = null;

            DataScope scope = DataScope.Self;
            string userId = String.Empty;
            string userDep = String.Empty;
            if (MyUser.IsAdmin(_User))
            {
                scope = DataScope.All;
            }
            else if (_User != null)
            {
                userId = _User.Id;
                userDep = _User.DepID;
                scope = _User.DataScope;
            }
            switch (Name.ToLower())
            {
                #region 系统处理
                case "listdep":
                case "totaldepeffective":
                case "totalprojectlog":
                    switch (scope)
                    {
                        case DataScope.Self:
                        case DataScope.Dep:
                            result = new string[3];
                            result[0] = "s_dep.id like {0}";
                            result[1] = userDep + "%";
                            result[2] = "所在部门的";
                            break;
                    }
                    break;
                case "listuser":
                case "totalusereffective":
                case "totalunwrite":
                    switch (scope)
                    {
                        case DataScope.Self:
                            result = new string[3];
                            result[0] = "s_user.id={0}";
                            result[1] = userId;
                            result[2] = "自己的";
                            break;
                        case DataScope.Dep:
                            result = new string[3];
                            result[0] = "s_user.DepID like {0}";
                            result[1] = userDep + "%";
                            result[2] = "所在部门的";
                            break;
                    }
                    break;
                case "queryloginfo":
                    switch (scope)
                    {
                        case DataScope.Self:
                            result = new string[3];
                            result[0] = "s_user.id={0}";
                            result[1] = userId;
                            result[2] = "自己的";
                            break;
                        case DataScope.Dep:
                            result = new string[3];
                            result[0] = "s_user.DepID like {0}";
                            result[1] = userDep + "%";
                            result[2] = "所在部门的";
                            break;
                    }
                    break;
                #endregion
            }
            return result;
        }
    }
}
