

namespace UtilitiesChat.Models.WS
{
    //Esta clase se usa para recibir los datos necesarios para realizar la validacion del "login" y la base datos

    public class AccessRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
