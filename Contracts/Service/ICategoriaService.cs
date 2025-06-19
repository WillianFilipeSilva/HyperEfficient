using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Contracts.Service
{
    public interface ICategoriaService
    {
        Task<MessageResponse> Insert(CategoriaInsertDTO categoria);
        Task<MessageResponse> Update(CategoriaEntity categoria);
        Task<MessageResponse> Delete(int id);
        Task<CategoriaGetAllResponse> GetAll();
        Task<CategoriaEntity> GetById(int id);
    }
}
