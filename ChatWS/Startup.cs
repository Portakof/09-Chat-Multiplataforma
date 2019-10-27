
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(ChatWS.Startup))]

namespace ChatWS
{
    public class Startup
    {
        //Se realiza la configuracion del signalR para que permita solicitudes de cualquier "IP"
        //Esto solo se configura en "ChatWS"
        public void Configuration(IAppBuilder app)
        {
            //"/signalr" se crea un java script de momento, cuando se jecuta crea un metodo llamado "enterUser()"
            app.Map("/signalr", map =>      
            {
                map.UseCors(CorsOptions.AllowAll);  //"AllowAll" permite todas las solicitudes, si se desea colocar una "IP" para que solo reciba informacion de la misma 
                var hubConfiguration = new HubConfiguration { };
                map.RunSignalR(hubConfiguration);
            });
        
        }
    }
}
