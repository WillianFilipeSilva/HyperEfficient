using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("equipamentos")]
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;

        public EquipamentoController(IEquipamentoService equipamentoService)
        {
            _equipamentoService = equipamentoService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> InsertEquipamento([FromBody] EquipamentoInsertDto equipamento)
        {
            return Ok(await _equipamentoService.Insert(equipamento));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> UpdateEquipamento([FromBody] Equipamento equipamento)
        {
            return Ok(await _equipamentoService.Update(equipamento));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> DeleteEquipamento(int id)
        {
            return Ok(await _equipamentoService.Delete(id));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<EquipamentoGetAllResponse>> GetAllEquipamentos()
        {
            return Ok(await _equipamentoService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Equipamento>> GetEquipamentoById(int id)
        {
            return Ok(await _equipamentoService.GetById(id));
        }

        [HttpGet("paged")]
        [Authorize]
        public async Task<ActionResult<GetPagedResponseBase<EquipamentoDto>>> GetPagedEquipamentos(
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10
        )
        {
            return Ok(await _equipamentoService.GetPaged(page, pageSize));
        }
    }
}