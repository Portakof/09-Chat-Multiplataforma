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
    public class ChatController : BaseController
    {        
        public ActionResult Messages(int id)
        {
            GetSession();

            List<MessagesResponse> lst = new List<MessagesResponse>();

            MessagesRequest OMessagesRequest = new MessagesRequest();
            OMessagesRequest.AccessToken = oUserSession.AccessToken;
            OMessagesRequest.IdRoom = id;

            RequestUtil oRequestUtil = new RequestUtil();
            Reply oReply = oRequestUtil.Execute<MessagesRequest>(Constants.Url.MESSAGES, "post", OMessagesRequest);

            lst= JsonConvert.DeserializeObject<List<MessagesResponse>>(JsonConvert.SerializeObject(oReply.data));

            lst = lst.OrderBy(d => d.DateCreated).ToList();

            ViewBag.idRoom = id;

            return View(lst);
        }
    }
}