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
    public partial class EditSubPlan : BaseWWW
    {
        string SqlConnectionString;

        SqlConnection m_Connection;
        protected string Url = "";
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
            c = string.Format("Select SearchID,Plan_Status from [dbtest].[dbo].[Plan] where Plan_ID='{0}'", ConcernID);
            SqlCommand com = new SqlCommand(c, m_Connection);
            SqlDataReader MyReader = com.ExecuteReader();
            MyReader.Read();
            if (MyReader[0].ToString() != CurrentUser.Id)
            {
                Label1.Text = "子方案不是由你在寻找，无法修改！";
                MyReader.Close();
            }
            else if (Convert.ToInt16 ( MyReader[1].ToString()) != 7)
            {
                Label1.Text = "子方案不是我在寻状态，无法修改！";
                MyReader.Close();
            }
            else
            {
                MyReader.Close();
                Label1.Visible = false;
                Url = "../tmp/MyEdit.aspx?n=editPlanSub6&id="+ConcernID ;
               
               
                Label1.Text = "我在寻成功！";
                //   Url = "../Tmp/MyEdit.aspx?n=edittechConsult&ID=" + ConcernID;

                // Label1.Text = "处理完成！";
            }
        }
    }
}