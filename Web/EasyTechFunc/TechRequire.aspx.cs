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
    public partial class TechRequire : BaseWWW
    {
        protected string Url = "";
        string SqlConnectionString;

        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            

            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;

            }
            else
                m_Connection.Open();
            string c = string.Format("select role from [dbtest].[dbo].[S_User] where s_User.ID = '{0}'", CurrentUser.Id);
            SqlCommand com = new SqlCommand(c, m_Connection);
          //  int role = Convert.ToInt16(com.ExecuteScalar().ToString());
            int role = Convert.ToInt16(Session["role"]);
            int Do = Convert.ToInt16(Session["Do"]);
            int Review = Convert.ToInt16(Session["Review"]);
            int Admin = Convert.ToInt16(Session["Admin"]);
            int Enterprise = Convert.ToInt16(Session["Enterprise"]);
            int Researcher = Convert.ToInt16(Session["Researcher"]);
            int Consult = Convert.ToInt16(Session["Consult"]);
            int techConsult = Convert.ToInt16(Session["techConsult"]);
            int Manager = Convert.ToInt16(Session["Manager"]);

            if (Admin == 1 || Researcher == 1 || Enterprise == 1)
            {
                Url = "../Tmp/MyQuery.aspx?n=listtechRequire5";
            }
            else
            {
                if (Manager == 1)
                {
                    Url = "../Tmp/MyQuery.aspx?n=listtechRequire6";
                }
                else
                {
                    if (Consult == 1 && techConsult == 0)
                    {
                        Url = "../Tmp/MyQuery.aspx?n=listtechRequire4";
                    }
                    else if (Consult == 0 && techConsult == 1)
                    {
                        Url = "../Tmp/MyQuery.aspx?n=listtechRequire2";
                    }
                    else if (Consult == 1 && techConsult == 1)
                    {
                        Url = "../Tmp/MyQuery.aspx?n=listtechRequire6";
                    }
                    else
                    {
                        Url = "../Tmp/MyQuery.aspx?n=listtechRequire5";
                    }
                }
            }

            //if (Do == 0 && Review == 0)
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire5";
            //}
            //else if (Do == 0 && Review == 1)
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire2";
            //}
            //else if (Do == 1 && Review == 0)
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire4";
            //}
            //else
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire6";
            //}
            //if (role == 2)//技术顾问:方案的创建者
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire2";
            //}
            //else if (role == 3)//企业联系人:企业创建者
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire3";
            //}
            //else if (role == 4)//企业顾问：企业创建者或需求审阅者
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire4";
            //}
            //else if (role == 5)//研究人员：方案的研究人员
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire5";
            //}
            //else if (role == 6)//项目经理：审阅者，处理者
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire6";
            //}
            //else if (role == 7)//管理员
            //{
            //    Url = "../Tmp/MyQuery.aspx?n=listtechRequire";
              


            //}
            //else
            //{
            //    ;
            //}
            Response.Redirect(Url);

        }
    }
}