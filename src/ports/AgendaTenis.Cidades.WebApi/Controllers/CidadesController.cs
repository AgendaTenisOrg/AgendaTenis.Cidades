using AgendaTenis.Cidades.WebApi.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.Cidades.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CidadesController : ControllerBase
{
    [HttpGet("obter")]
    public async Task<IActionResult> ObterCidadesPaginado([FromServices] CidadesServico servico, string parteNome, int page, int itemsPerPage)
    {
        var cidades = await servico.Obter(parteNome, page, itemsPerPage);

        return Ok(cidades);
    }

    [HttpGet("obter/{id}")]
    public async Task<IActionResult> ObterPorId([FromServices] CidadesServico servico, [FromRoute] int id)
    {
        var cidades = await servico.ObterPorId(id);

        return Ok(cidades);
    }
}
