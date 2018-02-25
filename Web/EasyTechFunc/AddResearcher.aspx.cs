using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class AddResearcher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string planID = Request["id"];
            Session["planID"] = planID;
            string requirid = Request["rqid"];
            Session["rqid"] = requirid;           

          //  string Url = "../tmp/MyQuery.aspx?n=listplanResearcher1";
            string Url = "../tmp/MyEdit.aspx?n=EditplanResearcher&id=" + planID + "&rqid=" + requirid;
            Response.Redirect(Url);
           
        }
    }
}