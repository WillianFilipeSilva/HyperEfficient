using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Contracts.Service
{
    public interface IUsuarioService
    {
        Task<MessageResponse> Insert(UsuarioInsertDTO usuario);
        Task<MessageResponse> Update(UsuarioEntity usuario);
        Task<MessageResponse> Delete(int id);
        Task<UsuarioGetAllResponse> GetAll();
        Task<UsuarioEntity> GetById(int id);
    }
}
