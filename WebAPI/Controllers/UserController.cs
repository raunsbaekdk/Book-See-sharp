using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using WebAPI.Models;


namespace WebAPI.Controllers {
    public class UserController : ApiController {
        static readonly IUserRepository Respository = new UserRepository();

        public IEnumerable<User> GetAllUsers() {
            return Respository.GetAllUsers();
        } 
    }
}
