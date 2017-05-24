using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.app
{
    public partial class downloadApp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request["suffix"])
                {
                    case "apk":
                        {
                            TransmitFile(ConfigurationManager.AppSettings["sourceFiles"].ToString() + "ZD_Address.apk");
                        }
                        break;
                    case "ipa":
                        {
                            TransmitFile(ConfigurationManager.AppSettings["sourceFiles"].ToString() + "ZD_Address.ipa");
                            break;
                        }
                }
            }
        }

        public void TransmitFile(string filePath)
        {
            try
            {
                string fileName = filePath.Replace(ConfigurationManager.AppSettings["sourceFiles"].ToString(), "");
                filePath = Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();
                    //指定Http Mime格式为压缩包
                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                    // Http 协议中有专门的指令来告知浏览器, 本次响应的是一个需要下载的文件. 格式如下:
                    // Content-Disposition: attachment;filename=filename.txt
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                    //不指明Content-Length用Flush的话不会显示下载进度   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.TransmitFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }
        }
    }
}