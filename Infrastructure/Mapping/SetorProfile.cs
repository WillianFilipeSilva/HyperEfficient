using AutoMapper;
using HyperEfficient.DTOs.Setor;
using HyperEfficient.Entity;

namespace HyperEfficient.Infrastructure.Mapping
{
    public class SetorProfile : Profile
    {
        public SetorProfile()
        {
            CreateMap<SetorInsertDTO, SetorEntity>();
        }
    }
}
