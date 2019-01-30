using Harman.Healthcare.BL;
using Harman.Healthcare.Contract.BL;
using Harman.Healthcare.Contract.DAL;
using Harman.Healthcare.DAL;
using Harman.Healthcare.Host.Filters;
using Harman.Healthcare.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Unity;

namespace Harman.Healthcare.Host
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services        
            IocConfig.InitializeIOCContainer();

            config.DependencyResolver = new UnityDependencyResolver(IocConfig.Container);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new CustomExceptionFilter());
            config.Services.Replace(typeof(IExceptionHandler), new CustomExceptionHandler());
        }

       
    }
}
