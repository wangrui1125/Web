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

namespace MyQuery.Web.Tmp
{
    public partial class MyImport : BaseImport
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            PageLoad(ddlTitle, btnAdmin, btnSubmit, type, trDatabase, dbtype, connectionstring, procedurename, method, parameters, IsFirstTitle, ulContent);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SubmitClick(fileImport, divErrFile);
        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            TitleSelectedIndexChanged();
        }

        protected void type_SelectedIndexChanged(object sender, EventArgs e)
        {
            typeSelectedIndexChanged(fileImport);
        }

    }
}
