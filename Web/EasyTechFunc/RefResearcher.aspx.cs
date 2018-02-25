using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Net.Json;

namespace MyQuery.Web.EasyTechFunc
{
    public partial class RefResearcher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rqID = Request["rqid"];
            string url = "http://www.kexiaomi.com/api/recommend/researcher?rqid="+rqID;
            string news;
            try
            {
                HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse();
                Stream stream = webreponse.GetResponseStream();
                StreamReader objReader = new StreamReader(stream, System.Text.Encoding.UTF8);
                //byte[] rsByte = new Byte[webreponse.ContentLength];  //save data in the stream
                string m_Html = objReader.ReadToEnd();
                
                //解析JSON语句
                JsonTextParser parser = new JsonTextParser();
                JsonObject obj = parser.Parse(m_Html);               
                int value_code=0;string value_msg=string.Empty;
                foreach (JsonObject field in obj as JsonObjectCollection){
                    string name = field.Name;
                    switch (name)
                    {
                        case "name":
                       value_code=(int)field.GetValue();
                            break;
                        case "msg":
                         value_msg=(string)field.GetValue();
                            break;
                     }
                    }
                if (value_code == 0)
                { news = "自动获取成功！"; }
                else{
                    news = "更新失败" + value_msg;
                }
            }
            catch (Exception)
            { 
               news="系统异常，请稍候再试！";
            }
            //System.Threading.Thread.Sleep(5000);
            Response.Write("<script type='text/javascript'>alert('"+news+"');parent.location.reload();</script>");
           // string Url=
            //Response.Redirect(Url);
        }
    }
}