using System;
using System.Collections.Generic;
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

        public IEnumerable<Reservation> GetAllReservations() {
            IEnumerable<Reservation> list = Respository.GetAllReservations();
            //if(list == null) {
            //    throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            //}
            return list;
        }

        [Route("{id:int}")]
        public void DeleteReservation(int id) {
            Reservation res = Respository.GetReservation(id);
            if(res == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            Respository.DeleteReservation(id);
        }

        
        public Reservation PostReservation(Reservation reservation) {
            Reservation res = Respository.PostReservation(reservation);
            if(res == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return res;
        }

        [Route("{id:int}")]
        public Reservation GetReservation(int id) {
            Reservation res = Respository.GetReservation(id);
            if(res == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return res;
        }
    }
}
