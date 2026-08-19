using System.Net.Http.Headers;
using System.Text;

namespace CursoApis.Middlewares
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        // Usuario y contraseña "hardcodeados" para el curso
        private const string DemoUsername = "platzi";
        private const string DemoPassword = "12345";

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                if (context.Request.Path.Value.Contains("swagger")
                    || context.Request.Path.Value.Contains("scalar")
                    || context.Request.Path.Value.Contains("openapi"))
                {
                    await _next(context);
                    return;
                }

                // Leer encabezado Authorization
                var authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);

                // Decodificar credenciales base64
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

                var username = credentials[0];
                var password = credentials[1];

                // Validar usuario/contraseña
                if (username == DemoUsername && password == DemoPassword)
                {
                    // Usuario autenticado → continuar pipeline
                    await _next(context);
                    return;
                }
            }
            catch
            {
                // Si ocurre error significa que no vino el header correctamente
            }

            // Si llega aquí → no autorizado
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Missing or invalid credentials.");
        }
    }

    public static class BasicAuthExtensions
    {
        public static IApplicationBuilder UseBasicAuth(this IApplicationBuilder app)
        {
            return app.UseMiddleware<BasicAuthMiddleware>();
        }
    }
}