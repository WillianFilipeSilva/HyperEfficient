using HyperEfficient.Dtos.Categoria;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services
{
    public interface ICategoriaService
    {
        Task<MessageResponse> Insert(CategoriaInsertDto categoria);
        Task<MessageResponse> Update(Categoria categoria);
        Task<MessageResponse> Delete(int id);
        Task<CategoriaGetAllResponse> GetAll();
        Task<Categoria> GetById(int id);
    }
}