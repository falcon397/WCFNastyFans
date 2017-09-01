using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Configuration;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using System.Data;

using System.ServiceModel.Activation;

namespace WCFNastyFans
{
    [AspNetCompatibilityRequirements(RequirementsMode
    = AspNetCompatibilityRequirementsMode.Allowed)]
    public class NastyFanService : INastyFans
    {

        public string getBuys()
        {
            //Create connection string, sql command, and data tables and sets needed to sort the data
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnAnalyzer"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("select date, seats, price_per_seat from buys", conn);

            return new getData(conn, cmd).returnJSON;
            
        }

        public string getActiveMembers()
        {
            //Create connection string, sql command, and data tables and sets needed to sort the data
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnAnalyzer"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("select date, activeMembers from activeMembers", conn);

            return new getData(conn, cmd).returnJSON;
        }

        public string getExchangeRate()
        {
            //Create connection string, sql command, and data tables and sets needed to sort the data
            MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnAnalyzer"].ConnectionString);
            MySqlCommand cmd = new MySqlCommand("select date, price from exchangeRate", conn);

            return new getData(conn, cmd).returnJSON;
        }
    }
}
