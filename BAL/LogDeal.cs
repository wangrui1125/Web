using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using MyQuery.Utils;
using MyQuery.DAL;

namespace MyQuery.BAL
{
    /// <summary>
    /// 系统日志处理
    /// </summary>
    public class LogDeal
    {
        private string userId = String.Empty;
        private string IP = String.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LogDeal()
        {
            if (HttpContext.Current.Session != null)
            {
                MyUser myUser = HttpContext.Current.Session["user"] as MyUser;
                if (myUser != null)
                {
                    userId = myUser.Id;
                }
            }
            IP = HttpContext.Current.Request.UserHostAddress;
        }

         /// <summary>
        /// 构造函数
        /// </summary>
        public LogDeal(string userid)
        {
            if (String.IsNullOrEmpty(userid))
            {
                userId = "";
            }
            else
            {
                userId = userid;
            }
            IP = HttpContext.Current.Request.UserHostAddress;
        }
        /// <summary>
        /// 计入一条日志
        /// </summary>
        /// <param name="des"></param>
        public void Write(string des)
        {
            /*//调用实体类写法
            LogInfo logInfo = new LogInfo();
            logInfo.UserId = userId;
            logInfo.IP = IP;
            logInfo.Des = des;
            logInfo.Insert();
             */
            MySqlParameters parameters = new MySqlParameters("LogInfo");
            parameters.EditSqlMode = SqlMode.Insert;
            parameters.Add("UserId", userId);
            parameters.Add("IP", IP);
            parameters.Add("Des", des);
            parameters.Add("Optime", DateTime.Now);
            Dao dao = new Dao();
            dao.SqlExecute(parameters, true);
        }
    }
}
