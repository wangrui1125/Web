using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using MyQuery.Utils;
using MyQuery.Work;
using System.Data.SqlClient;
using System.Reflection;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class Plan : BaseWWW
    {
        protected string Url = "";
        string SqlConnectionString;

        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = null;
            string rqid = Request["rqid"];
            //Session["Plan_id"] = id;
            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;

            }
            else
                m_Connection.Open();
            string c = string.Format("select count(*) from [dbtest].[dbo].[Plan] where [Plan].Rq_ID = '{0}' and [Plan].Plan_Owner=0", rqid);
            SqlCommand com = new SqlCommand(c, m_Connection);
            int count = Convert.ToInt16(com.ExecuteScalar().ToString());
            //id = com.ExecuteScalar().ToString();
            //string c = string.Format("select count(*) from [dbtest].[dbo].[Plan] where [Plan].Plan_ID = '{0}'", id);
            //SqlCommand com = new SqlCommand(c, m_Connection);
            //int count = Convert.ToInt16(com.ExecuteScalar().ToString());


            if (count == 0)
            {
                c = string.Format("insert into [dbtest].[dbo].[Plan]([Plan].Rq_ID,[Plan].Plan_Owner) values( '{0}',0)", rqid);
                com = new SqlCommand(c, m_Connection);
                com.ExecuteNonQuery();
               
            }
            c = string.Format("select [Plan].Plan_ID from [dbtest].[dbo].[Plan] where [Plan].Rq_ID = '{0}' and [Plan].Plan_Owner=0", rqid);
            com = new SqlCommand(c, m_Connection);

            id = com.ExecuteScalar().ToString();
            
                    Url ="../tmp/MyEdit.aspx?n=editPlan1&rqid="+rqid+"&id="+id;
                   // Url = "../tmp/MyDetail.aspx?n=detailPlan2&rqid=" + rqid + "&id=" + id;
            Response.Redirect(Url);
       
        }
    }
}