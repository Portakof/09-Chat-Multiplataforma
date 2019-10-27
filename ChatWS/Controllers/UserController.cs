
using System;
using System.Web.Http;
using UtilitiesChat.Models.WS;

namespace ChatWS.Controllers
{    
    public class UserController : ApiController //El "UserController" se crea para registrar los usuarios que van a utilizar el chat.
                                                
    {
        // "[HttpPost]" se usa para realizar una petición a un servidor enviando información, sea para insertar o consultar algo y esperar
        // una respuesta luego de finalizada la operación recibir algún tipo de mensaje o información. Por ejemplo yo utilizo el POST para
        //poder iniciar sesion en una aplicacion móvil, envío el usuario y la contraseña a un servidor esperando la respuesta si existe el
        //usuario y la contraseña.

        //Es un atributo cuya finalidad es establecer la forma en que la acción puede ser accedida; es decir si usas el que tiene GET solo 
        //"lo encontrara" cuando el método de llamada fue GET;para el caso del POST es similar cuando la invocación de una acción sea por POST.

        [HttpPost]  //En este metodo se recibe la informacion del "ChatWeb" para guardar el usuario en base de datos
        public Reply Register([FromBody] Models.Request.User model) //El metodo "Register" se crea para registrar un usuario
        {
            Reply oReply = new Reply(); //Se crea el objeto para devolver los datos

            try
            {
                using (Models.ChatDBEntities db = new Models.ChatDBEntities())  //se crea el objeto "db" con la instancia para porder manejar la base de datos "ChatDB"
                {
                    Models.user oUser = new Models.user();  //se crea el objeto "oUser" para manejar y almacenar los datos que seran enviados a la base datos "ChatDB"
                    oUser.email = model.Email;  //Se asignan los valores obtenidos para poder cargarlos en la tabla "user"
                    oUser.password = model.Password;
                    oUser.name = model.Name;
                    oUser.city = model.City;
                    oUser.date_create = DateTime.Now;   //"DateTime.Now" toma la fecha y hora actual del servidor
                    oUser.idState = 1;

                    db.user.Add(oUser); //se agrega el objeto oUser que es la representacion del "Entities Framework"
                    db.SaveChanges();   //se guardan los cambios para agregar el usuario nuevo en la base datos

                    oReply.result = 1;  //cuando se envia "1" significa que el proceso fue exitoso
                }
            }
            catch (Exception)
            {
                oReply.result = 0;  //cuando se envia "0" significa que el proceso fue exitoso
                oReply.message = "El problema es en metodo Register del UserController";
            }

            return oReply;
        }



        //En el caso del GET se usa para obtener información, por ejemplo si lo que vas hacer es enviar una array de información luego de
        //haber actualizado algo del lado del servidor, por ejemplo, cuando luego de conectarte a tu teléfono te llegan mensajes sin 
        //necesidad de enviar algún tipo de informacion.

        /*[HttpGet]
        public Reply Get()
        {
            Reply oReply = new Reply();
            using(Models.ChatDBEntities db=new Models.ChatDBEntities())
            {
                List<UserViewModel> lst = (from d in db.user    //dela tabla usuarios trae todos los datos
                                           select new UserViewModel //los seleciona y los acopla la forma del objeto "UserViewModel"
                                           {
                                               Name = d.name,   //guarda los atributos de la tabla usuario en el objeto "UserViewModel"
                                               City = d.city
                                           }).ToList();

                oReply.result = 1;
                oReply.data = lst;  //devuelve la lista creada en base a tabla usuarios acoplada a el objeto "UserViewModel"
            }
            return oReply;
        }*/


    }
}
