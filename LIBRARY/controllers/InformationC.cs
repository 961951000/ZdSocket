using LIBRARY.models;
using LIBRARY.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.controllers
{
    public class InformationC
    {
        string result = string.Empty;
        //修改栏目信息
        public string setTemplate(Information entity)
        {
            string update_sql1 = "update information set is_select=0 where is_deleted=0 and customer_id=" + entity.customerId;
            string update_sql2 = "update information set is_select=1 where is_deleted=0 and customer_id=" + entity.customerId + " and id in(" + entity.id + ")";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm1 = new MySqlCommand(update_sql1, conn, tran);
                    MySqlCommand comm2 = new MySqlCommand(update_sql2, conn, tran);
                    comm1.ExecuteNonQuery();
                    result = Convert.ToString(comm2.ExecuteNonQuery());
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

        //查询选中
        public List<Information> queryAllIsSelect(string customer_id)
        {
            string stlect_sql = "select id,name,is_select from information where is_deleted=0 and is_select=1 and customer_id=" + customer_id;
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(stlect_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Information> arr = new List<Information>();
                    while (dr.Read())
                    {
                        Information entity = new Information();
                        entity.id = Convert.ToString(dr["id"]);
                        entity.name = Convert.ToString(dr["name"]);
                        entity.is_select = Convert.ToBoolean(dr["is_select"]);
                        arr.Add(entity);
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
        //查询全部  and  App查询所有Information
        public List<Information> queryAll(string customer_id)
        {
            string stlect_sql = "select * from information where is_deleted=0 and customer_id=" + customer_id;
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(stlect_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Information> arr = new List<Information>();
                    while (dr.Read())
                    {
                        Information entity = new Information();
                        entity.id = Convert.ToString(dr["id"]);
                        entity.name = Convert.ToString(dr["name"]);
                        entity.is_select = Convert.ToBoolean(dr["is_select"]);
                        entity.customerId = Convert.ToString(dr["customer_id"]);
                        entity.isDeleted = Convert.ToBoolean(dr["is_deleted"]);
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