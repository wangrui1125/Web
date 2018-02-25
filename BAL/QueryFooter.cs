using System;
using System.Collections.Generic;
using System.Text;
using MyQuery.MyControl;
using System.Data;
using MyQuery.Utils;
using MyQuery.DAL;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对查询模板Foot的列计算接口的实现 
    /// by 贾世义 2009-1-28
    /// </summary>
    public class QueryFooter : IFooterCalc
    {
        #region IFooterCalc Members
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
        private object _dataSource = null;
        /// <summary>
        /// 当前处理的DataTable
        /// </summary>
        public object DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                _dataSource = value;
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
        /// 返回计算列的值
        /// 注意当case多于10个时建议再仿照此类另建一个实现类，配置中配成新实现的类名即可
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            object result = null;
            switch (Name.ToLower())
            {
                #region 系统处理
                case "depbyproc":
                    if ("manager".Equals(Column.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        DataTable dt = DataSource as DataTable;
                        //按照已有数据处理 当分页时获取不到
                        if (dt.Columns.Contains("parentid"))
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (Convert.IsDBNull(dr["parentid"]) || String.IsNullOrEmpty(dr["parentid"].ToString()))
                                {
                                    return "总经理:" + dr["manager"].ToString();
                                }
                            }
                        }
                        else
                        {
                            string sql = "select u.name from s_user u inner join s_dep d on d.id=u.depid where d.parentid is null or d.parentid=''";
                            return "总经理:" + new Dao().GetScalar(sql);
                        }
                    }
                    break;
                #endregion
            }
            return result;
        }

        #endregion
    }
}
