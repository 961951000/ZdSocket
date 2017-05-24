using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.contacts
{
    public partial class contactsShow : System.Web.UI.Page
    {
        InformationS informationS = new InformationS();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/login.aspx");
            }            
        }
        //返回模板
        public List<Information> getInformationAll()
        {
            return informationS.queryAll(Session["customerId"].ToString());
        }
        //返回模版选中
        public List<Information> queryAllIsSelect()
        {
            return informationS.queryAllIsSelect(Session["customerId"].ToString());
        }
       
        public Contacts prepareAdd()
        {
            return new ContactsS().prepareAdd(Session["customerId"].ToString());
        }
    }
}