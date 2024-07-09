using AgendaTenis.Cidades.WebApi.ConfiguracaoDeServicos;
using AgendaTenis.Partidas.WebApi.ConfiguracaoDeServicos;

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

        services.AdicionarConfiguracaoSwagger();


        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("Redis");
            options.InstanceName = "AgendaTenis.Cidades";
        });

        services.AdicionarCidadeServico(Configuration);
        services.AdicionarAutenticacaoJWT(Configuration);
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

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
