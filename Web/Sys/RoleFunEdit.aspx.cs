/*************************************************
Copyright (C),
Author:     作者 jiashiyi
Version:    版本 1.0
Date:       完成日期 2010-5-17
History:        // 修改历史记录列表，每条修改记录应包括修改日期、修改者及修改内容简述  
    1. Date:
       Author:
       Modification:
    2. ...
*************************************************/
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
using MyQuery.Utils;
using MyQuery.Work;

namespace MyQuery.Web.Sys
{
    public partial class RoleFunEdit : MyQuery.Work.BasePage
    {
        private string roleId;

        protected void Page_Load(object sender, EventArgs e)
        {
            RomoveLink();
            roleId = QueryString["id"];
            if (!Page.IsPostBack)
            {
                if (String.IsNullOrEmpty(roleId))
                {
                    Close("请先选择角色再进行菜单权限分配");
                }
                string strSql = "";
                try
                {
                    DataFrom dataFrom = new DataFrom();
                    lblRoleName.Text = dataFrom.GetScalar("select name from s_role where Id='" + roleId + "' and depid='" + CurrentUser.DepID + "'");
                    strSql = @"select f.id,f.Name as text,case when rf.roleid is null then 0 else 1 end as checked,f.parentid,f.sn 
                                from S_Fun f
                                left join S_RoleFun rf on rf.funid=f.id and rf.RoleId=" + roleId
                  + " where f.iflag=1 order by f.SN";
                    DataTable dt = dataFrom.GetDataTable(strSql);
                    if (dt != null)
                    {
                        WebHelper.BindTreeView(dt, "0", tvFunctions);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(strSql, ex);
                    RedirectError("角色功能编辑初始化失败，请稍候再试。原因:" + ex.Message);
                }
            }
        }

        private void RomoveLink()
        {
            int index = -1;
            for (int i = 0; i < Header.Controls.Count; i++)
            {
                HtmlGenericControl control = Header.Controls[i] as HtmlGenericControl;
                if (control != null && control.TagName.Equals("link", StringComparison.CurrentCultureIgnoreCase))
                {
                    index = i;
                    break;
                }
            }
            if (index > -1)
            {
                Header.Controls.RemoveAt(index);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isOk = true;
            foreach (TreeNode node in tvFunctions.Nodes)
            {
                if (node.ChildNodes.Count > 0 && !node.Checked)
                {
                    node.Checked = hasChildChecked(node);
                }
            }
            List<string> sqls = new List<string>();
            try
            {
                sqls.Add("delete from S_RoleFun where RoleId=" + roleId);
                SqlHelper.GetSqls(tvFunctions, sqls, "insert into S_RoleFun(RoleId,FunID) values(" + roleId + ",'{0}')");
                new DataFrom().SqlExecute(sqls);
            }
            catch (Exception ex)
            {
                isOk = false;
                Logger.Error(SqlHelper.GetSql(sqls), ex);
                RedirectError("提交失败，请稍候再试。原因:" + ex.Message);
            }
            if (isOk)
            {
                Close(null);
            }
        }
        /// <summary>
        /// 递归子是否被选中
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        private bool hasChildChecked(TreeNode parentNode)
        {
            bool result = false;
            foreach (TreeNode node in parentNode.ChildNodes)
            {
                if (node.ChildNodes.Count > 0)
                {
                    node.Checked = hasChildChecked(node);
                }
                if (node.Checked)
                {
                    result = true;
                }
            }
            return result;
        }

    }
}
