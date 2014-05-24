using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPI.Models {
    interface IAuthorizationSystem {
        bool IsAdmin(String mobile);
    }
}
