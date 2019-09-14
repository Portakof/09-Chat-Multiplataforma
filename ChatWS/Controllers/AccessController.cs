using ChatWS.Models.WS;
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
    public class AccessController : ApiController
    {
        [HttpPost]
        public Reply Login(AccessRequest model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            using (ChatDBEntities db = new ChatDBEntities())
            {
                var oUser = (from d in db.user
                             where d.email== model.Email && d.password == model.Password
                             select d).FirstOrDefault();
                if(oUser != null)
                {
                    string AccessToken = Guid.NewGuid().ToString();

                    oUser.access_token = AccessToken;
                    db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    oR.result = 1;
                    oR.data = AccessToken;
                }
                else
                {
                    oR.message = "Datos incorrectos";
                }
            }

            return oR;
        }
    }
}
