using LIBRARY.models;
using LIBRARY.Util;
using Log;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.controllers
{
    public class AdminC
    {
        string result = string.Empty;
        //修改密码
        public string modyfyPassword(Admin entity)
        {
            string update_sql = "update admin set password='" + Basic.setBase64(entity.newPassword) + "' where id=" + entity.id + " and password='" + Basic.setBase64(entity.password) + "'";
            if (!string.IsNullOrEmpty(entity.id))
            {
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
                }
            }
            return result;
        }
        //添加信息操作员
        public string addOperator(Admin entity)
        {
            string insert_sql = "insert into admin(username,password,role,customer_id,is_deleted) values(@username,@password,@role,@customer_id,@is_deleted)";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(insert_sql, conn, tran);
                    comm.Parameters.AddWithValue("@username", entity.username);
                    comm.Parameters.AddWithValue("@password", Basic.setBase64(entity.password));
                    comm.Parameters.AddWithValue("@role", "信息操作员");
                    comm.Parameters.AddWithValue("@customer_id", entity.customerId);
                    comm.Parameters.AddWithValue("@is_deleted", 0);
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
        //判断用户名是否存在
        public string ifAdminName(string username)
        {
            try
            {
                string where_sql = "admin where is_deleted=0";
                if (!string.IsNullOrEmpty(username))
                {
                    where_sql += " and username='" + username + "'";
                }
                return Convert.ToString(Basic.getTr(where_sql));
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错：" + ex.Message);
                return null;
            }
        }
        //密码重置
        public string resetPassword(Admin entity)
        {
            string update_sql = "update admin set password=@password where id=@id";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql, conn, tran);
                    comm.Parameters.AddWithValue("@password", Basic.setBase64(entity.password));
                    comm.Parameters.AddWithValue("@id", entity.id);
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
        //修改账户
        public string modifyAdmin(Admin entity)
        {
            string update_sql = "update admin set username=@username where id=@id";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql, conn, tran);
                    comm.Parameters.AddWithValue("@username", entity.username);
                    comm.Parameters.AddWithValue("@id", entity.id);
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
        //注销用户
        public string deleteAdmin(string id)
        {
            string update_sql = "update admin set is_deleted=1 where id=" + id;
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
        //判断登陆
        public Admin login(Admin entity)
        {

            string loging_sql = "select id,role,serial,customer_id from admin where is_deleted=0 and username=@username and password=@password";

            using (MySqlConnection conn = Factory.getConnection())
            {
                if (conn == null)
                {
                    return null;
                }
                try
                {
                    MySqlCommand comm = new MySqlCommand(loging_sql, conn);
                    comm.Parameters.AddWithValue("@username", entity.username);
                    comm.Parameters.AddWithValue("@password", Basic.setBase64(entity.password));
                    MySqlDataReader dr = comm.ExecuteReader();
                    while (dr.Read())
                    {
                        entity.id = Convert.ToString(dr["id"]);
                        entity.role = Convert.ToString(dr["role"]);
                        entity.serial = Convert.ToString(dr["serial"]);
                        entity.customerId = Convert.ToString(dr["customer_id"]);
                    }
                    conn.Close();
                    return entity;
                }
                catch (Exception ex)
                {
                    Loger.WriteError(ex);
                    return null;
                }
            }
        }

        //返回用户信息分页数据
        public Paging<Admin> query(Paging<Admin> page)
        {
            string select_sql = "select * from admin";
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
                    page.tr = Convert.ToInt32(Basic.getTr("admin" + where_sql));
                    page.tp = Basic.getTp(page.tr, page.ps);
                    page.datas = new HashSet<Admin>();
                    while (dr.Read())
                    {
                        Admin admin = new Admin();
                        admin.id = Convert.ToString(dr["id"]);
                        admin.username = Convert.ToString(dr["username"]);
                        admin.password = Basic.getBase64(Convert.ToString(dr["password"]));
                        admin.serial = Convert.ToString(dr["serial"]);
                        admin.role = Convert.ToString(dr["role"]);
                        page.datas.Add(admin);
                    }
                    return page;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return null;
                }
            }
        }

        //判断查询条件
        public string getWhereSql(Admin entity)
        {
            try
            {
                string where_sql = " where is_deleted=0 and customer_id=" + entity.customerId;
                if (!string.IsNullOrEmpty(entity.username))
                {
                    where_sql += " and username='" + entity.username + "'";
                }
                return where_sql + " order by id desc";
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错：" + ex.Message);
                return null;
            }
        }
    }
}
