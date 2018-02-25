using System;
using System.Collections.Generic;
using System.Text;
using MyQuery.MyControl;
using MyQuery.DAL;
using MyQuery.Utils;
using System.Web;

namespace MyQuery.BAL
{
    /// <summary>
    /// 针对查询模板Foot的列计算接口的实现 
    /// by 贾世义 2009-1-28
    /// </summary>
    public class QueryDefault : IDefaultCalc
    {
        #region IDefaultCalc Members
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
        #endregion

        /// <summary>
        /// 返回有Parameter计算的来的值
        /// 注意当else if多于10个时建议再仿照此类另建一个实现类，配置中配成新实现的类名即可
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        {
            if (String.IsNullOrEmpty(Parameter))
            {
                return null;
            }
            #region 自定义处理
            else if ("你设置的字符".Equals(Parameter, StringComparison.CurrentCultureIgnoreCase))
            {
                return "你获取的默认值";
            }
            #endregion
            else
            {
                return null;
            }
        }

    }
}
