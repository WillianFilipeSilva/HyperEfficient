using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

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
