using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChatWeb.Startup))]

namespace ChatWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Se configura para que se pueda trabajar el signalR
            app.MapSignalR();
        }
    }
}
