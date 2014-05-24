using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Models;
using WebAPI.Models;
using WebAPI.Security;

namespace WebAPI.Controllers {
    [Authorize]
    public class ReservationController : ApiController {
        static readonly IReservationRespository Respository = new ReservationRepository();
        // Ghetto solution
        private static readonly IAuthorizationSystem Authorizationuth = new AuthorizationSystem();

        public IHttpActionResult Get() {
            IEnumerable<Reservation> list = Respository.GetAllReservations();
            if(list == null) {
                return NotFound();
            }
            return Ok(list.AsQueryable());
        }

        public IHttpActionResult Delete(int id) {
            int i = Respository.DeleteReservation(id);
            if(i == -2) {
                return Unauthorized(new AuthenticationHeaderValue("Forms"));
            } else if(i == -1) {
                return InternalServerError(new Exception("Failed to delete reservation please try again!"));
            } else if(i == 0) {
                return NotFound();
            } else {
                return Ok();
            }
        }

        public IHttpActionResult Get(String regNo, DateTime date) {
            IEnumerable<Reservation> enumerable = Respository.GetBusReservation(regNo, date);
            if(enumerable == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(enumerable);
        }

        public IHttpActionResult Get(String regNo) {
            IEnumerable<Reservation> enumerable = Respository.GetBusReservation(regNo);
            if(enumerable == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(enumerable);
        }

        public IHttpActionResult Get(int id) {
            Reservation res = Respository.GetReservation(id);
            if(res == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(res);
        }

        public IHttpActionResult Post([FromBody] ReservationApiClass res) {
            Reservation result = Respository.PostReservation(res);
            if(result == null) {
                return InternalServerError(new Exception("Failed to create reservation please try again"));
            }
            return Created(new Uri(Request.RequestUri, "/get?id=" + result.Id), result);
        }
    }
}