using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain.user;
using psp.repository.mongo.Repositories;

namespace psp.api.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users
        public IList<User> GetAll()
        {
            var users = new UserRepository().GetAll();
            return users;
        }

        // GET api/user/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
