using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyQuery.Utils;
using MyQuery.Utils.TimerTask;
using System.Data;
using MyQuery.DAL;

namespace MyQuery.BAL
{
    /// <summary>
    /// 未填写日志提醒邮件 定时任务
    /// by 贾世义 2011-07-04
    /// </summary>
    public class TimerTaskUnWrite : ITimerTask
    {
        int _days = 1;
        bool _toHeader = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="days">超n天未填写日志，则发邮件提醒</param>
        /// <param name="toHeader">是否 同时发邮件提醒部门经理督促</param>
        public TimerTaskUnWrite(string days, string toHeader)
        {
            if (!String.IsNullOrEmpty(days))
            {
                _days = DataHelper.GetIntValue(days, 1);
            }
            _toHeader = Constants.TRUE_ID.EndsWith(toHeader);
        }
        /// <summary>
        /// 执行任务
        /// </summary>
        public void Run()
        {
            string msg = "任务开始时间：" + DateTime.Now.ToString();
            try
            {
                //查找超时未填报任务
                string sql = @"select S_User.Name,S_User.EMail
,(select max(workday) as workday from WorkWeek where datediff(day,WorkDay,getdate())>"
                    + _days + @" and Iflag=1) as workday"
                    + (_toHeader ? ",manager.EMail as HeaderEmail" : "") + @"
 from S_User";
                if (_toHeader)
                {
                    sql += @" left join s_dep on s_dep.id=s_user.depid
 left join s_user manager on s_dep.Manager=manager.id";
                }
                sql += @" where S_User.Iflag='1' and S_User.IsEffort=1 and s_user.id in (select distinct s_user.id
 from S_User
 inner join WorkWeek d on d.WorkDay>s_user.startdate
 left join ProjectWorkLog l on l.WorkDay=d.WorkDay and s_user.id=l.worker and l.EffortState=1
 where d.Iflag=1 and d.workday<=(select max(workday) as workday from WorkWeek where datediff(day,WorkDay,getdate())>" + _days + @" and Iflag=1)
 group by s_user.id,d.workday
 having isnull(sum(l.Effort),0)<8)";
                DataTable dt = new Dao().GetDataTable(sql);
                if (dt == null || dt.Rows.Count == 0)
                {
                    msg += "，没有超时未填报的人员";
                }
                else
                {
                    msg += "，超时未填报共" + dt.Rows.Count + "人";
                    string mailContent = TxtHelper.GetString(WebHelper.GetRootServerPath() + "Log\\Mail\\UnWrite" + Constants.HTML_SUFFIX);
                    string mailHeader = null;
                    if (_toHeader)
                    {
                        mailHeader = TxtHelper.GetString(WebHelper.GetRootServerPath() + "Log\\Mail\\UnWriteHeader" + Constants.HTML_SUFFIX);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        msg += "\r\n给" + dr["EMail"].ToString() + "发邮件";
                        try
                        {
                            MailHelper.SendMail(dr["EMail"].ToString(), "日志未填写提醒", String.Format(mailContent, dr["workday"]), null);
                            if (_toHeader && !String.IsNullOrEmpty(mailHeader))
                            {
                                msg += "，同时给领导" + dr["HeaderEmail"].ToString() + "发邮件";
                                MailHelper.SendMail(dr["HeaderEmail"].ToString(), "督促日志按时填写提醒", String.Format(mailHeader, dr["Name"], dr["workday"]), null);
                            }
                        }
                        catch (Exception ex)
                        {
                            msg += "，处理异常：" + ex.Message + "已放弃";
                        }
                    }
                }
                msg += "。完成";
            }
            catch (Exception ex)
            {
                msg += "，处理异常：" + ex.Message + "已放弃";
            }
            msg += "时间：" + DateTime.Now.ToString();
            LogHelper.WriteToFile("UnWrite", msg);
        }
    }
}
