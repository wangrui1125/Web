using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyQuery.Entity
{
    /// <summary>
    /// 实体类处理接口
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 将实体插入数据库
        /// </summary>
        /// <returns>主键值</returns>
        string Insert();
        /// <summary>
        /// 将实体修改提交到数据库
        /// </summary>
        /// <returns></returns>
        bool Update();
        /// <summary>
        /// 按照主键找到记录
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>一条记录的DataTable</returns>
        DataTable FindById(string id);
    }
}
