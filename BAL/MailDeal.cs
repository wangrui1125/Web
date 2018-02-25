using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MyQuery.DAL;
using MyQuery.Utils;

namespace MyQuery.BAL
{
    /// <summary>
    /// 邮件处理封装
    /// </summary>
    public class MailDeal
    {
        private Dao dao;
        private MyUser currentUser;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MailDeal(MyUser user)
        {
            currentUser = user;
            dao = new Dao();
        }
        /// <summary>
        /// 给常规任务相关人发邮件
        /// </summary>
        /// <param name="mailContent">内容</param>
        /// <param name="userids">多个用,分割</param>
        /// <param name="userid">不空时 则通知其部门领导</param>
        public void SendMailTask(string mailContent, string userids, string userid)
        {
            DataTable dt = null;
            if (String.IsNullOrEmpty(userids))
            {
                dt = dao.GetDataTable(@"select ProjectMilestone.*,s_user.Name as createrName,TechnicManager.Name as TechnicManagerName
                                from ProjectMilestone
                                inner join s_user on ProjectMilestone.UserId=s_user.id
                                left join s_user TechnicManager on ProjectMilestone.TechnicManager=TechnicManager.id
                                where ProjectMilestone.id=" + mailContent);
                userid = dt.Rows[0]["TechnicManager"].ToString();
                userids = dt.Rows[0]["UserId"].ToString() + "," + userid;
                mailContent = TxtHelper.GetString(WebHelper.GetRootServerPath() + "Project\\Mail\\MilestoneTask" + Constants.HTML_SUFFIX);
                string flagDes = "";
                switch (Convert.ToInt32(dt.Rows[0]["iflag"]))
                {
                    case 0:
                        flagDes = "关闭";
                        break;
                    case 1:
                        flagDes = "进行中";
                        break;
                }
                mailContent = String.Format(mailContent, dt.Rows[0]["Name"], currentUser.Name, dt.Rows[0]["createrName"], dt.Rows[0]["TechnicManagerName"], dt.Rows[0]["StartDate"], dt.Rows[0]["EndDate"], flagDes);
            }
            if (!String.IsNullOrEmpty(mailContent) && !String.IsNullOrEmpty(userids))
            {
                string sql = "select email from S_User where id " + SqlHelper.GetSqlInWhere(userids);
                if (!String.IsNullOrEmpty(userid))
                {
                    sql += " or id in (select s_dep.Manager from s_dep inner join s_user on s_dep.id=s_user.depid where s_user.id='" + userid + "')";
                }
                dt = dao.GetDataTable(sql);
                MailHelper.SendMail(DataHelper.GetString(dt.Rows, null), "任务处理提醒邮件", mailContent, null);
            }
        }
        /// <summary>
        /// 给常规任务相关人发邮件
        /// </summary>
        /// <param name="id">id</param>
        public void SendMailTask(int id)
        {
            SendMailTask(id.ToString(), null, null);
        }
        /// <summary>
        /// 给部门经理发邮件
        /// </summary>
        /// <param name="mailContent"></param>
        /// <param name="depid"></param>
        /// <param name="projectId"></param>
        public void SendMailProject(string mailContent, string depid, int projectId)
        {
            if (!String.IsNullOrEmpty(mailContent))
            {
                string sql = "select email from S_User where id in ";
                if (projectId == 0)
                {
                    sql += "(select s_dep.Manager from s_dep where s_dep.id='" + depid + "')";
                }
                else
                {
                    sql += "(select s_dep.Manager from s_dep inner join projectInfo on projectInfo.DepID=s_dep.id where projectInfo.id=" + projectId + ")";
                }
                MailHelper.SendMail(dao.GetScalar(sql), "项目审批提醒邮件", mailContent, null);
            }
        }
        /// <summary>
        /// 发送未通邮件
        /// </summary>
        /// <param name="mailContent"></param>
        /// <param name="ids"></param>
        /// <param name="isLog"></param>
        public void SendMailNoPass(string mailContent, string ids, bool isLog)
        {
            string sql = "select email from S_User where id in ";
            if (isLog)
            {
                sql += "(select worker from ProjectWorkLog where id in (" + ids + "))";
            }
            else
            {
                sql += "(select userid from ProjectInfo where id in (" + ids + "))";
            }
            MailHelper.SendMail(dao.GetScalar(sql), "审批未通过提醒邮件", mailContent, null);
        }
    }
}
