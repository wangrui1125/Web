using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class AddHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rqid = Request["rqid"];
            Session["rqid"] = rqid;
            string Url = "../tmp/MyEdit.aspx?n=edithelper&id="+rqid ;
            Response.Redirect(Url);
        }
    }
}