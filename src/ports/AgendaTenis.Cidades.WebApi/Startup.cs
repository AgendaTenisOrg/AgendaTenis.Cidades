using AgendaTenis.Cidades.WebApi.ConfiguracaoDeServicos;
using AgendaTenis.Cidades.WebApi.Middlewares;
using AgendaTenis.Cidades.WebApi.Servicos;
using Microsoft.OpenApi.Models;

namespace AgendaTenis.Cidades.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.RegistrarSwagger();

        services.AddScoped<CidadesServico>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("Redis");
            options.InstanceName = "AgendaTenis.Cidades";
        });

        services.AdicionarCidadeServico(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseMiddleware<AutenticacaoMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
