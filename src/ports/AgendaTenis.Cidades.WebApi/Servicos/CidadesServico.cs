using AgendaTenis.Cache.Core;
using AgendaTenis.Cidades.WebApi.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System.Runtime;
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

    public async Task<IEnumerable<MunicipioDto>> Obter(string parteNome, int pagina, int itensPorPagina)
    {
        var skip = (pagina - 1) * itensPorPagina;

        string cacheKey = $"municipios";

        var municipios = await _cache.GetRecordAsync<List<MunicipioDto>>(cacheKey);

        if (municipios is null)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios";

                municipios = await httpClient.GetFromJsonAsync<List<MunicipioDto>>(url, new JsonSerializerOptions(JsonSerializerDefaults.Web));

                var tempoAbsolutoDeExpiracao = TimeSpan.FromDays(1);

                await _cache.SetRecordAsync(cacheKey, municipios);
            }
        }

        return municipios
           .Where(c => c.Nome.Contains(parteNome, StringComparison.OrdinalIgnoreCase))
           .Skip(skip)
           .Take(itensPorPagina);
    }
}
