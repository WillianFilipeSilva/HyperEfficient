using AutoMapper;
using HyperEfficient.Contracts.Repositories;
using HyperEfficient.Contracts.Services;
using HyperEfficient.Dtos.Base;
using HyperEfficient.Dtos.MessageResponse;
using HyperEfficient.Dtos.Setor;
using HyperEfficient.Entities;

namespace HyperEfficient.Services;

public class SetorService : ISetorService
{
    private readonly IMapper _map;
    private readonly ISetorRepository _setorRepository;

    public SetorService(ISetorRepository setorRepository, IMapper map)
    {
        _setorRepository = setorRepository;
        _map = map;
    }

    public async Task<MessageResponse> Insert(SetorInsertDto dto)
    {
        if (await _setorRepository.Insert(_map.Map<SetorEntity>(dto)) <= 0)
            throw new KeyNotFoundException($"Não foi possível cadastrar o setor {dto.Nome}!");

        return new MessageResponse { Message = "Setor cadastrado com sucesso!" };
    }

    public async Task<MessageResponse> Update(SetorEntity setor)
    {
        if (await _setorRepository.Update(setor) <= 0)
            throw new KeyNotFoundException($"Não foi possível editar o setor {setor.Nome}!");

        return new MessageResponse { Message = "Setor editado com sucesso!" };
    }

    public async Task<MessageResponse> Delete(int id)
    {
        if (await _setorRepository.Delete(id) <= 0)
            throw new KeyNotFoundException("Não foi possível deletar o setor!");

        return new MessageResponse { Message = "Setor deletado com sucesso!" };
    }

    public async Task<SetorGetAllResponse> GetAll()
    {
        return new SetorGetAllResponse { Data = await _setorRepository.GetAll() ?? new List<SetorEntity>() };
    }

    public async Task<GetPagedResponseBase<SetorEntity>> GetPaged(int page, int pageSize)
    {
        var data = await _setorRepository.GetPaged(page, pageSize) ?? new List<SetorEntity>();
        var allData = await _setorRepository.GetAll() ?? new List<SetorEntity>();
        var totalItems = allData.Count();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        return new GetPagedResponseBase<SetorEntity>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalItems = totalItems
        };
    }

    public async Task<SetorEntity> GetById(int id)
    {
        return await _setorRepository.GetById(id) ?? throw new KeyNotFoundException("Setor não encontrado!");
    }
}