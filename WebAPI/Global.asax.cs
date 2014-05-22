using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;

namespace WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            

        }

        public override void Init() {
            this.AuthenticateRequest += new EventHandler(WebApiApplication_AuthenticateRequest);
            base.Init();
        }

        void WebApiApplication_AuthenticateRequest(object sender, EventArgs e) {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

            SampleIdentity id = new SampleIdentity(ticket);
            GenericPrincipal prin = new GenericPrincipal(id, null);

            HttpContext.Current.User = prin;
        }
    }
}