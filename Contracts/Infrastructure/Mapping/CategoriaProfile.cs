using AutoMapper;
using HyperEfficient.DTO;
using HyperEfficient.Entity;

namespace HyperEfficient.Infrastructure.Mapping;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CategoriaInsertDTO, CategoriaEntity>();
    }
}
