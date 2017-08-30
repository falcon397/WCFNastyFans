using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using MySql.Data.MySqlClient;

namespace WCFNastyFans
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : INastyFans
    {
        public string GetBuysData()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "192.168.1.2";
            conn_string.Port = 3306;
            conn_string.UserID = "analyzer";
            conn_string.Password = "9rmXo5X0z6UCXHBW";
            conn_string.Database = "analyzer";

            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            using (MySqlCommand cmd = new MySqlCommand("select * from buys"))
            {
                try
                {
                    conn.Open();
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                            }
                        }
                    }
                }

                finally
                {
                    conn.Close();
                }
            }

            return "";
        }
    }
}
