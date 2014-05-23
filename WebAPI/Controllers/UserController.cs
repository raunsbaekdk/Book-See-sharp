using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Models;
using WebAPI.Models;


namespace WebAPI.Controllers {
    [Authorize]
    public class UserController : ApiController {
        static readonly IUserRepository Respository = new UserRepository();

        public IHttpActionResult GetAllUsers() {
            IEnumerable<User> users = Respository.GetAllUsers();
            if(users == null) {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpGet]
        public IHttpActionResult GetReservations(int mobile) {
            IEnumerable<Reservation> reservations = Respository.GetUserReservations(mobile);
            if(reservations == null) {
                return NotFound();
            }
            return Ok(reservations);
        }
    }
}