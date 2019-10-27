
using System.Web.Mvc;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{

    //Todo lo controller que necesite sesion o seguridad heredara de "BaseController" porque este controller sera el encargado de obtener la sesion
    public class BaseController : Controller
    {
        public UserResponse oUserSession;       //Se crea la variable "UserResponse" para poder luego se utilizada

        protected void GetSession()
        {
            //Se obtiene la Session "User" creada en el "HomeController" y con "oUserResponse" es por el tipo de datos a devolver, tal cual son devueltos por la solicitud hecha al "ChatWS"
            //los datos son AccessToken, Name, City, Id
            oUserSession = (UserResponse)Session["User"];
        }
    }
}