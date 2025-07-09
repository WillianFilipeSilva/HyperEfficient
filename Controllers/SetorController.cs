using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.SetorDtos;
using HyperEfficient.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize]
        public async Task<ActionResult<MessageResponse>> InsertSetor([FromBody] SetorInsertDto setor)
        {
            return Ok(await _setorService.Insert(setor));
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> UpdateSetor([FromBody] Setor setor)
        {
            return Ok(await _setorService.Update(setor));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<MessageResponse>> DeleteSetor(int id)
        {
            return Ok(await _setorService.Delete(id));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<SetorGetAllResponse>> GetAllSetores()
        {
            return Ok(await _setorService.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Setor>> GetSetorById(int id)
        {
            return Ok(await _setorService.GetById(id));
        }

        [HttpGet("paged")]
        [Authorize]
        public async Task<ActionResult<GetPagedResponseBase<Setor>>> GetPagedSetores([FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            return Ok(await _setorService.GetPaged(page, pageSize));
        }
    }
}