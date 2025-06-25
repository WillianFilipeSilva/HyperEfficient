using HyperEfficient.Dtos.Categoria;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services;

public interface ICategoriaService
{
    Task<MessageResponse> Insert(CategoriaInsertDto categoria);
    Task<MessageResponse> Update(CategoriaEntity categoria);
    Task<MessageResponse> Delete(int id);
    Task<CategoriaGetAllResponse> GetAll();
    Task<CategoriaEntity> GetById(int id);
}