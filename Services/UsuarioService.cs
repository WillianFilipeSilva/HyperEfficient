using AutoMapper;
using HyperEfficient.Contracts.Infrastructure;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Usuario;
using HyperEfficient.Entities;
using HyperEfficient.Infrastructure.Middleware;
using static HyperEfficient.Infrastructure.Criptography.Criptography;

namespace HyperEfficient.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IAutentication _autentication;
    private readonly IMapper _map;
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper map, IAutentication autentication)
    {
        _usuarioRepository = usuarioRepository;
        _map = map;
        _autentication = autentication;
    }

    public async Task<MessageResponse> Insert(UsuarioInsertDto dto)
    {
        dto.Senha = GeneratePbkdf2Hash(dto.Senha);
        if (await _usuarioRepository.Insert(_map.Map<UsuarioEntity>(dto)) <= 0)
            throw new KeyNotFoundException($"Não foi possível cadastrar o usuário {dto.Nome}!");

        return new MessageResponse { Message = "Usuário cadastrado com sucesso!" };
    }

    public async Task<MessageResponse> Update(UsuarioEntity usuario)
    {
        if (await _usuarioRepository.Update(usuario) <= 0)
            throw new KeyNotFoundException($"Não foi possível editar o usuário: {usuario.Nome}!");

        return new MessageResponse { Message = "Usuário editado com sucesso!" };
    }

    public async Task<MessageResponse> Delete(int id)
    {
        if (await _usuarioRepository.Delete(id) <= 0)
            throw new KeyNotFoundException($"Não foi encontrado nenhum Usuário com o id: {id}");

        return new MessageResponse { Message = "Usuário deletado com sucesso!" };
    }

    public async Task<UsuarioGetAllResponse> GetAll()
    {
        return new UsuarioGetAllResponse { Data = await _usuarioRepository.GetAll() ?? new List<UsuarioEntity>() };
    }

    public async Task<UsuarioDto> GetById(int id)
    {
        return _map.Map<UsuarioDto?>(_usuarioRepository.GetById(id))
            ?? throw new KeyNotFoundException("Usuário não encontrado!");
    }

    public async Task<UsuarioLoginTokenDto> Login(UsuarioLoginDto usuarioLoginDto)
    {
        var usuario = await _usuarioRepository.GetByEmail(usuarioLoginDto.Email)
                      ?? throw new InvalidCredentialsException("Usuário ou senha inválidos!");

        if (!VerifyPbkdf2Hash(usuarioLoginDto.Senha, usuario.Senha))
            throw new InvalidCredentialsException("Usuário ou senha inválidos!");

        var token = _autentication.GenerateToken(usuario);

        return new UsuarioLoginTokenDto
        {
            Token = token,
            Usuario = _map.Map<UsuarioDto>(usuario)
        };
    }
}
