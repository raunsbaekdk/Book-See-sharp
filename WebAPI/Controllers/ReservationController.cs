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
    [Authorize]
    public class ReservationController : ApiController {
        static readonly IReservationRespository Respository = new ReservationRepository();

        public IEnumerable<Reservation> GetAllReservations() {
            IEnumerable<Reservation> list = Respository.GetAllReservations();
            if(list == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return list;
        }

        public void DeleteReservation(int id) {
            Reservation res = Respository.GetReservation(id);
            if(res == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            Respository.DeleteReservation(id);
        }

        public IEnumerable<Reservation> GetBusReservation(String regNo, DateTime date) {
            IEnumerable<Reservation> enumerable = Respository.GetBusReservation(regNo, date);
            if(enumerable == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return enumerable;
        }

        public IEnumerable<Reservation> GetBusReservation(String regNo) {
            IEnumerable<Reservation> enumerable = Respository.GetBusReservation(regNo);
            if(enumerable == null) {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return enumerable;
        }
            
        public Reservation PostReservation(string regNo, DateTime fromDate, DateTime toDate) {
            int mobile = Convert.ToInt32(HttpContext.Current.Session["username"]);
            Reservation reservation = new Reservation {
                Bus = new Bus() {RegNo = regNo},
                ToDate = toDate,
                FromDate = fromDate,
                User = new User() {Mobile = mobile}
            };

            Reservation res = Respository.PostReservation(reservation);
            if(res == null) {
                //throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return res;
         }

        public void GetReservation(int id) {
            Reservation res = Respository.GetReservation(id);
        }
    }
}