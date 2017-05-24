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
    public class CustomerC
    {
        //查询所有
        public Paging<Customer> query(Paging<Customer> page)
        {
            string select_sql = "select * from customer";
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
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return null;
                }
            }
        }

        //判断查询条件
        public string getWhereSql(Customer entity)
        {
            try
            {
                string where_sql = " where is_deleted=0 and id=" + entity.id;
                return where_sql + " order by id desc";
            }
            catch (Exception ex)
            {
                Console.WriteLine("出错：" + ex.Message);
                return null;
            }
        }

        //App查询所有Customer
        public List<Customer> getCustomer(Customer entity)
        {           
            string select_sql = "select * from customer";
            string where_sql = getWhereSql(entity);
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql + where_sql, conn);
                    MySqlDataReader dr = comm.ExecuteReader();
                    List<Customer> list = new List<Customer>();
                    while (dr.Read())
                    {
                        Customer customer = new Customer();
                        customer.id = Convert.ToString(dr["id"]);
                        customer.name = Convert.ToString(dr["name"]);
                        customer.customerCode = Convert.ToString(dr["customer_code"]);
                        customer.address = Convert.ToString(dr["address"]);
                        customer.contacts = Convert.ToString(dr["contacts"]);
                        customer.openDate = Convert.ToString(dr["open_date"]);
                        list.Add(customer);
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    Loger.WriteError(ex);
                    return null;
                }
            }

        }
    }
}