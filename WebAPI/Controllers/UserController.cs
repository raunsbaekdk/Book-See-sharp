using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Http;
using Models;
using WebAPI.Models;
using WebAPI.Security;


namespace WebAPI.Controllers {
    [Authorize]
    public class UserController : ApiController {
        static readonly IUserRepository Respository = new UserRepository();
        static readonly IAuthorizationSystem AuthorizationSystem = new AuthorizationSystem();

        public IHttpActionResult GetAllUsers() {
            IEnumerable<User> users = Respository.GetAllUsers();
            if(users == null) {
                return NotFound();
            }
            return Ok(users);
        }

        public IHttpActionResult Get(int mobile) {
            User u = Respository.Get(mobile);
            if(u == null) {
                return NotFound();
            }
            return Ok(u);
        }


        [HttpGet]
        public IHttpActionResult GetReservations(int mobile) {
            IEnumerable<Reservation> reservations = Respository.GetUserReservations(mobile);
            if(reservations == null) {
                return NotFound();
            }
            return Ok(reservations);
        }

        public IHttpActionResult Post([FromBody]UserApiClass user) {
            string username = User.Identity.Name;
            if(AuthorizationSystem.IsAdmin(username)) {
                User result = Respository.Post(user);
                if(user != null) {
                    return Created(new Uri(Request.RequestUri, "get?mobile="+user.Mobile), user);
                }
                else {
                    return InternalServerError(new Exception("Failed to create user please try again"));
                }
            }
            else {
                return Unauthorized(new AuthenticationHeaderValue("Forms admin authentication required"));
            }
        }

        public IHttpActionResult Delete(int mobile) {
            string username = User.Identity.Name;
            if(AuthorizationSystem.IsAdmin(username)) {
                Boolean res = Respository.Delete(mobile);
                if(res) {
                    return Ok();
                } else {
                    return InternalServerError(new Exception("Failed to create user please try again"));
                }
            } else {
                return Unauthorized(new AuthenticationHeaderValue("Forms"));
            }
        }
    }
}