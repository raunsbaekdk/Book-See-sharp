using System.Threading;

namespace WebAPI.Service {
    public class IdentityService : IIdentityService {
        public string CurrentUser() {
            return Thread.CurrentPrincipal.Identity.Name;
        }
    }
}