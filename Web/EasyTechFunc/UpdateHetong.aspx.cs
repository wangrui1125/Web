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
    public struct Contract
    {
        public string name ;
        public string path;
        public string statue;
        public string planid;
        public string rqid;
    }

    public partial class UpdateHetong : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /*
            //string id = Request["id"];
            string rqid = Request["rqid"];
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            string cp="";SqlCommand m_Command =null;SqlDataReader sdr=null;
            
            cp=string.Format("select Rq_Name from TechRequire where Rq_ID={0}",rqid);
            m_Command=new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();
            string rqname="";
            if (sdr.Read())
            {
                rqname=sdr.GetString(sdr.GetOrdinal("Rq_Name"));
            }
            sdr.Close();
            cp = string.Format("select id,name,upload_time,status from dfg_contract where rq_id={0}", rqid);

            
            m_Command = new SqlCommand(cp, m_Connection);
            sdr = m_Command.ExecuteReader();
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("文件名",typeof(string));   
            table.Columns.Add("上传时间",typeof(string));
            table.Columns.Add("状态",typeof(string));
            table.Columns.Add("方案ID",typeof(string));
            table.Columns.Add("需求ID",typeof(string));            
            while(sdr.Read())
            {
                DataRow tr1 = table.NewRow();
                tr1[0] = sdr.GetInt32(sdr.GetOrdinal("id")).ToString();
                tr1[1]=sdr.GetString(sdr.GetOrdinal("name"));
                tr1[2] = sdr.GetDateTime(sdr.GetOrdinal("upload_time")).ToString();  
                byte status = sdr.GetByte(sdr.GetOrdinal("status"));
                if (status == 0)
                {
                    tr1[3] = "未通过";
                }
                else
                {
                    tr1[3] = "通过";
                }
                //tr1[4] = id;
                tr1[5] = rqid;                
                table.Rows.Add(tr1);
                
            }
            m_Connection.Close();


            if (!IsPostBack)
            {
                DataGrid1.DataSource = table.DefaultView;
                DataGrid1.DataBind();
            }
            sdr.Close();

            */




        }
         
        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        protected void DataGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string id = Request["id"];
            string rqid = Request["rqid"];
            String savePath = Server.MapPath("~/Uploaded/")+"Tc_"+rqid+"//";

            // Before attempting to perform operations  
            // on the file, verify that the FileUpload   
            // control contains a file.  
            if (FileUpload1.HasFile)
            {
                // Get the name of the file to upload.  
                
                String fileName = FileUpload1.FileName;
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


                //上传数据库
                string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
                SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
                m_Connection.Open();
                string cp = ""; SqlCommand m_Command = null; SqlDataReader sdr = null;

                cp = string.Format("select name from [dfg_contract] where rq_id={0}", rqid);
                m_Command = new SqlCommand(cp, m_Connection);
                sdr = m_Command.ExecuteReader();
                HashSet<string> names=new HashSet<string> ();
                while (sdr.Read())
                {
                    string name  = sdr.GetString(sdr.GetOrdinal("name"));
                    names.Add(name);
                }
                sdr.Close();
                if (!names.Contains(fileName))
                {
                    cp = string.Format("select Plan_Des from [Plan] where Plan_ID={0}", rqid);
                    m_Command = new SqlCommand(cp, m_Connection);
                    sdr = m_Command.ExecuteReader();
                    string plandes = "";
                    if (sdr.Read())
                    {
                        plandes = sdr.GetString(sdr.GetOrdinal("Plan_Des"));
                    }
                    sdr.Close();
                    string account = "000000000000000";
                    string savePathweb = "/Uploaded" + "/Tc_" + rqid + "/" + fileName;
                    cp = string.Format("insert into dfg_contract(name,contract_draft_name,account,file_path,service_content,status,rq_id,upload_time)" +
                        "values('{0}','{1}','{2}','{3}','{4}',0,'{5}','{6}')", fileName, fileName, account, savePathweb, plandes, rqid, DateTime.Now);

                    m_Command = new SqlCommand(cp, m_Connection);
                    m_Command.ExecuteNonQuery();
                    m_Connection.Close();
                }
                //string url = "~/Appointment.aspx";
                //Response.Redirect("~/Appointment.aspx", true);
                 Response.Write("<script>window.open('/Tmp/MyQuery.aspx?n=listcontract&rqid="+rqid+"','_blank')</script>");
                //Response.AddHeader("Refresh", "0");
            }
            
        }
    }
}