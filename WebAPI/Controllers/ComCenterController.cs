using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using WebAPI.Models;

namespace WebAPI.Controllers {
    public class ComCenterController : ApiController {
        static readonly ICenterRespository Respository = new ComCenterRepository();

        public IEnumerable<ComCenter> GetAllComCenters() {
            return Respository.GetAllComCenters();
        } 
    }
}
