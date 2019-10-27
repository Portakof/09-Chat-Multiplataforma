

namespace UtilitiesChat.Models.WS
{
    //Esta clase se crea para que todos los metodos que necesiten seguridad puedan acceder a el y asi mantener la unificar el nombre
    //los metodos heredaran de esta clase
    public class SecurityRequest
    {
        public string AccessToken { get; set; }
    }
}
