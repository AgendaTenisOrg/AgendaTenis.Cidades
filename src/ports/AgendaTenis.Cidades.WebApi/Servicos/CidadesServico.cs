using AgendaTenis.Cache.Core;
using AgendaTenis.Cidades.WebApi.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace AgendaTenis.Cidades.WebApi.Servicos;

public class CidadesServico
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IDistributedCache _cache;

    public CidadesServico(
        IHttpClientFactory httpClientFactory, 
        IDistributedCache cache)
    {
        _httpClientFactory = httpClientFactory;
        _cache = cache;
    }

    public async Task<IEnumerable<CidadeDto>> Obter(string parteNome, int pagina, int itensPorPagina)
    {
        var cidades = await ObterCidades();

        return cidades
           .Where(c => c.Nome.Contains(parteNome, StringComparison.OrdinalIgnoreCase))
           .Skip((pagina - 1) * itensPorPagina)
           .Take(itensPorPagina);
    }

    public async Task<bool> VerificarSeCidadeExiste(int idCidade)
    {
        var cidades = await ObterCidades();

        return cidades.Any(c => c.Id == idCidade); 
    }

    private async Task<IEnumerable<CidadeDto>> ObterCidades()
    {
        string cacheKey = $"cidades";

        var cidades = await _cache.GetRecordAsync<List<CidadeDto>>(cacheKey);

        if (cidades is null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios";

                cidades = await httpClient.GetFromJsonAsync<List<CidadeDto>>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                await _cache.SetRecordAsync(cacheKey, cidades, TimeSpan.FromDays(1));
            }
        }

        return cidades;
    }
}
