using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using MyQuery.Work;
using MyQuery.Utils;
using System.IO;
using log4net;

namespace MyQuery.Web.Tmp
{
    /// <summary>
    /// Summary description for MyService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 获得认证信息
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetAuthenticate(string userAccount, string password)
        {
            Authenticate auth = new Authenticate();
            if (auth.IsPass(userAccount, password))
            {
                return auth.MyUser.Name;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得模板列表信息
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetTemplateList(string userAccount, string password)
        {
            Authenticate auth = new Authenticate();
            if (auth.IsPass(userAccount, password))
            {
                return new DataFrom().GetDataSet("select name,url from functioninfo where url like 'tmp/%' order by url");
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得模板xml信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public XmlDocument GetXml(string name, string userAccount, string password)
        {
            Authenticate auth = new Authenticate();
            if (auth.IsPass(userAccount, password))
            {
                XmlDocument doc = new XmlDocument();
                string fileName = null;
                if (!String.IsNullOrEmpty(name))
                {
                    fileName = WebHelper.GetMyXmlPath() + "\\query\\" + name + Constants.XML_SUFFIX;
                }
                if (!String.IsNullOrEmpty(name) && File.Exists(fileName))
                {
                    doc = FromXml.GetXml(name);
                }
                else
                {
                    doc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><root>" + WebHelper.GetPublicKey() + "</root>");
                }
                return doc;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 保存模板xml信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="xmlstring"></param>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod]
        public string SetXml(string name, string xmlstring, string userAccount, string password)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(xmlstring))
            {
                return Constants.FALSE_ID;
            }
            try
            {
                Authenticate auth = new Authenticate();
                if (auth.IsPass(userAccount, password))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlstring);
                    doc.Save(WebHelper.GetMyXmlPath() + "\\query\\" + name + Constants.XML_SUFFIX);
                    return Constants.TRUE_ID;
                }
                else
                {
                    return Constants.FALSE_ID;
                }
            }
            catch (Exception ex)
            {
                LogManager.GetLogger(this.GetType()).Error("保存xml模板错误", ex);
                return Constants.FALSE_ID;
            }
        }
    }
}
