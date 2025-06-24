using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.DTOs.Setor;
using HyperEfficient.Entities;

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
