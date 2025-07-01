using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services;

public interface IRegistroService
{
    Task<MessageResponse> Insert(RegistroInsertDto equipamento);
    Task<MessageResponse> Update(RegistroEntity equipamento);
    Task<MessageResponse> Delete(int id);
    Task<RegistroGetAllResponse> GetAll();
    Task<GetPagedResponseBase<RegistroEntity>> GetPaged(int page, int pageSize);
    Task<RegistroEntity> GetById(int id);
}