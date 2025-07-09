using AutoMapper;
using HyperEfficient.Dtos.SetorDtos;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping
{
    public class SetorProfile : Profile
    {
        public SetorProfile()
        {
            CreateMap<SetorInsertDto, Setor>();
        }
    }
}