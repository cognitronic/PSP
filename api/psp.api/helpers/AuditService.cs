using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using psp.repository.mongo.Repositories;
using psp.core.domain;

namespace psp.api.helpers
{
    public class AuditService
    {
        public static AuditLog SaveLog(AuditLog log)
        {
            return new AuditLogRepository().Save(log);
        }
    }
}