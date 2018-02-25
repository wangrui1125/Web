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
using System.IO;
using MyQuery.Utils;

namespace MyQuery.Web
{
    public partial class _Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (File.Exists(WebHelper.GetRootServerPath() + "www\\html\\Index.htm"))
            {
                Page.Response.Redirect("www/html/Index.htm");
            }
            else
            {
                Page.Response.Redirect("www/html/Index.aspx");
            }

        }

    }
}
