using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.SetorDtos;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services
{
    public interface ISetorService
    {
        Task<MessageResponse> Insert(SetorInsertDto equipamento);
        Task<MessageResponse> Update(Setor equipamento);
        Task<MessageResponse> Delete(int id);
        Task<SetorGetAllResponse> GetAll();
        Task<GetPagedResponseBase<Setor>> GetPaged(int page, int pageSize);
        Task<Setor> GetById(int id);
    }
}