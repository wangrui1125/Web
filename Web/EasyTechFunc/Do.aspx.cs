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
    public partial class Do : BaseWWW
    {
        string SqlConnectionString;
        
        SqlConnection m_Connection;
        protected string Url = "";
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
          //  c = string.Format("select role from [dbtest].[dbo].[S_User] where s_User.ID = '{0}'", CurrentUser.Id);
            SqlCommand com = new SqlCommand(c, m_Connection);
           // int role = Convert.ToInt16(com.ExecuteScalar().ToString());
         

                c = string.Format("Select Rq_ConsultID,Rq_flag from [dbtest].[dbo].[TechRequire] where Rq_ID='{0}'", ConcernID);
                com = new SqlCommand(c, m_Connection);
                SqlDataReader MyReader = com.ExecuteReader();
                MyReader.Read();


                if (Convert.ToInt16(MyReader[1].ToString()) != 4)
                {
                    news = "需求未通过审阅，无法处理！";
                    //Label1.Text = "需求未通过审阅，无法处理！";
                    MyReader.Close();
                    ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='../EasyTechFunc/TechRequire.aspx'</script>"); 

                }
                else
                {
                    MyReader.Close();
                    Session["Rq_ID"] = ConcernID;
                    //c = string.Format("Update [dbtest].[dbo].[TechRequire] set Rq_DoID='{0}',Rq_Owner='{0}',Rq_techConsultID='{0}',Rq_DoTime=GETDATE(),Rq_flag=5 where Rq_ID='{1}' ", CurrentUser.Id, ConcernID);
                    //com = new SqlCommand(c, m_Connection);
                    //com.ExecuteNonQuery();
                    //Url = "../Tmp/MyEdit.aspx?n=edittechConsult&ID=" + ConcernID;
                    Url = "WeChatTest.aspx?n=edittechManager&type=do&id=" + ConcernID;
                    Response.Redirect(Url);
                    // Label1.Text = "处理完成！";
                }
            }
        }
    }
//}