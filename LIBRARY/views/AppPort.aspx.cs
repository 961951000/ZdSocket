using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using System.Net;
using System.Text;
using System.Web.Configuration;
using Log;
using Newtonsoft.Json;
using Log = LIBRARY.models.Log;

namespace LIBRARY.views
{
    public partial class AppPort : System.Web.UI.Page
    {
        public static string PostUrl = ConfigurationManager.AppSettings["WebReference.Service.PostUrl"];
        private MySqlConnection db = new MySqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
        Admin admin = new Admin();
        AdminS adminS = new AdminS();
        Contacts contacts = new Contacts();
        ContactsS contactsS = new ContactsS();
        Customer customer = new Customer();
        CustomerS contactS = new CustomerS();
        InformationS informationS = new InformationS();
        PersonTemporary personTemporary = new PersonTemporary();
        PersonTemporaryS personTemporaryS = new PersonTemporaryS();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 刚刚加的
            try
            {
                var log = new models.Log { Address = Request.ServerVariables.Get("Remote_Addr").ToString() };
                var count = db.Query<int>("select count(*) from log").Single();//记录总数
                if (count < int.MaxValue / 2)
                {
                    db.Execute("insert into log values(null,@Address,@UpdateTime)", log);//记录日志
                }
                else
                {
                    db.Execute("truncate table log");//截断表
                }
            }
            catch (Exception ex)
            {
                Loger.WriteError(ex);
            }

