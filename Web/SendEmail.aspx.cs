using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyQuery.Utils;
using MyQuery.Work;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace MyQuery.Web
{
    class Body
    {
        public string name;
        public string Enp;
        public string requie_t;
        public string requie_d;        
        public string xiaomi;
        public string xiaomitel;
        public string researchers;
        public string emailtoid;
        public string emailtokey;
        public int enpid;
    }
    public partial class SendEmail : BaseWWW
    {
        private Authenticate auth = null;
        protected string randString()
        {
            string str = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ~!@#$%^&*()_+";//75个字符
            Random r = new Random();
            string result = string.Empty;

            //生成一个8位长的随机字符，具体长度可以自己更改
            for (int i = 0; i < 32; i++)
            {
                int m = r.Next(0, 75);//这里下界是0，随机数可以取到，上界应该是75，因为随机数取不到上界，也就是最大74，符合我们的题意
                string s = str.Substring(m, 1);
                result += s;
            }

            return result;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //the informaition need to send
            Body emailtobody=new Body();

           
            string type = Request["type"];
            string smtpServer = "smtp.yeah.net"; //SMTP服务器
            string mailFrom = "kexiaomi@yeah.net"; //登陆用户名
            string userPassword = "kexiaomi8";//登陆密码
          
            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            string c; //string tempstring_aindex;
            SqlCommand m_Command;
            SqlDataReader sdr;
            string mailTo = ""; 
            if (type.Equals("toEnp"))
            {
                string rqId = Request["rqid"];
                string researcherID = Request["researcherId"];
                //确定方案，以邮件方式发送给企业
                // 发送邮件设置 

                c = string.Format("select Enp_ID,Rq_name,Rq_EnpDes,Rq_DoID from TechRequire where Rq_Id={0}", rqId);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                int EnpId = 0; string xiaomiId = String.Empty;
                if (sdr.HasRows)
                {   sdr.Read();
                    EnpId = sdr.GetInt32(sdr.GetOrdinal("Enp_Id"));
                    emailtobody.requie_t=sdr.GetString(sdr.GetOrdinal("Rq_name"));
                    emailtobody.requie_d = sdr.GetString(sdr.GetOrdinal("Rq_EnpDes"));
                    emailtobody.enpid = EnpId;
                    xiaomiId=sdr.GetString(sdr.GetOrdinal("Rq_DoID"));
                }
                sdr.Close();


                //操作人信息
                c = string.Format("select Name,Tel from S_User where ID='{0}'", xiaomiId);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                //int EnpId = 0;
                if (sdr.HasRows)
                {
                    sdr.Read();
                    emailtobody.xiaomi = sdr.GetString(sdr.GetOrdinal("Name"));
                    emailtobody.xiaomitel = sdr.GetString(sdr.GetOrdinal("Tel"));
                }
                sdr.Close(); 
                
                //读取企业名称
                c = string.Format("select Enp_Name from Enterprise where Enp_ID='{0}'", EnpId);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                 if (sdr.HasRows)
                {
                    sdr.Read();
                    emailtobody.Enp = sdr.GetString(sdr.GetOrdinal("Enp_Name"));                    
                }
                sdr.Close();

                //读取企业联系人信息
                c = string.Format("select email,name from S_UserWeb where Enp_ID='{0}'", EnpId);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    emailtobody.name = sdr.GetString(sdr.GetOrdinal("name"));
                    mailTo = sdr.GetString(sdr.GetOrdinal("email"));
                }
                else {
                    sdr.Close();
                    c = string.Format("select name,email from Contact where Enp_Id='{0}'", EnpId);
                    m_Command = new SqlCommand(c, m_Connection);
                    sdr = m_Command.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        emailtobody.name = sdr.GetString(sdr.GetOrdinal("name"));
                        if (mailTo.Length > 0)
                        { mailTo += ";"; }
                        mailTo += sdr.GetString(sdr.GetOrdinal("email"));
                    }                
                }
                sdr.Close();

                //读取研究者信息
                c = string.Format("SELECT name FROM Researcher LEFT JOIN myresearcher ON Researcher.id=myresearcher.fid WHERE rq_id={0}", rqId);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                 if (sdr.HasRows)
                    {
                        sdr.Read();
                         emailtobody.researchers=sdr.GetString(sdr.GetOrdinal("name"));
                    }   
                  
                    sdr.Close();
               
                MatchCollection match = Regex.Matches(mailTo, @"[a-zA-Z0-9_-]+?(\.[a-zA-Z0-9_-]+?)*@([a-zA-Z0-9_-])+?((\.[a-zA-Z0-9_-]{2,3}){1,3})");//(.*?)/</a>/
                
                for (int i = 0; i < match.Count; i++)
                {
                     string temp=match[i].Value.Replace("\\","");
                     byte[] bytes = Encoding.Default.GetBytes(temp);
                     string str = Convert.ToBase64String(bytes);;           
                     string key = str+EnpId+"1"+"KRk7EQMrYsENknAx";
                     emailtobody.emailtoid =str;
                     emailtobody.emailtokey=System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key,"MD5");
                     MailMessage mailMessage=null;
                     //mailMessage = new MailMessage(mailFrom, temp); // 发送人和收件人 
                     mailMessage = new MailMessage(mailFrom, temp);
                     mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                     mailMessage.IsBodyHtml = true;//设置为HTML格式
                     mailMessage.Priority = MailPriority.Low;//优先级
                     mailMessage.Subject = "有研究人员对您的需求感兴趣";//主题
                     string body = "";
                     body = string.Format("<p>{0}:</p>" +
                         "<p>您好, 欢迎来到科小觅 , 贵{1} 提交的需求如下:</p>" +
                         "<p>需求名称：{2}</p>" +
                         "<p>需求内容：{3}</p>" +
                         "<p>由{4}负责协助整理，需要咨询请拨打{5}，并为您推荐适合的研究者{6}，请点击如下链接登录查看:</p>" +
                        "<p><a href='http://www.kexiaomi.com/auth/register?email=" + emailtobody.emailtoid + "&Enp="+emailtobody.enpid+"&type="+1+
                    "&secretKey=" + emailtobody.emailtokey + "'>科小觅登录</a></p>" + "若链接无效，请复制以下网址登录：http://www.kexiaomi.com/auth/register?email=" + emailtobody.emailtoid + "&Enp=" + emailtobody.enpid + 
                    "&type=" + 1 +"&secretKey=" + emailtobody.emailtokey +""
                    , emailtobody.name, emailtobody.Enp, emailtobody.requie_t, emailtobody.requie_d, emailtobody.xiaomi,
                         emailtobody.xiaomitel, emailtobody.researchers);
                    mailMessage.Body=body;
                //内容
                try
                {
                    String news = "发送成功！";
                   
                    smtpClient.Send(mailMessage); // 发送邮件       

                    Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");

                }
                catch (SmtpException ex)
                {
                    String news = "发送失败！";
                    ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>");

                }
            } 
                }               
                
               
            else if (type.Equals("toResearcher"))
            {
                // 发送邮件设置
                //String news=String.Empty;
                Body emailtoResearcher = new Body();
                //emailtoResearcher.


                //查找Researcher邮箱
                string rqId = Request["rqid"];
                string researcherID = Request["researcherId"];
                c = string.Format("select Email,Name from Researcher where ID='{0}'", researcherID);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    mailTo = sdr.GetString(sdr.GetOrdinal("Email"));
                    emailtoResearcher.emailtoid = mailTo;
                    emailtoResearcher.name = sdr.GetString(sdr.GetOrdinal("Name"));
                }
                sdr.Close();
                //查找需求

                c = string.Format("select Rq_Name from TechRequire where Rq_ID='{0}'", rqId);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    emailtoResearcher.requie_t = sdr.GetString(sdr.GetOrdinal("Rq_Name"));                    
                }
                sdr.Close();
                
               
               // Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");

                byte[] bytes = Encoding.Default.GetBytes(mailTo);
                string str = Convert.ToBase64String(bytes);
                string key = str + "2" + "KRk7EQMrYsENknAx";
                //emailtobody.emailtoid = str;
                string emailtokey = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");

                MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
                mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                mailMessage.IsBodyHtml = true;//设置为HTML格式
                mailMessage.Priority = MailPriority.Low;//优先级
                mailMessage.Subject = "欢迎登陆科小觅";//主题
                mailMessage.Body = String.Format("<p>尊敬的'{0}':</p>"+
                    "<p>您好! 欢迎您注册科小觅。科小觅是产业与科研的智能合作平台。为研究者提供精准靠谱的企业合作项目和产业动态，现已为您审核并挑选出与您的研究方向一致的需求'{1}'，企业负责人正在等待您的回应。</p>"+
                    "<p>您的登录邮箱为:{2}。请点击以下链接激活帐号并设置密码:</p>"+
                    "<p><a href='http://www.kexiaomi.com/auth/register?email=" + str + "&type=" + 2 +
                    "&secretKey=" + emailtokey + "'> http://www.kexiaomi.com/auth/register?email=" + str + "&type=" + 2 +
                    "&secretKey=" + emailtokey + "</a></p>"+
                    "<p>如果以上链接无法点击，请将上面的地址复制到你的浏览器(如IE)的地址栏。（该链接7日内有效。）", emailtoResearcher.name, emailtoResearcher.requie_t
                    ,emailtoResearcher.emailtoid);
                try
                {
                    //观察状态
                    c = string.Format("select * from myresearcher where rq_id='{0}' and fid='{1}'", rqId, researcherID);
                    m_Command = new SqlCommand(c, m_Connection);
                    sdr = m_Command.ExecuteReader();
                    Byte flagbyte = 0;
                    int linkid = 0;
                    int sendnumber = 0;
                    DateTime lastSend = DateTime.Now;
                    if (sdr.HasRows)
                    {
                        sdr.Read();
                        linkid = sdr.GetInt32(sdr.GetOrdinal("id"));
                        flagbyte = sdr.GetByte(sdr.GetOrdinal("contact_researcher_status"));
                        sendnumber=sdr.GetInt32(sdr.GetOrdinal("contact_researcher_nums"));
                        try
                        {
                            lastSend = sdr.GetDateTime(sdr.GetOrdinal("last_contact_time"));
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException exc)
                        { }
                    }
                    sdr.Close();
                    int flag = (int)flagbyte;
                    string news = ""; 
                    //如果超过3次
                    if (flag==3)
                    {
                        news = "该研究人员已经发送过通知超过3次，请耐心等待！";
                    }
                    else
                    {   //如果已经发送过
                        if (flag==2)
                        {
                            System.TimeSpan diff = DateTime.Now.Subtract(lastSend);
                            if (diff.TotalMinutes < 2)
                            {
                                news = "发送太快，请间隔五分钟再次发送邀请邮件！";
                            }
                            else
                            {
                                if (sendnumber > 3)
                                { flag = 3; }
                                c = string.Format("update myresearcher set contact_researcher_status={0},contact_researcher_nums='{1}',last_contact_time='{2}' where id='{3}' ",
                                flag, ++sendnumber, DateTime.Now, linkid);
                                m_Command = new SqlCommand(c, m_Connection);
                                m_Command.ExecuteNonQuery();
                                smtpClient.Send(mailMessage); // 发送邮件
                                news = String.Format("你已发送过邮件,这是第{0}次成功发送", sendnumber);
                            }
                          
                         }
                            //如果没有发送过，flag=0 or flag=1
                        else
                        {
                            c = string.Format("update myresearcher set contact_researcher_status={0},contact_researcher_nums='{1}',last_contact_time='{2}' where id='{3}' ",
                              2, sendnumber++, DateTime.Now, linkid);
                            m_Command = new SqlCommand(c, m_Connection);
                            m_Command.ExecuteNonQuery();
                            smtpClient.Send(mailMessage);// 发送邮件
                            //insertLink = false;
                            if (flag == 0)
                            {
                                news = "你选择的研究者，企业未指定" + "&& 发送成功";
                            }
                            else if (flag == 1)
                            {
                                news = "企业已指定，您未发生过" + "&& 发送成功";
                            }
                        }
                    }
                    Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                   // Page page = HttpContext.Current.Handler as Page;
                   // page.ClientScript.RegisterStartupScript(page.GetType(), "message", "var result = window.confirm('" + news + "')");
        //          ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>");
                }
                catch (SmtpException ex)
                {
                    String news = "发送失败！";
                    Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                   // ClientScript.RegisterStartupScript(Page.GetType(), "message", " <script>alert('" + news + "'); window.location.href='TechRqExecute.aspx'</script>");
                }
            }
            else if (type.Equals("forinvited"))
            {
                string ID = Request["id"];
                string EnpId=Request["EnqID"];
                //string researcherID = Request["researcherId"];                
                
                
                // 查找联系人 
                string emailto = String.Empty; string UserID = String.Empty;
                string ContactName=String.Empty;
                c = string.Format("select Name,Email,USerID from Contact where ID={0}", ID);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();               
                if (sdr.HasRows)
                {
                    sdr.Read();
                    emailto = sdr.GetString(sdr.GetOrdinal("Email"));
                    UserID = sdr.GetString(sdr.GetOrdinal("UserID")); 
                    ContactName=sdr.GetString(sdr.GetOrdinal("Name")); 
                }
                sdr.Close();
                if (UserID == String.Empty | UserID=="0")
                {

                    //Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                   if( UserID=="0")                        
                    Response.Write("<script type='text/javascript'>confirm('你已经发送过邀请邮件了，是否继续发送');window.close();</script>");


                   MatchCollection match = Regex.Matches(emailto, @"[a-zA-Z0-9_-]+?(\.[a-zA-Z0-9_-]+?)*@([a-zA-Z0-9_-])+?((\.[a-zA-Z0-9_-]{2,3}){1,3})");//(.*?)/</a>/

                    if (match.Count > 0)
                    {
                        string temp = match[0].Value;
                        byte[] bytes = Encoding.Default.GetBytes(temp);
                        string str = Convert.ToBase64String(bytes); ;
                        string key = str + EnpId + "1" + "KRk7EQMrYsENknAx";
                        //emailtobody.emailtoid = str;
                        string md5key = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5");
                        MailMessage mailMessage = null;
                        //mailMessage = new MailMessage(mailFrom, temp); // 发送人和收件人 
                        mailMessage = new MailMessage(mailFrom, mailFrom);
                        mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
                        mailMessage.IsBodyHtml = true;//设置为HTML格式
                        mailMessage.Priority = MailPriority.Low;//优先级
                        mailMessage.Subject = "欢迎注册科小觅";//主题
                        string body = "";
                        body = string.Format("<p>尊敬的{0}:</p>" +
                            "<p>您好, 感谢你注册科小觅</p>" +
                            "<p>科小觅是产业与科研的智能合作平台。方便的为企业提供包括清华、北大、中国科学院等国内顶尖科技资源。用于在企业的科技需求与各种科技资源间进行智能的发掘沟通，为企业提供知识产权转移、专家顾问推荐、技术分析、科技咨询、技术难题攻关等多种技术增值服务。</p>" +
                            "<p> 你的登录邮箱为{1}。请点击以下链接激活帐号并设置密码：<a href='http://www.kexiaomi.com/auth/register?email=" + str + "&Enp=" + EnpId + "&type=" + 1 +
                       "&secretKey=" + md5key + "'/a>。如果以上链接无法点击，请将上面的地址复制到你的浏览器(如IE)的地址栏。（该链接7日内有效。）</p>" +
                            "<p>科小觅项目组</p>"
                       , ContactName, temp);
                        mailMessage.Body = body; 
                        //内容
                        try
                        {
                            String news = "发送成功！";
                            smtpClient.Send(mailMessage); // 发送邮件  
                            Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");

                        }
                        catch (SmtpException ex)
                        {
                            String news = "发送失败！";
                            Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                        }




                    }
                    else
                    {
                        string news = "邮箱错误！";
                        Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                    }
                }
                else {
                    string news = "该用户已经注册";

                    Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
                }
                 
            }
                m_Connection.Close();

        }
    }
}
