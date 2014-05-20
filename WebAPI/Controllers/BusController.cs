using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;

namespace WebAPI.Models {
    public class BusController : ApiController {
        static readonly IBusRespository Respository = new BusRepository();
        public IEnumerable<Bus> GetAllBusses() {
            return Respository.GetAllBusses();
        } 
    }
}
