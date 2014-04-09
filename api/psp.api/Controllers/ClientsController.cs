using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;
using psp.api.helpers;

namespace psp.api.Controllers
{
    public class ClientsController : ApiController
    {
         private readonly ClientRepository _repository;

         public ClientsController()
        {
            _repository = new ClientRepository();
        }

        // GET api/site
        public IList<Client> GetAll()
        {
            var clients = _repository.GetAll().OrderByDescending(o => o.dateregistered).ToList<Client>();
            return clients;
        }

        // GET api/clients
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/clients/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/clients
        public Client Post([FromBody]Client client)
        {
            return _repository.Save(client);
        }

        // PUT api/clients/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/clients/5
        public void Delete(int id)
        {
        }
    }
}
