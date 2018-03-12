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
    public partial class CancelContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string Contractid = Request["Contractid"];
            string news="";

            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            string cp = ""; SqlCommand m_Command = null; SqlDataReader sdr = null;

            String savePath = Server.MapPath("~");
            cp = string.Format("select file_path from [dfg_contract] where id={0}", Contractid);
            m_Command = new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();
            while (sdr.Read())
            {
                savePath += sdr.GetString(0).Replace("/", "\\");
            
            }
            sdr.Close();
            
            File.Delete(savePath);
            cp = string.Format("delete from [dfg_contract] where id={0}", Contractid);

            try
            {
                m_Command = new SqlCommand(cp, m_Connection);
                m_Command.ExecuteNonQuery();
                m_Connection.Close();
                news = "已经成功删除文件";
            }
            catch (Exception ex)
            {
                news = "Something is wrong:" + e.ToString();
            }
                       
            //System.Threading.Thread.Sleep(5000);
            Response.Write("<script type='text/javascript'>alert('" + news + "');parent.location.reload();</script>");

            
        }
    }
}