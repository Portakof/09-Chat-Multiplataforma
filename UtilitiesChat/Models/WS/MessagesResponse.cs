using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesChat.Models.WS
{
    public class MessagesResponse   //Metodo con la estructura o lista a regresar
    {
        public DateTime DateCreated { get; set; }   //devuelve la fecha de creacion
        public int Id { get; set; }                 //por si se quiere eliminar el mensaje como en wasap con esto se identifica cual es
        public string Message { get; set; }
        public int IdRoom { get; set; }

        public int IdUser { get; set; }
        public string UserName { get; set; }
        public int TypeMessage { get; set; }        //Con este se identifica si el mensaje es tuyo o no si el mensaje aparece 
                                                    //a la derecha o la iauierda (1 propietario, 2 no propietario)
    }
}
