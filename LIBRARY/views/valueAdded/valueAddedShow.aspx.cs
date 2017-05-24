using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.valueAdded
{
    public partial class valueAddedShow : System.Web.UI.Page
    {
        ContactsS contactsS = new ContactsS();
        Contacts condition = new Contacts();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/Login.aspx");
            }
            else if (!IsPostBack)
            {
                if (Request.HttpMethod == "POST")
                {
                    string result = string.Empty;
                    switch (Request["requestMethod"])
                    {
                        case "requestSendEmail":
                            {
                                  string file = "";
                                  string files = "";
                                  for (int i = 0; i < Context.Request.Files.Count; i++) {
                                      HttpPostedFile postedFile = Request.Files[0];
                                      file = postedFile.FileName;
                                      if (!string.IsNullOrEmpty(file))
                                      {
                                          file = Server.MapPath(ConfigurationManager.AppSettings["emailSourceFiles"].ToString()) + postedFile.FileName;
                                          if (File.Exists(file))
                                          {
                                              File.Create(file).Dispose();
                                              File.Delete(file);
                                          }
                                          postedFile.SaveAs(file);
                                      }
                                     files += file + ",";         
                                   }
                                result = SendPhex(Request["email"], Server.UrlDecode(Request["Body_bak"]), Request["topic"], files, Request["smtp"], Request["name"], Request["upass"]);
                            }
                            break;
                        case "requestGetEmail":
                            {
                                result = Util.Basic.getJSON(getNameEmail());
                            }
                            break;
                        case "requestGetName":
                            {
                                result = Util.Basic.getJSON(queryName());
                            }
                            break;

                    }
                    Response.Write(result);
                    Response.End();
                }
            }
        }



        //查询person的name和email
        public List<Contacts> getNameEmail()
        {
            return contactsS.queryAll();
        }

        //模糊查询联系人列表中的name
        public List<Contacts> queryName()
        {
            return contactsS.queryName(Request["name"]);
        }

        //发送电子邮件
        public static string SendPhex(string email, string Body_bak, string topic, string file, string smtp, string name, string upass)
        {
            string[] to = email.Split(',');
            string[] files = file.Split(',');
            string body =  Body_bak;

            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = smtp; //指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(name, upass);//用户名和密码
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(name, "智定通讯录管理系统");//发件人，发件人名 
            for (int i = 0; i < to.Length; i++)
            {
                _mailMessage.To.Add(to[i]);  //收件人 
            }
            _mailMessage.SubjectEncoding = System.Text.Encoding.GetEncoding("gb2312");
            _mailMessage.Subject = topic;

            _mailMessage.Body = body;//内容
            for (int i = 0; i < files.Length; i++)
            {
                if (!string.IsNullOrEmpty(files[i]))
                {
                    System.Net.Mail.Attachment mailAttach_1 = new Attachment(@files[i]);//附件  
                    _mailMessage.Attachments.Add(mailAttach_1);
                }
            }
            _mailMessage.BodyEncoding = System.Text.Encoding.GetEncoding("gb2312");//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.High;//优先级  
            try
            {
                _smtpClient.Send(_mailMessage);
                return "123";
            }
            catch (Exception)
            {
                return "";
            }

        }

    }
}