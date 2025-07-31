using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;
using HyperEfficient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("registros")]
    public class RegistroController : ControllerBase
    {
        private readonly RegistroService _registroService;

        public RegistroController(RegistroService registroService)
        {
            _registroService = registroService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> InsertRegistro([FromBody] RegistroInsertDto registro)
        {
            return Ok(await _registroService.Insert(registro));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> UpdateRegistro([FromBody] Registro registro)
        {
            return Ok(await _registroService.Update(registro));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> DeleteRegistro(int id)
        {
            return Ok(await _registroService.Delete(id));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<RegistroGetAllResponse>> GetAllRegistros()
        {
            return Ok(await _registroService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Registro>> GetRegistroById(int id)
        {
            return Ok(await _registroService.GetById(id));
        }

        [HttpGet("paged")]
        [Authorize]
        public async Task<ActionResult<GetPagedResponseBase<Registro>>> GetPagedRegistros([FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            return Ok(await _registroService.GetPaged(page, pageSize));
        }

        [HttpPost("registrar/{equipamentoId}")]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> StartStopRegistro(int equipamentoId)
        {
            return Ok(await _registroService.StartStopRegistro(equipamentoId));
        }

        [HttpGet("{equipamentoId}/status")]
        public async Task<ActionResult<EquipamentoStatusDto>> Status(int equipamentoId)
        {
            return Ok(await _registroService.ObterConsumoAsync(equipamentoId));
        }

        [HttpPost("{equipamentoId}/ligar")]
        public async Task<IActionResult> Ligar(int equipamentoId)
        {
            await _registroService.LigarAsync(equipamentoId);
            return Ok(new { success = true });
        }

        [HttpPost("{equipamentoId}/desligar")]
        public async Task<IActionResult> Desligar(int equipamentoId)
        {
            await _registroService.DesligarAsync(equipamentoId);
            return Ok(new { success = true });
        }
    }
}