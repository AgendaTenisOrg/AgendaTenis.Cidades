using AgendaTenis.Cidades.WebApi.Servicos;

namespace AgendaTenis.Cidades.WebApi.ConfiguracaoDeServicos;

public static class CidadeServicoDI
{
    public static void AdicionarCidadeServico(this IServiceCollection services, IConfiguration configuration)
    {
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
