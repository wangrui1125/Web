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
    public partial class Editpasswd : MyQuery.Work.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WebHelper.SetControlAttributes(btnSubmit, new TextBoxVal[] { txtPwdOld, txtPasswd, txtPwdCheck });
                txtID.Text = CurrentUser.Id;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtPasswd.Text == txtPwdCheck.Text)
            {
                bool isOk = false;
                MySqlParameters mySql = new MySqlParameters("s_user");
                DataFrom dataFrom = new DataFrom();
                try
                {
                    mySql.EditSqlMode = SqlMode.Update;
                    mySql.Add("pwd", txtPasswd.Text);
                    mySql.Add("id", CurrentUser.Id, "and id={0}");
                    mySql.Add("oldpwd", txtPwdOld.Text, "and pwd={0}");
                    isOk = dataFrom.SqlExecute(mySql) == 1;
                }
                catch (Exception ex)
                {
                    Logger.Error(SqlHelper.GetSql(mySql, dataFrom.Dbtype), ex);
                }
                if (isOk)
                {
                    Alert("密码修改成功！下次请使用新密码");
                }
                else
                {
                    Alert("原密码输入错误，请重输！");
                }

            }
            else
            {
                Alert("两次密码输入不一致，请重输！");
            }
        }
    }
}
