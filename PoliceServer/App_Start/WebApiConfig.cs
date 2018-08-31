using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PoliceServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
//            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "GetPatteInfoRequest",
                routeTemplate: "api/{controller}/{patteNo}/{username}/{password}/{type}",
                defaults: new { type = "patteType" }
                );
            config.Routes.MapHttpRoute(
               name: "GetAuthentication",
               routeTemplate: "api/{controller}/{username}/{password}"
               );

        }
    }
}
