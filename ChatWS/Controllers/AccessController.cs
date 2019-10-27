using System;
using System.Linq;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS.Models;

namespace ChatWS.Controllers
{
    public class AccessController : ApiController
    {
        // "[HttpPost]" se usa para realizar una petición a un servidor enviando información, sea para insertar o consultar algo y esperar
        // una respuesta luego de finalizada la operación recibir algún tipo de mensaje o información.
        [HttpPost]  //En este metodo se recibe la informacion del "ChatWeb" para confirmar si el usuario existe 
        public Reply Login(AccessRequest model)
        {
            Reply oR = new Reply();     //Se crea el objeto para devolver los datos
            oR.result = 0;              //se inicia en "0" porque si hay algun error se enviara el mismo

            using (ChatDBEntities db = new ChatDBEntities())    //se crea el objeto "db" con la instancia para porder manejar la base de datos "ChatDB"
            {
                //se crea el objeto "oUser" para manejar y almacenar los datos obtenidos despues de realizar la consulta en la base datos "ChatDB"
                //Se crea un query o consulta a la base de datos "ChatDB"
                var oUser = (from d in db.user      //se crea la instancia de la tala a manejar en este caso "user"
                             where d.email== model.Email && d.password == model.Password    //se realiza la validacion o comparacion del objeto o datos recibidos por el usuario "ChatWeb" y los existentes en la base de datos "ChatDB"
                             select d).FirstOrDefault();    //"FirstOrDefault" si la consulta es igual a "1" significa que la comparacion fue exitosa y el usuario existe, de lo contrario devuelve un "null"
                if (oUser != null)      //Si es diferente de "null" quiere decir que existe el usuario
                {
                    string AccessToken = Guid.NewGuid().ToString();     //"Guid" crea la cadena de caracteres o "AccessToken"

                    oUser.access_token = AccessToken;       //se guarada el nuevo "AccessToken" en la instancia de la base datos "oUser"
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;        //se realiza la modificacion en la columna AccessToken de la instancia "oUser"
                    db.SaveChanges();       //se guardan los cambios realizados a la columna "AccessToken"

                    //Se cargan los atributos a devolver como respuesta despues de haber confirmado el "Login" del usuario como exitoso
                    UserResponse oUserResponse = new UserResponse();
                    oUserResponse.AccessToken = AccessToken;
                    oUserResponse.Name = oUser.name;
                    oUserResponse.City = oUser.city;
                    oUserResponse.Id = oUser.id;

                    oR.result = 1;      //se envia "1" porque el proceso fue exitoso
                    oR.data = oUserResponse;    //se devuelve en el "data" los atributos despues de haber confirmado el "Login" del usuario como exitoso
                }
                else
                {
                    oR.message = "Datos incorrectos";
                }
            }

            return oR;
        }
    }
}
