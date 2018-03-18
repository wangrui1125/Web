using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyQuery.Work;
using MyQuery.MyControl;
using MyQuery.Utils;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Net;
using System.IO;




namespace MyQuery.Web.EasyTechFunc
{
    public partial class ReplyFeedBack : BaseWWW
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string feedbackid = Request["feedbackid"];

                string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
                SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
                m_Connection.Open();
                string cp = ""; SqlCommand m_Command = null; SqlDataReader sdr = null;

                cp = "select service_id,user_id,create_at,msg from dfg_feedback where id=" + feedbackid;
                m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                string rqid = ""; string userid = "";
                string creat_at = ""; string msg = "";
                if (sdr.Read())
                {
                    rqid = sdr.GetInt32(0).ToString();
                    userid = sdr.GetInt32(1).ToString();
                    creat_at = sdr.GetDateTime(2).ToString();
                    msg = sdr.GetString(3);
                }
                sdr.Close();

                cp = "select Rq_Name from TechRequire where Rq_ID=" + rqid;
                m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                string rqname = "";
                if (sdr.Read())
                {
                    rqname = sdr.GetString(0).ToString();

                }
                sdr.Close();

                cp = "select name from S_UserWeb where id=" + userid;
                m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                string username = "";
                if (sdr.Read())
                {
                    username = sdr.GetString(0).ToString();

                }
                sdr.Close();

                cp = "select msg from dfg_feedback where reply_to_id=" + feedbackid;
                m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                string msgre = "";
                if (sdr.Read())
                {
                    msgre = sdr.GetString(0).ToString();

                }
                sdr.Close();
                m_Connection.Close();

                Label1.Text = "需求名称：" + rqname;
                Label2.Text = creat_at + "：" + username + ":" + msg;
                TextBox1.Text = msgre;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string feedbackid = Request["feedbackid"];
            string msgre = TextBox1.Text.Trim();
            


            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            string cp = ""; SqlCommand m_Command = null; SqlDataReader sdr = null;

            cp = "select id from dfg_feedback where reply_to_id=" + feedbackid;
            m_Command = new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();           
            if (sdr.Read())
            {
                int id = sdr.GetInt32(0);
                cp = string.Format("update dfg_feedback set msg='{0}',create_at='{1}' where id={2}", msgre, DateTime.Now,id);
            }
            else
            {
                cp = string.Format("insert into dfg_feedback (msg,reply_to_id,create_at,replier_id,service_type) values('{0}',{1},'{2}',{3},1)"
                                                           , msgre, feedbackid, DateTime.Now, CurrentUser.Id);
            }
            sdr.Close();
            m_Command = new SqlCommand(cp, m_Connection);
            m_Command.ExecuteNonQuery();
            m_Connection.Close();

            Response.Write("<script>window.open('/Tmp/MyQuery.aspx?n=listfeedback&&type=1','_blank')</script>");
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}