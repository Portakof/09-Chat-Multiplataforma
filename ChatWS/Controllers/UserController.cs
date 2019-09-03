using ChatWS.Models.ViewModel;
using ChatWS.Models.WS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatWS.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public Reply Get()
        {
            Reply oReply = new Reply();
            using(Models.ChatDBEntities db=new Models.ChatDBEntities())
            {
                List<UserViewModel> lst = (from d in db.user
                                           select new UserViewModel
                                           {
                                               Name = d.name,
                                               City = d.city
                                           }).ToList();

                oReply.result = 1;
                oReply.data = lst;
            }
            return oReply;
        }

       [HttpPost]
        public Reply Register([FromBody] Models.Request.User model)
        {
            Reply oReply = new Reply();

            try
            {            
            using (Models.ChatDBEntities db = new Models.ChatDBEntities())
            {
                Models.user oUser = new Models.user();
                oUser.email = model.Email;
                oUser.password = model.Password;
                oUser.name = model.Name;
                oUser.city = model.City;
                oUser.date_create = DateTime.Now;
                oUser.idState = 1;

                db.user.Add(oUser);
                db.SaveChanges();

                oReply.result = 1;
            }
            }
            catch (Exception)
            {
                oReply.result = 0;
                oReply.message = "Intenta de nuevo mas tarde";
            }
              
            return oReply;
        }



    }
}
