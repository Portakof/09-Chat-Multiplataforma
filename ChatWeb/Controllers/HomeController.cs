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

        [HttpPost]
        public ActionResult Login(UserAccessViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AccessRequest oAR = new AccessRequest();
            oAR.Email = model.Email;
            oAR.Password = UtilitiesChat.Tools.Encrypt.GetSHA256(model.Password);

            RequestUtil oRequesUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply = 
                oRequesUtil.Execute<AccessRequest>(Constants.Url.ACCESS, "post", oAR);

            UserResponse oUserResponse = 
                JsonConvert.DeserializeObject<UserResponse>
                (JsonConvert.SerializeObject(oReply.data));

            if (oReply.result == 1)
            {
                Session["User"] = oUserResponse;
                return RedirectToAction("Index", "Lobby");
            }
            //Session["User"] = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92";

            ViewBag.error = "Datos Incorrectos";

            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            ChatWeb.Models.ViewModels.RegisterViewModel model = new Models.ViewModels.RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(ChatWeb.Models.ViewModels.RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Models.Request.User oUser = new Models.Request.User();
            oUser.Name = model.Name;
            oUser.City = model.City;
            oUser.Email = model.Email;
            oUser.Password = model.Password;

            RequestUtil oRequesUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply = oRequesUtil.Execute<Models.Request.User>(Constants.Url.REGISTER, "post", oUser);

            return View();
        }

    }
}