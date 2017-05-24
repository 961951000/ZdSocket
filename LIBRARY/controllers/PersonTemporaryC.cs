using LIBRARY.models;
using LIBRARY.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY.controllers
{
    class PersonTemporaryC
    {
        string result = string.Empty;
        //驳回   
        public string no(string id)
        {
            string update_sql = "update person_temporary set is_confim=1,submit_time=@submit_time where id in(" + id + ")";
            using (MySqlConnection conn = Factory.getConnection())
            {
                MySqlTransaction tran = conn.BeginTransaction();
                try
                {
                    MySqlCommand comm = new MySqlCommand(update_sql, conn, tran);
                    comm.Parameters.AddWithValue("@submit_time",DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
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
        //根据id查询   
        public HashSet<PersonTemporary> query(string id)
        {
            string select_sql = "select id,name,client_name,client_business,position,email,operate_time,submit_time,person_id from person_temporary where id in(" + id + ")";
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    HashSet<PersonTemporary> arr = new HashSet<PersonTemporary>();
                    while (dr.Read())
                    {
                        PersonTemporary personTemporary = new PersonTemporary();
                        personTemporary.name = Convert.ToString(dr["name"]);
                        personTemporary.clientName = Convert.ToString(dr["client_name"]);
                        personTemporary.clientBusiness = Convert.ToString(dr["client_business"]);
                        personTemporary.position = Convert.ToString(dr["position"]);
                        personTemporary.email = Convert.ToString(dr["email"]);
                        personTemporary.personId = Convert.ToString(dr["person_id"]);
                        personTemporary.operateTime = Convert.ToString(dr["operate_time"]);
                        arr.Add(personTemporary);
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
        public Paging<PersonTemporary> query(Paging<PersonTemporary> page)
        {
            string select_sql = "select id,name,client_name,position,email,client_business,operate_time,submit_time from person_temporary";
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
                    page.tr = Convert.ToInt32(Basic.getTr("person_temporary" + where_sql));
                    page.tp = Basic.getTp(page.tr, page.ps);
                    page.datas = new HashSet<PersonTemporary>();
                    while (dr.Read())
                    {
                        PersonTemporary personTemporary = new PersonTemporary();
                        personTemporary.id = Convert.ToString(dr["id"]);
                        personTemporary.name = Convert.ToString(dr["name"]);
                        personTemporary.clientName = Convert.ToString(dr["client_name"]);
                        personTemporary.position = Convert.ToString(dr["position"]);
                        personTemporary.email = Convert.ToString(dr["email"]);
                        personTemporary.clientBusiness = Convert.ToString(dr["client_business"]);
                        personTemporary.submitTime = Convert.ToString(dr["submit_time"]);
                        personTemporary.operateTime = Convert.ToString(dr["operate_time"]);
                        page.datas.Add(personTemporary);
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
        public string getWhereSql(PersonTemporary entity)
        {
            try
            {
                string where_sql = " where customer_id=" + entity.customerId + " and is_confim=0";
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
