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


    public partial class ServiceEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string serId = Request["id"];
                string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
                SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
                m_Connection.Open();
                SqlCommand m_Command;

                string c = string.Format("select * from dfg_service_ticket where id='{0}'", serId);
                m_Command = new SqlCommand(c, m_Connection);
                SqlDataReader sdr = m_Command.ExecuteReader();
                string orderNumber = "";
                string serviceType = "";
                byte payTypecode = 0;
                string processState = "";
                byte serviceStatus = 0;
                if (sdr.Read())
                {
                    orderNumber = sdr.GetString(sdr.GetOrdinal("order_no"));
                    serviceType = sdr.GetString(sdr.GetOrdinal("service_type"));
                    payTypecode = sdr.GetByte(sdr.GetOrdinal("pay_type"));
                    processState = sdr.GetString(sdr.GetOrdinal("process_state"));
                    serviceStatus = sdr.GetByte(sdr.GetOrdinal("service_status"));
                }
                sdr.Close();
                Label3.Text = orderNumber.ToString();
                Label1.Text = serviceType;
                Label4.Text = DateTime.Now.Date.ToString();

                string[] paytypes = { "未支付", "微信支付", "其他支付" };
                string[] serviceTypes = { "产品推荐", "技术分析", "技术咨询", "联系专家", "知识产权", "文章订阅" };

                int servicetypecode = selectIndex(serviceTypes, serviceType.Replace(" ", ""));
                DropDownList1.DataSource = paytypes;
                DropDownList1.DataBind();
                DropDownList1.SelectedIndex = payTypecode;

                c = string.Format("select Name from dfg_f_code where ID='process_state' and Sn='{0}'", servicetypecode);
                m_Command = new SqlCommand(c, m_Connection);
                sdr = m_Command.ExecuteReader();
                List<string> processStateslist = new List<string>();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        string temp = sdr.GetString(sdr.GetOrdinal("Name"));
                        processStateslist.Add(temp);
                    }

                }
                string[] processStates = processStateslist.ToArray();
                int processStatecode = selectIndex(processStates, processState);

                DropDownList2.DataSource = processStates;
                DropDownList2.DataBind();
                DropDownList2.SelectedIndex = processStatecode - 1;
                m_Connection.Close();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            string serId = Request["id"];
            //支付信息更改
            int index = DropDownList1.SelectedIndex;
            string time = DateTime.Now.ToString();
            int amount = 0;
            if (TextBox1.Text != "")
            {
                amount = int.Parse(TextBox1.Text);
            }
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            SqlCommand com;
            string c = string.Format(" Update [dbtest].[dbo].[dfg_service_ticket] set amount={0},pay_type={1},pay_time='{2}',update_at='{4}' where id='{3}'", 
                amount,index,time,serId,time);
            com = new SqlCommand(c, m_Connection);
            com.ExecuteNonQuery();
            m_Connection.Close();

            string news = "成功提交";
            Response.Write("<script type='text/javascript'>alert('" + news + "');window.reload();</script>");




        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            string serId = Request["id"];
            //状态信息更改
            int index = DropDownList2.SelectedIndex;
            string time = DateTime.Now.ToString();
            

            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            SqlCommand m_Command;

            string c = string.Format("select service_type from dfg_service_ticket where id='{0}'", serId);
            m_Command = new SqlCommand(c, m_Connection);
            SqlDataReader sdr = m_Command.ExecuteReader();
            string service_type = "";
            while (sdr.Read())
            {
                service_type = sdr.GetString(0).Replace(" ","");
            }
            sdr.Close();

            c = string.Format("select Code from dfg_f_code where Name='{0}' and ID='service_type'", service_type);
            m_Command = new SqlCommand(c, m_Connection);
            sdr = m_Command.ExecuteReader();
            int service_code =0;
            while (sdr.Read())
            {
                service_code = int.Parse(sdr.GetString(0));
            }
            sdr.Close();
            c = string.Format("select Name from dfg_f_code where Sn='{0}' and ID='process_state'", service_code);
            m_Command = new SqlCommand(c, m_Connection);
            sdr = m_Command.ExecuteReader();
            List<string> processStateslist = new List<string>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    string temp = sdr.GetString(sdr.GetOrdinal("Name"));
                    processStateslist.Add(temp);
                }

            }
            sdr.Close();
            //上传数据
            string selectstate = processStateslist[index];
            c = string.Format(" Update [dbtest].[dbo].[dfg_service_ticket] set process_state='{0}',update_at='{1}' where id='{2}'",
              selectstate, time, serId);
            m_Command = new SqlCommand(c, m_Connection);
            m_Command.ExecuteNonQuery();
            m_Connection.Close();

            string news = "成功提交";
            Response.Write("<script type='text/javascript'>alert('" + news + "');window.reload();</script>");

        }

        private int selectIndex(string[] strs, string selstr)
        { 
            //输出是第几个量
            int i=0;
            foreach (string str in strs)
            {
                i++;
                if (str.Equals(selstr))
                {
                    break;
                }
                
            }

            return i;
        
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //上传文件
            
                // Specify the path on the server to  
                // save the uploaded file to.  
                String savePath =Server.MapPath("~/Uploaded/");

                // Before attempting to perform operations  
                // on the file, verify that the FileUpload   
                // control contains a file.  
                if (FileUpload1.HasFile)
                {
                    // Get the name of the file to upload.  
                    String fileName = FileUpload1.FileName;

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

                    // Notify the user of the name of the file  
                    // was saved under.  
                    TextBox3.Text = DateTime.Now.Date.ToString()+": Your file was saved as " + fileName;
                }
                else
                {
                    // Notify the user that a file was not uploaded.  
                    TextBox3.Text = "You did not specify a file to upload.";
                }
            
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}