using ChatWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatWeb.Filter
{
    //Esta clase se crea para realizar un filtro con el fin de obligar a la vista o pagina web a redireccionar todas las paginas
    //a la vista de inicio de sesion, solo permitira ir a otra pagina cuando se haya iniciado sesion correctamente y
    //se genere el "AccessToken"

    //Una vez creada la clase "VerifySession" se debe llamar o activar en "ChatWeb" carpeta "App_Start" en la clase "FilterConfig.cs"

    public class VerifySession : ActionFilterAttribute  //"ActionFilterAttribute" se hereda de esta clase que es de ".NET" especialmente para hacer filtros
    {
        //Es necesario sobreescribir el metodo ya existente "OnActionExecuting"
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);

                //"HomeController" siempre se tendra que ingresar esto porque si no se tiene aun sesion debe ser posble ingresar 
                //a "Login" y "Register" que son la forma de iniciar sesion o ya sea registrarse
                //por esto cuando se quiera ingresar a un controlador que necesite sesion pero no se tenga osea no se haya                 
                //ingresado usuario y contraseña nos redireccione a "HomeController" para poder iniciar la misma

                //Se confirma si la sesion fue creada exitosamente, debe ser creada en el  del "ChatWeb"
                //debe aparecer con el nombre "User"

                if (HttpContext.Current.Session["User"] == null)    //"== null" para saber que no hay sesion iniciada
                {
                    //"== null" no tienes sesion y "HomeController == false" no estas en el "Login" o "Register"
                    //te redirecciona a "Home/Login"
                    if (filterContext.Controller is HomeController == false)    //
                    {
                        filterContext.HttpContext.Response.Redirect("Home/Login");  //forma de redireccionar a un controlador
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            
        }
    }
}