using AutoMapper;
using HyperEfficient.Dtos.Registro;
using HyperEfficient.Entities;

namespace HyperEfficient.Infrastructure.Mapping
{
    public class RegistroProfile : Profile
    {
        public RegistroProfile()
        {
            CreateMap<RegistroInsertDto, RegistroEntity>();
        }
    }
}