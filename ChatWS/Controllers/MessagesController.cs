using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS.Models;

namespace ChatWS.Controllers
{
    public class MessagesController : BaseController //de "BaseController de "ChatWS"" permite usar el metodo "VerifyToken"
    {
        [HttpPost]  //el metodo es "post" porque recibe el "AccessToken"

        public Reply Get([FromBody] MessagesRequest model)  //se recibe del "FromBody" el metodo "MessagesRequest" el cual en model trae el "IdRoom"
        {
            Reply oR = new Reply();     //objeto que regresa los datos
            oR.result = 0;

            if (!VerifyToken(model))    //genera seguridad al verificar si el "AccessToken" es correcto
            {
                oR.message = "Metodo no permitido";
                return oR;
            }
            
            try
            {
                using(ChatDBEntities db = new ChatDBEntities())     //se crea el objeto que maneja la conexion con la base de datos
                {
                    //se crea la lista con los datos a devolver
                    List<MessagesResponse> lst = (from d in db.message.ToList()     //message es la tabla de la base datos 
                                                  where d.idState == 1 && d.idRoom == model.IdRoom  //se toman los que esten activos
                                                  orderby d.date_create descending //ordenar de forma descendente entrega los ultimos
                                                  select new MessagesResponse       //se llena el objeto "MessagesResponse"
                                                  {
                                                      Message = d.text,
                                                      Id = d.id,                                                      
                                                      IdUser = d.user.id, 
                                                      UserName = d.user.name,       //se aprobecha la conexion creada entre tablas para acceder a tabla "user" para obtener el Id
                                                      DateCreated = d.date_create,  //se obtiene la fecha del mensaje
                                                      TypeMessage = (
                                                                    new Func<int>(  //se crea la funcion y se especifica que devolvera un entero sea 1 o 2
                                                                        () =>   //de esta forma se crea la funcion anonima
                                                                        {
                                                                            try
                                                                            {
                                                                                if (d.user.id == oUserSession.Id)   //se verifica si el mensaje es mio = 1 0 no que seria 2
                                                                                    return 1;
                                                                                else
                                                                                    return 2;
                                                                            }
                                                                            catch
                                                                            {
                                                                                return 3;
                                                                            }
                                                                        }
                                                                        )()
                                                                    )
                                                  }).Take(20).ToList(); //Take se utiliza para devolver los ultimos 20 mensajes
                    oR.result = 1;  //
                    oR.data = lst;  //carga la lista creada en el objeto tipo Reply
                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error";
            }

            return oR;  //Retorna la lista que fue creada
        }
    }
}
