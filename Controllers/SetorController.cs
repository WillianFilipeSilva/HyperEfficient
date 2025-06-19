using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("setores")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _service;

        public SetorController(ISetorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertSetor(SetorInsertDTO setor)
        {
            return Ok(await _service.Insert(setor));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateSetor(SetorEntity setor)
        {
            return Ok(await _service.Update(setor));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteSetor(int id)
        {
            return Ok(await _service.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<SetorGetAllResponse>> GetAllSetores()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SetorEntity>> GetSetorById(int id)
        {
            return Ok(await _service.GetById(id));
        }
    }
}
