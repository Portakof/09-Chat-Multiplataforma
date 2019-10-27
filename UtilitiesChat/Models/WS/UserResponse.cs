

namespace UtilitiesChat.Models.WS
{
    //Esta clase se usa para devolver los atributos despues de haberse confirmado el "Login"
    //Tambien es usado por "BaseController" metodo "VerifyToken" para verificar si el usuario que realiza la solicitud 
    //ya tiene su "AccessToken" y si tiene que coincida con el existente en la base datos

    //Tambien los usa "RoomController" 

    public class UserResponse
    {
        public string AccessToken { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Id { get; set; }
    }
}
