using AutoMapper;
using HyperEfficient.DTOs.Equipamento;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping;

public class EquipamentoProfile : Profile
{
    public EquipamentoProfile()
    {
        CreateMap<EquipamentoInsertDTO, EquipamentoEntity>()
            .ForMember(d => d.Ativo, o => o.MapFrom(_ => true));
    }
}
