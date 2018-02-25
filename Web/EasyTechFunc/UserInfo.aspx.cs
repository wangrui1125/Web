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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;


namespace MyQuery.Web.EasyTechFunc
{
    public partial class UserInfo : BaseWWW
    {
        protected string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {
               Url = "../Tmp/MyDetail1.aspx?n=detailuserInfo&ID=" + CurrentUser.Id;
        }
    }
}