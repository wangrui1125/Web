using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using MyQuery.Utils;
using MyQuery.Work;
using System.Data.SqlClient;
using System.Reflection;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class QuerytechRq1 : BaseWWW
    {
        protected string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentUser.DepID == "4")
            {
                Url = "../Tmp/MyQuery.aspx?n=querytechRequire4&Rq_Type=1&ID=" + CurrentUser.Id;

            }
            else if (CurrentUser.DepID == "3")
            {
                Url = "../Tmp/MyQuery.aspx?n=querytechRequire3&ID=" + CurrentUser.Id + "&Rq_Type=1";
            }
            else if (CurrentUser.DepID == "2" || CurrentUser.DepID == "6" || CurrentUser.DepID == "7")
            {
                Url = "../Tmp/MyQuery.aspx?n=querytechRequire2&Rq_Type=1";

               
            }
           
            else
            {
                return;
            };

        }
    }
}