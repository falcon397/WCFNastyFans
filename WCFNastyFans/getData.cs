using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;

namespace WCFNastyFans
{
    public class getData
    {
        public string returnJSON;

        public getData(MySqlConnection conn, MySqlCommand cmd)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            //Always try your connections
            try
            {
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
            }

            //Because when they fail you make sure the garbage collector is working
            finally
            {
                conn.Close();
            }

            //Loop through row and column to parse the data for JSON serialization
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            //Serialize the JSON return object and send it back to the client
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            returnJSON = serialize.Serialize(rows);
        }
    }
}