using AutoMapper;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping
{
    public class EquipamentoProfile : Profile
    {
        public EquipamentoProfile()
        {
            CreateMap<EquipamentoInsertDto, Equipamento>().ForMember(e => e.Ativo, opt => opt.MapFrom(_ => false));

            CreateMap<EquipamentoDto, Equipamento>()
                .ForMember(dest => dest.CategoriaId, opt => opt.MapFrom(src => src.Categoria.Id))
                .ForMember(dest => dest.SetorId, opt => opt.MapFrom(src => src.Setor.Id));
        }
    }
}