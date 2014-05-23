using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Models;
using WebAPI.Models;

namespace WebAPI.Controllers {

    public class ReservationController : ApiController {
        static readonly IReservationRespository Respository = new ReservationRepository();

        [HttpGet]
        public IHttpActionResult Get() {
            IEnumerable<Reservation> list = Respository.GetAllReservations();
            if(list == null) {
                return NotFound();
            }
            return Ok(list.AsQueryable());
        }

        [HttpGet]
        public void Delete(int id) {
            Reservation res = Respository.GetReservation(id);
            if(res == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            Respository.DeleteReservation(id);
        }

        [HttpGet]
        public IHttpActionResult Get(String regNo, DateTime date) {
            IEnumerable<Reservation> enumerable = Respository.GetBusReservation(regNo, date);
            if(enumerable == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(enumerable);
        }

        [HttpGet]
        public IHttpActionResult Get(String regNo) {
            IEnumerable<Reservation> enumerable = Respository.GetBusReservation(regNo);
            if(enumerable == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(enumerable);
        }

        [HttpGet]
        public IHttpActionResult Get(int id) {
            Reservation res = Respository.GetReservation(id);
            if(res == null) {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok(res);
        }
        
        [HttpPost]
        //public IHttpActionResult Post([FromBody]Reservation reservation) {
        //    Reservation res = Respository.PostReservation(reservation);
        //    if(res == null) {
        //        return NotFound();
        //    }
        //    return Ok(res);
        //}

        public IHttpActionResult Post([FromBody] ReservationApiClass res) {
            Reservation result = Respository.PostReservation(res);
            if(result == null) {
                return InternalServerError(new Exception("Failed to create reservation please try again"));
            }
            return Created(new Uri(Request.RequestUri, "/api/comments/" + result.Id), result);
        }
    }
}