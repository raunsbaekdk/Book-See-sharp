using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Web;

namespace WebAPI.Service {
    public interface IIdentityService {
        string CurrentUser();
    }
}