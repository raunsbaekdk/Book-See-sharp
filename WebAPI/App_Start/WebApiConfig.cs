using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;
using WebAPI.Controllers;

namespace WebAPI {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Web API configuration and services
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}/",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "api/{controller}/{id}/",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}