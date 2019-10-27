using ChatWeb.Business;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    //Este "ChatController" invoca el servicio de "MessagesController" esto con el fin de obtener todos los mensajes 
    //al ingresar a una sala o room
    //Se herada de "BaseController" porque este es el encargado de obtener la sesion una vez heradado no es necesario buscarla
    //Se obtiene la Session "User" creada en el "HomeController" y con "oUserResponse" es por el tipo de datos a devolver, tal cual son devueltos por la solicitud hecha al "ChatWS"
    //los datos son AccessToken, Name, City, Id
    public class ChatController : BaseController //este "BaseController" es del ChatWeb
    {        
        public ActionResult Messages(int id)
        {
            GetSession();   //Se llama GetSession que esta en "BaseController" para obtener la sesion iniciada

            List<MessagesResponse> lst = new List<MessagesResponse>();

            MessagesRequest OMessagesRequest = new MessagesRequest();
            OMessagesRequest.AccessToken = oUserSession.AccessToken;    //Se guarda el "AccessToken" que se obtiene de la sesion obtenida por el "BaseController"
            OMessagesRequest.IdRoom = id;                               //y el Idroom

            //se invoca la clase "RequestUtil" en la cual se genera la solicitud que sera enviada al "MessagesController" de "ChatWS"
            RequestUtil oRequestUtil = new RequestUtil();
            //En "oReply" quedara almacenada los datos que fueron devueltos por la solicitud hecha al "ChatWS" los cuales son Id, Name, Descripcion
            //ACCESS = contiene la direccion a la cual se hara la solicitud en este caso http://localhost:51592/api/Messages 
            //"post" = es el metodo por el cual se realizara la solicitud
            //OMessagesRequest = contiene los parametros enviado en la solicitud--AccessToken, IdRoom
            Reply oReply = oRequestUtil.Execute<MessagesRequest>(Constants.Url.MESSAGES, "post", OMessagesRequest);     //para traer todos los mensajes de la sala o room

            //Deserealizamos el obejto "oReply" el cual trae los datos devueltos por la solicitud hecha al "ChatWS"
            lst = JsonConvert.DeserializeObject<List<MessagesResponse>>(JsonConvert.SerializeObject(oReply.data));   //deserealizamos el obejto "oReply" el cual trae la lista de mensajes

            lst = lst.OrderBy(d => d.DateCreated).ToList(); //se oredena la la lista en forma ascendente 

            //Se utiliza el diccionario "ViewBag" para pasar el parametro del "idRoom" el cual sera utilizado en "Messages.cshtml"
            ViewBag.idRoom = id;

            return View(lst);   //se envia el objeto con la lista a la vista web
        }
    }
}