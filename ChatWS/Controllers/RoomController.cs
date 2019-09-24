using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UtilitiesChat.Models.WS;
using ChatWS.Models;

namespace ChatWS.Controllers
{
    public class RoomController : BaseController
    {
        [HttpPost]
        public Reply List([FromBody] SecurityRequest model)
        {
            Reply oR = new Reply();
            oR.result = 0;
             
            if (!VerifyToken(model))
            {
                oR.message = "Metodo no permitido";
                return oR;
            }

            using(ChatDBEntities db=new ChatDBEntities())
            {
                List<ListRoomsResponse> lstRoomsResponse = (from d in db.room
                                                            where d.idState == 1
                                                            orderby d.name
                                                            select new ListRoomsResponse
                                                            {
                                                                Name = d.name,
                                                                Description = d.description,
                                                                Id = d.id
                                                            }).ToList();

                oR.data = lstRoomsResponse;
            }

            oR.result = 1;

            return oR;
        }
    }
}
