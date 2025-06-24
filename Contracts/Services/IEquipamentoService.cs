using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Service
{
    public interface IEquipamentoService
    {
        Task<MessageResponse> Insert(EquipamentoInsertDTO equipamento);
        Task<MessageResponse> Update(EquipamentoEntity equipamento);
        Task<MessageResponse> Delete(int id);
        Task<EquipamentoGetAllResponse> GetAll();
        Task<EquipamentoEntity> GetById(int id);
    }
}
