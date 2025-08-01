using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services
{
    public interface IEquipamentoService
    {
        Task<MessageResponse> Insert(EquipamentoInsertDto equipamento);
        Task<MessageResponse> Update(Equipamento equipamento);
        Task<MessageResponse> Delete(int id);
        Task<EquipamentoGetAllResponse> GetAll();
        Task<GetPagedResponseBase<EquipamentoDto>> GetPaged(int page, int pageSize);
        Task<Equipamento> GetById(int id);
        Task AtualizarConsumoEquipamentos();
        Task AtualizarConsumoEquipamento(int equipamentoId);
        Task AtualizarConsumoEquipamento(Equipamento equipamento);
    }
}