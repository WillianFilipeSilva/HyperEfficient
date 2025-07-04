using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<ActionResult<MessageResponse>> InsertRegistro([FromBody] RegistroInsertDto registro)
        {
            return Ok(await _registroService.Insert(registro));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> UpdateRegistro([FromBody] RegistroEntity registro)
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
        public async Task<ActionResult<RegistroEntity>> GetRegistroById(int id)
        {
            return Ok(await _registroService.GetById(id));
        }

        [HttpGet("paged")]
        [Authorize]
        public async Task<ActionResult<GetPagedResponseBase<RegistroEntity>>> GetPagedRegistros([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await _registroService.GetPaged(page, pageSize));
        }
    }
}