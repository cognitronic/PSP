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
        public WashLinkWashTotals WashLinkWashTotalsBySiteDate(Site site, DateTime date, string fromTime, string toTime)
        {
            var sQuery = "";
            var from =  string.IsNullOrEmpty(fromTime) ? "4:00:32 AM" : fromTime;
            var to = string.IsNullOrEmpty(toTime) ? "9:55:32 PM" : toTime;
            if (site.dbtype.Equals("mysql"))
            {
                sQuery = @"SELECT SUM(PK2) AS PrimeShineWash, 
                                    SUM(PK1) AS PlatinumWash, 
                                    SUM(PK3) AS ProtexWash, 
                                    SUM(PK4) AS PremierWash,
                                    'test' as rooter
                        FROM         Statistics
                        WHERE     timestamp between '" + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(fromTime, true) + @"' AND '"
                                                             + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(toTime, false) + @"'
                        group by date(timestamp);

                        SELECT     SUM(PK5) AS TireShine,
                                    'test' as rooter
                        FROM         StatisticsOptions
                        WHERE     timestamp between '" + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(fromTime, true) + @"' AND '"
                                                             + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(toTime, false) + @"'
                        group by date(timestamp);

                        SELECT     SUM(PK6) AS RainX,
                                    'test' as rooter
                        FROM         StatisticsOptions
                        WHERE     timestamp between '" + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(fromTime, true) + @"' AND '"
                                                             + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(toTime, false) + @"'
                        group by date(timestamp);

                        
                        SELECT     SUM(PK7) AS PlusPlus,
                                    'test' as rooter
                        FROM         StatisticsOptions
                        WHERE     timestamp between '" + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(fromTime, true) + @"' AND '"
                                                             + FormatMySqlDate(date.ToShortDateString()) + @" "
                                                             + FormatMySqlTime(toTime, false) + @"'
                        group by date(timestamp);";
                DataSet ds = new DataSet();
                try
                {
                    using (MySqlConnection con = new MySqlConnection(site.dbconnectionstring))
                    {
                        MySqlDataAdapter da = new MySqlDataAdapter(sQuery, site.dbconnectionstring);
                        da.SelectCommand.CommandType = CommandType.Text;
                        da.Fill(ds, "PrimeShine");
                    }
                    return TotalWashesMySQL(ds);
                }
                catch(Exception exc)
                {
                    AuditService.SaveLog(new AuditLog
                    {
                        auditDate = DateTime.Now,
                        message = "Could not connect to " + site.name + ".  The error thrown is: " + exc.Message,
                        type = psp.core.ResourceStrings.Audit_Connectivity,
                        name = "WashLinkWashTotalsBySiteDate Method"
                    });
                }
            }
            else if (site.dbtype.Equals("sql2005"))
            {
                sQuery = @"SELECT SUM(dbo.SECCountsDetailsPackages.Item2) AS PrimeShineWash, 
                        SUM(dbo.SECCountsDetailsPackages.Item1) AS PlatinumWash, 
                        SUM(dbo.SECCountsDetailsPackages.Item3) AS ProtexWash,
                        SUM(dbo.SECCountsDetailsPackages.Item4) AS PremierWash,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsPackages
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString() 
                                                    + " " + to + @"'

                        SELECT SUM(dbo.SECCountsDetailsOptions.Item4) AS RainX,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'

                        SELECT SUM(dbo.SECCountsDetailsOptions.Item6) AS TireShine,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'
                        
                        SELECT SUM(dbo.SECCountsDetailsOptions.Item7) AS PlusPlus,
                        'test' as rooter
                        FROM dbo.SECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'
";
                DataSet ds = new DataSet();
                try
                {
                    using (SqlConnection con = new SqlConnection(site.dbconnectionstring))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(sQuery, site.dbconnectionstring);
                        da.SelectCommand.CommandType = CommandType.Text;
                        da.Fill(ds, "PrimeShine");
                    }
                    return TotalWashes(ds);
                }
                catch (Exception exc) {
                    AuditService.SaveLog(new AuditLog
                    {
                        auditDate = DateTime.Now,
                        message = "Could not connect to " + site.name + ".  The error thrown is: " + exc.Message,
                        type = psp.core.ResourceStrings.Audit_Connectivity,
                        name = "WashLinkWashTotalsBySiteDate Method"
                    });
                }
            }
            else if (site.dbtype.Equals("sql2008"))
            {
                sQuery = @"SELECT SUM(dbo.TECCountsDetailsPackages.Item2) AS PrimeShineWash, 
                        SUM(dbo.TECCountsDetailsPackages.Item1) AS PlatinumWash, 
                        SUM(dbo.TECCountsDetailsPackages.Item3) AS ProtexWash,
                        SUM(dbo.TECCountsDetailsPackages.Item4) AS PremierWash,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsPackages
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'

                        SELECT SUM(dbo.TECCountsDetailsOptions.Item4) AS RainX,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'

                        SELECT SUM(dbo.TECCountsDetailsOptions.Item6) AS TireShine,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'
                        
                        SELECT SUM(dbo.TECCountsDetailsOptions.Item7) AS PlusPlus,
                        'test' as rooter
                        FROM dbo.TECCountsDetailsOptions
                        WHERE [Datetime] between '" + date.ToShortDateString() + @" " + from + "' and '" + date.ToShortDateString()
                                                    + " " + to + @"'
";
                DataSet ds = new DataSet();
                try
                {
                    using (SqlConnection con = new SqlConnection(site.dbconnectionstring))
                    {
                        SqlDataAdapter da = new SqlDataAdapter(sQuery, site.dbconnectionstring);
                        da.SelectCommand.CommandType = CommandType.Text;
                        da.Fill(ds, "PrimeShine");
                    }
                    return TotalWashes(ds);
                }
                catch (Exception exc) {

                    AuditService.SaveLog(new AuditLog
                    {
                        auditDate = DateTime.Now,
                        message = "Could not connect to " + site.name + ".  The error thrown is: " + exc.Message,
                        type = psp.core.ResourceStrings.Audit_Connectivity,
                        name = "WashLinkWashTotalsBySiteDate Method"
                    });
                }
            }
            return null;
        }

        protected string FormatMySqlDate(string date)
        {
            DateTime dt = DateTime.Parse(date);
            return DateTime.Parse(date).GetDateTimeFormats('u')[0].Substring(0, 10);
        }

        protected string FormatMySqlTime(string time, bool isStart)
        {
            var temp = "";
            var result = "";
            if (!string.IsNullOrEmpty(time))
            {
                if (time.Contains("PM"))
                {
                    temp = time.Replace(" PM", "");
                    var arr = time.Split(':');
                    var hour = Convert.ToInt16(arr[0]);
                    if (hour != 12)
                    {
                        arr[0] = (Convert.ToInt16(arr[0]) + hour).ToString();
                    }
                    result = string.Join(":", arr);
                }
                else
                {
                    result = time.Replace(" AM", "");
                }
            }
            else
            {
                if (isStart)
                {
                    result = "06:00:00";
                }
                else
                {
                    result = "23:00:00";
                }
            }

            return result;
        }

        private WashLinkWashTotals TotalWashes(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var washes =
                        from carwashes in ds.Tables[0].AsEnumerable()
                        join rainx in ds.Tables[1].AsEnumerable()
                        on carwashes.Field<string>("rooter") equals
                        rainx.Field<string>("rooter")
                        join tireshines in ds.Tables[2].AsEnumerable()
                        on carwashes.Field<string>("rooter") equals
                        tireshines.Field<string>("rooter")
                        join plusplus in ds.Tables[3].AsEnumerable()
                        on carwashes.Field<string>("rooter") equals
                        plusplus.Field<string>("rooter")
                        select new WashLinkWashTotals()
                        {
                            primeshinewash = carwashes["PrimeShineWash"] != null ? carwashes.Field<int>("PrimeShineWash") : 0,
                            protexwash = carwashes["ProtexWash"] != null ? carwashes.Field<int>("ProtexWash") : 0,
                            premierwash = carwashes["PremierWash"] != null ? carwashes.Field<int>("PremierWash") : 0,
                            tireshine = tireshines["TireShine"] != null ? tireshines.Field<int>("TireShine") : 0,
                            rainx = rainx["RainX"] != null ? rainx.Field<int>("RainX") : 0,
                            plusplus = plusplus["PlusPlus"] != null ? plusplus.Field<int>("PlusPlus") : 0

                        };

                return washes.First<WashLinkWashTotals>();
            }
            return null;
        }

        private WashLinkWashTotals TotalWashesMySQL(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                var washes =
                        from carwashes in ds.Tables[0].AsEnumerable()
                        join tireshines in ds.Tables[1].AsEnumerable()
                        on carwashes.Field<string>("rooter") equals
                        tireshines.Field<string>("rooter")
                        join rainx in ds.Tables[2].AsEnumerable()
                        on carwashes.Field<string>("rooter") equals
                        rainx.Field<string>("rooter")
                        join plusplus in ds.Tables[3].AsEnumerable()
                        on carwashes.Field<string>("rooter") equals
                        plusplus.Field<string>("rooter")
                        select new WashLinkWashTotals()
                        {
                            primeshinewash = carwashes["PrimeShineWash"] != null ? (int)carwashes.Field<double>("PrimeShineWash") : 0,
                            protexwash = carwashes["ProtexWash"] != null ? (int)carwashes.Field<double>("ProtexWash") : 0,
                            premierwash = carwashes["PremierWash"] != null ? (int)carwashes.Field<double>("PremierWash") : 0,
                            tireshine = tireshines["TireShine"] != null ? (int)tireshines.Field<double>("TireShine") : 0,
                            rainx = rainx["RainX"] != null ? (int)rainx.Field<double>("RainX") : 0,
                            plusplus = plusplus["PlusPlus"] != null ? (int)plusplus.Field<double>("PlusPlus") : 0

                        };

                return washes.First<WashLinkWashTotals>();
            }
            return null;
        }
    }
}