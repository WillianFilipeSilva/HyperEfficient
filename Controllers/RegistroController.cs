using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Registro;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("registros")]
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroService _registroService;

        public RegistroController(IRegistroService registroService)
        {
            _registroService = registroService;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertRegistro(RegistroInsertDTO registro)
        {
            return Ok(await _registroService.Insert(registro));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateRegistro(RegistroEntity registro)
        {
            return Ok(await _registroService.Update(registro));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteRegistro(int id)
        {
            return Ok(await _registroService.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<RegistroGetAllResponse>> GetAllRegistros()
        {
            return Ok(await _registroService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroEntity>> GetRegistroById(int id)
        {
            return Ok(await _registroService.GetById(id));
        }
    }
}
