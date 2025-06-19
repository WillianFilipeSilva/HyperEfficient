using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Contracts.Service
{
    public interface IRegistroService
    {
        Task<MessageResponse> Insert(RegistroInsertDTO equipamento);
        Task<MessageResponse> Update(RegistroEntity equipamento);
        Task<MessageResponse> Delete(int id);
        Task<RegistroGetAllResponse> GetAll();
        Task<RegistroEntity> GetById(int id);
    }
}
