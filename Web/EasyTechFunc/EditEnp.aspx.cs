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
    public partial class EditEnp : BaseWWW
    {
        string SqlConnectionString;
        protected string Url = "";
        SqlConnection m_Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
            string ConcernID = Request["ConcernID"];

            string c = null;
            SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            m_Connection = new SqlConnection(SqlConnectionString);
            if (m_Connection.State == System.Data.ConnectionState.Open)
            {
                ;

            }
            else
                m_Connection.Open();
            c = string.Format("Select CreaterID from [dbtest].[dbo].[Enterprise] where Enp_ID='{0}'", ConcernID);
            SqlCommand com = new SqlCommand(c, m_Connection);
            SqlDataReader MyReader = com.ExecuteReader();
            MyReader.Read();
            if (MyReader[0].ToString() != CurrentUser.Id)
            {
                Label1.Text = "不是本人创建，无权进行修改！";
                MyReader.Close();


            }

            else
            {
                MyReader.Close();
                
                Url = "../tmp/MyEdit.aspx?n=editEnterprise&id=" + ConcernID;
                Response.Redirect(Url);
                Label1.Visible = false;
                Label1.Text = "提交成功！";
            }
        }
    }
}