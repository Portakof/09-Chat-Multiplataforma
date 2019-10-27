using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using ChatWS.Models;

namespace ChatWS
{
    public class CounterHub : Hub
    {
        //Se sobreescribe el metodo el metodo "OnConnected" y "Task" es para trabajar en modo asincrono osea en otro hilo de ejecucion
        public override Task OnConnected()  //Este metodo se ejecuta cuando se conecta alguien al chat
        {
            //"All" para que a todos se le envie mensaje que entro un usuario y se crea "enterUser" con el java script creado en 
            //"ChatWS" "Startup.cs" metodo "app.Map("/signalr", map => "
            Clients.All.enterUser();    
            return base.OnConnected();
        }

        public void AddGroup(int idRoom)
        {
            //Esta funcion se utiliza para controlar o enviar los mensajes a una sala en especifico, 
            //esto para evitar que el mensaje llegue a todos
            Groups.Add(Context.ConnectionId, idRoom.ToString());
        }

        //El metodo "Send" es invocado por una funcion de javascript que se genera automaticamente en la clase "Startup.cs" de "ChatWS"
        //"Startup.cs" se genera una libreria y "url" llamada "/signalr" cabe aclarar que esta libreria se genera segun los metodos que hayan
        //en el "CounterHub.cs"
        //"/signalr" va a recibir los parametros que envia "Send"
        public void Send(int idRoom, string userName, int idUser, string message, string AccessToken)
        {
            if (VerifyToken(AccessToken))
            {
                string fecha = DateTime.Now.ToString();     //Varible para poder tomar la hora y fecha del servidor y luego mostrarlo en cada mensaje del chat


                using (ChatDBEntities db = new ChatDBEntities())     //se crea el objeto "db" con la instancia para porder manejar la base de datos "ChatDB"
                {
                    //"oMessage" se crea y se entrega el modelo que esta en "ChatWS/Models/DB.edmx" 
                    //este modelo es la tabla de "message" dela base datos "ChatDB"
                    var oMessage = new message();

                    //Se ingresan los valores del nuevo mensaje
                    oMessage.idRoom = idRoom;
                    oMessage.date_create = DateTime.Now;
                    oMessage.idUser = idUser;
                    oMessage.text = message;
                    oMessage.idState = 1;

                    //Se agrega el modelo anteriormente llenado 
                    db.message.Add(oMessage);

                    //Se guardan los cambios en la tabla "message" de la base datos "ChatDB"
                    db.SaveChanges();
                }

                //De esta manera se envia el mensaje a todos y todas las salas o Rooms 
                //Se envian los parametros y se llama al metodo que recibira el nuevo mensaje
                //Clients.All.sendChat(username, message, fecha, idUser);

                //De esta manera se tiene encuenta el "idRoom" o sala para enviar el mensaje a solo una sala 
                //Se envian los parametros y se llama al metodo que recibira el nuevo mensaje
                Clients.Group(idRoom.ToString()).sendChat(userName, message, fecha, idUser);

            }
        }

        protected bool VerifyToken(string AccessToken)
        {
            using (ChatDBEntities db = new ChatDBEntities())
            {
                var oUser = db.user.Where(d => d.access_token == AccessToken).FirstOrDefault();

                if (oUser != null)
                {
                    return true;
                }
            }
            return false;
        }

    }
}