using System.Web.Mvc;
using UtilitiesChat;
using ChatWeb.Business;

namespace ChatWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {        
            return View();
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