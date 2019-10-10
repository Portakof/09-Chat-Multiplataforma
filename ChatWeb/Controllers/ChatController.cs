using ChatWeb.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    //Este "ChatController" invoca el servicio de "MessagesController" esto con el fin de obtener todos los mensajes 
    //al ingresar a una sala o room
    public class ChatController : BaseController //este "BaseController" es del ChatWeb
    {        
        public ActionResult Messages(int id)
        {
            GetSession();

            List<MessagesResponse> lst = new List<MessagesResponse>();

            MessagesRequest OMessagesRequest = new MessagesRequest();
            OMessagesRequest.AccessToken = oUserSession.AccessToken;    //se recibe o obtiene el objeto con el AccessToken 
            OMessagesRequest.IdRoom = id;                               //y el Idroom

            RequestUtil oRequestUtil = new RequestUtil();   //Se crea un objeto y se hace la peticion en si al "MessagesController"
            Reply oReply = oRequestUtil.Execute<MessagesRequest>(Constants.Url.MESSAGES, "post", OMessagesRequest);     //para traer todos los mensajes de la sala o room

            lst= JsonConvert.DeserializeObject<List<MessagesResponse>>(JsonConvert.SerializeObject(oReply.data));   //deserealizamos el obejto "oReply" el cual trae la lista de mensajes

            lst = lst.OrderBy(d => d.DateCreated).ToList(); //se oredena la la lista en forma ascendente 

            ViewBag.idRoom = id;

            return View(lst);   //se envia el objeto con la lista a la vista web
        }
    }
}