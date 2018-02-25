using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyQuery.DAL;
using MyQuery.Utils;


namespace MyQuery.Entity
{
    ///<summary>
    ///	实体类,表LogInfo
    ///	Created on 2009-05-19 16:14:09
    ///</summary>
    public class LogInfo : IEntity
    {

        #region Private Members

        private int _id;
        private string _UserId;
        private string _IP;
        private string _Des;
        private DateTime _Optime;
        #endregion

        #region Constuctor(s)
        public LogInfo()
        {
            this._id = 0;
            this._UserId = String.Empty;
            this._IP = String.Empty;
            this._Des = String.Empty;
            this._Optime = DateTime.Now;
        }

        public LogInfo(string id)
            : this()
        {
            DataTable dt = FindById(id);
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                Object fv;
                fv = dr["id"];
                if (!Convert.IsDBNull(fv))
                {
                    this._id = Convert.ToInt32(fv);
                }
                fv = dr["UserId"];
                if (!Convert.IsDBNull(fv))
                {
                    this._UserId = Convert.ToString(fv);
                }
                fv = dr["IP"];
                if (!Convert.IsDBNull(fv))
                {
                    this._IP = Convert.ToString(fv);
                }
                fv = dr["Des"];
                if (!Convert.IsDBNull(fv))
                {
                    this._Des = Convert.ToString(fv);
                }
                fv = dr["Optime"];
                if (!Convert.IsDBNull(fv))
                {
                    this._Optime = Convert.ToDateTime(fv);
                }

            }
        }
        #endregion

        #region Public Properties

        public int ID
        {
            get { return _id; }
        }

        public string UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }

        public string Des
        {
            get { return _Des; }
            set { _Des = value; }
        }

        public DateTime Optime
        {
            get { return _Optime; }
            set { _Optime = value; }
        }

        #endregion

        #region IEntity Members
        /// <summary>
        ///将实体插入数据库
        /// </summary>
        /// <returns>主键值，当为String.Empty时执行插入失败</returns>
        public string Insert()
        {
            MySqlParameters mySql = getMySql(SqlMode.Insert);
            mySql.IsAddGetIDSql = true;
            return new Dao().GetScalar(mySql, true);
        }
        /// <summary>
        ///将实体修改提交到数据库
        /// </summary>
        /// <returns>是否成功</returns>
        public bool Update()
        {
            bool result = false;
            MySqlParameters mySql = getMySql(SqlMode.Update);
            result = new Dao().SqlExecute(mySql, true) > 0;
            return result;
        }
        /// <summary>
        /// 获得Sql集合
        /// </summary>
        /// <param name="sqlMode"></param>
        /// <returns></returns>
        private MySqlParameters getMySql(SqlMode sqlMode)
        {
            MySqlParameters mySql = new MySqlParameters("LogInfo");
            mySql.EditSqlMode = sqlMode;
            mySql.Add("UserId", _UserId);
            mySql.Add("IP", _IP);
            mySql.Add("Des", _Des);
            mySql.Add("Optime", _Optime);
            if (sqlMode == SqlMode.Update)
            {
                mySql.Add("ID", _id, "id={0}");
            }
            return mySql;
        }
        /// <summary>
        ///按照主键找到记录
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>包含一条记录的DataTable</returns>
        public DataTable FindById(string id)
        {
            MySqlParameters mySql = new MySqlParameters("LogInfo");
            mySql.EditSqlMode = SqlMode.Select;
            mySql.Add("*");
            mySql.Add("ID", id, "id={0}");
            return new Dao().GetDataTable(mySql, true);
        }
        #endregion
    }
}
