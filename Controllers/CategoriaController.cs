using HyperEfficient.Contracts.Services;
using HyperEfficient.DTOs.Categoria;
using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HyperEfficient.Controllers
{
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
        public async Task<ActionResult<MessageResponse>> InsertCategoria(CategoriaInsertDTO categoria)
        {
            return Ok(await _categoriaService.Insert(categoria));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateCategoria(CategoriaEntity nome)
        {
            return Ok(await _categoriaService.Update(nome));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteCategoria(int id)
        {
            return Ok(await _categoriaService.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<EquipamentoGetAllResponse>> GetAllCategorias()
        {
            return Ok(await _categoriaService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEntity>> GetCategoriaById(int id)
        {
            return Ok(await _categoriaService.GetById(id));
        }
    }
}
