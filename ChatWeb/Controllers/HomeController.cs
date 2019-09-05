using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.Request.User oUser = new Models.Request.User();
            oUser.Name = "Felipe";
            oUser.City = "San Pascual";
            oUser.Email = "felipin@mail.com";
            oUser.Password = "123456";

            Utils.RequestUtil oRequesUtil = new Utils.RequestUtil();
            Models.WS.Reply oReply = oRequesUtil.Execute<Models.Request.User>("http://localhost:51592/api/User/","post",oUser);

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