using System;

namespace UtilitiesChat.Models.WS
{
    //Metodo con la estructura o lista a regresar por el "MessagesController" de "ChatWS"
    //esta respuesta se da por la solicitud que realizo "ChatController" de "ChatWeb" 
    public class MessagesResponse   
    {
        public DateTime DateCreated { get; set; }   //Devuelve la fecha de creacion
        public int Id { get; set; }                 //Si se quiere eliminar un mensaje en especifico al estilo de wasap con esto se identifica cual es
        public string Message { get; set; }         //Los mensajes existentes como tal en la sala o Room
        
        public int IdUser { get; set; }             
        public string UserName { get; set; }
        public int TypeMessage { get; set; }        //Con este se identifica si el mensaje es tuyo o no 
                                                    //esto para realizar una comparacion en la vista del proyecto "ChatWeb" y decidir 
                                                    //la ubicaion del mensaja -- derecha o la izquierda (1 propietario, 2 no propietario)
    }
}
