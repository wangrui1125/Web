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
    /// 针对查询模板列计算接口的实现 
    /// by 贾世义 2009-1-28
    /// </summary>
    public class QueryColumn : IColumnCalc
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
        private object _dealObject = null;
        /// <summary>
        /// 当前处理的数据行
        /// </summary>
        public object DealObject
        {
            get
            {
                return _dealObject;
            }
            set
            {
                _dealObject = value;
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
            if (_column == null)
            {
                //系统处理执行SQL的反射 如无特殊处理请不要修改
                if (_User == null || String.IsNullOrEmpty(_User.Password))
                {
                    return null;
                }
                else
                {
                    return (new Dao()).GetScalar(_User.Password);
                }
            }
            else
            {
                object result = null;
                switch (Name.ToLower())
                {
                    #region 系统处理
                    case "listuser":
                    case "detailuser":
                        if ("brief".Equals(Column.Name, StringComparison.CurrentCultureIgnoreCase))
                        {
                            return ChinaHelper.GetBrief(DataHelper.GetValue(DealObject, "name").ToString());
                        }
                        break;
                    case "depbyxml":
                    case "depbyurl":
                        switch (Column.Name.ToLower())
                        {
                            case "manager":
                                return ((DataTable)DataSource).DataSet.Tables[1].Select("id='" + DataHelper.GetValue(DealObject, "managerid").ToString() + "'")[0]["name"];
                            case "childcount":
                                return ((DataTable)DataSource).Select("parentid='" + DataHelper.GetValue(DealObject, "id").ToString() + "'").Length;
                        }
                        break;
                    #endregion
                }
                return result;
            }
        }

    }
}
