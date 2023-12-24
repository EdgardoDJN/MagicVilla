using System.Net;

namespace MagicVilla_API.Modelos
{
    public class APIResponse
    {
        //Se va a encargar de todas las respuestas que se van a enviar a los endpoints
        //Con el fin de que sean similares
        public HttpStatusCode statusCode { get; set; }

        //A las propiedades booleanas siempre se le coloca Is adelante
        //Va a ser true por defecto
        public bool IsExitoso { get; set; } = true;

        //Si es que nuestros endpoints tienen algùn error
        public List<string> ErrorMessages { get; set; }

        public object Resultado { get; set; }
    }
}
