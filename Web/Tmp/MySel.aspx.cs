using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MyQuery.Work;
using MyQuery.Utils;

namespace MyQuery.Web.Tmp
{
    public partial class MySel : BasePage
    {
        protected string sParams = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            sParams = QueryString[Constants.MYQUERY_NAME];
            if (String.IsNullOrEmpty(sParams))
            {
                Close("必须传入设置的选择查询！");
            }
            else
            {
                if (QueryString.Count>1)
                {
                    for (int i = 1; i < QueryString.Count; i++)
                    {
                        string key = QueryString.Keys[i];
                        sParams += "&" + key + "=" + QueryString[key];
                    }
                }
            }
        }
    }
}
