using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace psp.api.datahelpers
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

        private readonly string connectionString = "Server=192.168.1.105;User=SYSDBA;Password=masterkey;Database=d:\\Databases\\PrimeShine\\SiteWatch\\SiteWatch.fdb";
        public IList<SiteWatchSalesItem> SitewatchSalesBySiteDate(string siteid, DateTime startdate)
        {
            string sQuery = "";//ConfigurationSettings.AppSettings["PSFirebirdConnectionString"];
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
                    i.reportcategory in ('52167', '59458', '500044', '500090', '500061', '500050', '500036', '49500343', '500043', '500058', '500060', '49500342', '49500003', '49500001', '1000003', '500059', '49500354', '49500351', '53631', '500025', '500091', '500057', '52179', '500031', '53724', '500028', '1000004', '500063', '500027', '49500349', '500062', '500064', '500037', '500063', '500103', '500100', '49000002', '500102', '500101', '500112', '49500344') and 
                    s.logdate  = '" + startdate.ToShortDateString() + @"'
                    group by i.reportcategory, i.name, i.objid, s.logdate, i.itemtype, s.site, si.amt, si.val";
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
               locationid = r["locationid"].ToString(),
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
                locationid = r["locationid"].ToString(),
                sitename = r["sitename"].ToString()
            })).ToList<SiteWatchSalesItem>();

            return list;
        }

        public string RunRewashNotification(string reportDate)
        {
            var list = new List<IAPIResponse>();
            foreach (var site in sites)
            {
                var rewash = new RewashNotificationResponse();
                
                foreach (var item in GetSiteWatchRewashData(site.sitewatchid, reportDate))
                {
                    rewash.RewashCount += item.total;
                }
                list.Add(rewash as IAPIResponse);
            }

            var totes = list.Count();

            
            return "";
        }
    }
}