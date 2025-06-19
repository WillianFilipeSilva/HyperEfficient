using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Relatorios;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _service;

        public RelatoriosController(IRelatorioService service)
        {
            _service = service;
        }

        [HttpGet("empresa")]
        public async Task<ActionResult<RelatorioEmpresaResponse>> Empresa([FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            return Ok(await _service.GetRelatorioEmpresa(dataInicio, dataFim));
        }

        [HttpGet("setor/{setorId}")]
        public async Task<ActionResult<RelatorioSetorResponse>> Setor(int setorId, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            return Ok(await _service.GetRelatorioSetor(setorId, dataInicio, dataFim));
        }

        [HttpGet("equipamento/{equipamentoId}")]
        public async Task<ActionResult<RelatorioEquipamentoResponse>> Equipamento(int equipamentoId, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            return Ok(await _service.GetRelatorioEquipamento(equipamentoId, dataInicio, dataFim));
        }
    }
}
