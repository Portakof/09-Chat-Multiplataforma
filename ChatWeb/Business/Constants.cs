using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace ChatWeb.Business
{
    public class Constants
    {
        public static string URL_API
        {
            get
            {
                return ConfigurationManager.AppSettings["url_ws"];
            }
        }

        public class Url
        {
            public static string REGISTER
            {
                get
                {
                    return URL_API + "/api/User/";
                }
            }
        }
    }
}