using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; //un pipeliene que se ejecutara para pasar  ala sigyuiente funcion si no hay error
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            this.env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);//procese el request que me esta envuiando el cliente
            } catch (Exception ex) {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                /*  context.Response.StatusCode */
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        //esto hace que devuelva toda la lista de erores e validacion una coleecion y queremos convwertirlo en formato json
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
                        break;

                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }


                //ahora validar si es que result esta vacio
                if (string.IsNullOrEmpty(result)) {
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace))  //que vuelva a instanciar al objeto codeErrorExcep
                }

                //var response = this.env.IsDevelopment()
                //    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                //    : new CodeErrorException((int)HttpStatusCode.InternalServerError);
                //    //respeusta del serviodr depende si se encuentra en desarrollo o produccion

                //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };  // formato en el cual queremos que se escriba el texto
                //var json = JsonSerializer.Serialize(response, options);//mensaje enviado al cliente   

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);//esto es lo que ya se envia como tal al cliente
            }
        }
    }
}
