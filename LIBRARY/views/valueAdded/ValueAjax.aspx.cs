using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace LIBRARY.Views.valueAdded
{
    public partial class ValueAjax1 : System.Web.UI.Page
    {
        Contacts contacts = new Contacts();
        ContactsS contactsS = new ContactsS();
        Message message = new Message();
        MessageS messageS = new MessageS();
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
                        case "requestGetContacts":
                            {
                                result = Util.Basic.getJSON(getContacts());
                            }
                            break;
                        case "requestQueryAllUnit":
                            {
                                result = Util.Basic.getJSON(queryAllContacts());
                            }
                            break;
                        case "requestQueryAllMessage":
                            {
                                result = Util.Basic.getJSON(queryAllMessage());
                            }
                            break;
                        case "requestAddFw":
                            {
                                result = Util.Basic.getJSON(addFw());
                            }
                            break; 
                        case "requestGetTitle":
                            {
                                result = Util.Basic.getJSON(getTitle());
                            }
                            break;
                        case "requestDel":
                            {
                                result = Util.Basic.getJSON(del());
                            }
                            break;
                        case "requestUpdate":
                            {
                                result = Util.Basic.getJSON(updateFw());
                            }
                            break;
                    }
                    Response.Write(result);
                    Response.End();
                }
            }

        }

        //返回用户信息分页数据
        public Paging<Contacts> getContacts()
        {
            Paging<Contacts> page = new Paging<Contacts>();
            page.pc = getPc();
            page.ps = Util.Basic.getPs();
            page.condition = getCondition();
            return contactsS.query(page);
        }

        //返回所有用户信息
        public HashSet<Contacts> queryAllContacts()
        {
            return contactsS.queryAll(contacts);
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
            contacts.zip = Request["zip"];
            contacts.name = Request["name"];
            contacts.clientAddress = Request["clientAddress"];
            contacts.clientName = Request["clientName"];
            contacts.customerId = Session["customerId"].ToString();
            return contacts;
        }

        //增加发文内容
        public string addFw()
        {
            message.id = Request["id"];
            message.title = Request["title"];
            message.content = Request["content"];
            message.customerId = Session["customerId"].ToString();
            return messageS.addFw(message);
        }

        //查询、显示发文内容
        public List<Message> getTitle()
        {
            return messageS.getTitle();
        }
        //查询、显示message
        public HashSet<Message> queryAllMessage()
        {
            return messageS.queryAll(message);
        }

        //删除发文内容
        public string del() {
            return messageS.del(Request["id"]);
        }

        //修改（编辑）发文内容
        public string updateFw() {
            message.id = Request["id"];
            message.title = Request["title"];
            message.content = Request["content"];
            return messageS.updateFw(message);
        }

    }
}