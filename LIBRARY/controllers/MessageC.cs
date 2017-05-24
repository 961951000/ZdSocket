using LIBRARY.models;
using LIBRARY.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LIBRARY.controllers
{
    public class MessageC
    {
        string result = string.Empty;
        //添加发文内容
        public string addFw(Message entity)
        {
            string insert_sql = "insert into message(id,title,content,customer_id) values(@id,@title,@content,@customer_id)";
            using (MySqlConnection conn = Factory.getConnection())
            {
               
                    MySqlCommand comm = new MySqlCommand(insert_sql, conn);
                    comm.Parameters.AddWithValue("@id", entity.id);
                    comm.Parameters.AddWithValue("@title",entity.title);
                    comm.Parameters.AddWithValue("@content",entity.content);
                    comm.Parameters.AddWithValue("@customer_id", entity.customerId);
                    return Convert.ToString(comm.ExecuteNonQuery());
               
            }
        }

        //查询message表的title
        public List<Message> getTitle()
        {
            string select_sql = "select title from message";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Message> arr = new List<Message>();
                    while (dr.Read())
                    {
                        Message entity = new Message();
                        entity.title = Convert.ToString(dr["title"]);
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


        //查询出所有的发文内容集合
        public HashSet<Message> queryAll(Message entity)
        {
            string select_sql1 = "select * from message";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql1, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    HashSet<Message> arr = new HashSet<Message>();
                    while (dr.Read())
                    {
                        Message message = new Message();
                        message.id = Convert.ToString(dr["id"]);
                        message.title = Convert.ToString(dr["title"]);
                        message.content = Convert.ToString(dr["content"]);
                        message.customerId = Convert.ToString(dr["customer_id"]);
                        arr.Add(message);
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

        //删除发文内容
        public string del(string id) {
            string del_sql = "delete from message where id="+id;
            using (MySqlConnection conn = Factory.getConnection())
            {
                    MySqlCommand comm = new MySqlCommand(del_sql, conn);
                    return Convert.ToString(comm.ExecuteNonQuery());
            }
        }



        //修改（编辑）发文内容
      public string updateFw(Message entity)
        {
            string update_sql = "update message set title=@title,content=@content where id=@id";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql, conn, tran);
                    comm.Parameters.AddWithValue("@title", entity.title);
                    comm.Parameters.AddWithValue("@content", entity.content);
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


    }
}