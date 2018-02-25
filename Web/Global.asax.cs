using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using MyQuery.Utils;
using MyQuery.Utils.TimerTask;
using MyQuery.Work;

namespace MyQuery.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("~/") + "log4net.config"));
        }
        /// <summary>
        /// 注册一缓存条目在5分钟内到期，到期后触发的调事件
        /// </summary>
        private void RegisterCacheEntry(string url)
        {
            HttpContext.Current.Cache.Add("TimerTaskNeedCache", url, null, DateTime.MaxValue,
                TimeSpan.FromMinutes(5), System.Web.Caching.CacheItemPriority.NotRemovable,
                new System.Web.Caching.CacheItemRemovedCallback(CacheItemRemovedCallback));
        }

        /// <summary>
        /// 缓存项过期时程序模拟点击页面，阻止应用程序结束
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="reason"></param>
        private void CacheItemRemovedCallback(string key, object value, System.Web.Caching.CacheItemRemovedReason reason)
        {
            System.Net.WebClient client = new System.Net.WebClient();
            try
            {
                client.DownloadData("http://localhost:" + value.ToString() + "/Error.aspx");
            }
            catch (Exception ex)
            {
                //log4net.LogManager.GetLogger(this.GetType()).Error("唤醒页面" + "http://" + value.ToString() + "调用错误", ex);
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Cache.Remove("TimerTaskNeedCache");
                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Application["isNeedTimerTaskCache"] != null && HttpContext.Current.Cache["TimerTaskNeedCache"] == null)
            {
                //缓存端口
                RegisterCacheEntry(HttpContext.Current.Request.Url.Port.ToString());
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}