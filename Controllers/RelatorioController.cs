using HyperEfficient.Contracts.Service;
using HyperEfficient.Dtos.Relatorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers;

[ApiController]
[Route("relatorios")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("empresa")]
    [Authorize]
    public async Task<ActionResult<RelatorioEmpresaResponse>> Empresa([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
    {
        return Ok(await _relatorioService.GetRelatorioEmpresa(dataInicio, dataFim));
    }

    [HttpGet("setor/{setorId}")]
    [Authorize]
    public async Task<ActionResult<RelatorioSetorResponse>> Setor(int setorId, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
    {
        return Ok(await _relatorioService.GetRelatorioSetor(setorId, dataInicio, dataFim));
    }

    [HttpGet("equipamento/{equipamentoId}")]
    [Authorize]
    public async Task<ActionResult<RelatorioEquipamentoResponse>> Equipamento(int equipamentoId, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
    {
        return Ok(await _relatorioService.GetRelatorioEquipamento(equipamentoId, dataInicio, dataFim));
    }
}