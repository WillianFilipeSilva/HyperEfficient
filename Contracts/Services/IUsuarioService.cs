using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Usuario;
using HyperEfficient.Entities;

namespace HyperEfficient.Contracts.Services
{
    public interface IUsuarioService
    {
        Task<MessageResponse> Insert(UsuarioInsertDto usuario);
        Task<MessageResponse> Update(UsuarioEntity usuario);
        Task<MessageResponse> Delete(int id);
        Task<UsuarioGetAllResponse> GetAll();
        Task<GetPagedResponseBase<UsuarioDto>> GetPaged(int page, int pageSize);
        Task<UsuarioDto?> GetById(int id);
        Task<UsuarioLoginTokenDto> Login(UsuarioLoginDto Usuario);
    }
}