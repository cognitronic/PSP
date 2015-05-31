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

namespace psp.repository.mongo.Repositories
{
    public class AuditLogRepository : BaseRepository<AuditLog>
    {
        public AuditLogRepository()
            : base(ResourceStrings.Mongo_Audit_Collection)
        {
            
            
        }
        public IList<AuditLog> GetAll()
        {
            return _collection.FindAllAs<AuditLog>().OrderBy(o => o.type).ToList<AuditLog>();
        }

        public AuditLog GetById(ObjectId id)
        {
            var query = Query<AuditLog>.EQ(e => e.Id, id);
            return _collection.FindOneAs<AuditLog>(query);
        }

        public IList<AuditLog> GetByType(string type)
        {
            var collection = _database.GetCollection<AuditLog>("audit");
            var query = Query<AuditLog>.EQ(e => e.type, type);
            return collection.Find(query).OrderBy(o => o.auditDate).ToList<AuditLog>();
        }

        public AuditLog Save(AuditLog log)
        {
            if (log.Id != null)
            {
                //code.Id = new ObjectId(code.sid);
            }
            _collection.Save<AuditLog>(log);
            return log;
        }
    }
}
