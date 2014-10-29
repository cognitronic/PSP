using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using psp.core.domain;
using psp.core;
using psp.repository.mongo.Cache;
using System.Text.RegularExpressions;
using psp.core.helpers;

namespace psp.repository.mongo.Repositories
{
    public class GSRRepository : BaseRepository<GSR>
    {
        public GSRRepository()
            : base(ResourceStrings.Mongo_GSR_Collection)
        {
        }
        public IList<GSR> GetAll()
        {
            return _collection.FindAllAs<GSR>().OrderBy(o => o.gsrDate).ToList<GSR>();
        }

        public GSR GetById(ObjectId id)
        {
            var query = Query<GSR>.EQ(e => e.Id, id);
            return _collection.FindOneAs<GSR>(query);
        }

        public GSR GetByGSRDate(DateTime date)
        {
            var dt = DateTime.Parse(date.ToShortDateString());
            var query = Query<GSR>.EQ(e => e.gsrDate, dt);
            return _collection.FindOneAs<GSR>(query);
        }

        public IList<GSR> GetBySiteId(string siteId)
        {
            var query = Query<GSR>.EQ(e => e.Id, new ObjectId(siteId));
            return _collection.FindAs<GSR>(query).ToList<GSR>();
        }

        public GSR GetBySiteIdGSRDate(string site, DateTime date)
        {
            var dt = DateTime.Parse(date.ToShortDateString());
            var query = Query.And(
                Query.EQ("sitename", site),
                Query.EQ("gsrdate", dt));
            return _collection.FindOneAs<GSR>(query);
        }

        public IList<GSR> GetAllRecordsByDate(DateTime date)
        {
            //var query = Query.Matches("gsrDate", BsonRegularExpression.Create(new Regex(@"^" + date.Replace("-", "/").Substring(0, date.Length - 5) + "/")));
            
            //var list = new List<IMongoQuery>();
            //foreach (var dt in myDates)
            //{
            //    list.Add(Query.EQ("gsrDate", dt.ToUniversalTime()));
            //}
            //var query = Query.And(list);
            var dt = DateTime.Parse(date.ToShortDateString());
            var query = Query<GSR>.EQ(e => e.gsrDate, dt.ToUniversalTime());
            var collection = _database.GetCollection<GSR>("gsr");
            return collection.Find(query).OrderBy(o => o.gsrDate).ToList<GSR>();
        }

        public IList<GSR> GetBySiteDateRange(DateTime startDate, DateTime endDate, string site)
        {
            var sd = DateTime.Parse(startDate.ToShortDateString());
            var ed = DateTime.Parse(endDate.ToShortDateString());
            var list = new List<IMongoQuery>();
            list.Add(Query.GTE("gsrDate", sd.ToUniversalTime()));
            list.Add(Query.LTE("gsrDate", ed.ToUniversalTime()));
            list.Add(Query<GSR>.EQ(e => e.siteName, site));
            var query = Query.And(list);
            var results = _collection.FindAs<GSR>(query);
            return results.ToList<GSR>();
        }

       

        public IList<WashLinkWashTotals> GetWashTotals(DateTime date)
        {
            var dt = DateTime.Parse(date.ToShortDateString());
            var collection = _database.GetCollection<GSR>("gsr");
            var query = Query<GSR>.EQ(e => e.gsrDate, dt.ToUniversalTime());
            
            var cursor = collection.FindAs<GSR>(query);
            //if(cursor.Count() < 1)
            //{
            //    cursor = collection.FindAs<GSR>(Query.EQ("gsrDate", date.Replace("-", "/") + " 12:00:00 AM"));
            //}
            cursor.SetFields(Fields.Include("washLinkTotalPrimeShine_count", "washLinkTotalProtex_count", "washLinkTotalPremier_count", "washLinkTotalTireGloss_count", "washLinkTotalPlusPlus_count", "washLinkTotalRainX_count", "siteName"));
            var totals = new List<WashLinkWashTotals>();
            foreach(var gsr in cursor.ToList())
            {
                totals.Add(new WashLinkWashTotals
                {
                    plusplus = gsr.washLinkTotalPlusPlus_count,
                    premierwash = gsr.washLinkTotalPremier_count,
                    primeshinewash = gsr.washLinkTotalPrimeShine_count,
                    protexwash = gsr.washLinkTotalProtex_count,
                    rainx = gsr.washLinkTotalRainX_count,
                    sitename = gsr.siteName,
                    tireshine = gsr.washLinkTotalTireGloss_count,
                    date = gsr.gsrDate
                });
            }
            return totals;
        }

        public GSR Save(GSR gsr)
        {
            //if (gsr.sid != null && gsr.sid != "")
            //{
            //    gsr.Id = new ObjectId(gsr.sid);
            //}
            _collection.Save<GSR>(gsr);
            return gsr;
        }

        public GSR Delete(GSR client)
        {
            var usr = client;
            var query = Query<GSR>.EQ(u => u.Id, new ObjectId(client.sid));
            _collection.Remove(query);
            return usr;
        }
    }
}
