
namespace UtilitiesChat.Models.WS
{
    //ES request porque es la que manda el cliente
    //Dependiendo del "IdRoom" se enviaran los mensajes de esa sala
    //despues de ejecutarse "MessagesRequest" este mismo llama a "SecurityRequest" y se obtiene por herencia el "AccessToken"

    public class MessagesRequest : SecurityRequest  //hereda de SecurityRequest para recibir de una el AccessToken
    {
        public int IdRoom { get; set; }     //Se recibe el Id de la sala o Room para ser tambien recibido en el controler del CHATWS "MessagesController"
    }
}
