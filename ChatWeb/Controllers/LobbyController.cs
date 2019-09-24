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
    public class LobbyController : BaseController
    {
        // GET: Lobby
        public ActionResult Index()
        {
            GetSession();

            List<ListRoomsResponse> lst = new List<ListRoomsResponse>();

            SecurityRequest oSecurityRequest = new SecurityRequest();
            oSecurityRequest.AccessToken = oUserSession.AccessToken;

            RequestUtil oRequesUtil = new RequestUtil();
             Reply oReply = oRequesUtil.Execute<SecurityRequest>
                (Constants.Url.ROOMS, "post", oSecurityRequest);

            lst = JsonConvert.DeserializeObject<List<ListRoomsResponse>>
                (JsonConvert.SerializeObject(oReply.data));

            return View(lst);
        }
    }
}