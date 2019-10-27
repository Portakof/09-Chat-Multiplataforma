
using System.Linq;
using UtilitiesChat.Models.WS;
using ChatWS.Models;
using System.Web.Http;

namespace ChatWS.Controllers
{
    public class BaseController : ApiController
    {
        public UserResponse oUserSession;   //Creamos la variable de tipo UserResponse

        //"VerifyToken" se utiliza este metodo para verificar si el usuario esta activo osea tiene "AccessToken"
        //"protected" porque cuando se ejcuta el proyecto se genera un conflicto entre "BaseController" y "RoomController"
        //esto debido a que los dos reciben la clase "SecurityRequest" y con esto se soluciona
        protected bool VerifyToken(SecurityRequest model)
        {
            using (ChatDBEntities db = new ChatDBEntities())    //se crea el objeto "db" con la instancia para porder manejar la base de datos "ChatDB"
            {
                //Se envia a comparar el "AccessToken" recibido con el existente en la base datos "ChatDB"
                //"FirstOrDefault" si la consulta es igual a "1" significa que la comparacion fue exitosa y el usuario existe, de lo contrario devuelve un "null"
                var oUser = db.user.Where(d => d.access_token == model.AccessToken).FirstOrDefault();

                if (oUser != null)      //Si es diferente de "null" quiere decir que existe el usuario
                {
                    oUserSession = new UserResponse();  //Se inicializa la variable anteriormente creada

                    oUserSession.AccessToken = oUser.access_token;
                    oUserSession.City = oUser.city;
                    oUserSession.Name = oUser.name;
                    oUserSession.Id = oUser.id;

                    return true;
                }
            }
            return false;
        }
    }
}
