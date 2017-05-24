using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace LIBRARY.Util
{
    public class Basic
    {
        public static string getTr(string str)
        {
            string count_sql = "select count(*) from " + str;
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(count_sql, conn);
                    return Convert.ToString(comm.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return "0";
                }
            }
        }
        public static int getTp(int tr, int ps)
        {
            return tr % ps == 0 ? tr / ps : tr / ps + 1;
        }

        //根据id查询名称
        public static string getName(string tableName, int id)
        {
            string select_sql = "select name from " + tableName + " where is_deleted =0 and id=@id";
            string name = string.Empty;
            using (MySqlConnection conn = Factory.getConnection())
            {
                try
                {
                    MySqlCommand comm = new MySqlCommand(select_sql, conn);
                    comm.Parameters.AddWithValue("@id", id);
                    MySqlDataReader dr = comm.ExecuteReader();
                    while (dr.Read())
                    {
                        name = Convert.ToString(dr["name"]);
                    }
                    return name;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("出错：" + ex.Message);
                    return "";
                }
            }
        }

        public static string getJSON(object o)
        {
            //DataContractJsonSerializer json = new DataContractJsonSerializer(o.GetType());
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    json.WriteObject(stream, o);
            //    return System.Text.Encoding.UTF8.GetString(stream.ToArray());
            //}
            return JsonConvert.SerializeObject(o);
        }

        //判断文件是否是图片
        public static Boolean isImage(string path)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        //base64加密
        public static string setBase64(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        //base64解密
        public static string getBase64(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            return Encoding.Default.GetString(outputb);
        }

        //获取每页记录条数
        public static int getPs()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["pagesize"].ToString());
        }

        //获取初始密码
        public static string getInitPassword()
        {
            return ConfigurationManager.AppSettings["initPassword"].ToString();
        }
    }
}