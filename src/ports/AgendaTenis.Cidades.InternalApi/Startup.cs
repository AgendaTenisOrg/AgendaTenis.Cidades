using AgendaTenis.Cidades.Core.Servicos;
using AgendaTenis.Cidades.InternalApi.ConfiguracaoDeServicos;
using AgendaTenis.Cidades.InternalApi.Middlewares;

namespace AgendaTenis.Cidades.InternalApi;

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


        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("Redis");
            options.InstanceName = "AgendaTenis.Cidades";
        });

        services.AdicionarCidadeServico(Configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment() || env.EnvironmentName == "Container")
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
