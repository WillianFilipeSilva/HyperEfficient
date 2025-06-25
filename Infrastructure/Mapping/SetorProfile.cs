using AutoMapper;
using HyperEfficient.Dtos.Setor;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping;

public class SetorProfile : Profile
{
    public SetorProfile()
    {
        CreateMap<SetorInsertDto, SetorEntity>();
    }
}