using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Categoria;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers;

[ApiController]
[Route("categorias")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<MessageResponse>> InsertCategoria([FromBody] CategoriaInsertDto categoria)
    {
        return Ok(await _categoriaService.Insert(categoria));
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<MessageResponse>> UpdateCategoria([FromBody] CategoriaEntity categoria)
    {
        return Ok(await _categoriaService.Update(categoria));
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<MessageResponse>> DeleteCategoria(int id)
    {
        return Ok(await _categoriaService.Delete(id));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<CategoriaGetAllResponse>> GetAllCategorias()
    {
        return Ok(await _categoriaService.GetAll());
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<CategoriaEntity>> GetCategoriaById(int id)
    {
        return Ok(await _categoriaService.GetById(id));
    }
}