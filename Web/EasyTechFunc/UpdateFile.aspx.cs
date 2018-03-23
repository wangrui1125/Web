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
    public partial class UpdateFile : BaseWWW
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<string> types = Data_Binding();
                this.CheckBoxList1.DataSource = types;
                this.CheckBoxList1.DataBind();
            }
        }

        private List<string> Data_Binding()
        {
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            SqlCommand m_Command;

            string c = "select Name from dfg_f_code where ID='file_type'";
            m_Command = new SqlCommand(c, m_Connection);
            SqlDataReader sdr = m_Command.ExecuteReader();
            List<string> types = new List<string>();
            while (sdr.Read())
            { 
                string type=sdr.GetString(0);
                types.Add(type);
            }
            sdr.Close();
            m_Connection.Close();
            return types;
           
        }




        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           

            // Before attempting to perform operations  
            // on the file, verify that the FileUpload   
            // control contains a file.  
            if (FileUpload1.HasFile)
            {
                // Get the name of the file to upload.  
                 string rqid = Request["rqid"];
                 string savePath = Server.MapPath("~/Uploaded/") + "Tc_" + rqid + "//";
                 string fileName = FileUpload1.FileName;

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                // Append the name of the file to upload to the path.  
                savePath += fileName;
                // Call the SaveAs method to save the   
                // uploaded file to the specified path.  
                // This example does not perform all  
                // the necessary error checking.                 
                // If a file with the same name  
                // already exists in the specified path,    
                // the uploaded file overwrites it.
                FileUpload1.SaveAs(savePath);

                string savePathweb = "/Uploaded" + "/Tc_" + rqid + "/" + fileName;
                string userid=CurrentUser.Id;
                string username=CurrentUser.Name;
                int checkid=CheckBoxList1.SelectedIndex;
                List<string> types = Data_Binding();
                string checkname=types[checkid];


                //上传数据库
                string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
                SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
                m_Connection.Open();
                string cp = ""; SqlCommand m_Command = null; SqlDataReader sdr = null;

                cp = string.Format("insert into dfg_report(report_name,create_at,add_user_id,add_user_name,location,filetype_id,filetype,service_type,service_id)" +
                    "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}')", fileName, DateTime.Now, userid, username, savePathweb, checkid, checkname, 1, rqid);

                m_Command = new SqlCommand(cp, m_Connection);
                m_Command.ExecuteNonQuery();
                m_Connection.Close();
               
                //string url = "~/Appointment.aspx";
                //Response.Redirect("~/Appointment.aspx", true);
                Response.Write("<script>window.open('/Tmp/MyQuery.aspx?n=listuploadfile&rqid=" + rqid + "','_blank')</script>");
                //Response.AddHeader("Refresh", "0");
            }
        }
    }
}