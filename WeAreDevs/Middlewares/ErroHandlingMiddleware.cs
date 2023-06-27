using Newtonsoft.Json;
using System.Net;
using WeAreDevs.Exceptions;
using WeAreDevs.Models;

namespace WeAreDevs.Middlewares
{
    public class ErroHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErroHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await GerenciarExcecoes(context, ex);
            }
        }

        private static Task GerenciarExcecoes(HttpContext context, Exception ex) 
        {
            var codigo = HttpStatusCode.InternalServerError;

            var mensagem = new Erro(ex);
            switch (ex)
            {
                case BadRequestException:
                    codigo = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedException:
                    codigo = HttpStatusCode.Unauthorized;
                    break;
                case NotFoundException:
                    codigo = HttpStatusCode.NotFound;
                    break;
            }

            var retorno = JsonConvert.SerializeObject(mensagem);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)codigo;
            return context.Response.WriteAsync(retorno);
        }
    }
}