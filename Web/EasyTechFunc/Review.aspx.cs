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
    public partial class Review : BaseWWW
    {
        string SqlConnectionString;
        string Url = null;
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
          // c = string.Format("select role from [dbtest].[dbo].[S_User] where s_User.ID = '{0}'", CurrentUser.Id);
            SqlCommand com = new SqlCommand(c, m_Connection);
            //int role = Convert.ToInt16(com.ExecuteScalar().ToString());
            //if (role != 4 && role != 6)
            //{
            //    news = "您无权限进行需求的审阅！";
            // //   Label1.Text = "您无权限进行需求的审阅！";
            //}
            //else
            //{
                c = string.Format("Select Rq_CreaterID,Rq_flag from [dbtest].[dbo].[TechRequire] where Rq_ID='{0}'", ConcernID);
                com = new SqlCommand(c, m_Connection);
                SqlDataReader MyReader = com.ExecuteReader();
                MyReader.Read();
                if (Convert.ToInt16(MyReader[1].ToString()) != 1)
                {
                    news = "需求不是“已提交”状态，无法进行审阅！";
                //    Label1.Text = "需求不是提交状态，无法进行审阅！";
                    MyReader.Close();
                    ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='../EasyTechFunc/TechRequire.aspx'</script>"); 
                }
                else
                {
                    MyReader.Close();
                    //c = string.Format(" Update [dbtest].[dbo].[TechRequire] set Rq_flag=2 , Rq_ConsultID='{0}',Rq_Consulttime='{1}' where Rq_ID=  '{2}'", CurrentUser.Id, DateTime.Now.ToString(), ConcernID);
                    //com = new SqlCommand(c, m_Connection);
                    //com.ExecuteNonQuery();
                    //news = "审阅提交成功！";
                    Session["Rq_ID"] = ConcernID;
                    //c = string.Format("Update [dbtest].[dbo].[TechRequire] set Rq_ConsultID='{0}',Rq_Owner='{0}',Rq_Manager='{0}',Rq_ManagerTime=GETDATE(),Rq_flag=2 where Rq_ID='{1}' ", CurrentUser.Id, ConcernID);
                    //com = new SqlCommand(c, m_Connection);
                    //com.ExecuteNonQuery();
                    

                    //原来网页写在XML上,不好修改参数，重新新建一个网页，用asp书写网页代替原有网页
                    //Url = "../Tmp/MyEdit1.aspx?n=edittechManager&ID=" + ConcernID;
                    Url = "WeChatTest.aspx?n=edittechManager&type=review&id=" + ConcernID;
                    Response.Redirect(Url);
                }
      
            }
         
  
        }
    }
//}