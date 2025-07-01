using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Setor;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services;

public interface ISetorService
{
    Task<MessageResponse> Insert(SetorInsertDto equipamento);
    Task<MessageResponse> Update(SetorEntity equipamento);
    Task<MessageResponse> Delete(int id);
    Task<SetorGetAllResponse> GetAll();
    Task<GetPagedResponseBase<SetorEntity>> GetPaged(int page, int pageSize);
    Task<SetorEntity> GetById(int id);
}