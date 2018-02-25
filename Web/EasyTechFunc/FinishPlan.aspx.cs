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
    public partial class FinishPlan : BaseWWW
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
            c = string.Format("Select Rq_techConsultID,Rq_flag from [dbtest].[dbo].[TechRequire] where Rq_ID='{0}'", ConcernID);
            SqlCommand com = new SqlCommand(c, m_Connection);
            SqlDataReader MyReader = com.ExecuteReader();
            MyReader.Read();
            if (MyReader[0].ToString() != CurrentUser.Id)
            {
                //   Label1.Text = "不是本人处理，无权进行操作！";
                MyReader.Close();
                news = "不是本人处理，无权进行操作！";


            }
            else if (Convert.ToInt16(MyReader[1].ToString()) != 5)
            {
                //   Label1.Text = "需求不是终止状态，无法完成！";
                MyReader.Close();
                news = "需求不是处理中状态，无法完成！";
            }
            else
            {
                MyReader.Close();
                c = string.Format(" Update [dbtest].[dbo].[TechRequire] set Rq_flag=6,Rq_Owner=null,Rq_Time=GETDATE() where Rq_ID=  '{0}'", ConcernID);
                com = new SqlCommand(c, m_Connection);
                com.ExecuteNonQuery();
                // Label1.Text = "方案完成！";
                news = "方案编写完成！";
            }
            ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>");

        }
    }
}