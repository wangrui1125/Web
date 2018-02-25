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
using System.Xml;
using System.IO;

using MyQuery.Work;
using MyQuery.MyControl;
using MyQuery.Utils;

namespace MyQuery.Web.Test
{
    /// <summary>
    /// 编辑举例
    /// 普通窗口 继承MyQuery.Work.BasePage 使用扩展则继承MyQuery.Work.BaseExpand
    /// 使用扩展信息注意 1、仅支持一个表 2、不能支持验证 3、仅处理select部分 4、扩展字段不能和已处理的字段重复 5、需要开发人员拷贝示例代码
    /// </summary>
    public partial class TestEdit : MyQuery.Work.BaseExpand
    {
        private int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 处理扩展信息 需要时完全拷贝
            InitExpand(MyInputs1);
            #endregion

            id = DataHelper.GetIntValue(QueryString["id"], 0);

            if (!Page.IsPostBack)
            {
                btnAdmin.Visible = WebHelper.GetIsAdminDegion() && CurrentUser.IsAdmin();
                if (btnAdmin.Visible)
                {
                    btnAdmin.Attributes.Add("onclick", "document.location.href='../Tmp/EditMyQuery.aspx?" + Constants.MYQUERY_NAME + "=" + name + "&t=s_test&sy=1';return false;");
                }
                //控件客户端控制
                WebHelper.SetControlAttributes(btnSave, new TextBoxVal[] { RDate, txtTitle });
                WebHelper.SetAttributesOfFile(FileName);
                //页面值初始化
                dataFrom.BindListCtrl("select id,case when parentid>0 then '　'+name else name end from s_fun where iflag=1 order by id,sn", FunID, false);
                if (id == 0)
                {
                    RDate.Text = System.DateTime.Today.ToString(Constants.DATE_FORMART);
                }
                else //当传入ID 时进行页面值初始化
                {
                    try
                    {
                        //利用MySqlParameters 不用拼写SQL
                        setMySql(SqlMode.Select);
                        //自己给控件赋值
                        setControl(dataFrom.GetDataTable(mySql));
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(SqlHelper.GetSql(mySql,dataFrom.Dbtype), ex);
                        Alert("初始化失败，请稍候再试");
                    }
                }
            }

            #region 处理扩展信息 控件生成
            DataBind(MyInputs1);
            #endregion
        }

        private void setControl(DataTable dt)
        {
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                WebHelper.SetControl(dt, RDate);
                WebHelper.SetControl(dt, FunID);
                txtTitle.Text = dr["title"].ToString();
                WebHelper.SetControl(dt,Notes);
                string fileName = dr["FileName"].ToString();
                if (!String.IsNullOrEmpty(fileName))
                {
                    FileImg.Visible = true;
                    FileImg.ImageUrl = "../Sys/Down.aspx?t=file&f=Test/Pimg" + id + fileName;
                }
                WebHelper.SetControl(dt, IClass);
                WebHelper.SetControl(dt, IFlag);

                #region 处理扩展信息 控件赋值
                SetExpand(MyInputs1, dt);
                #endregion

            }
        }
        /// <summary>
        /// 组织MySqlParameters
        /// </summary>
        /// <param name="sqlMode">SQL语句类别</param>
        private void setMySql(SqlMode sqlMode)
        {
            mySql = new MySqlParameters("S_Test");
            if (sqlMode == SqlMode.Select)
            {
                mySql.EditSqlMode = SqlMode.Select;
                mySql.Add("id", id, " and id={0}");
            }
            else if (id == 0)
            {
                mySql.EditSqlMode = SqlMode.Insert;
            }
            else
            {
                mySql.EditSqlMode = SqlMode.Update;
                mySql.Add("id", id, " and id={0}");
            }
            WebHelper.SetMySql(mySql, RDate);
            WebHelper.SetMySql(mySql, FunID);
            mySql.Add("title", txtTitle.Text);
            WebHelper.SetMySql(mySql, Notes);
            if (sqlMode == SqlMode.Select)
            {
                mySql.Add("FileName", null);
            }
            else if (!WebHelper.IsNullOrEmpty(FileName))
            {
                mySql.Add("FileName", getFileName(mySql.EditSqlMode));
            }
            WebHelper.SetMySql(mySql, IClass);
            WebHelper.SetMySql(mySql, IFlag);
            mySql.Add("optime", DateTime.Now);
            mySql.Add("UserID", CurrentUser.Id);

            #region 处理扩展信息 将字段、值 加入mySql
            AddMySql(MyInputs1);
            #endregion
        }

        private string getFileName(SqlMode sqlMode)
        {
            if (sqlMode == SqlMode.Select
                || String.IsNullOrEmpty(FileName.Value))
            {
                return "";
            }
            else
            {
                return Path.GetExtension(FileName.Value);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!WebHelper.IsNullOrEmpty(FileName))
            {
                if (!WebHelper.IsImageFile(FileName.Value))
                {
                    Alert("截图文件必须为png、jpg、jpeg文件", FileName.ClientID);
                    return;
                }
            }

            bool isOk = true;
            try
            {
                setMySql(SqlMode.Update);
                if (id == 0)
                {
                    mySql.IsAddGetIDSql = true;
                    id = DataHelper.GetIntValue(dataFrom.GetScalar(mySql), 0);
                }
                else
                {
                    isOk = dataFrom.SqlExecute(mySql) == 1;
                }
                if (!WebHelper.IsNullOrEmpty(FileName))
                {
                    string fileName = getFileName(mySql.EditSqlMode);
                    string sPath = WebHelper.GetFilePath() + "Test/"; //物理路径
                    if (!Directory.Exists(sPath))
                    {
                        Directory.CreateDirectory(sPath);
                    }

                    FileName.PostedFile.SaveAs(sPath + "Pimg" + id + fileName);
                }
            }
            catch (Exception ex)
            {
                isOk = false;
                Logger.Error(SqlHelper.GetSql(mySql,dataFrom.Dbtype), ex);
            }
            if (isOk)
            {
                Close(null);
            }
            else
            {
                Close("提交失败，请稍后再试");
            }
        }
    }
}
