using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class recommand : System.Web.UI.Page
    {
        protected string Url = null;
        protected string Url1 = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Url1 = "../Tmp/MyQuery.aspx?n=listtechRequireSearch";
            Url = "../Tmp/MyQuery.aspx?n=listtechRequireSearch1";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = TextBox1.Text.ToString();
            string s1 = null;
            if (s != null)
            {
                int len = s.Length;
                s1="%";
                for (int i = 0; i < s.Length; i++)
                {
                    s1 += s[i];
                    s1 += "%";
                }
            }

            if (s1 == null)
            {
                Url1 = "../Tmp/MyQuery.aspx?n=listtechRequireSearch";
                Url = null;
            }
            else
            {
                Url1 = "../Tmp/MyQuery.aspx?n=listtechRequireSearch&Search=" + s1;
                Url = "../Tmp/MyQuery.aspx?n=listtechRequireSearch1";
            }
        }
    }
}