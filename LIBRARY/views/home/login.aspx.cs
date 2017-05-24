using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.Views.home
{
    public partial class login : System.Web.UI.Page
    {
        AdminS adminS = new AdminS();
        Admin admin = new Admin();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod == "POST")
                {
                    string result = string.Empty;
                    switch (Request["requestMethod"]) // 请求的方法
                    {
                        case "requestLoginSerial":
                            {
                                HttpContext.Current.Session.Add("id", Request["id"]);
                                HttpContext.Current.Session.Add("username", Request["username"]);
                                HttpContext.Current.Session.Add("role", Request["role"]);
                                HttpContext.Current.Session.Add("serial", Request["serial"]);
                                HttpContext.Current.Session.Add("customerId", Request["customerId"]);
                                result = "1";
                            }
                            break;
                        case "requestLogin":
                            {
                                admin = ifLogin();
                                if (!string.IsNullOrEmpty(admin?.customerId))
                                {
                                    if (admin.role == "系统管理员")
                                    {
                                        result = Basic.getJSON(admin);
                                    }
                                    else
                                    {
                                        HttpContext.Current.Session.Add("id", admin.id);
                                        HttpContext.Current.Session.Add("username", admin.username);
                                        HttpContext.Current.Session.Add("role", admin.role);
                                        HttpContext.Current.Session.Add("serial", admin.serial);
                                        HttpContext.Current.Session.Add("customerId", admin.customerId);
                                        result = "1";
                                    }

                                }
                            }
                            break;

                        case "requestExitLog":
                            {
                                Session.Abandon();
                                result = "1";
                            }
                            break;
                    }
                    Response.Write(result);
                    Response.End();
                }
            }

        }

        public Admin ifLogin()
        {
            admin.username = Request["username"];
            admin.password = Request["password"];
            admin.serial = Request["serial"];
            return adminS.login(admin);
        }
    }
}