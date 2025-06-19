using AutoMapper;
using HyperEfficient.Contracts.Repository;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTO;
using HyperEfficient.Entity;
using HyperEfficient.Response;
using HyperEfficient.Response.MessageResponse;

namespace HyperEfficient.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _map;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper map)
        {
            _usuarioRepository = usuarioRepository;
            _map = map;
        }

        public async Task<MessageResponse> Insert(UsuarioInsertDTO dto)
        {
            await _usuarioRepository.Insert(_map.Map<UsuarioEntity>(dto));
            return new MessageResponse {Message = "Usuário cadastrado com sucesso!" };
        }

        public async Task<MessageResponse> Update(UsuarioEntity usuario)
        {
            await _usuarioRepository.Update(usuario);
            return new MessageResponse { Message = "Usuário editado com sucesso!" };
        }

        public async Task<MessageResponse> Delete(int id)
        {
            await _usuarioRepository.Delete(id);
            return new MessageResponse { Message = "Usuário deletado com sucesso!" };
        }

        public async Task<UsuarioGetAllResponse> GetAll()
        {
            var usuarios = await _usuarioRepository.GetAll();
            return new UsuarioGetAllResponse { Data = usuarios };
        }

        public async Task<UsuarioEntity> GetById(int id)
        {
            return await _usuarioRepository.GetById(id);
        }
    }
}
