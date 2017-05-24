using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.contacts
{
    public partial class exportToExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/login.aspx");
            }
            string fileType = ConfigurationManager.AppSettings["fileType"].ToString();
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ConfigurationManager.AppSettings["fileExt"].ToString();
            string excelContent = Server.UrlDecode(Request.Form["elxStr"]);
            string fileCss = Server.MapPath("/wwwroot/css/table.css");
            export(fileType, fileName, excelContent, fileCss);
        }
        //导出联系人信息
        public void export(string fileType, string fileName, string excelContent, string fileCss)
        {
            Response.ContentType = fileType;
            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
            Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            Response.Write("<head>");
            Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            #region 样式的读取           
            string cssText = string.Empty;
            StreamReader sr = new StreamReader(fileCss);
            var line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                cssText += line;
            }
            sr.Close();
            Response.Write("<style>" + cssText + "</style>");
            #endregion
            Response.Write("<!--[if gte mso 9]><xml>");
            Response.Write("<x:ExcelWorkbook>");
            Response.Write("<x:ExcelWorksheets>");
            Response.Write("<x:ExcelWorksheet>");
            Response.Write("<x:Name>Report Data</x:Name>");
            Response.Write("<x:WorksheetOptions>");
            Response.Write("<x:Print>");
            Response.Write("<x:ValidPrinterInfo/>");
            Response.Write("</x:Print>");
            Response.Write("</x:WorksheetOptions>");
            Response.Write("</x:ExcelWorksheet>");
            Response.Write("</x:ExcelWorksheets>");
            Response.Write("</x:ExcelWorkbook>");
            Response.Write("</xml>");
            Response.Write("<![endif]--> ");
            Response.Write(excelContent);//这里是前台页面的HTML
            Response.Flush();
            Response.End();
        }
    }
}