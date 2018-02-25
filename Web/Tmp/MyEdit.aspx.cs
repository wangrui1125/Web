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
using MyQuery.Work;
using MyQuery.MyControl;

namespace MyQuery.Web.Tmp
{
    /// <summary>
    /// 编辑模板
    /// 参数：n对应的编辑定制xml的名字（不含后缀）
    /// </summary>
    public partial class MyEdit: BaseEdit
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PageLoad(MyInputs1, MyBtns1, MyBtns2, form1, divTab, hrFirefox, divNotes);
        }

        protected void MyBtns1_ButtonCommand(object sender, MyCommandEventArgs e)
        {
            ButtonCommand(e);
        }
    }
}
