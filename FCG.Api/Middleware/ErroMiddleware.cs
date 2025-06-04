using System.Net;
using System.Text.Json;

namespace FCG.Api.Middleware
{
    public class ErroMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErroMiddleware> _logger;

        public ErroMiddleware(RequestDelegate next, ILogger<ErroMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new { Erro = "Ocorreu um erro inesperado. Tente novamente mais tarde." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
