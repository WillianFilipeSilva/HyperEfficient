using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.Entity;
using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.DTOs.MessageResponse;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("equipamentos")]
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService _service;

        public EquipamentoController(IEquipamentoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertEquipamento(EquipamentoInsertDTO equipamento)
        {
            return Ok(await _service.Insert(equipamento));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateEquipamento(EquipamentoEntity equipamento)
        {
            return Ok(await _service.Update(equipamento));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteEquipamento(int id)
        {
            return Ok(await _service.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<EquipamentoGetAllResponse>> GetAllEquipamentos()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipamentoEntity>> GetEquipamentoById(int id)
        {
            return Ok(await _service.GetById(id));
        }
    }
}
