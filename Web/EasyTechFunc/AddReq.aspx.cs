using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml;
using MyQuery.Utils;
using MyQuery.Work;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace MyQuery.Web.EasyTechFunc
{

    public enum FILETYPE:int
    {
        PDF, DOC, DOCX, EXCEL, TXT, NONE
    };
    public partial class AddReq : MyQuery.Work.BasePage
    {
        string[] result;
        string[] mymatch;
        int ReqItemCnt = 29;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect(Request.RawUrl);
        }
        protected int ReadDOCFile()
        {
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word._Application oWord = new Microsoft.Office.Interop.Word.Application();
            _Document oDoc = oWord.Documents.Open(ViewState["ServerFileFullName"],
            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
            ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            int index,tableindex,rowindex,colindex;
                ArrayList ResultTemp = new ArrayList();
                ArrayList mymatchTemp = new ArrayList();
                string text;string[] textresult;
                bool headsign;
                int temprowindex;
                bool noTable = false;
                try {
            switch (tableStyle.Items[tableStyle.SelectedIndex].Text)
            {
                case "段落方式":
                    text = oDoc.Content.Text;
                    result = Regex.Split(text, @"[\r\t]*([\S| ]*)[：:]+");
                    MatchCollection match;
                    match = Regex.Matches(text, @"[\r\t]*([\S| ]*)[：:]+");
                    string firststring = Regex.Replace(match[0].Value, @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
                    for (index = 1; index < match.Count; index++) if (Regex.Replace(match[index].Value, @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "") == firststring) break;
                    mymatch = new string[index];
                    for (int i = 0; i < index; i++) mymatch[i] = match[i].Value;
                    if (mymatch.Length < 1)
                    {
                        myinfo.InnerText = "出错了，请检查：是否误选了段落方式？文档的格式是否符合要求？";
                        myinfo.Visible = true;
                        oDoc.Close();
                        oWord.Quit();
                        return 2;                        
                    }
                    break;
                case "表格方式：第一行是标签":
                    if (oDoc.Tables.Count == 0)
                    {
                        noTable = true;
                        break;
                    }
                    headsign = true;
                    for (tableindex = 1; tableindex <= oDoc.Tables.Count; tableindex++)
                    {
                        for (rowindex = 1; rowindex <= oDoc.Tables[tableindex].Rows.Count; rowindex++)
                        {
                            if (oDoc.Tables[tableindex].Rows[rowindex].Cells.Count < 2) continue;
                            if (headsign)
                            {
                                temprowindex = 2;
                                while (temprowindex <= oDoc.Tables[tableindex].Rows[rowindex].Cells.Count)
                                {
                                    if (oDoc.Tables[tableindex].Cell(rowindex, temprowindex) != null) break;     //未验证过
                                    temprowindex++;
                                }
                                if (temprowindex > oDoc.Tables[tableindex].Rows[rowindex].Cells.Count) continue;
                                headsign = false;
                                for (colindex = 1; colindex <= oDoc.Tables[tableindex].Rows[rowindex].Cells.Count; colindex++)
                                {
                                    mymatchTemp.Add(oDoc.Tables[tableindex].Rows[rowindex].Cells[colindex].Range.Text);
                                }
                            }
                            else
                            {
                                for (colindex = 1; colindex <= oDoc.Tables[tableindex].Rows[rowindex].Cells.Count; colindex++)
                                {

                                    ResultTemp.Add(mymatchTemp[colindex - 1]);
                                    temprowindex = rowindex;
                                    //while (oDoc.Tables[tableindex].Rows[temprowindex].Cells[colindex] == null) temprowindex--;    //未验证过
                                    if (oDoc.Tables[tableindex].Rows[temprowindex].Cells[colindex] == null)
                                    {
                                        ResultTemp.Add(" ");
                                    }
                                    else
                                    {
                                        ResultTemp.Add(oDoc.Tables[tableindex].Rows[temprowindex].Cells[colindex].Range.Text);
                                    }
                                    //ResultTemp.Add(oDoc.Tables[tableindex].Rows[temprowindex].Cells[colindex].Range.Text);
                                }
                            }
                        }
                    }
                    break;
                case "表格方式：第一列是标签":
                    if (oDoc.Tables.Count == 0)
                    {
                        noTable = true;
                        break;
                    }
                    for (tableindex = 1; tableindex <= oDoc.Tables.Count; tableindex++)
                    {
                        for (rowindex = 1; rowindex <= oDoc.Tables[tableindex].Rows.Count; rowindex++)
                        {
                            if (oDoc.Tables[tableindex].Rows[rowindex].Cells.Count == 1)
                            {
                                text = oDoc.Tables[tableindex].Rows[rowindex].Cells[1].Range.Text;
                                textresult = Regex.Split(text, @"[\r\t]*([\S ]*)[：:]+");
                                match = Regex.Matches(text, @"[\r\t]*([\S| ]*)[：:]+");
                                if (tableindex == 1)
                                {
                                    for(int ind = 0;ind<match.Count;ind++)mymatchTemp.Add(match[ind].Value);
                                }
                                for (colindex = 0; colindex < textresult.Length; colindex++)
                                {
                                    ResultTemp.Add(textresult[colindex]);
                                }
                            }
                            else
                            {
                                for (colindex = 1; colindex <= oDoc.Tables[tableindex].Rows[rowindex].Cells.Count; colindex++)
                                {
                                    if (tableindex == 1 && colindex % 2 != 0)
                                    {
                                        mymatchTemp.Add(oDoc.Tables[tableindex].Rows[rowindex].Cells[colindex].Range.Text);
                                    }
                                    ResultTemp.Add(oDoc.Tables[tableindex].Rows[rowindex].Cells[colindex].Range.Text);
                                }
                            }
                        }
                    }
                    break;
            }
            if (noTable)
            {
                myinfo.InnerText = "出错了：您选择了表格的插入方式，但文档中并没有表格";
                myinfo.Visible = true;
                oDoc.Close();
                oWord.Quit();
                return 2;
             }
            if (tableStyle.Items[tableStyle.SelectedIndex].Text != "段落方式")
            {
                result = new string[ResultTemp.Count];
                mymatch = new string[mymatchTemp.Count];
                for (index = 0; index < ResultTemp.Count; index++) result[index] = ResultTemp[index].ToString();
                for (index = 0; index < mymatchTemp.Count; index++) mymatch[index] = mymatchTemp[index].ToString();
            }
                }
                catch (Exception ex)
                {
                    myinfo.InnerText = "出错了：请检查数据的格式:1,多个数据之间格式是否相同；2,是否选择了正确的表格识别插入方式";
                    myinfo.Visible = true;
                    oDoc.Close();
                    oWord.Quit();
                    return 2;
                }
            oDoc.Close();
            oWord.Quit();
            return 1;
        }
        protected int ReadDOCXFile()
        {
            return ReadDOCFile();
        }
        protected int ReadEXCELFile()
        {
            //创建Application对象
            object oMissing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Application xApp = new Microsoft.Office.Interop.Excel.Application();
            xApp.Visible = false;   //得到WorkBook对象, 可以用两种方式之一: 下面的是打开已有的文件
            xApp.UserControl = true;
            Microsoft.Office.Interop.Excel.Workbook xBook = xApp.Workbooks._Open(ViewState["ServerFileFullName"].ToString(),
                oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing, oMissing, oMissing, oMissing,
                oMissing, oMissing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = xBook.Sheets[1];
            int index = 1;
            ArrayList ResultTemp = new ArrayList();
            ArrayList mymatchTemp = new ArrayList();
            bool headsign;
            int rowindex,colindex,temprowindex;
            string text;string[] textresult;
            try
            {
                switch (tableStyle.Items[tableStyle.SelectedIndex].Text)
                {
                    case "段落方式":
                myinfo.InnerText = "Excel文件请选择表格方式";
                myinfo.Visible = true;
                xBook.Close();
                xApp.Quit();
                        return 2;
                        break;
                    case "表格方式：第一行是标签":
                        headsign = true;
                    for (rowindex = 1; rowindex <= worksheet.UsedRange.Rows.Count; rowindex++)
                    {
                        if (worksheet.UsedRange.Rows[rowindex].Cells.Count < 2) continue;
                        if (headsign)
                        {
                            temprowindex = 2;
                            while (temprowindex <= worksheet.UsedRange.Rows[rowindex].Cells.Count)
                            {
                                if (worksheet.Cells[rowindex, temprowindex].Value != null) break;
                                temprowindex++;
                            }
                            if (temprowindex > worksheet.UsedRange.Rows[rowindex].Cells.Count) continue;
                            headsign = false;
                            for (colindex = 1; colindex <= worksheet.UsedRange.Rows[rowindex].Cells.Count; colindex++)
                            {
                                if (worksheet.Cells[rowindex, colindex].Value != null) mymatchTemp.Add(worksheet.Cells[rowindex, colindex].Value);
                            }
                        }
                        else
                        {
                            for (temprowindex = 1; temprowindex <= mymatchTemp.Count; temprowindex++) if (worksheet.Cells[rowindex, temprowindex].Value != null) break;
                            if (temprowindex > mymatchTemp.Count) continue;
                            for (colindex = 1; colindex <= mymatchTemp.Count; colindex++)
                            {
                                ResultTemp.Add(mymatchTemp[colindex - 1]);
                                temprowindex = rowindex;
                                //while (worksheet.Cells[temprowindex, colindex].Value == null) temprowindex--;
                                if (worksheet.Cells[temprowindex, colindex].Value == null)
                                {
                                    ResultTemp.Add(" ");
                                }
                                else
                                {
                                    ResultTemp.Add(worksheet.Cells[temprowindex, colindex].Value);
                                }
                                
                            }
                        }
                    }
                        break;
                    case "表格方式：第一列是标签":
                                           for (rowindex = 1; rowindex <= worksheet.UsedRange.Rows.Count; rowindex++)
                        {
                            if (worksheet.UsedRange.Rows[rowindex].Cells.Count == 1)
                            {
                                text = worksheet.UsedRange.Rows[rowindex].Cells[1].Range.Text;
                                textresult = Regex.Split(text, @"[\r\t^]+([\S ]*)[：:\r]+");
                                if (textresult.Length >= 2)
                                {
                                    for (colindex = 0; colindex < textresult.Length; colindex++)
                                    {
                                        ResultTemp.Add(textresult[colindex]);
                                        if (Regex.IsMatch(textresult[colindex], @"[企业主要产品|企业科技需求|企业技术难题]+")) mymatchTemp.Add(textresult[colindex]);
                                    }
                                }
                            }
                            else
                            {
                                for (colindex = 1; colindex <= worksheet.UsedRange.Rows[rowindex].Cells.Count; colindex++)
                                {
                                    if (colindex % 2 != 0)
                                    {
                                        mymatchTemp.Add(worksheet.UsedRange.Rows[rowindex].Cells[colindex].Range.Text);
                                    }
                                    ResultTemp.Add(worksheet.UsedRange.Rows[rowindex].Cells[colindex].Range.Text);
                                }
                            }
                        }
                        break;
                }
                result = new string[ResultTemp.Count];
                mymatch = new string[mymatchTemp.Count];
                for (index = 0; index < ResultTemp.Count; index++)
                    result[index] = ResultTemp[index].ToString();
                for (index = 0; index < mymatchTemp.Count; index++)
                    mymatch[index] = mymatchTemp[index].ToString();
                xBook.Close();
                xApp.Quit();
                return 1;
            }
            catch (Exception ex)
            {
                myinfo.InnerText = "出错了：请检查:1,多个数据之间格式是否相同；2,是否选择了正确的表格识别插入方式；3，表格是否规范（是否有未使用的单元格）";
                myinfo.Visible = true;
                xBook.Close();
                xApp.Quit();
                return 2;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //myinfo5.Visible = true;
            //myinfo5.InnerText = "12222222222222222222";
            switch (btnSubmit.Text)
            {

                case "读取文件":
                    myinfo.Visible = false;
                    if (fileId.PostedFile.ContentLength != 0)
                    {

                        DateTime now = DateTime.Now;
                        ViewState["ServerFileName"] = now.ToFileTimeUtc().ToString();
                        ViewState["ServerFileFullName"] = WebHelper.GetAppConfig("filepath") + "\\Temp\\" + ViewState["ServerFileName"];
                       // ViewState["ServerFileFullName"] = "D:\\easytechnology\\Code\\file\\Temp" + ViewState["ServerFileName"];
                        ViewState["m_FileType"] = FILETYPE.NONE;
                        switch (fileId.PostedFile.ContentType)
                        {
                            case "application/pdf":
                                ViewState["ServerFileFullName"] += ".pdf";
                                ViewState["m_FileType"] = FILETYPE.PDF;
                                break;
                            case "application/msword":
                                ViewState["ServerFileFullName"] += ".doc";
                                ViewState["m_FileType"] = FILETYPE.DOC;
                                break;
                            case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                                ViewState["ServerFileFullName"] += ".docx";
                                ViewState["m_FileType"] = FILETYPE.DOCX;
                                break;
                            case "text/plain":
                                ViewState["ServerFileFullName"] += ".txt";
                                ViewState["m_FileType"] = FILETYPE.TXT;
                                break;
                            case "application/vnd.ms-excel":
                                ViewState["ServerFileFullName"] += ".xls";
                                ViewState["m_FileType"] = FILETYPE.EXCEL;
                                break;
                        }
                        if ((FILETYPE)ViewState["m_FileType"]!=FILETYPE.NONE)
                        {
                            fileId.PostedFile.SaveAs(ViewState["ServerFileFullName"].ToString());
                            switch ((FILETYPE)ViewState["m_FileType"])
                            {
                                case FILETYPE.PDF:
                                    break;
                                case FILETYPE.DOC:
                                    switch (ReadDOCFile())
                                    {
                                        case 1:
                                            break;
                                        case 2:
                                            return;
                                        default:
                                            break;
                                    }
                                    break;
                                case FILETYPE.DOCX:
                                    switch (ReadDOCXFile())
                                    {
                                        case 1:
                                            break;
                                        case 2:
                                            return;
                                        default:
                                            break;
                                    }
                                    break;
                                case FILETYPE.EXCEL:
                                    switch (ReadEXCELFile())
                                    {
                                        case 1:
                                            break;
                                        case 2:
                                            return;
                                        default:
                                            break;
                                    }
                                        break;
                                case FILETYPE.TXT:
                                    break;
                            }

                            //下面这句可能有问题。。。
                           // System.Diagnostics.Process.Start(ViewState["ServerFileFullName"].ToString());
                            ProcessString();
                            btnSubmit.Text = "导入数据库";
                            fileId.Visible = false;
                        }
                    }
                    break;
                case "导入数据库":
                    myinfo.Visible = false;
                    string temp = WebHelper.GetAppConfig("filepath") +"\\"+ViewState["ServerFileName"];
                    switch ((FILETYPE)ViewState["m_FileType"])
                    {
                        case FILETYPE.DOC:
                            temp += ".doc";
                            break;
                        case FILETYPE.DOCX:
                            temp += ".docx";
                            break;
                        case FILETYPE.EXCEL:
                            temp += ".xls";
                            break;
                        case FILETYPE.PDF:
                            temp += ".pdf";
                            break;
                        case FILETYPE.TXT:
                            temp += ".txt";
                            break;
                    }
                    File.Copy(ViewState["ServerFileFullName"].ToString(), temp);
                    ViewState["ServerFileFullName"] = temp;
                    //将字符串导入相应数据库字段
                    switch (ProcessDataBase())
                    {
                        case 1:
                            btnSubmit.Text = "完成";
                            break;
                        case 2:
                            myinfo.Visible = true;
                            string temp_ = string.Format("出错内容：\n '{1}'  出错标签：\n '{0}'", ViewState["ErrorLabel"], ViewState["UnMatched"]);
                            myinfo5.InnerText = temp_;
                            myinfo5.Visible = true;
                            btnSubmit.Text = "完成";
                            break;
                    }
                    TableAddReq.Visible = false;
                    break;
                case "完成":
                    Response.Redirect(Request.RawUrl);
                    break;
            
            
            }
        }
        protected void ProcessString()
        {
            HtmlTableRow row;
            HtmlTableCell cell_select;
            HtmlTableCell cell;
            HtmlSelect select;
            ListItem item;
            string firststring = mymatch[0];
            firststring = Regex.Replace(firststring, @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
            string temp_string = firststring;
            int index = 0;
            bool setlabel = false;
            string labelfile;
            int[] SelectLabel = null;
            string[] labelset;
            try
            {
                FileStream fs = new FileStream(WebHelper.GetAppConfig("filepath")+"//"+"selectedLabel.txt", FileMode.Open);
                StreamReader sw = new StreamReader(fs, Encoding.Default);
                labelfile = sw.ReadToEnd();
                sw.Close(); fs.Close();
                labelset = Regex.Split(labelfile, @",");
                if (labelset.Length > 1)
                {
                    SelectLabel = new int[labelset.Length - 1];
                    for (int i_ = 0; i_ < labelset.Length - 1; i_++)
                    {
                        SelectLabel[i_] = Convert.ToInt32(labelset[i_]);
                    }
                }
                if (SelectLabel != null && SelectLabel.Length == mymatch.Length)
                {
                    setlabel = true;
                }
            }
            catch(System.IO.FileNotFoundException ex)
            {
                setlabel = false;
            }
            do
            {
                row = new HtmlTableRow();
             
                cell = new HtmlTableCell();
                cell.ID = "Label" + index;
                cell.InnerText = temp_string;
                row.Cells.Add(cell);
                select = new HtmlSelect();
                select.ID = "select" + index;
                item = new ListItem("不作为条目录入（合并到上一个条目中）");   //0
                select.Items.Add(item);
                item = new ListItem("技术需求名称");                           //1
                select.Items.Add(item);
                item = new ListItem("科技需求内容(企业填写)");                 //2
                select.Items.Add(item);
                item = new ListItem("所属领域");                               //3 
                select.Items.Add(item);
                item = new ListItem("科技需求类型");                           //4
                select.Items.Add(item);
                item = new ListItem("预期结果");                                //5 
                select.Items.Add(item);
                item = new ListItem("参数细节");                                //6 
                select.Items.Add(item);
                item = new ListItem("预期完成时间");                          //7 
                select.Items.Add(item);
                item = new ListItem("拟提供技术资金");                         //8 
                select.Items.Add(item);
                item = new ListItem("拟解决方式");                             //9
                select.Items.Add(item);
                /*
                item = new ListItem("拟合作对象");                           //10
                select.Items.Add(item);
                 */
                item = new ListItem("项目所处阶段");                          //11
                select.Items.Add(item);
                item = new ListItem("已有对接设备条件");                        //12
                select.Items.Add(item);
                item = new ListItem("已有对接人员");                          //13
                select.Items.Add(item);
                item = new ListItem("相关实施经验");                          //14
                select.Items.Add(item);
                item = new ListItem("企业名称");                                //15
                select.Items.Add(item);
                item = new ListItem("企业地址");                                //16
                select.Items.Add(item);
                item = new ListItem("企业邮编");                                //17
                select.Items.Add(item);
                item = new ListItem("企业负责人");                                //18 
                select.Items.Add(item);
                item = new ListItem("企业简介");                                //19
                select.Items.Add(item);
                item = new ListItem("企业联系人");                                //20
                select.Items.Add(item);
                item = new ListItem("联系人邮箱");                                //21
                select.Items.Add(item);
                item = new ListItem("联系人所在部门");                            //22
                select.Items.Add(item);
                item = new ListItem("联系人手机");                                //23
                select.Items.Add(item);
                item = new ListItem("联系人座机");                                //24
                select.Items.Add(item);
                item = new ListItem("联系人QQ");                                //25
                select.Items.Add(item);
                item = new ListItem("联系人微信号");                                //26
                select.Items.Add(item);
                item = new ListItem("联系人职位");                                //27
                select.Items.Add(item);
                item = new ListItem("联系人传真");                                //28
                select.Items.Add(item);
                item = new ListItem("删除该条目");                                //29
                select.Items.Add(item);
                if (setlabel)
                {
                    select.SelectedIndex = SelectLabel[index];
                }
                cell_select = new HtmlTableCell();
                cell_select.Controls.Add(select);
                row.Cells.Add(cell_select);
                TableAddReq.Rows.Add(row);
                index++;
                if (index == mymatch.Length) break;
                temp_string = Regex.Replace(mymatch[index], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");

            } while (temp_string != firststring);

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            HtmlInputText text = new HtmlInputText();
            text.ID = "ID_ComingFrom";
            cell.ID = "ComingFrom";
            cell.InnerText = "ComingFrom";
            cell_select = new HtmlTableCell();
            cell_select.Controls.Add(text);
            row.Cells.Add(cell);
            row.Cells.Add(cell_select);
            TableAddReq.Rows.Add(row);

            row = new HtmlTableRow();
            cell = new HtmlTableCell();
            text = new HtmlInputText();
            text.ID = "ID_ComingDay";
            text.Attributes.Add("onclick", "WdatePicker()");
            cell.ID = "ComingDay";
            cell.InnerText = "ComingDay";
            cell_select = new HtmlTableCell();
            cell_select.Controls.Add(text);
            row.Cells.Add(cell);
            row.Cells.Add(cell_select);
            TableAddReq.Rows.Add(row);

            ViewState["ContentResult"] = result;
            ViewState["mymatch"] = mymatch;
            ViewState["ItemCount"] = TableAddReq.Rows.Count;
        }
        protected int ProcessDataBase()
        {
            ArrayList ItemList = new ArrayList();
            ArrayList ItemListValue = new ArrayList();
            string str, innertext;
            for (int index = 0; index < (int)ViewState["ItemCount"]-1; index++)
            {
                str = "select"+index;
                innertext = Request[str];
                    switch(innertext)
                    {
                        case "不作为条目录入（合并到上一个条目中）":
                            ItemListValue.Add(0);
                            break;
                        case "技术需求名称":
                            ItemListValue.Add(1);
                            break;
                        case "科技需求内容(企业填写)":
                            ItemListValue.Add(2);
                            break;
                        case "所属领域":
                            ItemListValue.Add(3);
                            break;
                        case "科技需求类型":
                            ItemListValue.Add(4);
                            break;
                        case "预期结果":
                            ItemListValue.Add(5);
                            break;
                        case "参数细节":
                            ItemListValue.Add(6);
                            break;
                        case "预期完成时间":
                            ItemListValue.Add(7);
                            break;
                        case "拟提供技术资金":
                            ItemListValue.Add(8);
                            break;
                        case "拟解决方式":
                            ItemListValue.Add(9);
                            break;
                        //case "拟合作对象":
                        //    ItemListValue.Add(10);
                        //    break;
                        case "项目所处阶段":
                            ItemListValue.Add(10);
                            break;
                        case "已有对接设备条件":
                            ItemListValue.Add(11);
                            break;
                        case "已有对接人员":
                            ItemListValue.Add(12);
                            break;
                        case "相关实施经验":
                            ItemListValue.Add(13);
                            break;
                        case "企业名称":
                            ItemListValue.Add(14);
                            break;
                        case "企业地址":
                            ItemListValue.Add(15);
                            break;
                        case "企业邮编":
                            ItemListValue.Add(16);
                            break;
                        case "企业负责人":
                            ItemListValue.Add(17);
                            break;
                        case "企业简介":
                            ItemListValue.Add(18);
                            break;
                        case "企业联系人":
                            ItemListValue.Add(19);
                            break;
                        case "联系人邮箱":
                            ItemListValue.Add(20);
                            break;
                        case "联系人所在部门":
                            ItemListValue.Add(21);
                            break;
                        case "联系人手机":
                            ItemListValue.Add(22);
                            break;
                        case "联系人座机":
                            ItemListValue.Add(23);
                            break;
                        case "联系人QQ":
                            ItemListValue.Add(24);
                            break;
                        case "联系人微信号":
                            ItemListValue.Add(25);
                            break;
                        case "联系人职位":
                            ItemListValue.Add(26);
                            break;
                        case "联系人传真":
                            ItemListValue.Add(27);
                            break;
                        case "删除该条目":
                            ItemListValue.Add(28);
                            break;
                    }
            }
            Array ItemValue = ItemListValue.ToArray();
            string writetofile = "";
            for (int i_ = 0; i_ < ItemValue.Length; i_++) writetofile += ItemValue.GetValue(i_) + ",";
            writetofile += ";";
            FileStream fs = new FileStream(WebHelper.GetAppConfig("filepath")+"\\"+"selectedLabel.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(writetofile); sw.Close();fs.Close();
            string[] DataBaseValue = new string[ReqItemCnt];
            string temp_string;
            for (int cnt = 0; cnt < ReqItemCnt; cnt++)DataBaseValue[cnt] = "";
            result = (string[])ViewState["ContentResult"];
            mymatch = (string[])ViewState["mymatch"];
            int a_index = 0;
            int b_index = 0;
            string firststring = Regex.Replace(mymatch[0], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", ""); 
            string SqlConnectionString = WebHelper.GetAppConfig("SqlConnectionString");
            SqlConnection m_Connection = new SqlConnection(SqlConnectionString);
            m_Connection.Open();
            string c; string tempstring_aindex;
            SqlCommand m_Command;
            tempstring_aindex =firststring;
            int FaultCnt = 0;
            int cnt_start = 0;
            temp_string = Regex.Replace(result[cnt_start], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
            while (temp_string != firststring)
            {
                cnt_start++;
                temp_string = Regex.Replace(result[cnt_start], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
            }
            myinfo.InnerText = "您插入的数据与现有数据存在重复，已被自动删除，重复的需求名称为：";
            myinfo2.InnerText = "您插入的联系人数据与现有数据存在重复，已被自动删除，重复的联系人姓名和其企业名称为：";
            bool waitforstart = false; bool return2 = false;
            int techrequrecnt = 0; int contactcnt = 0; int enterprisecnt = 0;
                for (int cnt = cnt_start; cnt < result.Length; cnt++)
                {
                    temp_string = Regex.Replace(result[cnt], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
                    if (cnt==result.Length-1&&b_index != 0)
                    {
                        DataBaseValue[(int)ItemValue.GetValue(b_index) - 1] += Regex.Replace(result[cnt], @"[^\u4e00-\u9fa5A-Za-z0-9!@#$%^&*()+-<>?！…（）、_—。,，“”‘’；：？|=￥【】[]《》{}~]*", "");
                    }
                    if ((temp_string == firststring && cnt != cnt_start) || cnt == result.Length - 1)
                    {
                        waitforstart = false;
                        c = string.Format("select * from Enterprise where Enp_Name='{0}'", DataBaseValue[13]);
                        m_Command = new SqlCommand(c, m_Connection);
                        SqlDataReader sdr = m_Command.ExecuteReader();
                        int Enp_id = -1;
                        if (sdr.HasRows)
                        {
                            sdr.Read();
                            Enp_id = sdr.GetInt32(sdr.GetOrdinal("Enp_ID"));
                        }
                        sdr.Close();
                        if (Enp_id == -1)
                        {
                            c = string.Format("insert into Enterprise (Enp_Name,Enp_Address,Enp_Zipcode,Enp_Des) values('{0}','{1}','{2}','{3}')",
                                DataBaseValue[13], DataBaseValue[14], DataBaseValue[15], DataBaseValue[17]);
                            m_Command = new SqlCommand(c, m_Connection);
                            try
                            {
                                enterprisecnt++;
                                m_Command.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                enterprisecnt--;
                                myinfo4.InnerText += ex.Message;
                                myinfo4.Visible = true;
                            }
                            c = string.Format("select * from Enterprise where Enp_Name = '{0}'", DataBaseValue[13]);
                            m_Command = new SqlCommand(c, m_Connection);
                            sdr = m_Command.ExecuteReader();
                            sdr.Read();
                            Enp_id = sdr.GetInt32(sdr.GetOrdinal("Enp_ID"));
                            sdr.Close();
                        }
                        c = string.Format("insert into Contact (Name,Department,email,Tel,Officephone,qq,weichat,title,fax,status,Enp_ID) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",
                            DataBaseValue[18], DataBaseValue[20], DataBaseValue[19], DataBaseValue[21], DataBaseValue[22], DataBaseValue[23], DataBaseValue[24], DataBaseValue[25], DataBaseValue[26], 1, Enp_id);
                        m_Command = new SqlCommand(c, m_Connection);
                        try
                        {
                            contactcnt++;
                            m_Command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            myinfo2.InnerText += DataBaseValue[18]+"，企业名称为"+DataBaseValue[13]+";";
                            myinfo2.Visible = true;
                            contactcnt--;
                        }
                        if (DataBaseValue[0] == " "||DataBaseValue[0] == "")
                        {
                            if (DataBaseValue[1].Length < 20)
                            {
                                DataBaseValue[0] = Enp_id.ToString() + DataBaseValue[1].Substring(0,DataBaseValue[1].Length);
                            }
                            else
                            {
                                DataBaseValue[0] = Enp_id.ToString() + DataBaseValue[1].Substring(0,20);
                            }
                            
                        }
                        string comingfrom = Request["ID_ComingFrom"];
                        string comingday = Request["ID_ComingDay"];
                        c = string.Format("insert into TechRequire (Rq_Name,Rq_EnpDes,Rq_Fields,Rq_Type,Rq_Expect,Rq_Parameter,Rq_Fund,Rq_ResolveManner,bak_state,bak_equpments,bak_Resource,bak_experience,Rq_flag,Rq_CreateTime,Rq_CreaterID,Rq_updatetime,Rq_updateID,Enp_ID,Rq_ComingFrom,Rq_ComingDay) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','1','{12}','{13}','{14}','{15}','{16}','{17}','{18}')",
                            DataBaseValue[0], DataBaseValue[1], DataBaseValue[2],
                            DataBaseValue[3], DataBaseValue[4], DataBaseValue[5],
                            DataBaseValue[7], DataBaseValue[8],
                            DataBaseValue[9], DataBaseValue[10], DataBaseValue[11],
                            DataBaseValue[12], DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CurrentUser.Id, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), CurrentUser.Id, Enp_id,
                            comingfrom,comingday);
                        m_Command = new SqlCommand(c, m_Connection);
                        //DataBaseValue[6] fund = DataBaseValue[6];
                        try
                        {
                            techrequrecnt++;
                            m_Command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 2627)
                            {
                                myinfo.InnerText += DataBaseValue[0] + ";";
                                myinfo.Visible = true;
                            }
                            else
                            {
                                myinfo4.InnerText += ex.Message;
                                myinfo4.Visible = true;
                            }
                            techrequrecnt--;
                        }
                        if (Regex.Replace(DataBaseValue[16], @"[^\u4e00-\u9fa5A-Za-z0-9!@#$%^&*()+-<>?！…（）、_—。,，“”‘’；：？|=￥【】[]《》{}~]*", "") != "")
                        {
                            c = string.Format("insert into Contact (Name,title,status,Enp_ID) values('{0}','{1}','{2}','{3}')",
                            DataBaseValue[16],"企业负责人",1, Enp_id);
                            m_Command = new SqlCommand(c, m_Connection);
                            try
                            {
                                contactcnt++;
                                m_Command.ExecuteNonQuery();
                            }
                            catch (SqlException ex)
                            {
                                contactcnt--;
                                myinfo4.InnerText += ex.Message;
                                myinfo4.Visible = true;
                            }
                        }
                        for (int _cnt = 0; _cnt < ReqItemCnt; _cnt++) DataBaseValue[_cnt] = "";
                        a_index = 0;
                        tempstring_aindex = Regex.Replace(mymatch[a_index], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
                    }
                    else
                    {
                        if (waitforstart) continue;
                    }
                    if (temp_string == tempstring_aindex)
                    {
                        FaultCnt = 0;
                        b_index = a_index;
                        //DataBaseValue[(int)ItemValue.GetValue(b_index) - 1] += Regex.Replace(result[cnt], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "") + ":";
                        a_index++;
                        if (a_index == mymatch.Length) a_index = 0;
                        while ((int)ItemValue.GetValue(a_index) == 0)
                        {
                            a_index++; //处理：不作为条目插入
                            if (a_index >= mymatch.Length)
                            {
                                a_index = 0;
                                break;
                            }
                        }
                        tempstring_aindex = Regex.Replace(mymatch[a_index], @"[^\u4e00-\u9fa5A-Za-z0-9-]*", "");
                    }
                    else
                    {
                        //进入数据库时对导入内容进行清理，删除一些奇怪的符号
                        if ((int)ItemValue.GetValue(b_index) != 0)
                        {
                            DataBaseValue[(int)ItemValue.GetValue(b_index) - 1] += Regex.Replace(result[cnt], @"[^\u4e00-\u9fa5A-Za-z0-9!@#$%^&*()+-<>?！…（）、_—。,，“”‘’；：？|=￥【】[]《》{}~]*", "");
                        }
                        FaultCnt++;
                    }
                    if (mymatch.Length>2&&FaultCnt == mymatch.Length)
                    {
                        ViewState["ErrorLabel"] += Regex.Replace(tempstring_aindex,@"\n|\r","")+"\n\n";
                        if (b_index != 0)
                        {
                            ViewState["UnMatched"] += Regex.Replace(DataBaseValue[(int)ItemValue.GetValue(b_index) - 1], @"\n|\r", "") + "\n\n";
                        }
                        else
                        {
                            ViewState["UnMatched"] += " \n\n";
                        }
                        waitforstart = true;
                        //return 2;
                        return2 = true;
                    }

                }
            myinfo3.InnerText = string.Format(
                "成功导入科技需求 '{0}'个，企业信息'{1}'个，联系人和负责人信息'{2}'个",
                techrequrecnt,enterprisecnt,contactcnt);
            myinfo3.Visible = true;
                if (return2) return 2;
                return 1;
        }

     

      
       
    }
}