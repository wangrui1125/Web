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
    public partial class AddTechManager : BaseWWW
    {
        string SqlConnectionString;

        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConcernID = Request["ConcernID"];
            string Rq_ID=Session ["Rq_ID"].ToString();
            string news = null;
            string c = null;
            string Url = null;
            
            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;

            }
            else
                m_Connection.Open();
            c = string.Format("Update [dbtest].[dbo].[TechRequire] set Rq_ConsultID='{0}' ,Rq_Owner='{0}',Rq_flag=2 where Rq_ID='{1}' ",  ConcernID,Rq_ID);

            SqlCommand com = new SqlCommand(c, m_Connection); 
            com.ExecuteNonQuery();
            news = "选择成功！";
            Url="../EasyTechFunc/TechRequire.aspx";
            ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.open('" + Url + "','_parent')</script>"); 
           
            
        }
    }
}