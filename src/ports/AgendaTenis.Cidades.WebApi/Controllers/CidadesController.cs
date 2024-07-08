﻿using AgendaTenis.Cidades.WebApi.Servicos;
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

    [HttpGet("existe")]
    public async Task<IActionResult> VerificarSeCidadeExiste([FromServices] CidadesServico servico, int idCidade)
    {
        var existe = await servico.VerificarSeCidadeExiste(idCidade);

        return Ok(existe);
    }
}
