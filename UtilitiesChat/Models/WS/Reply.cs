
namespace UtilitiesChat.Models.WS
{
    public class Reply  //Este objeto se crea para devolver los tres datos o aspectos importantes para el cliente.
    {
        public string message { get; set; }     //cuando haya un error o algo
        public int result { get; set; }         //especificar si es el resultado correcto
        public object data { get; set; }        //aqui ira la respuesta o datos que se envian o reciben
                                                //se puede enviar cualquier cosa
    }
}
