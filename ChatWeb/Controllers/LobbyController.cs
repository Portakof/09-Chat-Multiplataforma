using ChatWeb.Business;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using UtilitiesChat;
using UtilitiesChat.Models.WS;

namespace ChatWeb.Controllers
{
    //Se herada de "BaseController" porque este es el encargado de obtener la sesion una vez heradado no es necesario buscarla
    //Se obtiene la Session "User" creada en el "HomeController" y con "oUserResponse" es por el tipo de datos a devolver, tal cual son devueltos por la solicitud hecha al "ChatWS"
    //los datos son AccessToken, Name, City, Id
    public class LobbyController : BaseController   
    {
        // GET: Lobby
        public ActionResult Index()
        {
            GetSession();   //Se llama GetSession que esta en "BaseController" para obtener la sesion iniciada

            List<ListRoomsResponse> lst = new List<ListRoomsResponse>();

            SecurityRequest oSecurityRequest = new SecurityRequest();
            oSecurityRequest.AccessToken = oUserSession.AccessToken;    //Se guarda el "AccessToken" que se obtiene de la sesion obtenida por el "BaseController"

            //se invoca la clase "RequestUtil" en la cual se genera la solicitud que sera enviada al "ChatWS"
            RequestUtil oRequesUtil = new RequestUtil();

            //En "oReply" quedara almacenada los datos que fueron devueltos por la solicitud hecha al "ChatWS" los cuales son Id, Name, Descripcion
            //ACCESS = contiene la direccion a la cual se hara la solicitud en este caso http://localhost:51592/api/Room 
            //"post" = es el metodo por el cual se realizara la solicitud
            //oSecurityRequest = contiene los parametros enviado en la solicitud--AccessToken
            Reply oReply = oRequesUtil.Execute<SecurityRequest>(Constants.Url.ROOMS, "post", oSecurityRequest);

            //Deserealizamos el obejto "oReply" el cual trae los datos devueltos por la solicitud hecha al "ChatWS"
            lst = JsonConvert.DeserializeObject<List<ListRoomsResponse>>(JsonConvert.SerializeObject(oReply.data));

            return View(lst);   //Se envia a la vista la respuesta o lista devuelta por la solicitud a la base datos 
        }
    }
}