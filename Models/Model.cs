using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Models {
    public class ConvertFunctions {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }

    public class User {
        public string Name { get; set; }
        public int Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class ComCenter {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public int ContactPhone { get; set; }

    }

    public class Bus {
        public string RegNo { get; set; }
        public string comCenter { get; set; }
    }

    public class Reservation {
        public DateTime FromDaTe { get; set; }
        public DateTime ToDate { get; set; }
    }

}