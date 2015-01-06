using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using System.Configuration;
using MongoDB.Bson;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using IdeaSeed.Core.Mail;

namespace psp.api.helpers
{
    public class SiteWatch : BaseHelpers
    {
        private readonly IList<Site> sites;
        private readonly SiteRepository _siteRepository;
        public SiteWatch()
        {
            _siteRepository = new SiteRepository();
            sites = _siteRepository.GetAll();
        }

        private readonly string connectionString = ConfigurationManager.AppSettings["sitewatchConnectionString"];
        public IList<SiteWatchSalesItem> SitewatchSalesBySiteDate(string siteid, DateTime startdate, string fromTime, string toTime)
        {
            string sQuery = "";//ConfigurationSettings.AppSettings["PSFirebirdConnectionString"];
            if (string.IsNullOrEmpty(fromTime))
            {
                sQuery = @"Select i.objid, i.name item, i.itemtype, sum(si.qty) total, s.logdate, i.reportcategory, s.site locationid, si.amt, si.val
                    From
                    V_item i
                    Inner join v_SaleItems si on
                    i.objid = si.item
                    inner join v_sale s on
                    s.objid = si.saleid
                    where
                    s.site = '" + siteid + @"' and
                    s.site = si.site and
                    s.terminal <> 3500036 and
                    i.reportcategory in ('52167', '59458', '500044', '500090', '500061', '500050', '500036', '49500343', '500043', '500058', '500060', '49500342', '49500003', '49500001', '1000003', '500059', '49500354', '49500351', '53631', '500025', '500091', '500057', '52179', '500031', '53724', '500028', '1000004', '500063', '500027', '49500349', '500062', '500064', '500037', '500063', '500103', '500100', '49000002', '500102', '500101', '500112', '49500344','103965', '500052') and 
                    s.logdate  = '" + startdate.ToShortDateString() + @"'
                    group by i.reportcategory, i.name, i.objid, s.logdate, i.itemtype, s.site, si.amt, si.val";

            }
            else
            {
                sQuery = @"Select i.objid, i.name item, i.itemtype, sum(si.qty) total, s.logdate, i.reportcategory, s.site locationid, si.amt, si.val
                    From
                    V_item i
                    Inner join v_SaleItems si on
                    i.objid = si.item
                    inner join v_sale s on
                    s.objid = si.saleid
                    where
                    s.site = '" + siteid + @"' and
                    s.site = si.site and
                    s.terminal <> 3500036 and
                    i.reportcategory in ('52167', '59458', '500044', '500090', '500061', '500050', '500036', '49500343', '500043', '500058', '500060', '49500342', '49500003', '49500001', '1000003', '500059', '49500354', '49500351', '53631', '500025', '500091', '500057', '52179', '500031', '53724', '500028', '1000004', '500063', '500027', '49500349', '500062', '500064', '500037', '500063', '500103', '500100', '49000002', '500102', '500101', '500112', '49500344','103965', '500052') and 
                    s.logdate  = '" + startdate.ToShortDateString() + @"' and
                    s.logtime between " + ConvertToTenthsOfAMinute(fromTime).ToString() + " and " + ConvertToTenthsOfAMinute(toTime).ToString() + @"
                    group by i.reportcategory, i.name, i.objid, s.logdate, i.itemtype, s.site, si.amt, si.val";
            }
            var ds = new DataSet();
            var list = new List<SiteWatchSalesItem>();
            using(FbConnection con = new FbConnection(connectionString))
            {
                FbDataAdapter da = new FbDataAdapter(sQuery, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(ds, "PrimeShine");
            }
            list = (ds.Tables[0].AsEnumerable().Select(r => new SiteWatchSalesItem {
               objid = r["objid"].ToString(),
               item = r["item"].ToString(),
               itemtype = r["itemtype"].ToString(),
               total = r["total"].ToString(),
               logdate =r["logdate"].ToString(),
               reportcategory = r["reportcategory"].ToString(),
               locationid = Convert.ToInt16(r["locationid"]),
               amt = r["amt"].ToString(),
               val = r["val"].ToString()
            })).ToList<SiteWatchSalesItem>();

            return list;
        }

        public IList<SiteWatchSalesItem> GetSiteWatchRewashData(string site, string reportDate)
        {
            string sQuery = "";
            sQuery = @"Select i.objid, i.name item, sum(si.qty) total, s.logdate, s.site locationid, sit.sitename
                From
                V_item i
                Inner join v_SaleItems si on
                i.objid = si.item
                inner join v_sale s on
                s.objid = si.saleid
                inner join v_site sit on
                s.site = sit.id
                where
                s.site = '" + site + @"' and
                s.site = si.site and
                s.terminal <> 3500036 and
                i.objid in ('606552') and 
                s.logdate  = '" + DateTime.Parse(reportDate).ToShortDateString() + @"'
                group by i.name, i.objid, s.logdate, s.site, sit.sitename";
            //, '606554', '49501521','608809'
            var ds = new DataSet();
            var list = new List<SiteWatchSalesItem>();
            using (FbConnection con = new FbConnection(connectionString))
            {
                FbDataAdapter da = new FbDataAdapter(sQuery, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(ds, "PrimeShine");
            }
            list = (ds.Tables[0].AsEnumerable().Select(r => new SiteWatchSalesItem
            {
                objid = r["objid"].ToString(),
                item = r["item"].ToString(),
                total = r["total"].ToString(),
                logdate = r["logdate"].ToString(),
                locationid = Convert.ToInt16(r["locationid"]),
                sitename = r["sitename"].ToString()
            })).ToList<SiteWatchSalesItem>();

            return list;
        }

        private IList<SiteWatchSalesItem> GetSiteWashVolumeData(string reportDate)
        {
            
            StringBuilder sb = new StringBuilder();
            string sQuery = "";
            sQuery = @"select s.site, si.sitename, count(s.site) ttlcount
                from
                v_sale s
                inner join v_salefacts sf on
                s.objid = sf.saleid
                inner join v_site si on
                s.site = si.id
                where
                sf.fact = 54200 and
                s.site = sf.site and
                sf.profitcenter = 1 and
                si.id <> 99 and
                s.logdate between '" + DateTime.Parse(reportDate).ToShortDateString() + @"' and '" + DateTime.Parse(reportDate).ToShortDateString() + @"'
                group by s.site, si.sitename
                order by s.site";
            DataSet ds = new DataSet();
            var list = new List<SiteWatchSalesItem>();
            using (FbConnection con = new FbConnection(connectionString))
            {
                FbDataAdapter da = new FbDataAdapter(sQuery, connectionString);
                da.SelectCommand.CommandType = CommandType.Text;
                da.Fill(ds, "PrimeShine");
            }
            list = (ds.Tables[0].AsEnumerable().Select(r => new SiteWatchSalesItem
            {
                total = r["ttlcount"].ToString(),
                locationid = Convert.ToInt16(r["site"]),
                sitename = r["sitename"].ToString()
            })).ToList<SiteWatchSalesItem>();

            //sb.Append("<table>");
            //int total = 0;
            //sb.Append("<tr><td style='width: 100px;'>Site</td><td style='width: 50px;'>Count</td><td style='width: 100px;'>$ per Car</td></tr>");
            //foreach (var item in list)
            //{
            //    sb.Append("<tr><td>");
            //    sb.Append(item.sitename);
            //    sb.Append("</td><td>");
            //    sb.Append(item.total);
            //    sb.Append("</td><td>$");
            //    try
            //    {
            //        //sb.Append(TTAPerCar[row["site"].ToString()]);
            //    }
            //    catch (Exception exc)
            //    {
            //        sb.Append("0 - An error occurred processing this site.");
            //    }
            //    sb.Append("</td></tr>");
            //    total += Convert.ToInt32(item.total);
            //}
            //sb.Append("<tr><td>");
            //sb.Append("<b>Total Count:</b>     <font color='Red'>");
            //sb.Append(total.ToString());
            //sb.Append("</font></td></tr>");
            //sb.Append("</table>");
            return list;
        }

        private int ConvertToTenthsOfAMinute(string time)
        {
            var temp = time.Split(':');
            if(temp.Length > 0)
            {
                var convertedTime = int.Parse(temp[0]);
                if(time.Contains("PM") && convertedTime < 12)
                {
                    convertedTime += 12;
                }
                return convertedTime * 10 * 60;
            }
            return 0;
        }
        public string RunRewashNotification(Notification note, string reportDate)
        {
            var list = new List<IAPIResponse>();
            foreach (var site in sites)
            {
                foreach (var item in GetSiteWatchRewashData(site.sitewatchid.ToString(), reportDate))
                {
                    var rewash = new RewashNotificationResponse();
                    rewash.RewashCount += item.total;
                    list.Add(rewash as IAPIResponse);
                }
            }

            var totes = list.Count();

            //try
            //{
            //    EmailUtils.SendEmail(ConfigurationSettings.AppSettings["ContactMessageToAddress"],
            //        ConfigurationSettings.AppSettings["ContactMessageFromAddress"],
            //        "",
            //        ConfigurationSettings.AppSettings["ContactMessageBccAddress"],
            //        Request.Form["field_subject"],
            //        body,
            //        false,
            //        ""
            //        );
            //}
            //catch (Exception exc)
            //{
            //    return Json(new
            //    {
            //        Message = "Message did not send.  Please call " + ConfigurationSettings.AppSettings["PhoneNumber"],
            //        Status = "fail"
            //    });
            //}
            //return Json(new
            //{
            //    Message = "Your message has been received and someone will be in contact shortly, thanks!!",
            //    Status = "success"
            //});
            
            return "";
        }

        public IList<SiteWatchSalesItem> RunVolumeData(string reportDate)
        {
            var list = GetSiteWashVolumeData(reportDate);
            return list;
        }
    }
}