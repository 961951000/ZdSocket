using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.systemManage
{
    public partial class systemShow : System.Web.UI.Page
    {
        InformationS informationS = new InformationS();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/login.aspx");
            }
        }

        public List<Information> queryAllIsSelect()
        {
            return informationS.queryAllIsSelect(Session["customerId"].ToString());
        }
        public List<Information> queryColumn()
        {
            return informationS.queryAll(Session["customerId"].ToString());
        }
    }
}