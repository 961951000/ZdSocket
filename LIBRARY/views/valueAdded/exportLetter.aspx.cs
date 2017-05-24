using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.valueAdded
{
    public partial class exportLetter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/login.aspx");
            }

            /*Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMddHHmmss") + ConfigurationManager.AppSettings["fileExt"].ToString());
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = ConfigurationManager.AppSettings["fileType"].ToString();
            Response.Write("<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />");

            #region 样式的读取           
            string cssText = string.Empty;
            StreamReader sr = new StreamReader(Server.MapPath("/wwwroot/css/table.css"));
            var line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                cssText += line;
            }
            sr.Close();
            Response.Write("<style>" + cssText + "</style>");
            #endregion
            this.EnableViewState = false;*/
        }

        //返回所有联系人数据
        public List<Contacts> getContacts()
        {
            List<Contacts> list = new ContactsS().getContacts(getCondition());
            foreach (Contacts contacts in list)
            {
                contacts.img = Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf(Request.Url.Host.ToString()) + Request.Url.Host.ToString().Length) + ConfigurationManager.AppSettings["sourceFiles"].ToString() + contacts.id + contacts.img;
            }
            return list;
        }

        //获取用户信息搜索条件
        private Contacts getCondition()
        {
            Contacts contacts = new Contacts();
            contacts.name = Request["name"];
            contacts.phone = Request["phone"];
            contacts.clientName = Request["clientName"];
            contacts.customerId = Session["customerId"].ToString();
            return contacts;
        }

        //返回模版选中
        public List<Information> queryAllIsSelect()
        {
            return new InformationS().queryAllIsSelect(Session["customerId"].ToString());
        }
    }
}