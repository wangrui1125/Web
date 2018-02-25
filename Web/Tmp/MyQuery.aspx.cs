using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MyQuery.Work;
using MyQuery.MyControl;

namespace MyQuery.Web.Tmp
{
    /// <summary>
    /// 查询模板
    /// 参数：n对应的查询定制xml的名字（不含后缀）
    /// </summary>
    public partial class ListQuery : BaseQuery
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            PageLoad(MyInputs1, MyBtns1, MyBtns2, MyGridView1, btnSubject, lblDef, btnQuery, trToolTip, trQuery, tblSearch, divQuery, divNotes);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
           
                QueryClick();
          
            
        }

        protected void MyBtns1_ButtonCommand(object sender, MyCommandEventArgs e)
        {
            ButtonCommand(e);
        }

        protected void MyGridView1_PageIndexChanged(object sender, EventArgs e)
        {
            PageIndexChanged();
        }

        protected void MyGridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            Sorting(e);
        }

        protected void MyGridView1_RowCommand(object sender, CommandEventArgs e)
        {
            RowCommand();
        }
    }
}
