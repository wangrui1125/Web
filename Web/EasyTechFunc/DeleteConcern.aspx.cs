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
    public partial class DeleteConcern : BaseWWW
    {
        string SqlConnectionString;

        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConcernID = Request["ConcernID"];
            string flag = Request["flag"];
            string c = null;
            string news = null;
            string Url = null;
            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;

            }
            else
                m_Connection.Open();
           
            c = string.Format("delete from [dbtest].[dbo].[Concern] where UserID='{0}' and ConcernID='{1}' and flag='{2}'", CurrentUser.Id,ConcernID, flag );
            SqlCommand com = new SqlCommand(c, m_Connection);
            if (Convert.ToInt16(com.ExecuteNonQuery().ToString()) != 0)
            {
                news="取消关注成功！";
                 

            }
            if (Convert.ToInt16(flag) == 1)
            {
                Url = "EnterInfoConcern.aspx";
       
            }
            else if (Convert.ToInt16(flag) == 2)
            {
                Url = "TechRqConcern.aspx";
            }
            else if (Convert.ToInt16(flag) == 3)
            {
                Url = "ResearcherConcern.aspx";
            }
            else if (Convert.ToInt16(flag) == 4)
            {
                Url = "ConsultConcern.aspx";
            }
            else 
            {
                return;
            }

            ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='"+Url+"'</script>"); 
        }
    }
}