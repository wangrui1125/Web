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
    public partial class WeChatTest : BaseEdit
    {
        Dictionary<string,string> reviewnames= new Dictionary<string,string>();
        List<string> usersID = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SelectUsernames();
                DropDownList1.DataSource = reviewnames;
                DropDownList1.DataBind();
                string type = Request["type"];
                if (type.Equals("review"))
                {
                    Label1.Text = "请指定审阅人：";
                }
                if (type.Equals("do"))
                {
                    Label1.Text = "请指定编写人：";
                }
                if (type.Equals("exe"))
                {
                    Label1.Text = "请指定执行人：";
                }
            }          

        }

        protected void Button1_Click(object sender, EventArgs e)
        {            
            string type = Request["type"];
            string rqid = Request["id"];
            SelectUsernames();
            //userID
            string selectID =usersID.ElementAt(DropDownList1.SelectedIndex);
            //loguserID
            string name = CurrentUser.Id;
            //时间
            string datenow = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string time = Convert.ToInt64(ts.TotalSeconds).ToString(); 



            //flag
            string cower="";int typeid=0;
            if (type.Equals("review"))
            {
                cower = string.Format("update TechRequire set Rq_Owner='{0}',Rq_ConsultID='{0}',Rq_flag={1}," +
                                           "Rq_Manager='{2}',Rq_ManagerTime='{3}',Rq_Time='{3}'" +
                                           "where Rq_ID={4}", selectID, 2, name, datenow, rqid);
                typeid=3;
            }
            else if (type.Equals("do"))
            {
                cower = string.Format("update TechRequire set Rq_Owner='{0}',Rq_techConsultID='{0}',Rq_flag={1}," +
                                           "Rq_DoID='{2}',Rq_DoTime='{3}',Rq_Time='{3}'" +
                                           "where Rq_ID={4}", selectID, 5, name, datenow, rqid);
                typeid=4;
            }
            else if (type.Equals("exe"))
            {
                cower = string.Format("update TechRequire set Rq_Owner='{0}',Rq_techConsultID='{0}',Rq_flag={1}," +
                                           "Rq_ExeID='{2}',Rq_ExeTime='{3}',Rq_Time='{3}'" +
                                           "where Rq_ID={4}", selectID, 7, name, datenow, rqid);
                typeid = 5;
            }
           
            //更新数据库
            string userid = CurrentUser.Id;

            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();            
            SqlCommand m_Command;           
            m_Command = new SqlCommand(cower, m_Connection);            
            m_Command.ExecuteNonQuery();

            //发送微信消息            
            string alert = ""; string msg = "";
            //找到微信号 
            string private_key = "Qa6j5mc8d3d4fkaEasC4k8c535GDiji";                    
            string token = GetMD5(selectID + rqid + time +typeid+ private_key);
            string weburl = string.Format("http://kexiaomi.com/api/kxmback/sent-wechatmsg?sent_user={6}&uid={0}&rq_id={1}&time={2}&type={3}&token={4}&msg={5}",
                       selectID, rqid, time, typeid, token, msg, userid);
            try
            {
                        HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(weburl);
                        HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
                        Stream stream = webreponse.GetResponseStream();
                        StreamReader objReader = new StreamReader(stream, System.Text.Encoding.UTF8);
                        //byte[] rsByte = new Byte[webreponse.ContentLength];  //save data in the stream
                        string m_Html = objReader.ReadToEnd();
                        string[] msgs = m_Html.Split(',');
                        if (msgs[0].IndexOf("0") > -1)
                        {
                            alert = "已经成功通知小觅";
                        }
                        else
                        {
                            alert = "系统异常，请稍候再试！错误代码：" + m_Html;
                        }
               }
              catch (Exception ex)
              {
                   alert = ex.Message;
              }
            m_Connection.Close();
            //Page.RegisterStartupScript("", "<script type='text/javascript'>alert('" + alert + "');</script>");
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>");            
            //返回主页
            //string url = "../EasyTechFunc/TechRequire.aspx";
            //Response.Redirect(url);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string url = "../EasyTechFunc/TechRequire.aspx";
            Response.Redirect(url);
        }
        private void SelectUsernames()
        {
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            //订阅
            SqlCommand m_Command;
            SqlDataReader sdr;
            string cp = "select ID,name from S_User left join S_roleUser on S_User.ID=S_roleUser.UID  where S_roleUser.roleID=2 or S_roleUser.roleID=6 order by Name";
            m_Command = new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();
            while (sdr.Read())
            {
                string ID = sdr.GetString(sdr.GetOrdinal("ID"));
                string name = sdr.GetString(sdr.GetOrdinal("name"));
                if (!reviewnames.ContainsKey(ID))
                {
                    reviewnames.Add(ID, name);
                    usersID.Add(ID);
                }
            }
            sdr.Close();
            m_Connection.Close();
        
        }

        private string GetMD5(string sDataIn)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytValue, bytHash;
            bytValue = System.Text.Encoding.UTF8.GetBytes(sDataIn);
            bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sTemp = "";
            for (int i = 0; i < bytHash.Length; i++)
            {
                sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
            }
            return sTemp.ToLower();
        }

    }
}