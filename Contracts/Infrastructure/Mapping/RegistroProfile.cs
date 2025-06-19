using AutoMapper;
using HyperEfficient.DTO;
using HyperEfficient.Entity;

namespace HyperEfficient.Infrastructure.Mapping;

public class RegistroProfile : Profile
{
    public RegistroProfile()
    {
        CreateMap<RegistroInsertDTO, RegistroEntity>();
    }
}
