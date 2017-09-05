using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using System.Data;
using Newtonsoft.Json;

namespace WCFNastyFans
{
    public class NastyFanService : INastyFans
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetBuysData()
        {
            //Create connection string, sql command, and data tables and sets needed to sort the data
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnAnalyzer"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("select date, seats, price_per_seat from buys", conn);
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
            List<Dictionary<string, object>> lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(row);
            }

            //Serialize the JSON return object and send it back to the client
            JavaScriptSerializer serialize = new JavaScriptSerializer();
            return serialize.Serialize(lstRows);
        }
    }
}
