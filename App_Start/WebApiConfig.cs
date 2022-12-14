using ePaperWeb.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ePaperWeb
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //add basic authenication check
            config.Filters.Add(new BasicAuthenticationAttribute());

        }
    }
}
