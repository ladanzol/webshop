using Alb.Common.UI.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace Alb.Omdehsara.UI.MVC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "Get", id = RouteParameter.Optional }
            );
            config.Services.Replace(typeof(IExceptionHandler), new AlbExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new AlbExceptionLogger());
            
        }
    }
}
