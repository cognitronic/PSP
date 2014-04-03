using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MySql.Data.MySqlClient;
using System.Data.Odbc;
using System.Data;
using System.Data.SqlClient;
using psp.core.domain;

namespace psp.core.datahelpers
{
    public class WashLink
    {
        public IList<WashLinkWashTotals> WashLinkWashTotalsBySiteDate(Site site, DateTime date)
        {
            var sQuery = "";
            var list = new List<WashLinkWashTotals>();
            if (site.dbtype.Equals("mysql"))
            {
                sQuery = @"SELECT SUM(PK2) AS PrimeShineWash, SUM(PK1) AS PlatinumWash, SUM(PK3) AS ProtexWash
                    FROM         Statistics
                    WHERE     date(timestamp) = '" + FormatMySqlDate(date.ToShortDateString()) + @"'
                    group by date(timestamp);

                    SELECT     SUM(PK5) AS TireShine
                    FROM         StatisticsOptions
                    WHERE     date(timestamp) = '" + FormatMySqlDate(date.ToShortDateString()) + @"'
                    group by date(timestamp);";
                DataSet ds = new DataSet();
                using (MySqlConnection con = new MySqlConnection(site.dbconnectionstring))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sQuery, site.dbconnectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    //da.SelectCommand.Parameters.Add("@firstparam", "firstparamvalue");
                    da.Fill(ds, "PrimeShine");
                }
                list = (ds.Tables[0].AsEnumerable().Select(r => new WashLinkWashTotals
                {
                    primeshinewash = (int)r["PrimeShineWash"],
                    platinumwash = (int)r["PlatinumWash"],
                    protexwash = (int)r["ProtexWash"],
                    premierwash = (int)r["PremierWash"],
                    tireshine = (int)r["TireShine"]
                })).ToList<WashLinkWashTotals>();

                return list;
            }
            else if (site.dbtype.Equals("sql2005"))
            {
                sQuery = @"SELECT SUM(dbo.SECCountsDetailsPackages.Item2) AS PrimeShineWash, 
                    SUM(dbo.SECCountsDetailsPackages.Item1) AS PlatinumWash, 
                    SUM(dbo.SECCountsDetailsPackages.Item3) AS ProtexWash
                    FROM dbo.SECCountsDetailsPackages
                    WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'

                    SELECT SUM(dbo.SECCountsDetailsOptions.Item6) AS TireShine
                    FROM dbo.SECCountsDetailsOptions
                    WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'";
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(site.dbconnectionstring))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sQuery, site.dbconnectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    //da.SelectCommand.Parameters.Add("@firstparam", "firstparamvalue");
                    da.Fill(ds, "PrimeShine");
                }
                list = (ds.Tables[0].AsEnumerable().Select(r => new WashLinkWashTotals
                {
                    primeshinewash = (int)r["PrimeShineWash"],
                    platinumwash = (int)r["PlatinumWash"],
                    protexwash = (int)r["ProtexWash"],
                    premierwash = (int)r["PremierWash"],
                    tireshine = (int)r["TireShine"]
                })).ToList<WashLinkWashTotals>();

                return list;
            }
            else if (site.dbtype.Equals("sql2008"))
            {
                sQuery = @"SELECT SUM(dbo.TECCountsDetailsPackages.Item2) AS PrimeShineWash, 
                    SUM(dbo.TECCountsDetailsPackages.Item1) AS PlatinumWash, 
                    SUM(dbo.TECCountsDetailsPackages.Item3) AS ProtexWash
                    FROM dbo.TECCountsDetailsPackages
                    WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'

                    SELECT SUM(dbo.TECCountsDetailsOptions.Item6) AS TireShine
                    FROM dbo.TECCountsDetailsOptions
                    WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'";
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(site.dbconnectionstring))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sQuery, site.dbconnectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    //da.SelectCommand.Parameters.Add("@firstparam", "firstparamvalue");
                    da.Fill(ds, "PrimeShine");
                }
                list = (ds.Tables[0].AsEnumerable().Select(r => new WashLinkWashTotals
                {
                    primeshinewash = (int)r["PrimeShineWash"],
                    platinumwash = (int)r["PlatinumWash"],
                    protexwash = (int)r["ProtexWash"],
                    premierwash = (int)r["PremierWash"],
                    tireshine = (int)r["TireShine"]
                })).ToList<WashLinkWashTotals>();

                return list;
            }
            return null;
        }

        protected string FormatMySqlDate(string date)
        {
            DateTime dt = DateTime.Parse(date);
            return DateTime.Parse(date).GetDateTimeFormats('u')[0].Substring(0, 10);
        }
    }
}
