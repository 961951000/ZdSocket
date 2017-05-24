using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.resource
{
    public partial class resourceAjax : System.Web.UI.Page
    {
        Admin admin = new Admin();
        AdminS adminS = new AdminS();
        CustomerS customerS = new CustomerS();
        Customer customer = new Customer();
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
                        case "requestGetCustomer":
                            {
                                result = Basic.getJSON(queryCustomer());
                            }
                            break;
                        case "requestModyfyPassword":
                            {
                                result = modyfyPassword();
                            }
                            break;
                    }
                    Response.Write(result);
                    Response.End();
                }
            }
        }
        //查询用户
        public Paging<Customer> queryCustomer()
        {
            Paging<Customer> page = new Paging<Customer>();
            page.pc = getPc();
            page.ps = Basic.getPs();
            page.condition = getCondition();
            Paging<Customer> queryPage = customerS.query(page);
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
        private Customer getCondition()
        {
            customer.id = Session["customerId"].ToString();
            return customer;
        }
        //用户密码修改
        public string modyfyPassword()
        {
            admin.id = Session["id"].ToString();
            admin.password = Request["password"];
            admin.newPassword = Request["newPassword"];
            return adminS.modyfyPassword(admin);
        }
    }
}