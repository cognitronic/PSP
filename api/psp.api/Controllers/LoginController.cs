using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using psp.core.domain.user;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;


namespace psp.api.Controllers
{
    public class LoginController : ApiController
    {
        protected readonly UserRepository _userRepository;

        public LoginController()
        {
            _userRepository = new UserRepository();
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public User Get([FromBody]User user)
        {
            var u = (User)_userRepository.GetByEmail(user.email);
            return u;
        }

        [System.Web.Http.HttpGet]
        public User Authenticate([FromBody]User user)
        {
            var u = (User)_userRepository.GetByEmail(user.email);
            return u;
        }


        public HttpResponseMessage PostUser([FromBody]User credentials)
        {
            var user = (User)_userRepository.GetByEmail(credentials.email);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, user);
            response.Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(user));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { email = credentials.email }));
            return response;
        }

        // POST api/<controller>
        public void Post([FromBody]string email)
        {
            
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}