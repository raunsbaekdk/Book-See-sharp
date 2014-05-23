using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [Authorize]
    public class BusController : ApiController {
        static readonly IBusRespository Respository = new BusRepository();
        public IHttpActionResult GetAllBusses() {
            IEnumerable<Bus> busses = Respository.GetAllBusses();
            if(busses == null) {
                return NotFound();
            }
            return Ok(busses);
        } 
    }
}
