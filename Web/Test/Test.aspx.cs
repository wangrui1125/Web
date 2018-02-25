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
using MyQuery.Utils;
using AjaxControlToolkit;
using System.ComponentModel;
using System.Reflection;
using MyQuery.MyControl;
using System.Xml;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using MyQuery.Work;
using System.Text;

namespace MyQuery.Web.Test
{
    public partial class Test : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(Math.Sin(90 * Math.PI / 180));
        }
    }

}
