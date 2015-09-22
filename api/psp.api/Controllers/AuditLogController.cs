using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using psp.api.helpers;

namespace psp.api.Controllers
{
    [RoutePrefix("api/audit")]
    public class AuditLogController : ApiController
    {
        private readonly AuditLogRepository _repository;
        public AuditLogController()
        {
            _repository = new AuditLogRepository();
        }

        [Route("")]
        public IList<AuditLog> Get()
        {
            var logs = _repository.GetAll();
            return logs;
        }

        [Route("{type}")]
        public IList<AuditLog> Get(string type)
        {
            var logs = _repository.GetByType(type);
            return logs;
        }

        // POST api/auditlog
        public void Post([FromBody]string value)
        {
        }

        // PUT api/auditlog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/auditlog/5
        public void Delete(int id)
        {
        }
    }
}
