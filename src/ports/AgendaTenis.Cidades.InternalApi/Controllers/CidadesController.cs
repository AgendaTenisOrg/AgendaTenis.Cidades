using AgendaTenis.Cidades.Core.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.Cidades.InternalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CidadesController : ControllerBase
{
    [HttpGet("obter/{id}")]
    public async Task<IActionResult> ObterPorId([FromServices] CidadesServico servico, [FromRoute] int id)
    {
        var cidades = await servico.ObterPorId(id);

        return Ok(cidades);
    }
}
