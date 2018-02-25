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


namespace MyQuery.Web.Sys
{
    public partial class WorkDaySet : MyQuery.Work.BasePage
    {
        private DataTable dtWorkWeek = null;
        private DataFrom deal = null;
        /// <summary>
        /// 设置WorkWeek信息
        /// </summary>
        /// <returns></returns>
        private void SetWorkWeekInfo()
        {
            DateTime startDate = DateTime.Today.AddMonths(-1);
            DateTime endDate = DateTime.Today.AddMonths(1);
            if (this.IsPostBack && !String.IsNullOrEmpty(Request.Params["__EVENTARGUMENT"]))
            {
                string str = Request.Params["__EVENTARGUMENT"];
                if (str.StartsWith("V"))
                {
                    str = str.TrimStart('V');
                }

                this.ClientScript.RegisterHiddenField("__EVENTARGUMENT", str);

                startDate = new DateTime(2000, 1, 1);
                startDate = startDate.AddDays(Convert.ToInt32(str)).AddMonths(-1);
                endDate = startDate.AddMonths(2);
            }
            string workWeekSql = "SELECT WORKDAY,iflag FROM WORKWEEK WHERE WORKDAY BETWEEN '{0}' AND '{1}'";
            dtWorkWeek = deal.GetDataTable(String.Format(workWeekSql, startDate, endDate));

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            deal = new DataFrom();
            try
            {
                if (!this.IsPostBack)
                {
                    WebHelper.SetControlAttributes(btnBuild, new TextBoxVal[] { StartDate, EndDate });
                    string sDate = deal.GetScalar("SELECT MAX(WORKDAY)+1 FROM WORKWEEK");
                    DateTime startDate = DateTime.Today;
                    if (!String.IsNullOrEmpty(sDate))
                    {
                        StartDate.Enabled = false;
                        startDate = DataHelper.GetDateValue(sDate, DateTime.Today);
                    }
                    StartDate.Text = startDate.ToString(Constants.DATE_FORMART);
                    EndDate.Text = startDate.AddYears(1).ToString(Constants.DATE_FORMART);
                }
                SetWorkWeekInfo();
            }
            catch (Exception ex)
            {
                Logger.Error("初始化失败", ex);
                RedirectError("初始化失败，请稍候再试。原因:" + ex.Message);
                return;
            }
            if (!Page.IsPostBack)
            {
                if (dtWorkWeek == null || dtWorkWeek.Rows.Count == 0)
                {
                    Alert("尚无设置工作日期，请先生成（一般生成一年的数据后再根据节假日调整）");
                }
            }
        }
        protected void btnReturnCurrent_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void calendarWorkDay_SelectionChanged(object sender, EventArgs e)
        {
            SelectWorkDayState();
        }
        protected void calendarWorkDay_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected)
            {
                e.Cell.BackColor = System.Drawing.Color.Empty;

                e.Cell.BorderStyle = BorderStyle.Solid;
            }
            if (e.Day.Date <= DateTime.Today)
            {
                e.Day.IsSelectable = false;
            }

            e.Cell.Attributes["title"] = e.Day.Date.ToString(Constants.DATE_FORMART);

            if (dtWorkWeek != null && dtWorkWeek.Rows.Count > 0)
            {
                if (dtWorkWeek.Select("WORKDAY='" + e.Day.Date.ToString() + "' AND iflag=" + Constants.FALSE_ID).Length > 0)
                {
                    e.Cell.CssClass = "nowork";
                }
            }
        }
        private void SelectWorkDayState()
        {
            if (calendarWorkDay.SelectedDate != DateTime.MinValue)
            {
                DateTime selectedDate = calendarWorkDay.SelectedDate;

                DataRow[] drs = dtWorkWeek.Select("WORKDAY='" + selectedDate.ToString() + "'");
                if (drs != null && drs.Length > 0)
                {
                    WorkDayState.Checked = Constants.TRUE_ID.Equals(drs[0]["iflag"].ToString());
                }
            }
        }
        protected void btnBuild_Click(object sender, EventArgs e)
        {
            DateTime startDate = DataHelper.GetDateValue(StartDate.Text, DateTime.Today);
            DateTime endDate = DataHelper.GetDateValue(EndDate.Text, DateTime.Today);
            if (startDate > endDate)
            {
                Alert("您要生成的日期的开始日期不能大于截止日期", EndDate.ClientID);
                return;
            }
            int weekStart = DataHelper.GetIntValue(WebHelper.GetAppConfig("WeekStart"), 0);
            int weekcount = DataHelper.GetWeekOfYear(startDate);
            //如果开始日期不是周开始日期向前修正为周开始日期
            startDate = DataHelper.GetWeekStart(startDate, weekStart);
            //如果总星期计数存在，则取最大值加1，否则将其置为1
            int weekid = DataHelper.GetIntValue(deal.GetScalar("select max(weekid)+1 from workweek"), 1);
            string insertSql = "insert workweek (weekid, workday, iflag, weeksn) values ({0}, '{1}', {2}, {3})";
            while (startDate <= endDate)
            {
                //周跨年度是作为下一年的 第一周
                if (startDate.AddDays(7).Year > startDate.Year)
                {
                    weekcount = 1;
                }
                for (int j = 0; j < 7; j++)
                {
                    if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        deal.SqlExecute(String.Format(insertSql, weekid, startDate, 0, weekcount));
                    }
                    else
                    {
                        deal.SqlExecute(String.Format(insertSql, weekid, startDate, 1, weekcount));
                    }
                    startDate = startDate.AddDays(1);
                }
                weekcount++;
                weekid++;
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        protected void btnSet_Click(object sender, EventArgs e)
        {
            if (calendarWorkDay.SelectedDate != DateTime.MinValue)
            {
                MySqlParameters mySql = new MySqlParameters("workweek");
                try
                {
                    mySql.EditSqlMode = SqlMode.Update;
                    mySql.Add("iflag", WorkDayState.Checked ? 1 : 0);
                    mySql.Add("workday", calendarWorkDay.SelectedDate, "workday={0}");
                    deal.SqlExecute(mySql);

                    SetWorkWeekInfo();
                }
                catch (Exception ex)
                {
                    Logger.Error(SqlHelper.GetSql(mySql, deal.Dbtype), ex);
                    RedirectError("提交失败，请稍候再试。原因:" + ex.Message);
                }
            }
        }
    }
}
