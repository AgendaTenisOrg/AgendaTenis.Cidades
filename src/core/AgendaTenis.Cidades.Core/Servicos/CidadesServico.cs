﻿using AgendaTenis.Cache.Core;
using AgendaTenis.Cidades.Core.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Json;
using System.Text.Json;

namespace AgendaTenis.Cidades.Core.Servicos;

public class CidadesServico
{
    private readonly HttpClient _httpClient;
    private readonly IDistributedCache _cache;

    public CidadesServico(HttpClient httpClient, IDistributedCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<IEnumerable<CidadeDto>> Obter(int pagina, int itensPorPagina, string parteNome)
    {
        var cidades = await ObterCidades();

        if (!string.IsNullOrEmpty(parteNome))
        {
            cidades = cidades.Where(c => c.Nome.Contains(parteNome, StringComparison.OrdinalIgnoreCase));
        }

        return cidades
           .Skip((pagina - 1) * itensPorPagina)
           .Take(itensPorPagina);
    }

    public async Task<CidadeDto> ObterPorId(int id)
    {
        // Pode parecer muito custoso obter todas as cidades para depois filtrar por Id, mas na verdade o método ObterCidades é otimizado para obter as cidades uma única vez e armazenar em cache.
        // Com isso, essa operação tem boa performance.
        var cidades = await ObterCidades();

        return cidades.FirstOrDefault(c => c.Id == id);
    }

    private async Task<IEnumerable<CidadeDto>> ObterCidades()
    {
        string cacheKey = $"cidades";

        var cidades = await _cache.GetRecordAsync<List<CidadeDto>>(cacheKey, lancarException: false);

        if (cidades is null)
        {
            cidades = await _httpClient.GetFromJsonAsync<List<CidadeDto>>($"municipios", new JsonSerializerOptions(JsonSerializerDefaults.Web));

            await _cache.SetRecordAsync(cacheKey, cidades, TimeSpan.FromDays(1), lancarException: false);
        }

        return cidades;
    }
}
