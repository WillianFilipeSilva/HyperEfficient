using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Categoria;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;

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

        public async Task<MessageResponse> Insert(CategoriaInsertDto dto)
        {
            if (await _categoriaRepository.Insert(_map.Map<CategoriaEntity>(dto)) <= 0)
            {
                throw new KeyNotFoundException($"Não foi possível cadastrar a categoria {dto.Nome}!");
            }

            return new MessageResponse { Message = "Categoria cadastrada com sucesso!" };
        }

        public async Task<MessageResponse> Update(CategoriaEntity categoria)
        {
            if (await _categoriaRepository.Update(categoria) <= 0)
            {
                throw new KeyNotFoundException($"Não foi possível editar a categoria: {categoria.Nome}!");
            }

            return new MessageResponse { Message = "Categoria editada com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            if (await _categoriaRepository.Delete(id) <= 0)
            {
                throw new KeyNotFoundException("Não foi possível deletar a categoria!");
            }

            return new MessageResponse { Message = "Categoria deletada com sucesso!" };
        }

        public async Task<CategoriaGetAllResponse> GetAll()
        {
            return new CategoriaGetAllResponse { Data = await _categoriaRepository.GetAll() ?? new List<CategoriaEntity>() };
        }

        public async Task<CategoriaEntity> GetById(int id)
        {
            return await _categoriaRepository.GetById(id) ?? throw new KeyNotFoundException("Categoria não encontrada!");
        }
    }
}