using LIBRARY.models;
using LIBRARY.services;
using LIBRARY.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Log;
using Dapper;
namespace LIBRARY.controllers
{
    public class ContactsC
    {
        string result = string.Empty;

        public List<Query> Query(Query entity)
        {
            var sql = new StringBuilder("SELECT a.`id` AS 'Id',a.`name` AS 'Name',a.`birthday` AS 'Birthday',a.`position` AS 'Position',a.`phone` AS 'Phone',a.`email` AS 'Email',a.`qq` AS 'Qq',a.`fax` AS 'Fax',a.`img` AS 'Img',a.`operate_time` AS 'OperateTime',a.`client_name` AS 'ClientName',a.`client_address` AS 'ClientAddress',a.`client_phone` AS 'ClientPhone',a.`client_business` AS 'ClientBusiness',a.`client_url` AS 'ClientUrl',a.`zip` AS 'Zip',a.`nature` AS 'Nature',a.`legal_person` AS 'LegalPerson',IF (a.`phone_show` = 1,'true','false') AS 'PhoneShow',IF (a.`position_show` = 1,'true','false') AS 'PositionShow',a.`customer_id` AS 'CustomerId',IF (a.`is_deleted` = 1,'true','false') AS 'IsDeleted',a.`classify` AS 'Classify',a.`password` AS 'Password',b.`name` AS 'CustomerName',b.`customer_code` AS 'CustomerCode',b.`address` AS 'CustomerAddress',b.`contacts` AS 'CustomerContacts',b.`open_date` AS 'CustomerOpenDate',IF (b.`is_deleted` = 1,'true','false') AS 'CustomerIsDeleted' FROM person AS a LEFT JOIN customer AS b ON b.id = a.customer_id WHERE 1 = 1");
            #region 设置查询条件

            if (entity.Id != null)
            {
                sql.Append(" AND a.`id` = @Id");
            }
            if (!string.IsNullOrEmpty(entity.Name))
            {
                sql.Append(" AND a.`name` = @Name");
            }
            if (!string.IsNullOrEmpty(entity.Birthday))
            {
                sql.Append(" AND a.`birthday` = @Birthday");
            }
            if (!string.IsNullOrEmpty(entity.Position))
            {
                sql.Append(" AND a.`position` = @Position");
            }
            if (!string.IsNullOrEmpty(entity.Phone))
            {
                sql.Append(" AND a.`phone` = @Phone");
            }
            if (!string.IsNullOrEmpty(entity.Email))
            {
                sql.Append(" AND a.`email` = @Email");
            }
            if (!string.IsNullOrEmpty(entity.Qq))
            {
                sql.Append(" AND a.`qq` = @Qq");
            }
            if (!string.IsNullOrEmpty(entity.Fax))
            {
                sql.Append(" AND a.`fax` = @Fax");
            }
            if (!string.IsNullOrEmpty(entity.Img))
            {
                sql.Append(" AND a.`img` = @Img");
            }
            if (!string.IsNullOrEmpty(entity.OperateTime))
            {
                sql.Append(" AND a.`operate_time` = @OperateTime");
            }
            if (!string.IsNullOrEmpty(entity.ClientName))
            {
                sql.Append(" AND a.`client_name` = @ClientName");
            }
            if (!string.IsNullOrEmpty(entity.ClientAddress))
            {
                sql.Append(" AND a.`client_address` = @ClientAddress");
            }
            if (!string.IsNullOrEmpty(entity.ClientPhone))
            {
                sql.Append(" AND a.`client_phone` = @ClientPhone");
            }
            if (!string.IsNullOrEmpty(entity.ClientBusiness))
            {
                sql.Append(" AND a.`client_business` = @ClientBusiness");
            }
            if (!string.IsNullOrEmpty(entity.ClientUrl))
            {
                sql.Append(" AND a.`client_url` = @ClientUrl");
            }
            if (!string.IsNullOrEmpty(entity.Zip))
            {
                sql.Append(" AND a.`zip` = @Zip");
            }
            if (!string.IsNullOrEmpty(entity.Nature))
            {
                sql.Append(" AND a.`nature` = @Nature");
            }
            if (!string.IsNullOrEmpty(entity.LegalPerson))
            {
                sql.Append(" AND a.`legal_person` = @LegalPerson");
            }
            if (!string.IsNullOrEmpty(entity.PhoneShow))
            {
                sql.Append(" AND `phone_show` = @PhoneShow");
            }
            if (!string.IsNullOrEmpty(entity.PositionShow))
            {
                sql.Append(" AND a.`position_show` = @PositionShow");
            }
            if (entity.CustomerId != null)
            {
                sql.Append(" AND a.`customer_id` = @CustomerId");
            }
            if (!string.IsNullOrEmpty(entity.IsDeleted))
            {
                sql.Append(" AND a.`is_deleted` = @IsDeleted");
            }
            if (!string.IsNullOrEmpty(entity.Classify))
            {
                sql.Append(" AND a.`classify` = @Classify");
            }
            if (!string.IsNullOrEmpty(entity.Password))
            {
                sql.Append(" AND a.`password` = @Password");
            }
            if (!string.IsNullOrEmpty(entity.CustomerName))
            {
                sql.Append(" AND b.`name` = @CustomerName");
            }
            if (!string.IsNullOrEmpty(entity.CustomerCode))
            {
                sql.Append(" AND b.`customer_code` = @CustomerCode");
            }
            if (!string.IsNullOrEmpty(entity.CustomerAddress))
            {
                sql.Append(" AND b.`address` = @CustomerAddress");
            }
            if (!string.IsNullOrEmpty(entity.CustomerContacts))
            {
                sql.Append(" AND b.`contacts` = @CustomerContacts");
            }
            if (!string.IsNullOrEmpty(entity.CustomerOpenDate))
            {
                sql.Append(" AND b.`open_date` = @CustomerOpenDate");
            }
            if (!string.IsNullOrEmpty(entity.CustomerIsDeleted))
            {
                sql.Append(" AND b.`is_deleted` = @CustomerIsDeleted");
            }
            #endregion
            return Query(sql.ToString(), entity).ToList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="sql">SQL</param>
        /// <param name="param">查询条件</param>
        /// <returns>对象集合</returns>
        public IEnumerable<T> Query<T>(string sql, T param)
        {
            using (var db = new MySqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                return db.Query<T>(sql, param);
            }
        }
        //导入联系人
        public string addList(List<Contacts> list)
        {
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    var count = 0;
                    foreach (Contacts entity in list)
                    {
                        //if (string.IsNullOrEmpty(entity.email))
                        //{
                        //    Loger.WriteDebug("导入联系人邮箱信息不能为空！");
                        //}
                        //else if (Convert.ToInt32(Basic.getTr("person where email='" + entity.email + "'")) > 0)
                        //{
                        //    Loger.WriteDebug($"导入联系人邮箱信息重复：{entity.email}！");
                        //}
                        //else
                        //{
                        string sql = "insert into person(name,birthday,position,phone,email,qq,fax,img,operate_time,client_name,client_address,client_phone,client_business,client_url,zip,nature,classify,legal_person,phone_show,position_show,customer_id,is_deleted,password) values(@name,@birthday,@position,@phone,@email,@qq,@fax,@img,@operate_time,@client_name,@client_address,@client_phone,@client_business,@client_url,@zip,@nature,@classify,@legal_person,@phone_show,@position_show,@customer_id,0,@password)";
                        MySqlCommand cmd = new MySqlCommand(sql, conn, tran);
                        MySqlParameter[] pars = {
                            new MySqlParameter("@name",entity.name),
                            new MySqlParameter("@birthday", entity.birthday),
                            new MySqlParameter("@position", entity.position),
                            new MySqlParameter("@phone", entity.phone),
                            new MySqlParameter("@email", entity.email),
                            new MySqlParameter("@qq", entity.qq),
                            new MySqlParameter("@fax", entity.fax),
                            new MySqlParameter("@img", entity.img),
                            new MySqlParameter("@operate_time", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")),
                            new MySqlParameter("@client_name", entity.clientName),
                            new MySqlParameter("@client_address", entity.clientAddress),
                            new MySqlParameter("@client_phone", entity.clientPhone),
                            new MySqlParameter("@client_business", entity.clientBusiness),
                            new MySqlParameter("@client_url", entity.clientUrl),
                            new MySqlParameter("@zip", entity.zip),
                            new MySqlParameter("@nature", entity.nature),
                            new MySqlParameter("@classify", entity.classify),
                            new MySqlParameter("@legal_person", entity.legalPerson),
                            new MySqlParameter("@phone_show", entity.phoneShow),
                            new MySqlParameter("@position_show", entity.positionShow),
                            new MySqlParameter("@customer_id", entity.customerId),
                            new MySqlParameter("@password", Basic.setBase64(ConfigurationManager.AppSettings["initPassword"].ToString()))
                        };
                        cmd.Parameters.AddRange(pars);
                        count += cmd.ExecuteNonQuery();
                        //}
                    }
                    tran.Commit();
                    result = count.ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    result = "0";
                    tran.Rollback();
                }
                finally
                {
                    conn.Close();
                }
                return result;
            }
        }
        //查询准备
        public Contacts prepareAdd(string customer_id)
        {
            string select_sql = "select * from person where customer_id=" + customer_id + " order by id desc";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Contacts> arr = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts contacts = new Contacts();
                        contacts.id = Convert.ToString(dr["id"]);
                        contacts.name = Convert.ToString(dr["name"]);
                        contacts.birthday = Convert.ToString(dr["birthday"]);
                        contacts.position = Convert.ToString(dr["position"]);
                        contacts.phone = Convert.ToString(dr["phone"]);
                        contacts.email = Convert.ToString(dr["email"]);
                        contacts.qq = Convert.ToString(dr["qq"]);
                        contacts.fax = Convert.ToString(dr["fax"]);
                        contacts.img = Convert.ToString(dr["img"]);
                        contacts.operateTime = Convert.ToString(dr["operate_time"]);
                        contacts.clientName = Convert.ToString(dr["client_name"]);
                        contacts.clientAddress = Convert.ToString(dr["client_address"]);
                        contacts.clientPhone = Convert.ToString(dr["client_phone"]);
                        contacts.clientBusiness = Convert.ToString(dr["client_business"]);
                        contacts.clientUrl = Convert.ToString(dr["client_url"]);
                        contacts.zip = Convert.ToString(dr["zip"]);
                        contacts.nature = Convert.ToString(dr["nature"]);
                        contacts.classify = Convert.ToString(dr["classify"]);
                        contacts.legalPerson = Convert.ToString(dr["legal_person"]);
                        contacts.phoneShow = Convert.ToString(dr["phone_show"]);
                        contacts.positionShow = Convert.ToString(dr["position_show"]);
                        arr.Add(contacts);
                        break;
                    }
                    return arr[0];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return new Contacts();
                }
            }
        }
        //信息确认
        public int informationConfim(HashSet<PersonTemporary> entitys)
        {
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    int i = 0;
                    foreach (PersonTemporary entity in entitys)
                    {
                        string update_sql = "update person set operate_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                        if (!string.IsNullOrEmpty(entity.name))
                        {
                            update_sql += ",name='" + entity.name + "'";
                        }

                        if (!string.IsNullOrEmpty(entity.clientName))
                        {
                            update_sql += ",client_name='" + entity.clientName + "'";
                        }

                        if (!string.IsNullOrEmpty(entity.position))
                        {
                            update_sql += ",position='" + entity.position + "'";
                        }
                        if (!string.IsNullOrEmpty(entity.email))
                        {
                            update_sql += ",email='" + entity.email + "'";
                        }
                        if (!string.IsNullOrEmpty(entity.clientBusiness))
                        {
                            update_sql += ",client_business='" + entity.clientBusiness + "'";
                        }
                        MySqlCommand comm = new MySqlCommand(update_sql + " where id=" + entity.personId, conn, tran);
                        comm.ExecuteNonQuery();
                        i++;
                    }
                    tran.Commit();
                    return i;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("出错：" + ex.Message);
                    return 0;
                }
            }
        }
        //添加联系人
        public string addContacts(Contacts entity)
        {
            string insert_sql = "insert into person(name,birthday,position,phone,email,qq,fax,img,operate_time,client_name,client_address,client_phone,client_business,client_url,zip,nature,classify,legal_person,phone_show,position_show,customer_id,is_deleted,password) values(@name,@birthday,@position,@phone,@email,@qq,@fax,@img,@operate_time,@client_name,@client_address,@client_phone,@client_business,@client_url,@zip,@nature,@classify,@legal_person,@phone_show,@position_show,@customer_id,@is_deleted,@password)";
            string select_sql = "select last_insert_id()";
            string img = entity.img;
            if (!string.IsNullOrEmpty(img))
            {
                img = "-" + DateTime.Now.ToString("hhmmss") + entity.img;
            }

            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(entity.email) && Convert.ToInt32(Basic.getTr("person where email='" + entity.email + "'")) > 0)
                    {
                    }
                    else
                    {
                        MySqlCommand comm = new MySqlCommand(insert_sql, conn, tran);
                        MySqlCommand comm1 = new MySqlCommand(select_sql, conn);
                        comm.Parameters.AddWithValue("@name", entity.name);
                        comm.Parameters.AddWithValue("@birthday", entity.birthday);
                        comm.Parameters.AddWithValue("@position", entity.position);
                        comm.Parameters.AddWithValue("@phone", entity.phone);
                        comm.Parameters.AddWithValue("@email", entity.email);
                        comm.Parameters.AddWithValue("@qq", entity.qq);
                        comm.Parameters.AddWithValue("@fax", entity.fax);
                        comm.Parameters.AddWithValue("@img", img);
                        comm.Parameters.AddWithValue("@operate_time", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                        comm.Parameters.AddWithValue("@client_name", entity.clientName);
                        comm.Parameters.AddWithValue("@client_address", entity.clientAddress);
                        comm.Parameters.AddWithValue("@client_phone", entity.clientPhone);
                        comm.Parameters.AddWithValue("@client_business", entity.clientBusiness);
                        comm.Parameters.AddWithValue("@client_url", entity.clientUrl);
                        comm.Parameters.AddWithValue("@zip", entity.zip);
                        comm.Parameters.AddWithValue("@nature", entity.nature);
                        comm.Parameters.AddWithValue("@classify", entity.classify);
                        comm.Parameters.AddWithValue("@legal_person", entity.legalPerson);
                        comm.Parameters.AddWithValue("@phone_show", entity.phoneShow);
                        comm.Parameters.AddWithValue("@position_show", entity.positionShow);
                        comm.Parameters.AddWithValue("@customer_id", entity.customerId);
                        comm.Parameters.AddWithValue("@is_deleted", 0);
                        comm.Parameters.AddWithValue("@password", Basic.setBase64(ConfigurationManager.AppSettings["initPassword"].ToString()));
                        if (Convert.ToInt32(comm.ExecuteNonQuery()) > 0)
                        {
                            if (!string.IsNullOrEmpty(img))
                            {
                                result = comm1.ExecuteScalar() + img;
                            }
                        }
                        tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("出错：" + ex.Message);
                }
                return result;
            }
        }
        //修改头像
        public string modifyImg(string id, string img)
        {
            string str = "-" + DateTime.Now.ToString("hhmmss") + img;
            string update_sql = "update person set operate_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',img='" + str + "' where id=" + id;
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql, conn, tran);
                    if (Convert.ToInt32(comm.ExecuteNonQuery()) > 0)
                    {
                        result = str;
                    }
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("出错：" + ex.Message);
                }
                return result;
            }
        }

        //删除
        public string delSureContacts(Contacts entity)
        {
            string delete_sql = "delete from person where id=" + entity.id;
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlCommand comm = new MySqlCommand(delete_sql, conn);
                return Convert.ToString(comm.ExecuteNonQuery());
            }
        }


        //修改
        public string modifyContacts(Contacts entity)
        {
            string update_sql = "update person set operate_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',phone_show=" + entity.phoneShow + ",position_show=" + entity.positionShow;
            if (!string.IsNullOrEmpty(entity.name))
            {
                update_sql += ",name='" + entity.name + "'";
            }

            if (!string.IsNullOrEmpty(entity.birthday))
            {
                update_sql += ",birthday='" + entity.birthday + "'";
            }

            if (!string.IsNullOrEmpty(entity.position))
            {
                update_sql += ",position='" + entity.position + "'";
            }
            if (!string.IsNullOrEmpty(entity.phone))
            {
                update_sql += ",phone='" + entity.phone + "'";
            }
            if (!string.IsNullOrEmpty(entity.email))
            {
                update_sql += ",email='" + entity.email + "'";
            }
            if (!string.IsNullOrEmpty(entity.qq))
            {
                update_sql += ",qq='" + entity.qq + "'";
            }
            if (!string.IsNullOrEmpty(entity.fax))
            {
                update_sql += ",fax='" + entity.fax + "'";
            }

            if (!string.IsNullOrEmpty(entity.clientName))
            {
                update_sql += ",client_name='" + entity.clientName + "'";
            }

            if (!string.IsNullOrEmpty(entity.clientAddress))
            {
                update_sql += ",client_address='" + entity.clientAddress + "'";
            }
            if (!string.IsNullOrEmpty(entity.clientPhone))
            {
                update_sql += ",client_phone='" + entity.clientPhone + "'";
            }
            if (!string.IsNullOrEmpty(entity.clientBusiness))
            {
                update_sql += ",client_business='" + entity.clientBusiness + "'";
            }
            if (!string.IsNullOrEmpty(entity.clientUrl))
            {
                update_sql += ",client_url='" + entity.clientUrl + "'";
            }
            if (!string.IsNullOrEmpty(entity.zip))
            {
                update_sql += ",zip='" + entity.zip + "'";
            }
            if (!string.IsNullOrEmpty(entity.nature))
            {
                update_sql += ",nature='" + entity.nature + "'";
            }
            if (!string.IsNullOrEmpty(entity.classify))
            {
                update_sql += ",classify='" + entity.classify + "'";
            }
            if (!string.IsNullOrEmpty(entity.legalPerson))
            {
                update_sql += ",legal_person='" + entity.legalPerson + "'";
            }

            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql + " where id=" + entity.id, conn, tran);
                    result = Convert.ToString(comm.ExecuteNonQuery());
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("出错：" + ex.Message);
                }
                return result;
            }
        }

        public HashSet<Contacts> queryAll(Contacts entity)
        {
            string select_sql = "select * from person";
            string where_sql = getWhereSql(entity);
            string page_sql = select_sql + where_sql;
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(page_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    HashSet<Contacts> arr = new HashSet<Contacts>();
                    while (dr.Read())
                    {
                        Contacts contacts = new Contacts();
                        contacts.id = Convert.ToString(dr["id"]);
                        contacts.name = Convert.ToString(dr["name"]);
                        contacts.birthday = Convert.ToString(dr["birthday"]);
                        contacts.position = Convert.ToString(dr["position"]);
                        contacts.phone = Convert.ToString(dr["phone"]);
                        contacts.email = Convert.ToString(dr["email"]);
                        contacts.qq = Convert.ToString(dr["qq"]);
                        contacts.fax = Convert.ToString(dr["fax"]);
                        contacts.img = Convert.ToString(dr["img"]);
                        contacts.operateTime = Convert.ToString(dr["operate_time"]);
                        contacts.clientName = Convert.ToString(dr["client_name"]);
                        contacts.clientAddress = Convert.ToString(dr["client_address"]);
                        contacts.clientPhone = Convert.ToString(dr["client_phone"]);
                        contacts.clientBusiness = Convert.ToString(dr["client_business"]);
                        contacts.clientUrl = Convert.ToString(dr["client_url"]);
                        contacts.zip = Convert.ToString(dr["zip"]);
                        contacts.nature = Convert.ToString(dr["nature"]);
                        contacts.classify = Convert.ToString(dr["classify"]);
                        contacts.legalPerson = Convert.ToString(dr["legal_person"]);
                        contacts.phoneShow = Convert.ToString(dr["phone_show"]);
                        contacts.positionShow = Convert.ToString(dr["position_show"]);
                        arr.Add(contacts);
                    }
                    return arr;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return null;
                }
            }
        }
        //查询所有
        public Paging<Contacts> query(Paging<Contacts> page)
        {
            string select_sql = "select * from person";
            string where_sql = getWhereSql(page.condition);
            string limit_sql = " limit @firstItem,@endItem";
            string page_sql = select_sql + where_sql + limit_sql;
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(page_sql, conn);
                    comm.Parameters.AddWithValue("@firstItem", (page.pc - 1) * page.ps);
                    comm.Parameters.AddWithValue("@endItem", page.ps);
                    MySqlDataReader dr = comm.ExecuteReader();
                    page.tr = Convert.ToInt32(Basic.getTr("person" + where_sql));
                    page.tp = Basic.getTp(page.tr, page.ps);
                    page.datas = new HashSet<Contacts>();
                    while (dr.Read())
                    {
                        Contacts contacts = new Contacts();
                        contacts.id = Convert.ToString(dr["id"]);
                        contacts.name = Convert.ToString(dr["name"]);
                        contacts.birthday = Convert.ToString(dr["birthday"]);
                        contacts.position = Convert.ToString(dr["position"]);
                        contacts.phone = Convert.ToString(dr["phone"]);
                        contacts.email = Convert.ToString(dr["email"]);
                        contacts.qq = Convert.ToString(dr["qq"]);
                        contacts.fax = Convert.ToString(dr["fax"]);
                        contacts.img = Convert.ToString(dr["img"]);
                        contacts.operateTime = Convert.ToString(dr["operate_time"]);
                        contacts.clientName = Convert.ToString(dr["client_name"]);
                        contacts.clientAddress = Convert.ToString(dr["client_address"]);
                        contacts.clientPhone = Convert.ToString(dr["client_phone"]);
                        contacts.clientBusiness = Convert.ToString(dr["client_business"]);
                        contacts.clientUrl = Convert.ToString(dr["client_url"]);
                        contacts.zip = Convert.ToString(dr["zip"]);
                        contacts.nature = Convert.ToString(dr["nature"]);
                        contacts.classify = Convert.ToString(dr["classify"]);
                        contacts.legalPerson = Convert.ToString(dr["legal_person"]);
                        contacts.phoneShow = Convert.ToString(dr["phone_show"]);
                        contacts.positionShow = Convert.ToString(dr["position_show"]);
                        page.datas.Add(contacts);
                    }
                    return page;
                }
                catch (Exception ex)
                {
                    Loger.WriteError("出错：" + ex.Message);
                    return null;
                }
            }
        }

        //判断查询条件
        public string getWhereSql(Contacts entity)
        {
            try
            {
                string where_sql = " where is_deleted=0 and customer_id=" + entity.customerId;
                if (!string.IsNullOrEmpty(entity.name))
                {
                    where_sql += " and name like '%" + entity.name + "%'";
                }
                if (!string.IsNullOrEmpty(entity.phone))
                {
                    where_sql += " and phone like '%" + entity.phone + "%'";
                }
                if (!string.IsNullOrEmpty(entity.clientName))
                {
                    where_sql += " and client_name like '%" + entity.clientName + "%'";
                }
                return where_sql + " order by position desc";
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错：" + ex.Message);
                return null;
            }
        }

        //判断查询条件
        public string getWhereJoinSql(Contacts entity)
        {
            try
            {
                string where_sql = " where a.is_deleted=0 and a.customer_id=" + entity.customerId;
                if (!string.IsNullOrEmpty(entity.name))
                {
                    where_sql += " and a.name like '%" + entity.name + "%'";
                }
                if (!string.IsNullOrEmpty(entity.phone))
                {
                    where_sql += " and a.phone like '%" + entity.phone + "%'";
                }
                if (!string.IsNullOrEmpty(entity.clientName))
                {
                    where_sql += " and a.client_name like '%" + entity.clientName + "%'";
                }
                return where_sql + " order by a.position desc";
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错：" + ex.Message);
                return null;
            }
        }
        //判断登陆
        public Contacts login(Contacts entity)
        {
            //string loging_sql = "select * from person where is_deleted=0 and phone=@phone and password=@password and customer_id=@customer_id";
            string loging_sql = "SELECT a.*, b.name AS customerName FROM person AS a LEFT JOIN customer AS b ON b.id = a.customer_id WHERE a.is_deleted=0 AND a.phone=@phone AND a.password=@password AND a.customer_id=@customer_id";
            string select_sql = "select id from person where phone=@phone";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm1 = new MySqlCommand(loging_sql, conn);
                    comm1.Parameters.AddWithValue("@phone", entity.phone);
                    comm1.Parameters.AddWithValue("@customer_id", entity.customerId);
                    comm1.Parameters.AddWithValue("@password", Basic.setBase64(entity.password));

                    MySqlDataReader dr = comm1.ExecuteReader();
                    while (dr.Read())
                    {
                        entity.id = Convert.ToString(dr["id"]);
                        entity.customerId = Convert.ToString(dr["customer_id"]);
                        entity.clientName = Convert.ToString(dr["client_name"]);
                        entity.position = Convert.ToString(dr["position"]);
                        entity.name = Convert.ToString(dr["name"]);
                        entity.birthday = Convert.ToString(dr["birthday"]);
                        entity.email = Convert.ToString(dr["email"]);
                        entity.qq = Convert.ToString(dr["qq"]);
                        entity.fax = Convert.ToString(dr["fax"]);
                        entity.clientAddress = Convert.ToString(dr["client_address"]);
                        entity.clientPhone = Convert.ToString(dr["client_phone"]);
                        entity.clientBusiness = Convert.ToString(dr["client_business"]);
                        entity.clientUrl = Convert.ToString(dr["client_url"]);
                        entity.zip = Convert.ToString(dr["zip"]);
                        entity.relevanceId = Convert.ToString(dr["relevance_id"]);
                        entity.customerName = Convert.ToString(dr["customerName"]);
                    }
                    dr.Close();


                    MySqlCommand comm2 = new MySqlCommand(select_sql, conn);
                    comm2.Parameters.AddWithValue("@phone", entity.phone);
                    var id = comm2.ExecuteScalar();
                    string update_sql = "update person set relevance_id=" + id + " where phone=@phone";

                    MySqlCommand comm3 = new MySqlCommand(update_sql, conn);
                    comm3.Parameters.AddWithValue("@phone", entity.phone);
                    comm3.ExecuteNonQuery();
                    return entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return entity;
                }
                //finally
                //{
                //    if (conn != null)
                //    {
                //        conn.Close();
                //    }
                //}

            }
        }




        //添加账号的时候判断登陆
        public Contacts loginRele(Contacts entity, string firstPhone)
        {
            string loging_sql = "SELECT a.*, b.name AS customerName FROM person AS a LEFT JOIN customer AS b ON b.id = a.customer_id WHERE a.is_deleted=0 AND a.phone=@phone AND a.password=@password";
            //string loging_sql = "select * from person where is_deleted=0 and phone=@phone and password=@password";
            string select_sql = "select relevance_id from person where phone=@phone";//上一个phone
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm1 = new MySqlCommand(loging_sql, conn);
                    comm1.Parameters.AddWithValue("@phone", entity.phone);
                    comm1.Parameters.AddWithValue("@password", Basic.setBase64(entity.password));

                    MySqlDataReader dr = comm1.ExecuteReader();//查询
                    while (dr.Read())
                    {
                        entity.id = Convert.ToString(dr["id"]);
                        entity.customerId = Convert.ToString(dr["customer_id"]);
                        entity.clientName = Convert.ToString(dr["client_name"]);
                        entity.position = Convert.ToString(dr["position"]);
                        entity.name = Convert.ToString(dr["name"]);
                        entity.birthday = Convert.ToString(dr["birthday"]);
                        entity.email = Convert.ToString(dr["email"]);
                        entity.qq = Convert.ToString(dr["qq"]);
                        entity.fax = Convert.ToString(dr["fax"]);
                        entity.clientAddress = Convert.ToString(dr["client_address"]);
                        entity.clientPhone = Convert.ToString(dr["client_phone"]);
                        entity.clientBusiness = Convert.ToString(dr["client_business"]);
                        entity.clientUrl = Convert.ToString(dr["client_url"]);
                        entity.zip = Convert.ToString(dr["zip"]);
                        entity.relevanceId = Convert.ToString(dr["relevance_id"]);
                        entity.customerName = Convert.ToString(dr["customerName"]);
                    }
                    dr.Close();


                    MySqlCommand comm2 = new MySqlCommand(select_sql, conn);
                    comm2.Parameters.AddWithValue("@phone", firstPhone);
                    var relevance_id = comm2.ExecuteScalar();//只返回一条数据
                    string update_sql = "update person set relevance_id=" + relevance_id + " where phone=@phone";

                    MySqlCommand comm3 = new MySqlCommand(update_sql, conn);
                    comm3.Parameters.AddWithValue("@phone", entity.phone);
                    comm3.ExecuteNonQuery();//执行增删改
                    return entity;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return entity;
                }
            }
        }

        //App查询所有Contacts
        public List<Contacts> getContacts(Contacts entity)
        {

            string select_sql = "SELECT a.*, b.name AS customerName FROM person AS a LEFT JOIN customer AS b ON b.id = a.customer_id";
            //string select_sql = "select * from person";
            string where_sql = getWhereJoinSql(entity);
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql + where_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Contacts> list = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts contacts = new Contacts();
                        contacts.id = Convert.ToString(dr["id"]);
                        contacts.name = Convert.ToString(dr["name"]);
                        contacts.birthday = Convert.ToString(dr["birthday"]);
                        contacts.position = Convert.ToString(dr["position"]);
                        contacts.phone = Convert.ToString(dr["phone"]);
                        contacts.email = Convert.ToString(dr["email"]);
                        contacts.qq = Convert.ToString(dr["qq"]);
                        contacts.fax = Convert.ToString(dr["fax"]);
                        contacts.img = Convert.ToString(dr["img"]);
                        contacts.operateTime = Convert.ToString(dr["operate_time"]);
                        contacts.clientName = Convert.ToString(dr["client_name"]);
                        contacts.clientAddress = Convert.ToString(dr["client_address"]);
                        contacts.clientPhone = Convert.ToString(dr["client_phone"]);
                        contacts.clientBusiness = Convert.ToString(dr["client_business"]);
                        contacts.clientUrl = Convert.ToString(dr["client_url"]);
                        contacts.zip = Convert.ToString(dr["zip"]);
                        contacts.nature = Convert.ToString(dr["nature"]);
                        contacts.classify = Convert.ToString(dr["classify"]);
                        contacts.legalPerson = Convert.ToString(dr["legal_person"]);
                        contacts.phoneShow = Convert.ToString(dr["phone_show"]);
                        contacts.positionShow = Convert.ToString(dr["position_show"]);
                        contacts.password = Basic.getBase64(Convert.ToString(dr["password"]));
                        contacts.customerName = Convert.ToString(dr["customerName"]);
                        list.Add(contacts);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
        }
        //上传信息修改
        public string modifyContactsTemporary(Contacts entity)
        {
            string update_sql = "update person_temporary set is_confim=0,operate_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            if (!string.IsNullOrEmpty(entity.name))
            {
                update_sql += ",name='" + entity.name + "'";
            }

            if (!string.IsNullOrEmpty(entity.clientName))
            {
                update_sql += ",client_name='" + entity.clientName + "'";
            }

            if (!string.IsNullOrEmpty(entity.position))
            {
                update_sql += ",position='" + entity.position + "'";
            }
            if (!string.IsNullOrEmpty(entity.email))
            {
                update_sql += ",email='" + entity.email + "'";
            }
            if (!string.IsNullOrEmpty(entity.clientBusiness))
            {
                update_sql += ",client_business='" + entity.clientBusiness + "'";
            }
            update_sql += " where person_id=" + entity.personId;

            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql, conn, tran);
                    result = Convert.ToString(comm.ExecuteNonQuery());
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("出错：" + ex.Message);
                }
                return result;
            }
        }
        //上传信息添加
        public string addContactsTemporary(Contacts entity)
        {
            string insert_sql = "insert into person_temporary(client_business,name,client_name,position,person_id,email,operate_time,customer_id,is_confim) values(@client_business,@name,@client_name,@position,@person_id,@email,@operate_time,(select customer_id from person where id=" + entity.personId + "),0)";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(insert_sql, conn, tran);
                    comm.Parameters.AddWithValue("@client_business", entity.clientBusiness);
                    comm.Parameters.AddWithValue("@name", entity.name);
                    comm.Parameters.AddWithValue("@client_name", entity.clientName);
                    comm.Parameters.AddWithValue("@position", entity.position);
                    comm.Parameters.AddWithValue("@person_id", entity.personId);
                    comm.Parameters.AddWithValue("@email", entity.email);
                    comm.Parameters.AddWithValue("@client_address", entity.clientAddress);
                    comm.Parameters.AddWithValue("@operate_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    result = Convert.ToString(comm.ExecuteNonQuery());
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Console.WriteLine("出错：" + ex.Message);
                }
                return result;
            }
        }

        //查询person表的name 和  phone、id(sendPhone)
        public List<Contacts> queryAll()
        {
            string select_sql = "select id,name,phone from person";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Contacts> arr = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts entity = new Contacts();
                        entity.id = Convert.ToString(dr["id"]);
                        entity.name = Convert.ToString(dr["name"]);
                        entity.phone = Convert.ToString(dr["phone"]);
                        arr.Add(entity);
                    }
                    return arr;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        }

        //查询person表中所有phone
        public List<Contacts> queryPhone()
        {
            string select_sql = "select distinct phone from person";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Contacts> arr = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts entity = new Contacts();
                        entity.phone = Convert.ToString(dr["phone"]);
                        arr.Add(entity);
                    }
                    return arr;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        }

        public List<Contacts> queryName(string name)
        {
            string selectName_sql = "select name,phone from person where name like '%" + name + "%'";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(selectName_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Contacts> arr = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts entity = new Contacts();
                        entity.name = Convert.ToString(dr["name"]);
                        entity.phone = Convert.ToString(dr["phone"]);
                        arr.Add(entity);
                    }
                    return arr;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
        }

        //查询关联账号
        public List<dynamic> selectRele(Contacts entity)
        {
            string select_sql = "select a.*,b.name clientName from(select id,phone,password,relevance_id,customer_id,name from person where relevance_id=(select relevance_id from person where phone=@phone)) a left join customer b on a.customer_id=b.id;";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    comm.Parameters.AddWithValue("@phone", entity.phone);
                    MySqlDataReader dr = comm.ExecuteReader();

                    List<dynamic> arr = new List<dynamic>();
                    while (dr.Read())
                    {
                        dynamic model = new ExpandoObject();
                        model.id = Convert.ToString(dr["id"]);
                        model.phone = Convert.ToString(dr["phone"]);
                        model.password = Basic.getBase64(Convert.ToString(dr["password"]));
                        model.relevanceId = Convert.ToString(dr["relevance_id"]);
                        model.customerId = Convert.ToString(dr["customer_id"]);
                        model.name = Convert.ToString(dr["name"]);
                        model.clientName = Convert.ToString(dr["clientName"]);
                        arr.Add(model);
                    }
                    dr.Close();
                    return arr;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }

            }
        }

        //取消账号关联
        public object cancelRele(string phone)
        {
            string select_sql = "select id from person where phone=@phone";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm1 = new MySqlCommand(select_sql, conn);
                    comm1.Parameters.AddWithValue("@phone", phone);
                    var id = comm1.ExecuteScalar();
                    string update_sql = "update person set relevance_id=null where phone=@phone";

                    MySqlCommand comm2 = new MySqlCommand(update_sql, conn);
                    comm2.Parameters.AddWithValue("@phone", phone);
                    comm2.ExecuteNonQuery();//执行增删改
                    return comm2.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
        }

        //查询同一个手机号的所有组织，公司
        public List<Contacts> queryAllPhone(Contacts mode)
        {
            string select_sql = "select id,name,phone,password,client_name,customer_id from person where phone=@phone and password=@password";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    comm.Parameters.AddWithValue("@phone", mode.phone);
                    comm.Parameters.AddWithValue("@password", Basic.setBase64(mode.password));
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Contacts> arr = new List<Contacts>();
                    while (dr.Read())
                    {
                        Contacts entity = new Contacts();
                        entity.id = Convert.ToString(dr["id"]);
                        entity.name = Convert.ToString(dr["name"]);
                        entity.phone = Convert.ToString(dr["phone"]);
                        entity.password = Basic.getBase64(Convert.ToString(dr["password"]));
                        entity.clientName = Convert.ToString(dr["client_name"]);
                        entity.customerId = Convert.ToString(dr["customer_id"]);
                        arr.Add(entity);
                    }
                    return arr;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }

        }


    }
}