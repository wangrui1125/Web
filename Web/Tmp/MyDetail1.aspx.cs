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
    public partial class MyDetail1 : BaseDetail
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageLoad(MyBtns1, MyBtns2, phContent, divTab, hrFirefox, divNotes);
           
       
        }

        protected void MyBtns1_ButtonCommand(object sender, MyCommandEventArgs e)
        {
            ButtonCommand(e);
        }
    }
}