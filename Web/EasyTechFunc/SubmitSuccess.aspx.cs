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
    public partial class SubmitSuccess : BaseWWW
    {
        string SqlConnectionString;

        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConcernID = Request["ConcernID"];
            string news = null;
            string c = null;
            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;

            }
            else
                m_Connection.Open();
            c = string.Format("Select Rq_ConsultID,Rq_flag from [dbtest].[dbo].[TechRequire] where Rq_ID='{0}'", ConcernID);
            SqlCommand com = new SqlCommand(c, m_Connection);
            SqlDataReader MyReader = com.ExecuteReader();
            MyReader.Read();
            if (MyReader[0].ToString() != CurrentUser.Id)
            {
              //  Label1.Text = "不是本人审阅，无权进行修改！";
                news = "不是本人审阅，无权进行修改！";
                MyReader.Close();


            }
            else if (Convert.ToInt16(MyReader[1].ToString()) != 2)
            {
              //  Label1.Text = "需求不是审阅中状态，无法审阅通过！";
                news = "需求不是审阅中状态，无法审阅通过！";
                MyReader.Close();
            }
            else
            {
                MyReader.Close();
                c = string.Format(" Update [dbtest].[dbo].[TechRequire] set Rq_flag=4,Rq_Owner=null where Rq_ID=  '{0}'", ConcernID);
                com = new SqlCommand(c, m_Connection);
                com.ExecuteNonQuery();
              //  Label1.Text = "审阅通过！";
                news = "审阅通过！";
            }
            ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>"); 

        }
    }
}