using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using MyQuery.Utils;
using MyQuery.Work;

namespace MyQuery.Web.Tmp
{
    public partial class down : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            WebHelper.OutFile(Page, WebHelper.GetFilePath() + QueryString["f"], null);
        }
    }
}
