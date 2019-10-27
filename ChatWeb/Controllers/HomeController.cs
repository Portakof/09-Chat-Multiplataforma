using System.Web.Mvc;
using UtilitiesChat;
using ChatWeb.Business;
using ChatWeb.Models.ViewModels;
using UtilitiesChat.Models.WS;
using Newtonsoft.Json;

namespace ChatWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            UserAccessViewModel model = new UserAccessViewModel();
            return View(model);
        }

        [HttpPost]  //En este metodo especifica que se recibe la informacion
        //"model"trae los parametros que llena el formulario creado en "Login.cshtml"
        //"Login.cshtml" es la vista creada para que el usuario pueda ingresar los datos de registro
        public ActionResult Login(UserAccessViewModel model)
        {
            //De esta manera se valida o se ejecuta los "DataAnnotations" colocados en "RegisterViewModel.cs"
            //se hacen en el metodos post "HttpPost" cuando se hace el guardado
            if (!ModelState.IsValid)    //se valida los "DataAnnotations"
            {
                return View(model);     //si no se cumple los "DataAnnotations" se devuelve la vista para que sea necesario llenar los campos nuevamente
            }

            AccessRequest oAR = new AccessRequest();
            oAR.Email = model.Email;

            //Se llama el metodo "GetSHA256" el cual se encarga de encriptar el "Password" que ingresa el usuario y viene de la vista
            //de esta forma se almacena en "oAR" y se envia encriptado para no ser visible en la red cuando se hace el envio de la solicitud
            oAR.Password = UtilitiesChat.Tools.Encrypt.GetSHA256(model.Password);

            //se invoca la clase "RequestUtil" en la cual se genera la solicitud que sera enviada al "ChatWS"
            RequestUtil oRequesUtil = new RequestUtil();

            //En "oReply" quedara almacenada los datos que fueron devueltos por la solicitud hecha al "ChatWS" los cuales son AccessToken, Name, City, Id
            //ACCESS = contiene la direccion a la cual se hara la solicitud en este caso http://localhost:51592/api/Access 
            //"post" = es el metodo por el cual se realizara la solicitud
            //oAR = contiene los parametros enviado en la solicitud--Email,Password encriptado
            Reply oReply = oRequesUtil.Execute<AccessRequest>(Constants.Url.ACCESS, "post", oAR);

            //Deserealizamos el obejto "oReply" el cual trae los datos devueltos por la solicitud hecha al "ChatWS"
            UserResponse oUserResponse = JsonConvert.DeserializeObject<UserResponse>(JsonConvert.SerializeObject(oReply.data));

            if (oReply.result == 1)     //Con "1" se confirma que la solicitud hecha al "ChatWS" fue exitosa
            {
                //Se crea la Session "User" y con "oUserResponse" se le carga los valores devueltos por la solicitud hecha al "ChatWS"
                //los cuales son AccessToken, Name, City, Id
                Session["User"] = oUserResponse;
                return RedirectToAction("Index", "Lobby");  //De esta forma se redirecciona a otro controller en este caso a "LobbyControler" y su metodo "Index"
            }

            //"ViewBag" es un diccionario y se puede usar en todo el proyecto
            ViewBag.error = "Datos Incorrectos";    

            return View(model);     //Este "return" se devuelve a la vista porque hubo un error
        }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]  //En este metodo especifica que se recibe la informacion
        //"model"trae los parametros que llena el formulario creado en "Register.cshtml"
        //"Register.cshtml" es la vista creada para que el usuario pueda ingresar los datos de registro
        public ActionResult Register(RegisterViewModel model)
        {
            //De esta manera se valida o se ejecuta los "DataAnnotations" colocados en "RegisterViewModel.cs"
            //se hacen en el metodos post "HttpPost" cuando se hace el guardado
            if (!ModelState.IsValid)    //se valida los "DataAnnotations"
            {
                return View(model);     //si no se cumple los "DataAnnotations" se devuelve la vista para que sea necesario llenar los campos nuevamente
            }

            //"model" trae los datos ingresados por el cliente para guardarlos en "oUser" y asi realizar la solicitud a "ChatWS" y actualizar las base datos "ChatDB"
            Models.Request.User oUser = new Models.Request.User();
            oUser.Name = model.Name;
            oUser.City = model.City;
            oUser.Email = model.Email;
            oUser.Password = model.Password;

            //se invoca la clase "RequestUtil" en la cual se genera la solicitud que sera enviada al "ChatWS"
            RequestUtil oRequesUtil = new RequestUtil();
            //En "oReply" quedara almacenada los datos que fueron devueltos por la solicitud hecha al "ChatWS"
            //REGISTER = contiene la direccion a la cual se hara la solicitud en este caso http://localhost:51592/api/User 
            //"post" = es el metodo por el cual se realizara la solicitud
            //oUser = contiene los parametros enviado en la solicitud--Name,City,Email,Password
            Reply oReply = oRequesUtil.Execute<Models.Request.User>(Constants.Url.REGISTER, "post", oUser);

            return View();
        }

    }
}