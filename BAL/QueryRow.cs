using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyQuery.MyControl;
using MyQuery.Utils;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对查询模板自定义行计算接口的实现 
    /// by 贾世义 2009-1-28
    /// </summary>
    public class QueryRow : IRowCalc
    {
        #region IRowCalc Members

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
        /// 当前处理的DataSet 当前的DataTable索引为0
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

        private string _rowName = null;
        /// <summary>
        /// 当前处理行的名称
        /// </summary>
        public string RowName
        {
            get
            {
                return _rowName;
            }
            set
            {
                _rowName = value;
            }
        }

        #endregion
        /// <summary>
        /// 返回增加了新处理的行数据的数据源
        /// </summary>
        /// <returns></returns>
        public object Add()
        {
            object result = DataSource;
            switch (Name.ToLower())
            {
                default:
                    //根据行名称进行处理
                    if ("row1".Equals(RowName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        //需要的处理
                    }
                    break;
            }
            return result;
        }
    }
}
