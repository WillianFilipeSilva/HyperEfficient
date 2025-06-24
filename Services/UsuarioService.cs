using AutoMapper;
using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Service;
using HyperEfficient.DTOs.MessageResponse;
using HyperEfficient.DTOs.Usuario;
using HyperEfficient.Entity;
using static HyperEfficient.Infrastructure.Criptografia.Criptografia;

namespace HyperEfficient.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _map;
        private readonly IAutentication _autentication;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper map, IAutentication autentication)
        {
            _usuarioRepository = usuarioRepository;
            _map = map;
            _autentication = autentication;
        }

        public async Task<MessageResponse> Insert(UsuarioInsertDTO dto)
        {
            dto.Senha = GeneratePBKDF2Hash(dto.Senha);
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

        public async Task<UsuarioLoginTokenDTO> Login(UsuarioLoginDTO usuarioLoginDto)
        {
            var usuario = await _usuarioRepository.GetByEmail(usuarioLoginDto.Email);

            usuarioLoginDto.Senha = GeneratePBKDF2Hash(usuarioLoginDto.Senha);
            string token = _autentication.GenerateToken(usuario);

            return new UsuarioLoginTokenDTO()
            {
                Token = token,
                Usuario = usuario
            };
        }
    }
}
