using AutoMapper;
using HyperEfficient.Contracts.Repository;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _map;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper map)
        {
            _categoriaRepository = categoriaRepository;
            _map = map;
        }

        public async Task<MessageResponse> Insert(CategoriaInsertDTO dto)
        {
            await _categoriaRepository.Insert(_map.Map<CategoriaEntity>(dto));
            return new MessageResponse { Message = "Categoria cadastrada com sucesso!" };
        }

        public async Task<MessageResponse> Update(CategoriaEntity categoria)
        {
            await _categoriaRepository.Update(categoria);
            return new MessageResponse { Message = "Categoria editada com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            await _categoriaRepository.Delete(id);
            return new MessageResponse { Message = "Categoria deletada com sucesso!" };
        }

        public async Task<CategoriaGetAllResponse> GetAll()
        {
            var categorias = await _categoriaRepository.GetAll();
            return new CategoriaGetAllResponse { Data = categorias };
        }

        public async Task<CategoriaEntity> GetById(int id)
        {
            return await _categoriaRepository.GetById(id);
        }
    }
}
