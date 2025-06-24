using AutoMapper;
using HyperEfficient.Contracts.Repository;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.DTOs.MessageResponse;
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

        public async Task<MessageResponse> Insert(EquipamentoInsertDTO dto)
        {
            await _equipamentoRepository.Insert(_map.Map<EquipamentoEntity>(dto));
            return new MessageResponse { Message = "Equipamento cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(EquipamentoEntity equipamento)
        {
            await _equipamentoRepository.Update(equipamento);
            return new MessageResponse { Message = "Equipamento editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            await _equipamentoRepository.Delete(id);
            return new MessageResponse { Message = "Equipamento deletado com sucesso!" };
        }

        public async Task<EquipamentoGetAllResponse> GetAll()
        {
            var equipamentos = await _equipamentoRepository.GetAll();
            return new EquipamentoGetAllResponse { Data = equipamentos };
        }

        public async Task<EquipamentoEntity> GetById(int id)
        {
            return await _equipamentoRepository.GetById(id);
        }
    }
}
