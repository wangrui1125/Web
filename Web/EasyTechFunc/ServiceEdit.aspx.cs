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
            string serId = Request["id"];

            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            SqlCommand m_Command;

            string c = string.Format("select * from dfg_service_ticket where id='{0}'",serId);
            m_Command = new SqlCommand(c, m_Connection);
            SqlDataReader sdr = m_Command.ExecuteReader();
            string orderNumber="";
            string serviceType="";
            byte payTypecode = 0 ;
            string processState="";
            byte serviceStatus=0;
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
            string[] serviceTypes = { "产品推荐", "技术分析", "技术咨询","联系专家","知识产权","文章订阅"};

            int servicetypecode = selectIndex(serviceTypes, serviceType.Replace(" ",""));



            DropDownList1.DataSource = paytypes;
            DropDownList1.DataBind();
            DropDownList1.SelectedIndex = payTypecode;

            c = string.Format("select Name from dfg_f_code where ID='process_state' and Sn='{0}'", servicetypecode);
            m_Command = new SqlCommand(c, m_Connection);
            sdr = m_Command.ExecuteReader();
            List<string> processStateslist=new List<string>();
            if(sdr.HasRows)
            {
                while(sdr.Read())
                {
                    string temp=sdr.GetString(sdr.GetOrdinal("Name"));    
                    processStateslist.Add(temp);
                }
                
            }
            string[] processStates = processStateslist.ToArray();
            int processStatecode = selectIndex(processStates, processState);

            DropDownList2.DataSource = processStates;
            DropDownList2.DataBind();
            DropDownList2.SelectedIndex = processStatecode-1;















        }

        protected void Button1_Click(object sender, EventArgs e)
        {

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

    }
}