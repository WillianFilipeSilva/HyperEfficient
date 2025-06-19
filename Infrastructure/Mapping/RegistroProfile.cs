using AutoMapper;
using HyperEfficient.DTOs.Registro;
using HyperEfficient.Entity;

namespace HyperEfficient.Infrastructure.Mapping;

public class RegistroProfile : Profile
{
    public RegistroProfile()
    {
        CreateMap<RegistroInsertDTO, RegistroEntity>();
    }
}
