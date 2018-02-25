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
    public partial class Queryenp : BaseWWW
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
            int role = Convert.ToInt16(com.ExecuteScalar().ToString());

            if (role == 2)//技术顾问:方案的创建者
            {
            }
            else if (role == 3)//企业联系人:企业创建者
            {
                Url = "../Tmp/MyQuery.aspx?n=listEnterprise4";
            }
            else if (role == 4)//企业顾问：企业创建者或需求审阅者
            {
                Url = "../Tmp/MyQuery.aspx?n=listEnterprise4";
            }
            else if (role == 5)//研究人员：方案的研究人员
            {
            }
            else if (role == 6)//项目经理：审阅者，处理者
            {
                Url = "../Tmp/MyQuery.aspx?n=listEnterprise6";
             }
            else if (role == 7)//管理员
            {
                Url = "../Tmp/MyQuery.aspx?n=listEnterprise";
            }
            else
            {
                ;
            }
        }

        //protected void Concern_Click(object sender, EventArgs e)
        //{
        //    if (m_Connection.State == System.Data.ConnectionState.Open)
        //    {
        //        ;

        //    }
        //    else
        //        m_Connection.Open();
        //    string c = null;
        //    c = string.Format("Select count(*) from [dbtest].[dbo].[Concern] where UserID='{0}' and ConcernID='{1}' and flag='{2}'", CurrentUser.Id, GridView1.Rows[0].Cells[0].ToString(), '1');
        //    SqlCommand com = new SqlCommand(c, m_Connection);
        //    if (Convert.ToInt16(com.ExecuteScalar().ToString()) == 1)
        //    {
        //        Alert("已关注！");

        //    }
        //    else
        //    {
        //        c = string.Format(" INSERT INTO [dbtest].[dbo].[Concern] VALUES ( '{0}','{1}','{2}') ", CurrentUser.Id, GridView1.Rows[0].Cells[0].Text.ToString(), '1');
        //        com = new SqlCommand(c, m_Connection);
        //        com.ExecuteNonQuery();
        //    }


        //}
    }
}