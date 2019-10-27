
using System.Security.Cryptography;
using System.Text;

namespace UtilitiesChat.Tools
{
    //La siguiente función nos sirve para realizar una encriptación en SHA256 en C# .Net/
    //Se encriptara la informacion ingresada por el usuario en el servicio web "ChatWeb"
    //de esta forma no seran visibles las contraseña
    //Recordar que tambien se encriptaran las contraseñas en la base datos "ChatDB"

    public class Encrypt
    {
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }
}
