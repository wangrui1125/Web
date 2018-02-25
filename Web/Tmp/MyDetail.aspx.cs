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
using MyQuery.Work;
using MyQuery.MyControl;

namespace MyQuery.Web.Tmp
{
    /// <summary>
    /// 详述模板
    /// </summary>
    public partial class MyDetail : BaseDetail
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PageLoad(MyBtns1, MyBtns2, phContent, divTab, hrFirefox, divNotes);
            divTitle.Visible = !"0".Equals(Page.Request.QueryString["title"]);
        }

        protected void MyBtns1_ButtonCommand(object sender, MyCommandEventArgs e)
        {
            ButtonCommand(e);
        }

    }
}
