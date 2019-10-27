using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using UtilitiesChat.Models.WS;

namespace UtilitiesChat
{
    public class RequestUtil    //S crea para realizar una solicitud tipo post, get, delete a cualquier servicio web
    {
        public Reply oReply { get; set; }   //se crea el objeto de tipo Reply

        public RequestUtil()    //se crea el constructor de la clase
        {
            oReply = new Reply();   //se inicializa el objeto tipo reply 
        }
        //Metodo que invoca, ejecuta o solicita a un servicio web sin importar el objeto que se envia
        //esto atravez de "Generis"
        //Un "Generis" es explicacion Execute<T>
        //<T> = es un alias de un tipo de objeto
        //string url = contiene la url a invocar http://localhost:51592/api/User
        //string method = recibe un metodo por el cual enviar la "url" en este caso "post"
        //T objectRequest = se recibe un objeto que se va a enviar serializado en json
        //El "Generis" permite realizar es proceso sin importar el tipo de objeto

        public Reply Execute<T>(string url, string method, T objectRequest)
        {
            oReply.result = 0;
            string result = "";

            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                string json = JsonConvert.SerializeObject(objectRequest);   //se combierte a tipo "json" el objeto recibido "objectRequest" para ser enviado al ChatWS ejemplo {"Name":"Hector","Password":"123456","Email":"mail@hdeleon.net","City":"Guadalajara"}

                WebRequest request = WebRequest.Create(url);    //este objeto realiza la solicitud de tipo web
                //estos cuatro metodos simulan las peticiones del Fiddler
                request.Method = method;    //coloca como tipo "post"
                request.PreAuthenticate = true;
                request.ContentType = "application/json;charset=utf-8"; //permite que el api o ChatWS espere un objeto tipo "json", charset=utf-8 tipo de codificacion a enviar por si se usan caractres raros como la " ñ "
                request.Timeout = 60000;    //tiempo maximo de una solicitud si se demora mas genera una Exception

                //se escribe en el body la solicitud
                using (var oStreamWriter = new StreamWriter(request.GetRequestStream()))    //se crea un flujo y se abre con "GetRequestStream"
                {
                    oStreamWriter.Write(json);  //Este metodo escribe el objeto que anteriormente fue combertido en tipo "json"
                    oStreamWriter.Flush();      //se libera el flujo creado
                }

                //Una vez se tipo prepara la solicitud crada con los pasos anteriores 
                //realizamos como tal la solicitud
                var oHttpResponse = (HttpWebResponse)request.GetResponse(); //esto hace como tal la solicitud
                using (var oStreamReader = new StreamReader(oHttpResponse.GetResponseStream()))
                {
                    result = oStreamReader.ReadToEnd(); //se almace en un string la respuesta recibida 
                }

                oReply = JsonConvert.DeserializeObject<Reply>(result);  //"result" contiene la respuesta tipo "json" con este metodo se convierte a otro tipo de objeto

            }
            catch (TimeoutException e)  //esta Exception se genera cuando "request.Timeout = 60000" se desborda
            {
                oReply.message = "Servidor sin respuesta";
            }
            catch (Exception e)
            {

                oReply.message = "Ocurrio un error UtilitiesChat en RequestUtil";
            }

            return oReply;
        }

    }
}
