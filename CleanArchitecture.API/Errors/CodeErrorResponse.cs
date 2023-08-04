namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);  //si el mensaje es nulo quiero que el quue se encrgjue de procesar e mensaje sea el getDfaultMessage
        }
        private string GetDefaultMessageStatusCode(int statusCode, string? message = null) {
            return statusCode switch
            {
                400 => "El request enviando tiene errores",
                401 => "No tienes autorizacion para este recurso",
                404 => "No se encontró el recurso solicitado",
                500 => "Se produjeron errores en el servidor",
                _ => string.Empty
            };
        }
    }
}
