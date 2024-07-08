namespace AgendaTenis.Cidades.WebApi.Middlewares;

public class AutenticacaoMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;

    public AutenticacaoMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _secretKey = configuration.GetValue<string>("Chave");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Chave", out var extractedSecretKey))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Chave secreta não foi informada");
            return;
        }

        if (!_secretKey.Equals(extractedSecretKey))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Cliente não autorizado.");
            return;
        }

        await _next(context);
    }
}
