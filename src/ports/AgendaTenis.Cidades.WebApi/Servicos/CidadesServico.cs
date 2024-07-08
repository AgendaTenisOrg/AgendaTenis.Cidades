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
        var skip = (pagina - 1) * itensPorPagina;

        string cacheKey = $"cidades";

        var cidades = await _cache.GetRecordAsync<List<CidadeDto>>(cacheKey);

        if (cidades is null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios";

                cidades = await httpClient.GetFromJsonAsync<List<CidadeDto>>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                var tempoAbsolutoDeExpiracao = TimeSpan.FromDays(1);

                await _cache.SetRecordAsync(cacheKey, cidades);
            }
        }

        return cidades
           .Where(c => c.Nome.Contains(parteNome, StringComparison.OrdinalIgnoreCase))
           .Skip(skip)
           .Take(itensPorPagina);
    }
}
