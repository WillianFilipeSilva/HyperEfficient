using AutoMapper;
using HyperEfficient.DTOs.Registro;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping;

public class RegistroProfile : Profile
{
    public RegistroProfile()
    {
        CreateMap<RegistroInsertDTO, RegistroEntity>();
    }
}
