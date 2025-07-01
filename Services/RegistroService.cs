using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;

namespace HyperEfficient.Services;

public class RegistroService : IRegistroService
{
    private readonly IMapper _map;
    private readonly IRegistroRepository _registroRepository;

    public RegistroService(IRegistroRepository registroRepository, IMapper map)
    {
        _registroRepository = registroRepository;
        _map = map;
    }

    public async Task<MessageResponse> Insert(RegistroInsertDto dto)
    {
        if (await _registroRepository.Insert(_map.Map<RegistroEntity>(dto)) <= 0)
            throw new KeyNotFoundException($"Não foi possível cadastrar o registro!");

        return new MessageResponse { Message = "Registro cadastrado com sucesso!" };
    }

    public async Task<MessageResponse> Update(RegistroEntity registro)
    {
        if (await _registroRepository.Update(registro) <= 0)
            throw new KeyNotFoundException("Não foi possível editar o registro!");

        return new MessageResponse { Message = "Registro editado com sucesso!" };
    }

    public async Task<MessageResponse> Delete(int id)
    {
        if (await _registroRepository.Delete(id) <= 0)
            throw new KeyNotFoundException("Não foi possível deletar o registro!");

        return new MessageResponse { Message = "Registro deletado com sucesso!" };
    }

    public async Task<RegistroGetAllResponse> GetAll()
    {
        return new RegistroGetAllResponse { Data = await _registroRepository.GetAll() ?? new List<RegistroEntity>() };
    }

    public async Task<GetPagedResponseBase<RegistroEntity>> GetPaged(int page, int pageSize)
    {
        var data = await _registroRepository.GetPaged(page, pageSize) ?? new List<RegistroEntity>();
        var allData = await _registroRepository.GetAll() ?? new List<RegistroEntity>();
        var totalItems = allData.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return new GetPagedResponseBase<RegistroEntity>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }

    public async Task<RegistroEntity> GetById(int id)
    {
        return await _registroRepository.GetById(id)
               ?? throw new KeyNotFoundException("Registro não encontrado!");
    }
}