using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Livraria2.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            var codigo = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(new
            {
                Codigo = codigo,
                Tipo = ex.GetType().Name,
                Erro = ex.Message,
                Detalhes = ex.StackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) codigo;
            return context.Response.WriteAsync(result);
        }
    }
}
