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
using MyQuery.Utils;
using MyQuery.Work;
using System.Data.SqlClient;

namespace MyQuery.Web
{
    
    public partial class _Default : BaseWWW
    {
        protected string welcome = "";       
        protected string Url = "";
     
        protected void Page_Load(object sender, EventArgs e)
        {
            string error = null;
            try
            {
                if (CurrentUser == null)
                {
                    //域账户可以直接访问本页进入
                    string userId = Page.Request.LogonUserIdentity.Name;
                    string DomainName = WebHelper.GetAppConfig("DomainName");
                    
                    if (String.IsNullOrEmpty(userId) || String.IsNullOrEmpty(DomainName))
                    {
                        error = "您没有登录，请使用Login.aspx登陆";
                    }
                    else
                    {
                        if (userId.StartsWith(DomainName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            userId = userId.Substring(DomainName.Length + 1);
                        }
                        else
                        {
                            userId = CookieUserAccount;
                        }
                        if (String.IsNullOrEmpty(userId))
                        {
                            error = "您没有登录域，请使用Login.aspx登陆";
                        }
                        else
                        {
                            Authenticate auth = new Authenticate();
                            auth.GetUser(userId);
                            if (String.IsNullOrEmpty(auth.Error))
                            {
                                SetSessionUser(auth.MyUser);
                                new DataFrom(null, CurrentUser, QueryString).SetLogInfo("成功登录");
                            }
                            else
                            {
                                error = auth.Error + "，如改用其它用户请使用Login.aspx登陆";
                            }
                        }
                    }
                }
                if (String.IsNullOrEmpty(error))
                {
                    string SqlConnectionString;
                    SqlConnection m_Connection;
                    SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
                    m_Connection = new SqlConnection(SqlConnectionString);
                    if (m_Connection.State == System.Data.ConnectionState.Open)
                    {
                        ;

                    }
                    else
                        m_Connection.Open();

                    string c = string.Format("select role from [dbtest].[dbo].[S_User] where s_User.ID = '{0}'", CurrentUser.Id);
                    SqlCommand com = new SqlCommand(c, m_Connection);
                    int role = Convert.ToInt16(com.ExecuteScalar().ToString());


                    c = string.Format("select Name from [dbtest].[dbo].[S_Role] where ID='{0}'", role);
                    com = new SqlCommand(c, m_Connection);
                    string rolename = com.ExecuteScalar().ToString();

                    welcome = "您好！<span>" + CurrentUser.Name+"</span>"
                                + "<a style=\"color:#4d88db\";>(" + rolename + ")</a><br/>今天是<span>" + DateTime.Today.ToString("yyyy年MM月dd日") + "</span>";
                    
                    DefaultMenu urMenu = new DefaultMenu(CurrentUser);
                    menuHolder.Controls.Add(urMenu);
                    Url = urMenu.Url;
                   
                    
                    int Build = 0;
                    int Review = 0;
                    int Do=0;
                    int RealDo = 0;
                    int Admin = 0;
                    int Enterprise = 0;
                    int Manager = 0;
                    int Researcher = 0;
                    int Consult = 0;
                    int techConsult = 0;
                    
                    c = string.Format("select RoleID from [dbtest].[dbo].[S_RoleUser] where S_RoleUser.UID='{0}'",CurrentUser .Id);
                    com = new SqlCommand(c, m_Connection);
                    SqlDataReader MyReader = com.ExecuteReader();
                    while (MyReader.Read())
                    {
                        if (Convert.ToInt16(MyReader[0].ToString()) == 2)
                        {
                            techConsult = 1;
                            Do = 1;
                            RealDo = 1;
                        }
                        else if (Convert.ToInt16(MyReader[0].ToString()) == 3)
                        {
                            Build = 1;
                            Enterprise = 1;
                        }
                        else if (Convert.ToInt16(MyReader[0].ToString()) == 4)
                        {
                            Build = 1;
                            Review = 1;
                            Consult = 1;
                        }
                        else if (Convert.ToInt16(MyReader[0].ToString()) == 5)
                        {
                            Researcher =1;
                        }
                        else if (Convert.ToInt16(MyReader[0].ToString()) == 6)
                        {
                            Review = 1;
                            Do = 1;
                            Manager = 1;
                        }
                        else if (Convert.ToInt16(MyReader[0].ToString()) == 7)
                        {
                            Admin=1;
                        }
                        else
                            ;
                    }
                    MyReader.Read();
                    

                    Session["role"] = role;
                    Session["Build"] = Build;
                    Session["Review"] = Review;
                    Session["Do"] = Do;
                    Session["RealDo"] = RealDo;
                    Session["Admin"] = Admin;
                    Session["Enterprise"] = Enterprise;
                    Session["Consult"] = Consult;
                    Session["Manager"] = Manager;
                    Session["techConsult"] = techConsult;
                    Session["Researcher"] = Researcher;

                   
                }
            }
            catch (Exception ex)
            {
                log4net.ILog logger = log4net.LogManager.GetLogger(this.GetType());
                logger.Error("主页加载失败", ex);
            }
            if (!String.IsNullOrEmpty(error))
            {
                WebHelper.Confirm(Page, error, "Login.aspx", null);
                
            }
        }
    }
}
