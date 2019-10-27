using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS.Models;

namespace ChatWS.Controllers
{
    //Se herada de "BaseController" porque este es el encargado de obtener la sesion una vez heradado no es necesario buscarla
    //Se obtiene la Session "User" creada en el "HomeController" y con "oUserResponse" es por el tipo de datos a devolver, tal cual son devueltos por la solicitud hecha al "ChatWS"
    //los datos son AccessToken, Name, City, Id
    public class MessagesController : BaseController //de "BaseController de "ChatWS"" permite usar el metodo "VerifyToken"
    {
        [HttpPost]  //el metodo es "post" porque recibe el "AccessToken"
        public Reply Get([FromBody] MessagesRequest model)  //se recibe del "FromBody" el metodo "MessagesRequest" el cual en model trae el "IdRoom"
        {
            Reply oR = new Reply();     //objeto que regresa los datos
            oR.result = 0;

            if (!VerifyToken(model))    //Si Genera seguridad al verificar si el "AccessToken" es correcto debe recibir "true"
            {
                oR.message = "Metodo no permitido";     //Mensaje a enviar cuando el "AccessToken" es invalido
                return oR;
            }
            
            try
            {
                using(ChatDBEntities db = new ChatDBEntities())     //se crea el objeto que maneja la conexion con la base de datos
                {
                    //Se genera la consulta a la base datos "ChatDB" y con los datos devueltos de la consulta 
                    //se crea la lista con los datos a devolver
                    List<MessagesResponse> lst = (from d in db.message.ToList()     //message es la tabla de la base datos a la cual se accedera
                                                  //Se toman los que esten activos "d.idState == 1" y se valida la sala de la cual seran tomados los mensajes "d.idRoom == model.IdRoom"
                                                  where d.idState == 1 && d.idRoom == model.IdRoom  
                                                  orderby d.date_create descending //Ordenarlos en base a "date_create" de forma descendente, entrega los ultimos
                                                  select new MessagesResponse       //se llena el objeto "MessagesResponse"
                                                  {
                                                      Message = d.text,             //Mensajes existentes en el Room o sala
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
                                                                                //"oUserSession" es la variable creada en "ChatWS" "BaseController" del metodo "VerifyToken" la cual contiene el Id del usuario que tiene la sesion iniciada
                                                                                //Esta se verifica con el "id" de la base datos
                                                                                if (d.user.id == oUserSession.Id)   //se verifica si el mensaje es mio = 1, de lo contrario seria 2
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
                                                  }).Take(20).ToList(); //"Take(valor opcional)" se utiliza para filtar los ultimos mensajes a devolver, "ToList" convierte a lista
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
