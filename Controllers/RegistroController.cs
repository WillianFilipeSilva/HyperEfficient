using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("registros")]
    public class RegistroController : ControllerBase
    {
        private readonly IRegistroService _service;

        public RegistroController(IRegistroService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertRegistro(RegistroInsertDTO registro)
        {
            return Ok(await _service.Insert(registro));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateRegistro(RegistroEntity registro)
        {
            return Ok(await _service.Update(registro));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteRegistro(int id)
        {
            return Ok(await _service.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<RegistroGetAllResponse>> GetAllRegistros()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroEntity>> GetRegistroById(int id)
        {
            return Ok(await _service.GetById(id));
        }
    }
}
