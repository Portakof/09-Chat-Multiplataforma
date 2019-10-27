
using System.Web.Mvc;
using System.Web.Routing;

namespace ChatWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //Estaba action = "Index" se cambio por action = "Login" esto porque ya no hay y no se usara el medodo
                //Index en el HomeController, de esta forma queda predeterminado que la vista inicie por Login 
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
