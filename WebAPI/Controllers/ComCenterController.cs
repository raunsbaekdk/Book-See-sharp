using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;
using WebAPI.Models;

namespace WebAPI.Controllers {
    [Authorize]
    public class ComCenterController : ApiController {
        static readonly ICenterRespository Respository = new ComCenterRepository();

        public IHttpActionResult GetAllComCenters() {
            IEnumerable<ComCenter> centers = Respository.GetAllComCenters();
            if(centers == null) {
                return NotFound();
            }
            return Ok(centers);
        } 
    }
}
