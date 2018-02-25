using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyQuery.Work;
using MyQuery.Utils;

namespace MyQuery.Web.Tmp
{
    public partial class SelChartType : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string charttype = Page.Request.QueryString["chart"];
                if (!String.IsNullOrEmpty(charttype))
                {
                    WebHelper.SetSelCtrl(charttype.ToLower(), dlCharttype);
                }
            }
        }
    }
}