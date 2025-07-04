using AutoMapper;
using HyperEfficient.Dtos.Equipamento;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping
{
    public class EquipamentoProfile : Profile
    {
        public EquipamentoProfile()
        {
            CreateMap<EquipamentoInsertDto, EquipamentoEntity>().ForMember(d => d.Ativo, o => o.MapFrom(_ => true));
        }
    }
}