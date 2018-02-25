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
    public partial class WeChatProgressReminder : BaseEdit
    {
        Dictionary<string, string> selectnames = new Dictionary<string, string>();
        List<string> usersID = new List<string>();
//int guwennumb = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int enp_id = 0; byte rq_flag = 0; string rq_name = ""; string rqflgename = "";
                string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
                SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
                m_Connection.Open();
                //订阅
                SqlCommand m_Command;
                SqlDataReader sdr;
                string rqid = Request["ConcernID"];

                string cp = "select Rq_Name,Rq_flag,Enp_ID from TechRequire where Rq_ID=" + rqid;
                 m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                if (sdr.Read())
                {
                    rq_name = sdr.GetString(sdr.GetOrdinal("Rq_Name"));
                    rq_flag = sdr.GetByte(sdr.GetOrdinal("Rq_flag"));
                    enp_id = sdr.GetInt32(sdr.GetOrdinal("Enp_ID"));
                }
                sdr.Close();
                cp = string.Format("select Name from f_code where ID='Rq_flag' and Code={0}", rq_flag);
                m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                if (sdr.Read())
                {
                    rqflgename = sdr.GetString(sdr.GetOrdinal("Name"));                   
                }               
                Label2.Text = rq_name;
                Label4.Text = rqflgename;
                SelectUsernames();
                DropDownList1.DataSource = selectnames;
                DropDownList1.DataBind();
                if (enp_id.ToString() == "")
                {
                    Response.Write("<script type='text/javascript'>alert('抱歉，需求没有提供企业信息！！');window.close();</script>");
                }
            }
        }

        private int SelectUsernames()
        {
            int guwennumb = 0;
            int enp_id = 0;
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            //订阅
            SqlCommand m_Command;
            SqlDataReader sdr;
            string rqid = Request["ConcernID"];

            string cp = "select Enp_ID from TechRequire where Rq_ID=" + rqid;
            m_Command = new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();
            if (sdr.Read())
            {
                enp_id = sdr.GetInt32(sdr.GetOrdinal("Enp_ID"));
            }
            sdr.Close();
            string type = enp_id.ToString();    
       
            cp = "select ID,Name from S_User left join Enterprise on S_User.ID=Enterprise.guwenID  where Enterprise.Enp_ID="+type;
            m_Command = new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();
            while (sdr.Read())
            {
                string ID = sdr.GetString(sdr.GetOrdinal("ID"));
                string name = sdr.GetString(sdr.GetOrdinal("name"));
                if (!selectnames.ContainsKey(ID))
                {
                    selectnames.Add(ID, name);
                    usersID.Add(ID);
                    guwennumb++;
                }
            }
            sdr.Close();

            string cp2 = "select ID,Name from S_UserWeb left join Enterprise on S_UserWeb.Enp_ID=Enterprise.Enp_ID  where Enterprise.Enp_ID=" + type;

            m_Command = new SqlCommand(cp2, m_Connection);
            sdr = m_Command.ExecuteReader();
            while (sdr.Read())
            {
                string ID = sdr.GetInt32(sdr.GetOrdinal("ID")).ToString() ;
                string name = sdr.GetString(sdr.GetOrdinal("name"));
                if (!selectnames.ContainsKey(ID))
                {
                    selectnames.Add(ID, name);
                    usersID.Add(ID);
                }
            }
            sdr.Close();
            m_Connection.Close();
            return guwennumb;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int num= SelectUsernames();            
            string selectID = "";
            string rqid = Request["ConcernID"];
            string datenow = DateTime.Now.ToString("yyyy-MM-dd");
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string time = Convert.ToInt64(ts.TotalSeconds).ToString();
            
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            SqlCommand m_Command;
           
            //发送微信消息
            int typeid = 0;
            int index = DropDownList1.SelectedIndex;
            selectID = usersID.ElementAt(index);
            if (index < num)
            { typeid = 5; }
            else
            { typeid = 6; }
            string cselct = "select wechatOpenid from S_User where ID='" + selectID + "'";
            m_Command = new SqlCommand(cselct, m_Connection);
            SqlDataReader sdr = m_Command.ExecuteReader();
            string alert = ""; string msg = "";
            msg = TextBox1.Text;
            string private_key = "Qa6j5mc8d3d4fkaEasC4k8c535GDiji";
            string token = GetMD5(selectID + rqid + time + typeid + private_key);
            string weburl = string.Format("http://kexiaomi.com/api/kxmback/sent-wechatmsg?uid={0}&rq_id={1}&time={2}&type={3}&token={4}&msg={5}",
                        selectID, rqid, time, typeid, token,msg);
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
                    alert = "系统异常，请稍候再试！错误代码："+m_Html;
                }
            }
            catch (Exception)
            {
                alert = "系统异常，请稍候再试！";
            }
            m_Connection.Close();
            //Page.RegisterStartupScript("", "<script type='text/javascript'>alert('" + alert + "');</script>");
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>");
            //返回主页
            //string url = "../EasyTechFunc/TechRequire.aspx";
            //Response.Redirect(url);
            m_Connection.Close();
            //Page.RegisterStartupScript("", "<script type='text/javascript'>alert('" + alert + "');</script>");
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>"); 
        
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string url = "../EasyTechFunc/TechRequire.aspx";
            Response.Redirect(url);
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