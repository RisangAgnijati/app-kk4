using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace app_kk3
{
    internal class Connection
    {
        public static MySqlConnection getConnection()
        {
            MySqlConnection conn = null;

            try
            {
                string sConnstring = "server = localhost;database= db_kk3;uid=root;password=;";
                conn = new MySqlConnection(sConnstring);
            }
            catch (MySqlException sqlex)
            {
                throw new Exception(sqlex.Message.ToString());
            }
            return conn;
        }
    }
}
