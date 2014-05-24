using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models {
    public class ReservationApiClass {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Mobile { get; set; }
        public string RegNo { get; set; }
    }

    public class UserApiClass {
        public string Name { get; set; }
        public int Mobile { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public String Email { get; set; }
    }
}