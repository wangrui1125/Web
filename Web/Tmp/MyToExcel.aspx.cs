using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyQuery.Work;

namespace MyQuery.Web.Tmp
{
    /// <summary>
    /// 导出模板
    /// 参数：n对应的导出定制xml的名字（不含后缀）
    /// s是否提供选择
    /// f对应的查询 当无时导出与查询为同一个配置文件
    /// </summary>
    public partial class MyToExcel : BaseExcel
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PageLoad(btnAdmin, MyInputs1, phContent);
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            OutToExcel(true);
        }

    }
}
