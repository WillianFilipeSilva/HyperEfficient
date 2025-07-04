using AutoMapper;
using HyperEfficient.Dtos.Usuario;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioInsertDto, UsuarioEntity>().ForMember(dest => dest.CriadoEm, opt => opt.MapFrom(src => DateTime.UtcNow)).ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => true));

            CreateMap<UsuarioDto, UsuarioEntity>().ReverseMap();
        }
    }
}