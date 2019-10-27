

namespace UtilitiesChat.Models.WS
{
    //Esta clase es para crear y manejar la estructura que tendran los Room o salas
    //atraves de esta clase se recibira y se enviara la estructura
    public class ListRoomsResponse
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
