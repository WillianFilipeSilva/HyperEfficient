using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Services
{
    public class EquipamentoService : IEquipamentoService
    {
        private readonly IEquipamentoRepository _equipamentoRepository;
        private readonly IMapper _map;

        public EquipamentoService(IEquipamentoRepository equipamentoRepository, IMapper map)
        {
            _equipamentoRepository = equipamentoRepository;
            _map = map;
        }

        public async Task<MessageResponse> Insert(EquipamentoInsertDto dto)
        {
            if (await _equipamentoRepository.Insert(_map.Map<EquipamentoEntity>(dto)) <= 0)
            {
                throw new KeyNotFoundException($"Não foi possível cadastrar o equipamento {dto.Nome}!");
            }

            return new MessageResponse { Message = "Equipamento cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(EquipamentoEntity equipamento)
        {
            if (await _equipamentoRepository.Update(equipamento) <= 0)
            {
                throw new KeyNotFoundException($"Não foi possível editar o equipamento {equipamento.Nome}!");
            }

            return new MessageResponse { Message = "Equipamento editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            if (await _equipamentoRepository.Delete(id) <= 0)
            {
                throw new KeyNotFoundException("Não foi possível deletar o equipamento!");
            }

            return new MessageResponse { Message = "Equipamento deletado com sucesso!" };
        }

        public async Task<EquipamentoGetAllResponse> GetAll()
        {
            return new EquipamentoGetAllResponse { Data = await _equipamentoRepository.GetAll() ?? new List<EquipamentoEntity>() };
        }

        public async Task<GetPagedResponseBase<EquipamentoEntity>> GetPaged(int page, int pageSize)
        {
            IEnumerable<EquipamentoEntity> data = await _equipamentoRepository.GetPaged(page, pageSize) ?? new List<EquipamentoEntity>();
            IEnumerable<EquipamentoEntity> allData = await _equipamentoRepository.GetAll() ?? new List<EquipamentoEntity>();
            int totalItems = allData.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new GetPagedResponseBase<EquipamentoEntity>
            {
                Data = data,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }

        public async Task<EquipamentoEntity> GetById(int id)
        {
            return await _equipamentoRepository.GetById(id) ?? throw new KeyNotFoundException("Equipamento não encontrado!");
        }
    }
}