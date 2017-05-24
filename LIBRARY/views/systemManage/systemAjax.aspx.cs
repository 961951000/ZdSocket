using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.systemManage
{
    public partial class systemAjax : System.Web.UI.Page
    {
        Admin admin = new Admin();
        AdminS adminS = new AdminS();
        InformationS informationS = new InformationS();
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
                        case "requestAddOperator":
                            {
                                result = addOperator();
                            }
                            break;
                        case "requestIfAdminName":
                            {
                                result = ifAdminName();
                            }
                            break;
                        case "requestResetPassword":
                            {
                                result = resetPassword();
                            }
                            break;
                        case "requestModifyAdmin":
                            {
                                result = modifyAdmin();
                            }
                            break;
                        case "requestGetAdminAll":
                            {
                                result = Basic.getJSON(getAdminAll());
                            }
                            break;
                        case "requestModifyColumn":
                            {
                                result = modifyColumn();
                            }
                            break;
                        case "requestGetColumnIsSelect":
                            {
                                result = Basic.getJSON(queryColumnIsSelect());
                            }
                            break;
                        case "requestGetColumn":
                            {
                                result = Basic.getJSON(queryColumn());
                            }
                            break;
                    }
                    Response.Write(result);
                    Response.End();
                }
            }
        }
        //添加操作员
        public string addOperator()
        {
            admin.username = Request["username"];
            admin.password = ConfigurationManager.AppSettings["initPassword"].ToString();
            admin.customerId = Session["customerId"].ToString();
            return adminS.addOperator(admin);
        }
        //判断用户是否存在
        public string ifAdminName()
        {
            return adminS.ifAdminName(Request["username"]);
        }
        //密码重置
        public string resetPassword()
        {
            admin.id = Request["id"];
            admin.password = Basic.getInitPassword();
            return adminS.resetPassword(admin);
        }
        //修改账户
        public string modifyAdmin()
        {
            admin.id = Request["id"];
            admin.username = Request["username"];
            return adminS.modifyAdmin(admin);
        }
        //返回用户信息分页数据
        public Paging<Admin> getAdminAll()
        {
            Paging<Admin> page = new Paging<Admin>();
            page.pc = getPc();
            page.ps = Basic.getPs();
            page.condition = getCondition();
            return adminS.query(page);
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
        private Admin getCondition()
        {
            admin.customerId = Session["customerId"].ToString();
            return admin;
        }
        //修改栏目信息
        public string modifyColumn()
        {
            Information information = new Information();
            information.id = Request["id"];
            information.customerId = Session["customerId"].ToString();
            return new InformationS().setTemplate(information);
        }

        public List<Information> queryColumnIsSelect()
        {
            return informationS.queryAllIsSelect(Session["customerId"].ToString()); ;
        }
        public List<Information> queryColumn()
        {
            return informationS.queryAll(Session["customerId"].ToString());
        }
    }
}