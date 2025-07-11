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
            if (await _equipamentoRepository.Insert(_map.Map<Equipamento>(dto)) <= 0)
                throw new KeyNotFoundException($"Não foi possível cadastrar o equipamento {dto.Nome}!");

            return new MessageResponse { Message = "Equipamento cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(Equipamento equipamento)
        {
            if (await _equipamentoRepository.Update(equipamento) <= 0)
                throw new KeyNotFoundException($"Não foi possível editar o equipamento {equipamento.Nome}!");

            return new MessageResponse { Message = "Equipamento editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            if (await _equipamentoRepository.Delete(id) <= 0)
                throw new KeyNotFoundException("Não foi possível deletar o equipamento!");

            return new MessageResponse { Message = "Equipamento deletado com sucesso!" };
        }

        public async Task<EquipamentoGetAllResponse> GetAll()
        {
            return new EquipamentoGetAllResponse
            {
                Data = await _equipamentoRepository.GetAll() ?? new List<Equipamento>()
            };
        }

        public async Task<Equipamento> GetById(int id)
        {
            return await _equipamentoRepository.GetById(id) ??
                   throw new KeyNotFoundException("Equipamento não encontrado!");
        }

        public async Task<GetPagedResponseBase<EquipamentoDto>> GetPaged(int page, int pageSize)
        {
            var data = await _equipamentoRepository.GetEquipamentoProjection(page, pageSize) ??
                       new List<EquipamentoDto>();

            var allData =
                await _equipamentoRepository.GetAll() ?? new List<Equipamento>();

            var totalItems = allData.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return new GetPagedResponseBase<EquipamentoDto>
            {
                Data = data,
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalItems
            };
        }
    }
}