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

namespace MyQuery.Web.Sys
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new DataFrom().SetLogInfo("注销");
            Page.Session.Abandon();
            //Page.Response.Write("<script type='text/javascript'>window.top.opener=null;window.top.open('','_self');window.top.close()</script>");
            Page.Response.Write("<script type='text/javascript'>window.top.location='/login.aspx';</script>");
        }
    }
}
