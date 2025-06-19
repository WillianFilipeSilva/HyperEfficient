using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertUsuario(UsuarioInsertDTO usuario)
        {
            return Ok(await _service.Insert(usuario));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateUsuario(UsuarioEntity usuario)
        {
            return Ok(await _service.Update(usuario));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteUsuario(int id)
        {
            return Ok(await _service.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioGetAllResponse>> GetAllsuarios()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEntity>> GetUsuarioById(int id)
        {
            return Ok(await _service.GetById(id));
        }
    }
}
