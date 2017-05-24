using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.resource
{
    public partial class resourceShow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["username"] == null)
            {
                Response.Redirect("../home/login.aspx");
            }
        }
    }
}