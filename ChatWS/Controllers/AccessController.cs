using System;
using System.Linq;
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

                    UserResponse oUserResponse = new UserResponse();
                    oUserResponse.AccessToken = AccessToken;
                    oUserResponse.Name = oUser.name;
                    oUserResponse.City = oUser.city;
                    oUserResponse.Id = oUser.id;

                    oR.result = 1;
                    oR.data = oUserResponse;
                }
                else
                {
                    oR.message = "Datos incorrectos";
                    //pepe@mail.com
                    //8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92
                    //8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92
                    
                }
            }

            return oR;
        }
    }
}
