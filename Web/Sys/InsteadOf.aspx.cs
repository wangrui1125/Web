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
using System.Xml.Linq;
using System.Xml;
using System.IO;
using MyQuery.Utils;
using MyQuery.Work;

namespace MyQuery.Web.Sys
{
    public partial class InsteadOf : MyQuery.Work.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtID.Text = CurrentUser.Id;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Length > 0)
            {
                DataFrom dataFrom = new DataFrom(CurrentUser, QueryString);
                try
                {
                    Authenticate auth = new Authenticate();
                    MyUser user = auth.GetUser(txtID.Text);
                    if (String.IsNullOrEmpty(auth.Error))
                    {
                        dataFrom.SetLogInfo("你现在为" + user.Name + "工作");
                        new DefaultMenu().SetFunctions(user);
                        SetSessionUser(user);
                        txtID.Text = user.Name;
                        txtID.Enabled = false;
                        btnSubmit.Enabled = false;
                        Alert("你现在为" + user.Name + "工作");
                    }
                    else
                    {
                        dataFrom.SetLogInfo(txtID.Text + auth.Error);
                        Alert(txtID.Text + auth.Error);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("代替失败", ex);
                }
            }
            else
            {
                Alert("帐户不能为空，请重输！");
            }
        }
    }
}
