using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyQuery.WFWork;

namespace MyQuery.BAL
{
    /// <summary>
    /// 自动任务执行类
    /// </summary>
    public class WFFunction:IWFunction
    {
        private WorkFlowDeal _WFDeal = null;
        /// <summary>
        /// 工作流处理对象
        /// </summary>
        public WorkFlowDeal WFDeal
        {
            get
            {
                return _WFDeal;
            }
            set
            {
                _WFDeal = value;
            }
        }
        /// <summary>
        /// 执行处理
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            return true;
        }
    }
}
