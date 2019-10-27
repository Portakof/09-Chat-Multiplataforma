﻿using System.Web;
using System.Web.Mvc;

namespace ChatWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filter.VerifySession());    //Activacion del filtro creado en la clase "VerifySession"
        }
    }
}
