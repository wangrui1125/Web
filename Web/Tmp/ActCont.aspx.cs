using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using MyQuery.Work;
using MyQuery.Utils;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace MyQuery.Web.Tmp
{
    public partial class ActCont : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string SqlConnectionString;
            SqlConnection m_Connection;            
            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;
            }
            else
            m_Connection.Open();

            string ContectID = Request["id"];
            string c = string.Format("Select Name,Tel,Enp_ID,UserID,email from [dbtest].[dbo].[Contact] where ID='{0}'", ContectID);
            SqlCommand com = new SqlCommand(c, m_Connection);
            SqlDataReader sdr = com.ExecuteReader();
            string name="";
            string Tel="";
            Int64 Enp_ID=0;
            string UserID = "";
            string email = "";
            if (sdr.Read())
            {                
                name = sdr.GetString(0);
                Tel= sdr.GetString(1);
                Enp_ID= sdr.GetInt64(2);
                UserID=sdr.GetString(3);
                email= sdr.GetString(4);
            }
            sdr.Close();

            string news = "";
            if (UserID == "")
            {
                //检测合法性
                string password = "$2y$10$vocCG.kj3YdC3eKPZ9Bt/eCWdjhdw.U9lJgiV0yDW936p8t1wg0Bi";
                bool flags = true;
                if (name == "")
                {
                    news = "不存在用户名";
                    flags = false;
                }
               if (Tel == "")
                {
                    news = "电话不可以为空";
                    flags = false;
                }
               if (!CheckPhoneIsAble(Tel))
                {
                    news = "不是合法的手机号码";
                    flags = false;
                }
                if (flags)
                {
                    c = string.Format("select Count(*) from S_UserWeb where tel='{0}'", Tel);
                    com = new SqlCommand(c, m_Connection);
                    if (Convert.ToInt16(com.ExecuteScalar().ToString()) > 0)
                    {
                        news = "此号码已经被注册";
                        flags = false;
                    }
                }
                if (flags)
                {
                        c = string.Format("INSERT INTO S_UserWeb (name,password,email,tel,role,Enp_ID,created_at) values ('{0}','{1}','{2}','{3}', {4},{5},'{6}')",
                        name, password,email,Tel,0,Enp_ID,DateTime.Now.ToString());
                    com = new SqlCommand(c, m_Connection);
                    try
                    {
                        com.ExecuteNonQuery();                        
                        c = string.Format("Update Contact set UserID ='{1}' where ID='{0}'",
                        ContectID,Tel);
                        com = new SqlCommand(c, m_Connection);
                        com.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        news = ex.ToString();
                    }                    
                }
            }
            else
            {
                news = "此用户已经激活，ID为" + UserID;
            }
            m_Connection.Close();
            Response.Write("<script type='text/javascript'>alert('" + news + "');window.close();</script>");
        }       
        private bool CheckPhoneIsAble(string input)
        {
            if (input.Length < 11)
            {
                return false;
            }           
           

            Regex regexphon = new Regex(@"^(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
            if (regexphon.IsMatch(input))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}