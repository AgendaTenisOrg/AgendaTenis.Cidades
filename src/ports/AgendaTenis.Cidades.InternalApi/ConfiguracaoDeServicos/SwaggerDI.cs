using Microsoft.OpenApi.Models;

namespace AgendaTenis.Cidades.InternalApi.ConfiguracaoDeServicos;

public static class SwaggerDI
{
    public static void RegistrarSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });

            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Chave",
                Type = SecuritySchemeType.ApiKey,
                Description = "Chave secreta para autenticação"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        In = ParameterLocation.Header
                    },
                    new string[] {}
                }
            });
        });
    }
}
