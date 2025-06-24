using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Usuario;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertUsuario(UsuarioInsertDTO usuario)
        {
            return Ok(await _usuarioService.Insert(usuario));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateUsuario(UsuarioEntity usuario)
        {
            return Ok(await _usuarioService.Update(usuario));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteUsuario(int id)
        {
            return Ok(await _usuarioService.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioGetAllResponse>> GetAllsuarios()
        {
            return Ok(await _usuarioService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEntity>> GetUsuarioById(int id)
        {
            return Ok(await _usuarioService.GetById(id));
        }

        [HttpGet("login")]
        public async Task<ActionResult<UsuarioLoginTokenDTO>> Login(UsuarioLoginDTO user)
        {
            try
            {
                return Ok(await _usuarioService.Login(user));
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }
    }
}
