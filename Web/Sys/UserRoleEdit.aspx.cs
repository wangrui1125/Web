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
using System.Collections.Generic;
using MyQuery.Work;

namespace MyQuery.Web.Sys
{
    public partial class UserRoleEdit : MyQuery.Work.BasePage
    {
        private string userId = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(QueryString["id"]))
                {
                    btnSave.Enabled = false;
                }
                else
                {
                    userId = QueryString["id"];
                    if (!Page.IsPostBack)
                    {
                        DataFrom dataFrom = new DataFrom();
                        string userName = dataFrom.GetScalar("select name from s_user where id='" + userId + "'");
                        literalUsers.Text = String.Format("{0}[{1}]", userName, userId);

                        string sql = @"SELECT s_role.ID,s_role.NAME,case when s_roleuser.RoleID is null then 0 else 1 end as issel 
 FROM s_role
 left join  s_roleuser on s_roleuser.RoleID=s_role.ID and s_roleuser.uid='" + userId + @"'
 WHERE s_role.Iflag>0 and '" + CurrentUser.DepID + "' like s_role.depid+'%'";

                        dataFrom.BindListCtrl(sql, chkListRoles, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("初始化失败", ex);
                RedirectError("初始化失败，请稍候再试。原因:" + ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> sqls = new List<string>();
            string insertSql = "INSERT INTO s_roleuser(ROLEID,uid) VALUES('{0}','{1}')";
            string sql = "DELETE FROM s_roleuser WHERE uid='{0}'";
            sqls.Add(String.Format(sql, userId));
            int role = -1;
            foreach (ListItem li in chkListRoles.Items)
            {
                if (li.Selected)
                {
                    sqls.Add(String.Format(insertSql, li.Value, userId));
                    role = Convert.ToInt16(li.Value);
                }
            }
            string sql1 = "Update s_user set role='{0}' WHERE id='{1}'";
            sqls.Add(String.Format(sql1, role, userId));
            new DataFrom().SqlExecute(sqls);
            Close(null);
        }

    }
}
