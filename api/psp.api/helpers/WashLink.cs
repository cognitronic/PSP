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

namespace psp.api.helpers
{
    public class WashLink : BaseHelpers
    {
        public WashLinkWashTotals WashLinkWashTotalsBySiteDate(Site site, DateTime date)
        {
            var sQuery = "";
            if (site.dbtype.Equals("mysql"))
            {
                sQuery = @"SELECT SUM(PK2) AS PrimeShineWash, 
                                    SUM(PK1) AS PlatinumWash, 
                                    SUM(PK3) AS ProtexWash, 
                                    SUM(PK4) AS PremierWash,
                                    'test' as rooter
                        FROM         Statistics
                        WHERE     date(timestamp) = '" + FormatMySqlDate(date.ToShortDateString()) + @"'
                        group by date(timestamp);

                        SELECT     SUM(PK5) AS TireShine,
                                    'test' as rooter
                        FROM         StatisticsOptions
                        WHERE     date(timestamp) = '" + FormatMySqlDate(date.ToShortDateString()) + @"'
                        group by date(timestamp);
                        
                        SELECT     SUM(PK6) AS RainX,
                                    'test' as rooter
                        FROM         StatisticsOptions
                        WHERE     date(timestamp) = '" + FormatMySqlDate(date.ToShortDateString()) + @"'
                        group by date(timestamp);";
                DataSet ds = new DataSet();
                using (MySqlConnection con = new MySqlConnection(site.dbconnectionstring))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(sQuery, site.dbconnectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds, "PrimeShine");
                }
                return TotalWashesMySQL(ds);
            }
            else if (site.dbtype.Equals("sql2005"))
            {
                sQuery = @"SELECT SUM(dbo.SECCountsDetailsPackages.Item2) AS PrimeShineWash, 
                        SUM(dbo.SECCountsDetailsPackages.Item1) AS PlatinumWash, 
                        SUM(dbo.SECCountsDetailsPackages.Item3) AS ProtexWash,
                        SUM(dbo.SECCountsDetailsPackages.Item4) AS PremierWash,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsPackages
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" 4:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'

                        SELECT SUM(dbo.SECCountsDetailsOptions.Item6) AS TireShine,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" 4:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'
                        
                        SELECT SUM(dbo.SECCountsDetailsOptions.Item7) AS RainX,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" 4:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'
";
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(site.dbconnectionstring))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sQuery, site.dbconnectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds, "PrimeShine");
                }

                return TotalWashes(ds);
            }
            else if (site.dbtype.Equals("sql2008"))
            {
                sQuery = @"SELECT SUM(dbo.TECCountsDetailsPackages.Item2) AS PrimeShineWash, 
                        SUM(dbo.TECCountsDetailsPackages.Item1) AS PlatinumWash, 
                        SUM(dbo.TECCountsDetailsPackages.Item3) AS ProtexWash,
                        SUM(dbo.TECCountsDetailsPackages.Item4) AS PremierWash,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsPackages
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'

                        SELECT SUM(dbo.TECCountsDetailsOptions.Item6) AS TireShine,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'
                        
                        SELECT SUM(dbo.TECCountsDetailsOptions.Item7) AS RainX,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" 6:00:32 AM' and '" + date.ToShortDateString() + @" 9:55:32 PM'
";
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(site.dbconnectionstring))
                {
                    SqlDataAdapter da = new SqlDataAdapter(sQuery, site.dbconnectionstring);
                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(ds, "PrimeShine");
                }
                return TotalWashes(ds);
            }
            return null;
        }

        protected string FormatMySqlDate(string date)
        {
            DateTime dt = DateTime.Parse(date);
            return DateTime.Parse(date).GetDateTimeFormats('u')[0].Substring(0, 10);
        }

        private WashLinkWashTotals TotalWashes(DataSet ds)
        {
            var washes =
                    from carwashes in ds.Tables[0].AsEnumerable()
                    join tireshines in ds.Tables[1].AsEnumerable()
                    on carwashes.Field<string>("rooter") equals
                    tireshines.Field<string>("rooter")
                    join rainx in ds.Tables[2].AsEnumerable()
                    on carwashes.Field<string>("rooter") equals
                    rainx.Field<string>("rooter")
                    select new WashLinkWashTotals()
                    {
                        primeshinewash = carwashes.Field<int>("PrimeShineWash"),
                        protexwash = carwashes.Field<int>("ProtexWash"),
                        premierwash = carwashes.Field<int>("PremierWash"),
                        tireshine = tireshines.Field<int>("TireShine"),
                        rainx = rainx.Field<int>("RainX")

                    };

            return washes.First<WashLinkWashTotals>();
        }

        private WashLinkWashTotals TotalWashesMySQL(DataSet ds)
        {
            var washes =
                    from carwashes in ds.Tables[0].AsEnumerable()
                    join tireshines in ds.Tables[1].AsEnumerable()
                    on carwashes.Field<string>("rooter") equals
                    tireshines.Field<string>("rooter")
                    join rainx in ds.Tables[2].AsEnumerable()
                    on carwashes.Field<string>("rooter") equals
                    rainx.Field<string>("rooter")
                    select new WashLinkWashTotals()
                    {
                        primeshinewash = (int)carwashes.Field<double>("PrimeShineWash"),
                        protexwash = (int)carwashes.Field<double>("ProtexWash"),
                        premierwash = (int)carwashes.Field<double>("PremierWash"),
                        tireshine = (int)tireshines.Field<double>("TireShine"),
                        rainx = (int)rainx.Field<double>("RainX")

                    };

            return washes.First<WashLinkWashTotals>();
        }
    }
}