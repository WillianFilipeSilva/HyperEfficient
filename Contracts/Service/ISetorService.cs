using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Contracts.Service
{
    public interface ISetorService
    {
        Task<MessageResponse> Insert(SetorInsertDTO equipamento);
        Task<MessageResponse> Update(SetorEntity equipamento);
        Task<MessageResponse> Delete(int id);
        Task<SetorGetAllResponse> GetAll();
        Task<SetorEntity> GetById(int id);
    }
}
