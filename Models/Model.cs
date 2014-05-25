using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public string Email { get; set; }
        public int Mobile { get; set; }
        public bool IsAdmin { get; set; }
    }

    public class ComCenter {
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public int ContactPhone { get; set; }
        public IEnumerable<Bus> Busses { get; set; }

    }

    public class Bus {
        public string RegNo { get; set; }
        public string ComCenter { get; set; }
    }

    public class Reservation {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public User User { get; set; }
        public Bus Bus { get; set; }
    }
}