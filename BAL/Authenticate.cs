using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.Collections.Specialized;
using MyQuery.DAL;
using MyQuery.Utils;

namespace MyQuery.BAL
{
    /// <summary>
    /// 用户认证业务类
    /// by 贾世义 2011-9-8
    /// </summary>
    public class Authenticate
    {
        private string _error = null;
        /// <summary>
        /// 获取错误信息 失败时才有值
        /// </summary>
        public string Error
        {
            get { return _error; }
        }
        private MyUser _myUser = null;
        /// <summary>
        /// 获取错误信息 成功时有效
        /// </summary>
        public MyUser MyUser
        {
            get { return _myUser; }
        }
        private string _table = null;
        /// <summary>
        /// 认证表
        /// </summary>
        public string Table
        {
            set { _table = value; }
        }

        private string _email = null;
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email
        {
            get { return _email; }
        }
        private Dao dao;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Authenticate()
        {
            dao = new Dao();
        }
        /// <summary>
        /// 是否能够通过认证
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsPass(string userAccount, string password)
        {
            if (String.IsNullOrEmpty(_table))
            {
                _error = "没有认证的表";
                return false;
            }
            else
            {
                DataTable dt = getUserTable(userAccount, password);
                if (dt == null || dt.Rows.Count < 1)
                {
                    _error = "获取用户信息出错(账户不存在或密码错误)";
                    return false;
                }
                else if (dt.Columns.Contains("iflag") && !Constants.TRUE_ID.Equals(dt.Rows[0]["iflag"].ToString()))
                {
                    _error = "用户不是[正常]状态";
                    return false;
                }
                else
                {
                    _myUser = new MyUser(dt.Rows[0]["id"].ToString(), password, "");

                    setUserApd(_myUser, dt);

                    return true;
                }
            }
        }
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private DataTable getUserTable(string userAccount, string password)
        {
            MySqlParameters mySql = new MySqlParameters(_table);
            mySql.EditSqlMode = SqlMode.Select;
            mySql.Add("id");
            mySql.Add("name");
            mySql.Add("city");
            mySql.Add("iflag");
            mySql.Add("email");
            mySql.Add("userAccount", userAccount, "and (id={0} or email={0})");
            if (!String.IsNullOrEmpty(password))
            {
                mySql.Add("password", password, "and pwd={0}");
            }
            return dao.GetDataTable(mySql, true);
        }
        /// <summary>
        /// 设定扩展信息
        /// </summary>
        /// <param name="myUser"></param>
        /// <param name="dt"></param>
        private void setUserApd(MyUser myUser, DataTable dt)
        {
            if (dt.Columns.Contains("name"))
            {
                myUser.Name = dt.Rows[0]["name"].ToString();
            }
            if (dt.Columns.Contains("city"))
            {
                myUser.DepID = dt.Rows[0]["city"].ToString();
            }
            if (dt.Columns.Contains("email"))
            {
                _email = dt.Rows[0]["name"].ToString();
            }
        }
        /// <summary>
        /// 通过ID 获得user对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MyUser GetUser(string id)
        {
            MyUser myUser = new MyUser(id, "", "");
            DataTable dt = getUserTable(id, null);
            if (dt == null || dt.Rows.Count < 1)
            {
                _error = "获取用户信息出错(账户不存在)";
            }
            else if (dt.Columns.Contains("iflag") && !Constants.TRUE_ID.Equals(dt.Rows[0]["iflag"].ToString()))
            {
                _error = "用户不是[在职]状态";
            }
            else
            {
                _error = null;
                setUserApd(myUser, dt);
            }
            return myUser;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="old"></param>
        /// <param name="pwd"></param>
        public void UpdatePwd(string old, string pwd)
        {
            MySqlParameters mySql = new MySqlParameters(_table);
            mySql.EditSqlMode = SqlMode.Update;
            mySql.Add("pwd", pwd);
            mySql.Add("userAccount", _myUser.Id, "and id={0}");
            if (!String.IsNullOrEmpty(old))
            {
                mySql.Add("password", old, "and pwd={0}");
            }
            if (dao.SqlExecute(mySql, true) == 0)
            {
                _error = "修改密码错误";
            }
        }
    }
}
