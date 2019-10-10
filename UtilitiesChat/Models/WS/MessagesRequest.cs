using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesChat.Models.WS
{
    //ES request porque es la que manda el cliente
    //despues de ejecutarse "MessagesRequest" este mismo llama a "SecurityRequest"

    public class MessagesRequest : SecurityRequest  //hereda de SecurityRequest para recibir de una el AccessToken
    {
        public int IdRoom { get; set; }     //Se recibe el Id de la sala o Room para ser tambien recibido en el controler del CHATWS "MessagesController"
    }
}
