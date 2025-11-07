using Aerolineas.DTO;
using Aerolineas.Models;
using AutoMapper;
namespace Aerolineas.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vuelo, VueloSimpleDTO>();
        // CreateMap<VueloDTO, Vuelo>()
        //     .ForMember(dest => dest.Aeronave, null);
        CreateMap<Aeronave, AeronaveSimpleDTO>();

        CreateMap<Vuelo, VueloDTO>()
            .ForMember(dest => dest.Aeronave, opt => opt.MapFrom(src => src.Aeronave));

        CreateMap<Aeronave, AeronaveDTO>()
            .ForMember(dest => dest.Vuelos, opt => opt.MapFrom(src => src.Vuelos));
    }
}