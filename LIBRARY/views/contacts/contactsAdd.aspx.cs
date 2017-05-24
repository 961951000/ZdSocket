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

namespace LIBRARY.views.contacts
{
    public partial class contactsAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["username"] == null)
                {
                    Response.Redirect("../home/login.aspx");
                }
                addContacts();
                Response.Redirect("contactsShow.aspx");
            }

        }

        //新增联系信息
        public string addContacts()
        {
            Contacts contacts = new Contacts();
            contacts.name = Request["name"];
            contacts.birthday = Request["birthday"];
            contacts.position = Request["position"];
            contacts.phone = Request["phone"];
            contacts.email = Request["email"];
            contacts.qq = Request["qq"];
            contacts.fax = Request["fax"];
            contacts.clientName = Request["clientName"];
            contacts.clientAddress = Request["clientAddress"];
            contacts.clientPhone = Request["clientPhone"];
            contacts.clientBusiness = Request["clientBusiness"];
            contacts.clientUrl = Request["clientUrl"];
            contacts.zip = Request["zip"];
            contacts.nature = Request["nature"];
            contacts.classify = Request["classify"];
            contacts.legalPerson = Request["legalPerson"];
            contacts.phoneShow = Request["phoneShow"];
            contacts.positionShow = Request["positionShow"];
            if (Request.Files.Count > 0)
            {
                contacts.img = Path.GetExtension(Request.Files[0].FileName);
            }
            contacts.customerId = Session["customerId"].ToString();
            string result = new ContactsS().addContacts(contacts);
            if (!string.IsNullOrEmpty(result))
            {

                saveFile(result);
            }
            return "1";
        }
        //保存文件(上传头像)
        public void saveFile(string fileName)
        {
            HttpPostedFile postedFile = Request.Files[0];            
            string filePos = Server.MapPath(ConfigurationManager.AppSettings["sourceFiles"].ToString()) + fileName;
            if (File.Exists(filePos))
            {
                File.Delete(filePos);
            }
            postedFile.SaveAs(filePos);
        }
    }
}
