using AutoMapper;
using HyperEfficient.Dtos.Categoria;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CategoriaInsertDto, CategoriaEntity>();
    }
}