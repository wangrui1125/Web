using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using MyQuery.Utils;
using MyQuery.Work;
using MyQuery.BAL;
using System.Collections.Generic;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class EditMark : MyQuery.Work.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataDeal deal = new DataDeal();
                deal.BindCodeList("ColorType", MarkColor, false);
                deal.BindCodeList("MarkCategory", CategoryID, false);
                deal.BindCodeList("Rq_Status", TechLevel, false);

                if ("tech".Equals(QueryString["entity"]))
                {
                    txtKeywords.Text = deal.GetScalar("select Rq_Keywords from Rq_words_alg where Rq_ID=" + QueryString["id"]); 
                    txtNotes.Text = deal.GetScalar("select Rq_EnpDes from TechRequire where Rq_ID=" + QueryString["id"]);
                    Literal1.Text = deal.GetScalar("select dbo.f_GetMark('" + QueryString["entity"] + "'," + QueryString["id"] + ")");
                    string temp = deal.GetScalar("select Rq_Status from TechRequire where Rq_ID=" + QueryString["id"]);
                    Literal2.Text = deal.GetScalar("select name from f_code where ID='Rq_status' and f_code.Code like " + temp);

                }
                else if ("researcher".Equals(QueryString["entity"]))
                {
                    DataTable dt = deal.GetTable("select Extract from Researcher where id=" + QueryString["id"]);
                    if (!DataHelper.IsNullOrEmpty(dt))
                    {
                        txtKeywords.Text = "";
                        txtNotes.Text = dt.Rows[0]["Extract"].ToString();
                    }
                }                
                CategoryID_SelectedIndexChanged(null, null);
            }
        }

        protected void CategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategoryID.SelectedValue.Length > 0)
            {
                new DataDeal().BindListCtrl(@"select D_Mark.MarkID,D_Mark.MarkDes,case when Mark.ID IS not NULL then 1 else 0 end as selected 
 from D_Mark
 LEFT join Mark on Mark.Entity='" + QueryString["entity"] + "' and Mark.EntityID='" + QueryString["id"] + @"' and  Mark.MarkDes=D_Mark.MarkID
 where D_Mark.CategoryID=" + CategoryID.SelectedValue, Marks, false);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<String> sqls = new List<string>();
            sqls.Add("delete from Mark where Entity='" + QueryString["entity"] + "' and EntityID='" + QueryString["id"] + "' and MarkType=" + CategoryID.SelectedValue);
            foreach (ListItem li in Marks.Items)
            {
                if (li.Selected)
                {
                    sqls.Add(@"INSERT INTO Mark(Entity
           ,Marker
           ,MarkTime
           ,MarkDes
           ,MarkColor
           ,EntityID
           ,MarkType) VALUES('" + QueryString["entity"] + @"'
           ,'" + CurrentUser.Id + @"'
           ,getdate()
           ,'" + li.Value + @"'
           ," + MarkColor.SelectedValue + @"
           ,'" + QueryString["id"] + @"'
           ," + CategoryID.SelectedValue + ")");
                }
            }
            DataDeal deal = new DataDeal();
            deal.SqlExecute(sqls);
            Literal1.Text = deal.GetScalar("select dbo.f_GetMark('" + QueryString["entity"] + "'," + QueryString["id"] + ")");
            Alert("标签提交成功，你可以继续选择其他体系和分类，继续打标签", MarkColor.ClientID);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int Rq_Satae=0;
            switch (TechLevel.SelectedIndex)
            {
                case 0:
                    Rq_Satae = 0;
                    break;
                case 1:
                    Rq_Satae = 1;
                    break;
                case 2:
                    Rq_Satae = 2;
                    break;
            }

            String sql = String.Format("update TechRequire set Rq_status={0} where Rq_ID={1}",Rq_Satae,QueryString["id"]);
            DataDeal deal = new DataDeal();
            deal.SqlExecute(sql);
            string temp = deal.GetScalar("select Rq_Status from TechRequire where Rq_ID=" + QueryString["id"]);
            Literal2.Text = deal.GetScalar("select name from f_code where ID='Rq_status' and f_code.Code like " + temp);
            Alert("需求等级标签修改成功");

        }

        protected void TechLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void btnSave_Clickc(object sender, EventArgs e)
        {
            String sql = String.Format("delete from Mark where Entity='tech' and EntityID={0}", QueryString["id"]);
            DataDeal deal = new DataDeal();
            deal.SqlExecute(sql);
            Alert("标签清空");
            Literal1.Text = deal.GetScalar("select dbo.f_GetMark('" + QueryString["entity"] + "'," + QueryString["id"] + ")");
        }
    }
}
