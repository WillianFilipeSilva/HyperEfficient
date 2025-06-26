using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Usuario;
using HyperEfficient.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers;

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
    [AllowAnonymous]
    public async Task<ActionResult<MessageResponse>> InsertUsuario([FromBody] UsuarioInsertDto usuario)
    {
        return Ok(await _usuarioService.Insert(usuario));
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<MessageResponse>> UpdateUsuario([FromBody] UsuarioEntity usuario)
    {
        return Ok(await _usuarioService.Update(usuario));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<MessageResponse>> DeleteUsuario(int id)
    {
        return Ok(await _usuarioService.Delete(id));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UsuarioGetAllResponse>> GetAllUsuarios()
    {
        return Ok(await _usuarioService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
    {
        return Ok(await _usuarioService.GetById(id));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UsuarioLoginTokenDto>> Login([FromBody] UsuarioLoginDto usuario)
    {
        return Ok(await _usuarioService.Login(usuario));
    }
}