            #endregion
            if (!IsPostBack)
            {
                Loger.WriteDebug($"requestMethod={Request["requestMethod"]}");
                string result = string.Empty;
                try
                {
                    switch (Request["requestMethod"])
                    {
                        case "requestQuery":
                            {
                                result = JsonConvert.SerializeObject(Query()[0]); ;
                            }
                            break;
                        case "requestLogin":
                            {
                                result = Basic.getJSON(login());

                            }
                            break;
                        case "requestCancelRele":
                            {
                                result = Convert.ToString(cancelRele());

                            }
                            break;
                        case "requestLoginRele":
                            {
                                result = Basic.getJSON(loginRele());
                            }
                            break;
                        case "requestIfEmail":
                            {
                                result = ifPhone();
                            }
                            break;
                        case "requestSelectRele":
                            {
                                result = Basic.getJSON(selectRele());
                            }
                            break;
                        case "requestQueryAllPhone":
                            {
                                result = Basic.getJSON(queryAllPhone());
                            }
                            break;
                        case "requestSendSecurityCode":
                            {
                                result = SendSecurityCode();
                            }
                            break;
                        case "requestloginSecurityCode":
                            {
                                result = Basic.getJSON(loginSecurityCode());
                            }
                            break;
                    }
                    if (!string.IsNullOrEmpty(Request["id"]))
                    {
                        switch (Request["requestMethod"])
                        {
                            case "requestUploadImg":
                                {
                                    result = uploadImg();
                                }
                                break;
                            case "requestUpload":
                                {
                                    result = upload();
                                }
                                break;
                        }
                    }
                    else if (!string.IsNullOrEmpty(Request["customerId"]))
                    {
                        switch (Request["requestMethod"])
                        {
                            case "requestGetCustomer":
                                {
                                    result = Basic.getJSON(getCustomer());
                                }
                                break;
                            case "requestGetContacts":
                                {
                                    result = Basic.getJSON(getContacts());
                                }
                                break;
                            case "requestGetInformation":
                                {
                                    result = Basic.getJSON(getInformation());
                                }
                                break;
                            case "requestSelectRele":
                                {
                                    result = Basic.getJSON(selectRele());
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Loger.WriteError($"requestMethod={Request["requestMethod"]}", ex);
                }
                Response.Write(result);
                Response.End();
            }
        }

        private List<Query> Query()
        {
            var entity = new Query
            {
                Name = Request["Name"],
                Birthday = Request["Birthday"],
                Position = Request["Position"],
                Phone = Request["Phone"],
                Email = Request["Email"],
                Qq = Request["Qq"],
                Fax = Request["Fax"],
                Img = Request["Img"],
                OperateTime = Request["OperateTime"],
                ClientName = Request["ClientName"],
                ClientAddress = Request["ClientAddress"],
                ClientPhone = Request["ClientPhone"],
                ClientBusiness = Request["ClientBusiness"],
                ClientUrl = Request["ClientUrl"],
                Zip = Request["Zip"],
                Nature = Request["Nature"],
                LegalPerson = Request["LegalPerson"],
                PhoneShow = Request["PhoneShow"],
                PositionShow = Request["PositionShow"],
                IsDeleted = Request["IsDeleted"],
                Classify = Request["Classify"],
                Password = Request["Password"],
                CustomerName = Request["CustomerName"],
                CustomerCode = Request["CustomerCode"],
                CustomerAddress = Request["CustomerAddress"],
                CustomerContacts = Request["CustomerContacts"],
                CustomerOpenDate = Request["CustomerOpenDate"],
                CustomerIsDeleted = Request["CustomerIsDeleted"],
            };
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                try
                {
                    entity.Id = Convert.ToInt32(Request["id"]);
                }
                catch (Exception)
                {
                    Loger.WriteDebug("AppPort.Query:id无效");
                }
            }
            if (!string.IsNullOrEmpty(Request["CustomerId"]))
            {
                try
                {
                    entity.CustomerId = Convert.ToInt32(Request["CustomerId"]);
                }
                catch (Exception)
                {
                    Loger.WriteDebug("AppPort.Query:CustomerId无效");
                }
            }
            return contactsS.Query(entity);
        }

        //登录
        public Contacts login()
        {
            contacts.phone = Request["phone"];
            contacts.password = Request["password"];
            contacts.customerId = Request["customerId"];
            return contactsS.login(contacts);
        }

        //验证码登录
        public Contacts loginSecurityCode()
        {
            string phone = Request["phone"];
            string inputCode = Request["code"];
            string returnPhone = Convert.ToString(Session["phone"]);
            string returnCode = Convert.ToString(Session["code"]);
            string codeTime = Convert.ToString(Session["codeTime"]);
            string select_sql = "select distinct phone,password from person where phone=@phone";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    //MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    //comm.Parameters.AddWithValue("@phone", phone);
                    //MySqlDataReader dr = comm.ExecuteReader();
                    //Contacts entity = new Contacts();
                    //while (dr.Read())
                    //{
                    //    entity.phone = Convert.ToString(dr["phone"]);
                    //    entity.password = Convert.ToString(dr["password"]);
                    //    break;
                    //}
                    Contacts entity = conn.QuerySingle<Contacts>(select_sql, new { phone = phone });
                    entity.password = Basic.getBase64(entity.password);
                    if (phone == returnPhone && inputCode == returnCode && !string.IsNullOrEmpty(codeTime) && (DateTime.Now - DateTime.Parse(codeTime)).TotalMinutes <= 30)
                    {
                        return entity;
                    }
                    else
                    {
                        return entity;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        }

        //添加账号登录
        public Contacts loginRele()
        {
            string firstPhone = Request["firstPhone"];
            contacts.phone = Request["phone"];
            contacts.password = Request["password"];
            return contactsS.loginRele(contacts, firstPhone);
        }

        //返回当前客户数据
        public List<Customer> getCustomer()
        {
            customer.id = Request["customerId"];
            return contactS.getCustomer(customer);
        }

        //取消账号关联
        public object cancelRele()
        {
            string phone = Request["phone"];
            return contactsS.cancelRele(phone);
        }

        //返回所有联系人数据
        public List<Contacts> getContacts()
        {
            List<Contacts> list = contactsS.getContacts(getCondition());
            foreach (Contacts contacts in list)
            {
                var url = Request.Url.ToString();
                var host = url.Substring(url.IndexOf(Request.Url.Host));
                host = host.Remove(host.LastIndexOf(Request.Path));
                contacts.img = host + ConfigurationManager.AppSettings["sourceFiles"].ToString() + contacts.id + contacts.img;
            }
            return list;
        }

        //获取用户信息搜索条件
        private Contacts getCondition()
        {
            contacts.name = Request["name"];
            contacts.email = Request["email"];
            contacts.clientName = Request["clientName"];
            contacts.customerId = Request["customerId"];
            contacts.clientBusiness = Request["customerId"];

            return contacts;
        }
        //返回所有栏目数据
        public List<Information> getInformation()
        {
            return informationS.queryAll(Request["customerId"]);
        }

        //上传信息
        public string upload()
        {
            contacts.name = Request["name"];
            contacts.clientName = Request["clientName"];
            contacts.position = Request["position"];
            contacts.clientBusiness = Request["clientBusiness"];
            contacts.email = Request["email"];
            contacts.personId = Request["id"];
            contacts.clientAddress = Request["address"];

            return contactsS.upload(contacts);
        }

        //判断邮箱是否存在
        public string ifPhone()
        {
            if (!string.IsNullOrEmpty(Request["phone"]))
            {
                return contactsS.ifPhone(Request["phone"], "person");
            }
            else
            {
                return "0";
            }
        }

        //上传头像
        public string uploadImg()
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
                else
                {
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


        //查询关联账号
        public List<dynamic> selectRele()
        {
            contacts.phone = Request["phone"];
            contacts.password = Request["password"];
            return contactsS.selectRele(contacts);
        }

        //查询同一个手机号的所有组织，公司
        public List<Contacts> queryAllPhone()
        {
            contacts.phone = Request["phone"];
            contacts.password = Request["password"];
            return contactsS.queryAllPhone(contacts);
        }

        //短信验证码接口
        public string SendSecurityCode()
        {
            string phone = Request["phone"];
            List<Contacts> list = contactsS.queryPhone();
            foreach (Contacts obj in list)
            {
                if (phone == obj.phone)
                {
                    Random rd = new Random();
                    string code = rd.Next(100000, 1000000).ToString();
                    HttpContext.Current.Session.Add("phone", phone);
                    HttpContext.Current.Session.Add("code", code);
                    HttpContext.Current.Session.Add("codeTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    string un = "N8301225";// 账号
                    string pw = "qHhn4EKX3m1daa";// 密码
                    string msg = "【智定科技】验证码：" + code + "，请在30分钟内输入。如非本人操作，可不用理会 。";// 短信内容 

                    string postStrTpl = "un={0}&pw={1}&phone={2}&msg={3}&rd=1";

                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] postData = encoding.GetBytes(string.Format(postStrTpl, un, pw, phone, msg));
                    System.GC.Collect();
                    HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(PostUrl);
                    myRequest.KeepAlive = false;
                    myRequest.Method = "POST";
                    myRequest.ContentType = "application/x-www-form-urlencoded";
                    myRequest.ContentLength = postData.Length;
                    myRequest.Timeout = 5000;
                    Stream newStream = myRequest.GetRequestStream();
                    // Send the data.
                    newStream.Write(postData, 0, postData.Length);
                    newStream.Flush();
                    newStream.Close();

                    HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                    if (myResponse.StatusCode == HttpStatusCode.OK)
                    {
                        StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        myResponse.Close();
                        myRequest.Abort();
                        return code;
                    }
                    else
                    {
                        myRequest.Abort();
                        myResponse.Close();
                        return "error";
                    }

                }
            }

            return "NoFoundPhone";
        }
    }
}