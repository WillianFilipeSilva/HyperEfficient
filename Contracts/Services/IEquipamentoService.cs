using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services
{
    public interface IEquipamentoService
    {
        Task<MessageResponse> Insert(EquipamentoInsertDto equipamento);
        Task<MessageResponse> Update(EquipamentoEntity equipamento);
        Task<MessageResponse> Delete(int id);
        Task<EquipamentoGetAllResponse> GetAll();
        Task<GetPagedResponseBase<EquipamentoEntity>> GetPaged(int page, int pageSize);
        Task<EquipamentoEntity> GetById(int id);
    }
}