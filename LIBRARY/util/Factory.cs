using Log;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace LIBRARY.Util
{
    public class Factory
    {
        public static MySqlConnection getConnection()
        {
            try
            {
                var connection = new MySqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                Loger.WriteError(ex);
                return null;               
            }

        }
    }
}