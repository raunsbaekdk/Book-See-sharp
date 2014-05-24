using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPI.Security {
    interface IAuthorizationSystem {
        bool IsAdmin(String mobile);
    }
}
