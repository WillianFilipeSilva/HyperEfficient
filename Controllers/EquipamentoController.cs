using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;

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
        public async Task<ActionResult<MessageResponse>> InsertEquipamento(EquipamentoInsertDTO equipamento)
        {
            return Ok(await _equipamentoService.Insert(equipamento));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateEquipamento(EquipamentoEntity equipamento)
        {
            return Ok(await _equipamentoService.Update(equipamento));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteEquipamento(int id)
        {
            return Ok(await _equipamentoService.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<EquipamentoGetAllResponse>> GetAllEquipamentos()
        {
            return Ok(await _equipamentoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoEntity>> GetEquipamentoById(int id)
        {
            return Ok(await _equipamentoService.GetById(id));
        }
    }
}
