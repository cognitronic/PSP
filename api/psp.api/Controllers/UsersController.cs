using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using psp.core.domain.user;
using psp.repository.mongo.Repositories;
using MongoDB.Bson;

namespace psp.api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly UserRepository _userRepository;

        public UsersController()
        {
            _userRepository = new UserRepository();
        }
        // GET api/users
        public IList<User> Get()
        {
            var users = _userRepository.GetAll();
            return users;
        }

        // GET api/user/5
        public User Get(string id)
        {
            return _userRepository.GetById(new ObjectId(id));
        }

        // POST api/user
        public User Post([FromBody]User user)
        {
           return  _userRepository.Save(user);
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public User Delete([FromBody]User usr)
        {
            return _userRepository.Delete(usr);
        }
    }
}
