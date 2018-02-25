using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyQuery.Utils;

namespace MyQuery.Web
{
    /// <summary>
    /// 错误提示页面 参数i接收提示信息
    /// by 贾世义
    /// </summary>
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Image1.ImageUrl = MyQuery.Utils.WebHelper.GetRootPath() + "Img/error.gif";
            Image1.Attributes.Add("ondblclick", "document.getElementById('" + txtErr.ClientID + "').style.display=''");
            string error = Page.Request.QueryString["i"];
            if (!String.IsNullOrEmpty(error))
            {
                int p = error.IndexOf(LanguageResource.Current.PUBLIC_Reason);
                if (p == -1)
                {
                    lblErr.Text = error;
                }
                else
                {
                    lblErr.Text = error.Substring(0, p);
                    txtErr.Text = error.Substring(p);
                }
            }
            if (Session["error"] != null)
            {
                txtErr.Text = Session["error"].ToString();
            }
        }
    }
}
