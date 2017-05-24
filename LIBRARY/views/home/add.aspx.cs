using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIBRARY.views.home
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.HttpMethod == "POST")
                {
                    string result = string.Empty;
                    switch (Request["requestMethod"])
                    {
                        case "requestIfAdminName":
                            {
                                result = ifAdminName();
                            }
                            break;
                        case "requestVerificationCode":
                            {
                                result = verificationCode();
                            }
                            break;
                        case "requestAddCustomer":
                            {
                                result = addCustomer();
                            }
                            break;
                        case "requestGetCustomer":
                            {
                                result = Basic.getJSON(query());
                            }
                            break;
                    }
                    Response.Write(result);
                    Response.End();
                }
            }
        }

        //判断用户是否存在
        public string ifAdminName()
        {
            return new AdminS().ifAdminName(Request["username"]);
        }
        public string addCustomer()
        {
            string result = string.Empty;
            string sql1 = "insert into customer(name,customer_code,address,contacts,open_date,is_deleted) values(@name,@customer_code,@address,@contacts,@open_date,@is_deleted)";
            string sql2 = "select last_insert_id()";
            string sql3 = "insert into admin(username,password,role,customer_id,is_deleted,serial) values(@username,@password,@role,@customer_id,@is_deleted,@serial)";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm1 = new MySqlCommand(sql1, conn);
                    MySqlCommand comm2 = new MySqlCommand(sql2, conn);
                    MySqlCommand comm3 = new MySqlCommand(sql3, conn);
                    comm1.Parameters.AddWithValue("@name", Request["name"]);
                    comm1.Parameters.AddWithValue("@customer_code", Request["customerCode"]);
                    comm1.Parameters.AddWithValue("@address", Request["address"]);
                    comm1.Parameters.AddWithValue("@contacts", Request["contacts"]);
                    comm1.Parameters.AddWithValue("@open_date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    comm1.Parameters.AddWithValue("@is_deleted", 0);
                    comm1.ExecuteNonQuery();
                    string customer_id = Convert.ToString(comm2.ExecuteScalar());
                    comm3.Parameters.AddWithValue("@username", Request["username"]);
                    comm3.Parameters.AddWithValue("@password", Basic.setBase64(ConfigurationManager.AppSettings["initPassword"].ToString()));
                    comm3.Parameters.AddWithValue("@role", "系统管理员");
                    comm3.Parameters.AddWithValue("@customer_id", customer_id);
                    comm3.Parameters.AddWithValue("@is_deleted", 0);
                    comm3.Parameters.AddWithValue("@serial", Request["serial"]);
                    comm3.ExecuteNonQuery();
                    List<string> list = new List<string>();
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('name','姓名'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('birthday','出生年月'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('unit','工作单位'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('position','职务'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('unit_address','单位地址'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('zip','邮编'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('client_phone','单位电话'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('phone','手机'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('email','E-mail'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('img','照片'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('qq','QQ'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('fax','传真'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('nature','性质'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('legal_person','法人代表'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('url','公司网址'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('classify','分类'," + customer_id + ",1,0)");
                    list.Add("insert into information(parent_name,name,customer_id,is_select,is_deleted) values('business','主营业务'," + customer_id + ",1,0)");
                    foreach (string sql in list)
                    {
                        MySqlCommand comm = new MySqlCommand(sql, conn);
                        comm.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    result = "1";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("事务回滚：" + ex.Message);
                    transaction.Rollback();
                }
            }
            return result;
        }
        //发送验证码
        public string verificationCode()
        {
            string checkCode = Convert.ToString(new Random().Next(100000, 1000000));
            string To = ConfigurationManager.AppSettings["To"].ToString();//收件人地址
            string From = ConfigurationManager.AppSettings["From"].ToString();//发件人地址
            string Body = "尊敬的用户：<br/><br/>&nbsp;&nbsp;您申请了添加客户操作，校验码是：" + checkCode; //邮件正文
            string TiTtle = "添加客户"; //邮件的主题
            string Password = ConfigurationManager.AppSettings["Password"].ToString(); ; //发件人密码
            SendMail mail = new SendMail(To, From, Body, TiTtle, Password);
            mail.Send();
            return checkCode;
        }

        //查询所有
        public Paging<Customer> query()
        {
            string select_sql = "select * from customer";
            string where_sql = getWhereSql();
            string limit_sql = " limit @firstItem,@endItem";
            string page_sql = select_sql + where_sql + limit_sql;
            Paging<Customer> page = new Paging<Customer>();
            page.pc = getPc();
            page.ps = Basic.getPs();
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlCommand comm = new MySqlCommand(page_sql, conn);
                comm.Parameters.AddWithValue("@firstItem", (page.pc - 1) * page.ps);
                comm.Parameters.AddWithValue("@endItem", page.ps);
                MySqlDataReader dr = comm.ExecuteReader();
                page.tr = Convert.ToInt32(Basic.getTr("customer" + where_sql));
                page.tp = Basic.getTp(page.tr, page.ps);
                page.datas = new HashSet<Customer>();
                while (dr.Read())
                {
                    Customer customer = new Customer();
                    customer.id = Convert.ToString(dr["id"]);
                    customer.name = Convert.ToString(dr["name"]);
                    customer.customerCode = Convert.ToString(dr["customer_code"]);
                    customer.address = Convert.ToString(dr["address"]);
                    customer.contacts = Convert.ToString(dr["contacts"]);
                    customer.openDate = Convert.ToString(dr["open_date"]);
                    page.datas.Add(customer);
                }
                return page;
            }
        }

        //判断查询条件
        public string getWhereSql()
        {
            return " where is_deleted=0 order by id desc";
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
    }
}