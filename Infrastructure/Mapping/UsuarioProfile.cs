using AutoMapper;
using HyperEfficient.DTOs.Usuario;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<UsuarioInsertDTO, UsuarioEntity>()
            .ForMember(d => d.CriadoEm, o => o.MapFrom(_ => DateTime.UtcNow))
            .ForMember(d => d.Ativo, o => o.MapFrom(_ => true));
    }
}
