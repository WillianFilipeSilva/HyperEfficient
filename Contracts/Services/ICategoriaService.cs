using HyperEfficient.DTOs.Categoria;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services
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
