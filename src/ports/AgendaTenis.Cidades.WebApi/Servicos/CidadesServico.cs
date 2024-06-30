using AgendaTenis.Cidades.WebApi.DTOs;
using AgendaTenis.Cidades.WebApi.Utils;
using System.Text.Json;

namespace AgendaTenis.Cidades.WebApi.Servicos;

public class CidadesServico
{
    private IEnumerable<CidadeDto> _cidades;
    private object _lock = new object();

    public IEnumerable<CidadeDto> ObterCidadesPorPadrao(string parteNome, int page, int itemsPerPage)
    {
        var cidades = this.CarregarCidades();

        if (!string.IsNullOrEmpty(parteNome))
        {
            var parteNomeNormalizado = TextoUtils.Normalizar(parteNome);
            cidades = cidades.Where(c => TextoUtils.Normalizar(c.Nome).Contains(parteNomeNormalizado));
        }

        var cidadesPaginado = cidades.Skip((page - 1) * itemsPerPage).Take(itemsPerPage);

        return cidadesPaginado;
    }

    private IEnumerable<CidadeDto> CarregarCidades()
    {
        if (_cidades is null)
        {
            lock (_lock)
            {
                if (_cidades is null)
                {
                    var jsonData = File.ReadAllText("city.list.json");

                    _cidades = JsonSerializer.Deserialize<IEnumerable<CidadeDto>>(jsonData);
                }
            }
        }

        return _cidades;
    }
}
