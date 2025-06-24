using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.DTOs.Usuario;
using HyperEfficient.Entity;

namespace HyperEfficient.Contracts.Service
{
    public interface IUsuarioService
    {
        Task<MessageResponse> Insert(UsuarioInsertDTO usuario);
        Task<MessageResponse> Update(UsuarioEntity usuario);
        Task<MessageResponse> Delete(int id);
        Task<UsuarioGetAllResponse> GetAll();
        Task<UsuarioEntity> GetById(int id);
        Task<UsuarioLoginTokenDTO> Login(UsuarioLoginDTO Usuario);

    }
}
