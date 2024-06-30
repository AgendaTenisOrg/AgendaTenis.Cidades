using AgendaTenis.Cidades.WebApi.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.Cidades.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CidadesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObterPorPadrao([FromServices] CidadesServico servico, string parteNome, int page, int itemsPerPage)
    {
        var cidades = servico.ObterCidadesPorPadrao(parteNome, page, itemsPerPage);

        return Ok(cidades);
    }
}
