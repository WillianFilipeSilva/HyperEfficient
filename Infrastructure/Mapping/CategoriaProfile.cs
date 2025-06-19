using AutoMapper;
using HyperEfficient.DTOs.Categoria;
using HyperEfficient.Entity;

namespace HyperEfficient.Infrastructure.Mapping;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CategoriaInsertDTO, CategoriaEntity>();
    }
}
