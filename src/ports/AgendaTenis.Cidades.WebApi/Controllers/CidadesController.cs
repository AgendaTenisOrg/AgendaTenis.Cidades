using AgendaTenis.Cidades.Core.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.Cidades.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CidadesController : ControllerBase
{
    [HttpGet("obter")]
    [Authorize]
    public async Task<IActionResult> ObterCidadesPaginado([FromServices] CidadesServico servico, int page, int itemsPerPage, string parteNome = "")
    {
        var cidades = await servico.Obter(page, itemsPerPage, parteNome);

        return Ok(cidades);
    }
}
