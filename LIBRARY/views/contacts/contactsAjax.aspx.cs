using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
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
    public partial class contactsAjax : System.Web.UI.Page
    {
        ContactsS contactsS = new ContactsS();
        Contacts contacts = new Contacts();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["username"] == null)
                {
                    Response.Redirect("../home/login.aspx");
                }
                if (Request.HttpMethod == "POST")
                {
                    string result = string.Empty;
                    switch (Request["requestMethod"])
                    {
                        case "requestInformationConfimNo":
                            {
                                result = informationConfimNo();
                            }
                            break;
                        case "requestInformationConfim":
                            {
                                result = informationConfim();
                            }
                            break;
                        case "requestGetPersonTemporary":
                            {
                                result = Basic.getJSON(getPersonTemporary());
                            }
                            break;
                        case "requestModifyContacts":
                            {
                                result = modifyContacts();
                            }
                            break;
                        case "requestDelSureContacts":
                            {
                                result = delSureContacts();
                            }
                            break;
                        case "requestModifyImg":
                            {
                                result = modifyImg();
                            }
                            break;
                        case "requestGetContacts":
                            {
                                result = Basic.getJSON(query());
                            }
                            break;

                        default:
                            {
                                result = "no method";
                            }
                            break;
                    }
                    Response.Write(result);
                    Response.End();
                }
            }
        }

        //上传信息驳回
        public string informationConfimNo()
        {
            return contactsS.informationConfimNo(Request["id"]);
        }
        //上传信息确认
        public string informationConfim()
        {
            return contactsS.informationConfim(Request["id"]);
        }
        //返回用户信息分页数据
        public Paging<PersonTemporary> getPersonTemporary()
        {
            Paging<PersonTemporary> page = new Paging<PersonTemporary>();
            page.pc = getPc();
            page.ps = Basic.getPs();
            page.condition = getPersonTemporaryCondition();
            Paging<PersonTemporary> queryPage = new PersonTemporaryS().query(page);
            return queryPage;
        }

        //获取用户信息搜索条件
        private PersonTemporary getPersonTemporaryCondition()
        {
            PersonTemporary personTemporary = new PersonTemporary();
            personTemporary.customerId = Session["customerId"].ToString();
            return personTemporary;
        }
        //修改头像
         public string modifyImg()
         {
             if (Request.Files.Count > 0)
             {
                 HttpPostedFile postedFile = Request.Files[0];
                 string id = Request["id"];
                 string fileExtension = Path.GetExtension(postedFile.FileName);
                 if (fileExtension != ".gif" && fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".bmp" && fileExtension != ".png")
                 {
                     return "formatErr";
                 }
                 else if (Request.Files[0].ContentLength / 1048576 > 2)
                 {
                     return "sizeErr";
                 }
                 else {
                     string img = new ContactsS().modifyImg(id, fileExtension);
                     if (!string.IsNullOrEmpty(img))
                     {
                         string filePos = Server.MapPath(ConfigurationManager.AppSettings["sourceFiles"].ToString()) + id + img;
                         if (File.Exists(filePos))
                         {
                             File.Delete(filePos);
                         }
                         postedFile.SaveAs(filePos);
                     }
                     return ConfigurationManager.AppSettings["sourceFiles"].ToString() + id + img;
                 }
             }
             else
             {
                 return string.Empty;
             }
         }

        //删除联系人信息
        public string delSureContacts() {
            contacts.id = Request["id"];
            return contactsS.delSureContacts(contacts);
        }

        //修改联系人信息
        public string modifyContacts()
        {
            contacts.id = Request["id"];
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
            return contactsS.modifyContacts(contacts);
        }
        //返回用户信息分页数据
        public Paging<Contacts> query()
        {
            Paging<Contacts> page = new Paging<Contacts>();
            page.pc = getPc();
            page.ps = Basic.getPs();
            page.condition = getCondition();
            Paging<Contacts> queryPage = contactsS.query(page);
            foreach (Contacts contacts in queryPage.datas)
            {
                contacts.img = ConfigurationManager.AppSettings["sourceFiles"].ToString() + contacts.id + contacts.img;
            }
            return queryPage;
        }
        //获取当前页码
        private int getPc()
        {
            string pageCode = Request["pc"];
            if (string.IsNullOrEmpty(pageCode))
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(pageCode);
            }
        }
        //获取用户信息搜索条件
        private Contacts getCondition()
        {
            contacts.name = Request["name"];
            contacts.phone = Request["phone"];
            contacts.clientName = Request["clientName"];
            contacts.customerId = Session["customerId"].ToString();
            return contacts;
        }
    }
}