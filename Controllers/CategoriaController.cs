using Microsoft.AspNetCore.Mvc;
using HyperEfficient.Contracts.Service;
using HyperEfficient.Entity;
using HyperEfficient.DTOs.Categoria;
using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.DTOs.MessageResponse;

namespace HyperEfficient.Controllers
{
    [ApiController]
    [Route("categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;

        public CategoriaController(ICategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<MessageResponse>> InsertCategoria(CategoriaInsertDTO categoria)
        {
            return Ok(await _service.Insert(categoria));
        }

        [HttpPut]
        public async Task<ActionResult<MessageResponse>> UpdateCategoria(CategoriaEntity nome)
        {
            return Ok(await _service.Update(nome));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MessageResponse>> DeleteCategoria(int id)
        {
            return Ok(await _service.Delete(id));
        }

        [HttpGet]
        public async Task<ActionResult<EquipamentoGetAllResponse>> GetAllCategorias()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaEntity>> GetCategoriaById(int id)
        {
            return Ok(await _service.GetById(id));
        }
    }
}
