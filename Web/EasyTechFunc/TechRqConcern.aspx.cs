using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using MyQuery.Utils;
using MyQuery.Work;
using System.Data.SqlClient;
using System.Reflection;


namespace MyQuery.Web.EasyTechFunc
{
    public partial class TechRqConcern : BaseWWW
    {
        protected string Url = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            int Do = Convert.ToInt16(Session["Do"]);

            int Review = Convert.ToInt16(Session["Review"]);
            int Admin = Convert.ToInt16(Session["Admin"]);
            int Enterprise = Convert.ToInt16(Session["Enterprise"]);
            int Researcher = Convert.ToInt16(Session["Researcher"]);
            int Consult = Convert.ToInt16(Session["Consult"]);
            int techConsult = Convert.ToInt16(Session["techConsult"]);
            int Manager = Convert.ToInt16(Session["Manager"]);

            if (Admin == 1 || Researcher == 1 || Enterprise == 1)
            {
                Url = "../Tmp/MyQuery.aspx?n=mytechCon5&ID=" + CurrentUser.Id;
            }
            else
            {
                if (Manager == 1)
                {
                    Url = "../Tmp/MyQuery.aspx?n=mytechCon6&ID=" + CurrentUser.Id;
                }
                else
                {
                    if (Consult == 1 && techConsult == 0)
                    {
                        Url = "../Tmp/MyQuery.aspx?n=mytechCon4&ID=" + CurrentUser.Id;
                    }
                    else if (Consult == 0 && techConsult == 1)
                    {
                        Url = "../Tmp/MyQuery.aspx?n=mytechCon2&ID=" + CurrentUser.Id;
                    }
                    else if (Consult == 1 && techConsult == 1)
                    {
                        Url = "../Tmp/MyQuery.aspx?n=mytechCon6&ID=" + CurrentUser.Id;
                    }
                    else
                    {
                        Url = "../Tmp/MyQuery.aspx?n=mytechCon5&ID=" + CurrentUser.Id;
                    }
                }
            }


                //if (Do == 0 && Review == 0)
                //{
                //    Url = "../Tmp/MyQuery.aspx?n=mytechCon5&ID=" + CurrentUser.Id;
                //}
                //else if (Do == 0 && Review == 1)
                //{
                //    Url = "../Tmp/MyQuery.aspx?n=mytechCon2&ID=" + CurrentUser.Id;
                //}
                //else if (Do == 1 && Review == 0)
                //{
                //    Url = "../Tmp/MyQuery.aspx?n=mytechCon4&ID=" + CurrentUser.Id;
                //}
                //else
                //{
                //    Url = "../Tmp/MyQuery.aspx?n=mytechCon6&ID=" + CurrentUser.Id;
                //}
                //     Url = "../Tmp/MyQuery.aspx?n=mytechCon&ID=" + CurrentUser.Id;
                //Url = "../Tmp/MyQuery.aspx?n=mytechCon&Rq_Type=1";
                Response.Redirect(Url);
            }
        }
    }
