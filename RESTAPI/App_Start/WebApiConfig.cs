﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RESTAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
            name: "TicketMsgApi",
            routeTemplate: "api/{controller}/{id}/{message}",
            defaults: new { id = RouteParameter.Optional, message = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
            name: "TicketApi",
            routeTemplate: "api/{controller}/{id}/{reporter}/{message}",
            defaults: new { id = RouteParameter.Optional, reporter = RouteParameter.Optional, message = RouteParameter.Optional }
            );
  
        }
    }
}
