using AgendaTenis.Cidades.Core.Servicos;

namespace AgendaTenis.Cidades.InternalApi.ConfiguracaoDeServicos;

public static class CidadeServicoDI
{
    public static void AdicionarCidadeServico(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CidadesServico>();

        services.AddHttpClient<CidadesServico>(
            client =>
            {
                var config = configuration.GetSection("Servicos:LocalidadesIBGE").Get<LocalidadesIBGEConfiguracao>();

                client.BaseAddress = new Uri(config.Url);
            });
    }
}

public class LocalidadesIBGEConfiguracao
{
    public string Url { get; set; }
}
