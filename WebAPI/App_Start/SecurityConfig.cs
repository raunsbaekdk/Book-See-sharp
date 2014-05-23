using System.Web.Http;

namespace WebAPI {
    public class SecurityConfig {
        public static void ConfigureGlobcal(HttpConfiguration globalConfig) {
            globalConfig.MessageHandlers.Add(new Authentication);
        }
         
    }
}