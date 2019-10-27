
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS.Models;

namespace ChatWS.Controllers
{
    public class RoomController : BaseController
    {
        [HttpPost]
        public Reply List([FromBody] SecurityRequest model)
        {
            Reply oR = new Reply();     //Objeto que regresa los datos
            oR.result = 0;              //Se inicializa en "0" por si hay algun error retorna ese valor
             
            if (!VerifyToken(model))    //Si Genera seguridad al verificar si el "AccessToken" es correcto debe recibir "true"
            {
                oR.message = "Metodo no permitido";     //Mensaje a enviar cuando el "AccessToken" es invalido
                return oR;
            }

            using(ChatDBEntities db=new ChatDBEntities())    //Se crea el objeto "db" con la instancia para porder manejar la base de datos "ChatDB"
            {
                List<ListRoomsResponse> lstRoomsResponse = (from d in db.room     //room es la tabla de la base datos a la cual se accedera
                                                            where d.idState == 1    //Se toman los que esten activos
                                                            orderby d.name          //Ordenarlos en base a "name" 
                                                            select new ListRoomsResponse        //Se llena el objeto "ListRoomsResponse" 
                                                            {
                                                                Name = d.name,
                                                                Description = d.description,
                                                                Id = d.id
                                                            }).ToList();    //"ToList" convierte a lista 

                oR.data = lstRoomsResponse;     //Se carga a "data" la consulta hecha a la base datos
            }

            oR.result = 1;

            return oR;
        }
    }
}
