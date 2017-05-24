using LIBRARY.models;
using LIBRARY.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.systemManage
{
    public partial class derivedStructure : System.Web.UI.Page
    {
        InformationS informationS = new InformationS();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/login.aspx");
            }
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyyyMMddHHmmss") + ConfigurationManager.AppSettings["fileExt"].ToString());
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = ConfigurationManager.AppSettings["fileType"].ToString();
            this.EnableViewState = false;
        }

        public List<Information> queryAllIsSelect()
        {
            return informationS.queryAllIsSelect(Session["customerId"].ToString());
        }

    }
}