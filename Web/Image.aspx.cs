using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MyQuery.Web
{
    public partial class Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //取消缓存
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            //创建随机数对象 
            string checkCode = new Random().Next(100000, 999999).ToString();
            //验证码值存放到Session中用来比较
            Session["checkcode"] = checkCode;
            // 生成图片（增加背景噪音线、前景噪音点）
            string bgFilePath = Server.MapPath("Img\\bg" + new Random().Next(5) + ".jpg");
            System.Drawing.Image imgObj = System.Drawing.Image.FromFile(bgFilePath);
            //建立位图对象
            Bitmap image = new Bitmap(imgObj, checkCode.Length * 15, 25);
            //System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)(checkCode.Length * 15), 25);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                //g.Clear(Color.White);

                //画图片的背景噪音线
                int i;
                for (i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                //定义一个含10种字体的数组
                String[] fontFamily = { "Arial", "Verdana", "Comic Sans MS", "Impact", "Haettenschweiler", "幼圆", "黑体", "隶书", "宋体", "楷体_GB2312" };
                FontStyle[] fontStyle = { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline };
                Font font = new System.Drawing.Font(fontFamily[random.Next(9)], 16, fontStyle[random.Next(5)]);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Response.ClearContent();
                Response.ContentType = "image/Gif";
                Response.BinaryWrite(ms.ToArray());
            }
            catch
            {
                g.Dispose();
                image.Dispose();
            }
        }

    }
}
