﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ChatWS
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Estas lineas de codigo obligan a que toda solicitud que se realice al API o "ChatWS"
            //la respuesta o return que realiza que se genera se convierta a "json".
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

        }
    }
}
