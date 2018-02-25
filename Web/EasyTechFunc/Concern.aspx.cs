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
    public partial class Delete : BaseWWW
    {
        string SqlConnectionString;
       
        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)

        {
           
            string ConcernID = Request["ConcernID"];
            string flag=Request ["flag"];
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
            if (Convert.ToInt16(flag) <= 4)
            {
                c = string.Format("Select count(*) from [dbtest].[dbo].[Concern] where UserID='{0}' and ConcernID='{1}' and flag='{2}'", CurrentUser.Id, ConcernID, flag);
                SqlCommand com = new SqlCommand(c, m_Connection);
                if (Convert.ToInt16(com.ExecuteScalar().ToString()) == 1)
                {
                 //   Label1.Text = "已关注！";
                    news = "已关注！";
                }
                else  
                {
                    c = string.Format(" INSERT INTO [dbtest].[dbo].[Concern] VALUES ( '{0}','{1}','{2}') ", CurrentUser.Id, ConcernID, flag);
                    com = new SqlCommand(c, m_Connection);
                    com.ExecuteNonQuery();
                    if (Convert.ToInt16(flag) == 2)
                    {
                        c = string.Format("Select Enp_ID from [dbtest].[dbo].[TechRequire] where Rq_ID='{0}' ",ConcernID);
                        com = new SqlCommand(c, m_Connection);
                        string Enp_ID = com.ExecuteScalar().ToString();
                        c = string.Format("Select count(*) from [dbtest].[dbo].[Concern] where UserID='{0}' and ConcernID='{1}' and flag='{2}'", CurrentUser.Id, Enp_ID, flag);
                        com = new SqlCommand(c, m_Connection);
                        if (Convert.ToInt16(com.ExecuteScalar().ToString()) == 0)
                        {
                           
                        c = string.Format(" INSERT INTO [dbtest].[dbo].[Concern] VALUES ( '{0}','{1}',1) ", CurrentUser.Id, Enp_ID);
                        com = new SqlCommand(c, m_Connection);
                        com.ExecuteNonQuery();

                        }
                       
                    }
                 //   Label1.Text = "关注成功！";
                    news = "关注成功！";
                }
                if (Convert.ToInt16(flag) == 1)
                {
                    Url = "EnterInforExecute.aspx";

                }
                else if (Convert.ToInt16(flag) == 2)
                {

                    Url = "TechRqExecute.aspx";
                }
                else if (Convert.ToInt16(flag) == 3)
                {
                    Url = "ResearcherExecute.aspx";
                }
                else if (Convert.ToInt16(flag) == 4)
                {
                    Url = "ConsultExecute.aspx";
                }
                else
                {
                    Url = "../tmp/MyQuery.aspx?n=listplanResearcher1";
                }

                ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='" + Url + "'</script>"); 
           
            }
            else if(Convert.ToInt16(flag) ==5)
            {
                string rqid = Session ["rqid"].ToString();
                //c = string.Format("Select count(*) from [dbtest].[dbo].[ConcernResearcher] where UserID='{0}' and ConcernID='{1}'", planID, ConcernID);
                c = string.Format("Select count(*) from [dbtest].[dbo].[myresearcher] where rq_id='{0}' and fid='{1}'",rqid, ConcernID);
                SqlCommand com = new SqlCommand(c, m_Connection);
                if (Convert.ToInt16(com.ExecuteScalar().ToString()) == 1)
                {
                   // Label1.Text = "已加入！";
                    news = "已经添加过！";

                }
                else
                {
                    c = string.Format(" INSERT INTO [dbtest].[dbo].[myresearcher](rq_id, fid) VALUES ( '{0}','{1}') ", rqid, ConcernID);
                    com = new SqlCommand(c, m_Connection);
                    com.ExecuteNonQuery();
                    //Label1.Text = "添加成功！";
                    news = "添加成功！";
                }
                Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                //ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "');  window.history.go(-1)'</script>");            
            }
            else if (Convert.ToInt16(flag) == 6)
            { 
                string rqid = Session ["rqid"].ToString();
                c = string.Format("Select count(*) from [dbtest].[dbo].[ConcernHelper] where UserID='{0}' and ConcernID='{1}'", rqid, ConcernID);
                SqlCommand com = new SqlCommand(c, m_Connection);
                if (Convert.ToInt16(com.ExecuteScalar().ToString()) == 1)
                {
                   // Label1.Text = "已加入！";
                    news = "已经添加过！";

                }
                else
                {
                    c = string.Format(" INSERT INTO [dbtest].[dbo].[ConcernHelper] VALUES ( '{0}','{1}','{2}') ", rqid, ConcernID, flag);
                    com = new SqlCommand(c, m_Connection);
                    com.ExecuteNonQuery();
                    //Label1.Text = "添加成功！";
                    news = "添加成功！";
                }
                Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                //ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "');  window.history.go(-1)'</script>"); 
       
            }
            else if (Convert.ToInt16(flag) == 7 || Convert.ToInt16(flag) == 8)
            {
                c = string.Format(" update [dbtest].[dbo].[ConcernHelper] set flag='{0}' where ConcernID='{1}' and UserID='{2}' ", flag,CurrentUser.Id, ConcernID);
                SqlCommand com = new SqlCommand(c, m_Connection);
                com.ExecuteNonQuery();
                news = "操作成功！";
                Url = "../EasyTechFunc/TechRqExecute.aspx?ID="+CurrentUser .Id;
                ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='" + Url + "'</script>"); 
           
         
            }
            else
            {
            }
           
  
        }
    }
}