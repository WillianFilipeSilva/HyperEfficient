using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Setor;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("setores")]
    public class SetorController : ControllerBase
    {
        private readonly ISetorService _setorService;

        public SetorController(ISetorService setorService)
        {
            _setorService = setorService;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertSetor(SetorInsertDTO setor)
        {
            return Ok(await _setorService.Insert(setor));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateSetor(SetorEntity setor)
        {
            return Ok(await _setorService.Update(setor));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteSetor(int id)
        {
            return Ok(await _setorService.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<SetorGetAllResponse>> GetAllSetores()
        {
            return Ok(await _setorService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SetorEntity>> GetSetorById(int id)
        {
            return Ok(await _setorService.GetById(id));
        }
    }
}
