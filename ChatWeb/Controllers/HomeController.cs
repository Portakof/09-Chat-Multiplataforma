using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilitiesChat;
using ChatWeb.Business;

namespace ChatWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.Request.User oUser = new Models.Request.User();
            oUser.Name = "Gatito";
            oUser.City = "San Pascual";
            oUser.Email = "Ratoncito@mail.com";
            oUser.Password = "123456";

            RequestUtil oRequesUtil = new RequestUtil();
            UtilitiesChat.Models.WS.Reply oReply = oRequesUtil.Execute<Models.Request.User>(Constants.Url.REGISTER,"post",oUser);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}