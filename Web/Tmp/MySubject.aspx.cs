using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using MyQuery.Utils;
using MyQuery.Work;
using MyQuery.MyControl;

namespace MyQuery.Web.Tmp
{
    /// <summary>
    /// 查询模板
    /// 参数：n对应的查询定制xml的名字（不含后缀）
    /// </summary>
    public partial class MySubject : BasePage
    {
        #region 内部变量与属性
        /// <summary>
        /// 设置获取参数集合 (Session保存 导航到其他页使用)
        /// </summary>
        private MySqlParameters sqlParameters
        {
            get { return Page.Session[Constants.MYGRIDVIEW + name] as MySqlParameters; }
            set { Page.Session[Constants.MYGRIDVIEW + name] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            name = QueryString[Constants.MYQUERY_NAME];
            //仅首次加载初始化
            if (String.IsNullOrEmpty(name))
            {
                Close("模板运行需要传递n参数,即对应的查询定制xml的名字（不含后缀）");
                return;
            }
            if (!Page.IsPostBack)
            {
                btnUse.Attributes.Add("onclick", "if (getSelectId('" + name + Constants.MYQUERY_SELECTID + "')) {return true;} else {alert('请先选中一个方案，再点[使用]');return false;}");
                btnDel.Attributes.Add("onclick", "if (getSelectId('" + name + Constants.MYQUERY_SELECTID + "')) {return true;} else {alert('请先选中一个方案，再点[删除]');return false;}");
            }
            //初始化参数
            MyInputs1.InputsType = MyInputsType.Detail;
            //初始化样式
            Template.SetGridViewStyle(MyGridView1);

            XmlDocument doc = null;
            //xml获取和基本判断
            try
            {
                doc = FromXml.GetXml(name);
            }
            catch (Exception ex)
            {
                Logger.Error("读取xml错误", ex);
                RedirectError("初始化失败，请稍候再试。原因:" + ex.Message);
                return;
            }
            //开始加载xml
            XmlNode myquery = XmlHelper.GetNode(doc.DocumentElement, "myquery");
            if (myquery == null)
            {
                Close("xml文件不是系统提供的定制查询配置文件");
                return;
            }
            //设置分页属性
            FromXml.SetGridViewPage(null, MyGridView1);
            try
            {
                //获得数据来源
                DataFrom dataFrom = new DataFrom(myquery, CurrentUser, QueryString);
                MyGridView1.Columns = FromXml.GetSubjectColumns(myquery, dataFrom, MyInputs1, sqlParameters);
                MyGridView1.KeyColumnNames = "subject-id";

                if (!Page.IsPostBack)
                {
                    MyGridView1.DataBind(Template.GetSubjectData(name, MyInputs1.Columns, CurrentUser.Id));
                    MyInputs1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("初始化失败", ex);
                RedirectError("初始化失败，请稍候再试。原因:" + ex.Message);
            }
        }
        /// <summary>
        /// 页变更回发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MyGridView1_PageIndexChanged(object sender, EventArgs e)
        {
            MyGridView1.DataSource = Template.GetSubjectData(name, MyInputs1.Columns, CurrentUser.Id);
            MyInputs1.DataBind();
        }
        /// <summary>
        /// 排序回发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MyGridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            MyGridView1.DataSource = Template.GetSubjectData(name, MyInputs1.Columns, CurrentUser.Id);
            MyInputs1.DataBind();
        }
        /// <summary>
        /// 使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUse_Click(object sender, EventArgs e)
        {
            string id = Page.Request.Form[name + Constants.MYQUERY_SELECTID];
            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    Template.SetSubjectParameters(sqlParameters, MyInputs1.Columns, id);
                }
                catch (Exception ex)
                {
                    Logger.Error("获取方案条件失败", ex);
                }
                Close(null);
            }
            else
            {
                MyInputs1.DataBind();
                MyGridView1.DataBind(Template.GetSubjectData(name, MyInputs1.Columns, CurrentUser.Id));
                Alert("请先选中一个方案，再点[使用]");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            bool isOk = true;
            try
            {
                //添加
                MySqlParameters mySqlParameters = new MySqlParameters(null);
                mySqlParameters.Add("name", name, null);
                mySqlParameters.Add("title", txtName.Text, null);
                mySqlParameters.Add("userid", ckbPrivate.Checked ? CurrentUser.Id : "public", null);
                mySqlParameters.Add("creater", CurrentUser.Id, null);
                isOk = Template.SaveSubjectData(mySqlParameters, MyInputs1.Columns);
            }
            catch (Exception ex)
            {
                isOk = false;
                Logger.Error("添加查询方案失败", ex);
                RedirectError("添加失败，请稍候再试。原因:" + ex.Message);
                return;
            }
            if (isOk)
            {
                Close(null, false);
            }
            else
            {
                MyInputs1.DataBind();
                MyGridView1.DataBind(Template.GetSubjectData(name, MyInputs1.Columns, CurrentUser.Id));
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            bool isOk = true;
            string id = Page.Request.Form[name + Constants.MYQUERY_SELECTID];
            if (!String.IsNullOrEmpty(id))
            {
                try
                {
                    isOk = Template.DelSubjectData(id, CurrentUser.Id);
                }
                catch (Exception ex)
                {
                    isOk = false;
                    Logger.Error("删除查询方案失败", ex);
                    RedirectError("删除查询方案失败，请稍候再试。原因:" + ex.Message);
                }
                if (!isOk)
                {
                    Alert("您不能删除别人的查询方案");
                }
            }
            else
            {
                isOk = false;
                Alert("请先选中一个方案，再点[使用]");
            }
            MyInputs1.DataBind();
            MyGridView1.DataBind(Template.GetSubjectData(name, MyInputs1.Columns, CurrentUser.Id));
        }
    }
}
