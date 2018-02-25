using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using MyQuery.Utils;
using MyQuery.Work;
namespace MyQuery.Web.EasyTechFunc
{
    public partial class SendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rqId = Request["rqid"];
            string researcherID = Request["researcherId"];
            string type = Request["type"];
            string smtpServer = "smtp.126.com"; //SMTP服务器
            string mailFrom = "easytechnology@126.com"; //登陆用户名
            string userPassword = "kxmsql8!!";//登陆密码

            string techName="";
            string enpName="";
            string techContent="";

            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            string c; string tempstring_aindex;
            SqlCommand m_Command;

            c = string.Format("select state from L_RqTrack where rqId='{0}' and researcherId='{1}'", rqId, researcherID);
            m_Command = new SqlCommand(c, m_Connection);
            SqlDataReader sdr = m_Command.ExecuteReader();
            if (sdr.HasRows)
            {
                sdr.Read();
                int state = sdr.GetInt32(sdr.GetOrdinal("state"));
                if (state > 1)
                {
                    Page page = HttpContext.Current.Handler as Page;
                    page.ClientScript.RegisterStartupScript(page.GetType(),"message","var result = window.confirm('" + "企业已登陆查看过" +"')");
                    return;
                }
            }
            sdr.Close();


            c = string.Format("select Enp_ID,Rq_Name,Rq_EnpDes from TechRequire where Rq_ID='{0}'",rqId);
                       m_Command = new SqlCommand(c, m_Connection);
                        sdr = m_Command.ExecuteReader();
                        int Enp_id = -1;
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            techName = sdr.GetString(sdr.GetOrdinal("Rq_Name"));
                            techContent = sdr.GetString(sdr.GetOrdinal("Rq_EnpDes"));
                            Enp_id = sdr.GetInt32(sdr.GetOrdinal("Enp_ID"));
                        }
                        sdr.Close();
            c = string.Format("select Enp_Name from Enterprise where Enp_ID='{0}'",Enp_id);
                       m_Command = new SqlCommand(c, m_Connection);
                        sdr = m_Command.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            enpName = sdr.GetString(sdr.GetOrdinal("Enp_Name"));
                        }
                        sdr.Close();
                        string researcherDepartment = "";

            

                        c = string.Format("select Department from Researcher where ID='{0}'", researcherID);
                        m_Command = new SqlCommand(c, m_Connection);
                        sdr = m_Command.ExecuteReader();
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            researcherDepartment = sdr.GetString(sdr.GetOrdinal("Department"));
                        }
                        sdr.Close();

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置  

            c = string.Format("select ID,email from Contact where Enp_ID='{0}'", Enp_id);
            m_Command = new SqlCommand(c, m_Connection);
            sdr = m_Command.ExecuteReader();
            string mailTo = "to_wenqiangli@163.com";
            int contactID = 0;
            if (sdr.HasRows)
            {
                sdr.Read();
                mailTo = sdr.GetString(sdr.GetOrdinal("email"));
                contactID = sdr.GetInt32(sdr.GetOrdinal("ID"));
            }
            sdr.Close();


            
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人

            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级
            mailMessage.Subject = "欢迎登陆科小觅";//主题
            mailMessage.Body =
                "您好！\n"+
                "您的需求‘"+techName+"’在‘"+researcherDepartment+"’找到匹配的研究者，点击下面链接查看：<a href='http://www.kexiaomi.com/EnpLogin?type=enp&researcherId=" + researcherID +
    "&rqId=" + rqId + "'>登陆我的科小觅</a>。若上面的链接无法访问，请复制以下链接到浏览器访问：www.kexiaomi.com/EnpLogin?type=enp&researcherId="
    + researcherID + "&rqId=" + rqId+"&contactId="+contactID+"\n"
    +techName+"\n"
    + techContent + "\n（<a href='http://www.kexiaomi.com'>科小觅团队</a>）";

          /*
                mailMessage.Body = "欢迎登陆科小觅查看:<a href='www.kexiaomi.com/ResearcherLogin?researcherId="+researcherID+
                    "&rqId=" + rqId + "'>登陆我的科小觅</a>。若上面的链接无法访问，请复制以下链接到浏览器访问：www.kexiaomi.com/ResearcherLogin?researcherId="
                    + researcherID +"&rqId=" + rqId;//内容
           */

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                String news = "发送成功！";

                c = string.Format("select state from L_RqTrack where rqId='{0}' and researcherId='{1}'", rqId,researcherID);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                bool insertLink = true;
                while (sdr.HasRows)
                {
                    if (!sdr.Read()) break;
                    int state = sdr.GetInt32(sdr.GetOrdinal("state"));
                    if (state > 1)
                    {
                        insertLink = false;
                    }
                }

                if(insertLink)
                {
                    sdr.Close();
                    c = string.Format("insert into L_RqTrack (rqId,researcherId,state,time) values('{0}','{1}','{2}','{3}')", rqId, researcherID, 1, DateTime.Now);
                    m_Command = new SqlCommand(c, m_Connection);
                    m_Command.ExecuteNonQuery();
                }
                sdr.Close();

                ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>"); 

            }
            catch (SmtpException ex)
            {
                String news = "发送失败！";
                ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>"); 

            }

        }
    }
}