using System.Security.Claims;
using System.Text;

namespace Aerolineas.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        // solo para la validacion de ticket
        // Solo procesar si hay header X-API-KEY
        if (context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey))
        {
            var configuredApiKey = Environment.GetEnvironmentVariable("ApiKey") ?? throw new InvalidOperationException("No hay ApiKey en env");

            if (!string.IsNullOrEmpty(configuredApiKey) && configuredApiKey.Equals(apiKey))
            {
                // Crear claims para el usuario API
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "ApiClient"),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim("AuthType", "ApiKey")
                };

                var identity = new ClaimsIdentity(claims, "ApiKey");
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
            }
        }

        await _next(context);
    }

}