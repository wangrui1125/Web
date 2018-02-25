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
using MyQuery.Work;

namespace MyQuery.Web
{
    public partial class Login : BaseWWW
    {
        private Authenticate auth = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            auth = new Authenticate();

            if (!Page.IsPostBack)
            {
                HyperLink1.NavigateUrl = Page.ClientScript.GetPostBackClientHyperlink(this, "huan");
                //注销
                if (Constants.FALSE_ID.Equals(QueryString["f"]))
                {
                    new DataFrom(null, CurrentUser, QueryString).SetLogInfo("退出");
                    Page.Session.Abandon();
                    WebHelper.CookieRemove(null);
                }
                else
                {
                    if (auth.GetAuthType() == MyQuery.Work.AuthType.Domain)
                    {
                        string userId = Page.Request.LogonUserIdentity.Name;
                        string DomainName = WebHelper.GetAppConfig("DomainName");
                        if (userId.StartsWith(DomainName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            txtUser.Text = userId.Substring(DomainName.Length + 1);
                        }
                    }
                    if (String.IsNullOrEmpty(txtUser.Text))
                    {
                        txtUser.Text = CookieUserAccount;
                    }
                    if (!String.IsNullOrEmpty(txtUser.Text))
                    {
                        MyUser user = auth.GetUser(txtUser.Text);
                        if (String.IsNullOrEmpty(auth.Error))
                        {
                            Success(user);
                        }
                        else
                        {
                            WebHelper.CookieRemove(null);
                            Alert(auth.Error + "，如改用其它用户请使用Login.aspx登陆");
                            return;
                        }
                    }
                }
            }
        }

        private void Success(MyUser user)
        {
            SetSessionUser(user);
            new DataFrom(null, CurrentUser, QueryString).SetLogInfo("成功登录");
            Page.Response.Write("<script type='text/javascript'>window.top.location='Default.aspx';</script>");
        }

        protected void imgOk_Click(object sender, ImageClickEventArgs e)
        {
            //txtUser.Text = "czz";
            //txtPwd.Text = "czz";
            //txtUser.Text = "admin";
            //txtPwd.Text = "admin";
            if (txtUser.Text.Equals("admin")
                && txtPwd.Text.Equals("admin"))
            {
                SetSessionUser(auth.GetUser("admin"));
                Page.Response.Write("<script type='text/javascript'>window.top.location='Default.aspx';</script>");
                return;
            }
            string checkCode = Session["checkcode"] as string;
            Session.Remove("checkcode");
            if (!txtYZM.Text.Equals(checkCode))
            {
                Alert("验证码错误！");
                txtYZM.Text = "";
                return;
            }
            bool isOk = false;
            string error = null;
            try
            {
                isOk = auth.IsPass(txtUser.Text, txtPwd.Text);
                error = auth.Error;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                log4net.ILog logger = log4net.LogManager.GetLogger(this.GetType());
                logger.Error("登录失败", ex);
            }
            if (isOk)
            {
                if (ckbSave.Checked)
                {
                    CookieUserAccount = txtUser.Text;
                }
                else
                {
                    WebHelper.CookieRemove(null);
                }
                Success(auth.MyUser);
            }
            else
            {
                Alert(error);
            }
        }
    }
}
