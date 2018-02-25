using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using MyQuery.Utils;
using MyQuery.Work;

namespace MyQuery.Web.Sys
{
    public partial class down : BaseWWW
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filePath = WebHelper.GetFilePath();
            string fileName = QueryString["f"];
            Response.Clear();
            Response.Buffer = true;

            this.EnableViewState = false;
            if (!String.IsNullOrEmpty(fileName) && !fileName.StartsWith(".."))
            {
                if ("file".Equals(QueryString["t"]))
                {
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(fileName)));
                    //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                    Response.ContentType = "application/octet-stream";
                }
                //文件存在直接获取
                if (File.Exists(filePath + fileName))
                {
                    Response.WriteFile(filePath + fileName);
                }
                else
                {
                    if (fileName.IndexOfAny(("' (").ToCharArray()) != -1)
                    {
                        return;
                    }
                    //需要从数据库先获取
                    DataTable dt = null;
                    MySqlParameters mySql = new MySqlParameters("InfoAdds");
                    mySql.EditSqlMode = SqlMode.Select;
                    mySql.Add(" top 1 content");
                    mySql.Add("FilePath", fileName, "FilePath={0}");
                    try
                    {
                        dt = new DataFrom().GetDataTable(mySql);
                    }
                    catch (Exception ex)
                    {
                        log4net.LogManager.GetLogger(this.GetType()).Error(mySql.GetSql(DBType.Sql2005, false), ex);
                    }
                    if (dt != null
                        && dt.Rows.Count == 1
                        && !Convert.IsDBNull(dt.Rows[0][0]))
                    {
                        byte[] content = (byte[])dt.Rows[0][0];

                        Response.BinaryWrite(content);
                        Response.Flush();

                        System.IO.FileStream fs = new System.IO.FileStream(filePath + fileName, System.IO.FileMode.CreateNew);
                        System.IO.BinaryWriter w = new System.IO.BinaryWriter(fs);
                        w.Write(content);
                        w.Close();
                    }
                }
            }
            Response.End();
        }
    }
}
