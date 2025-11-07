using Aerolineas.DTO;
using Aerolineas.Models;
using AutoMapper;
namespace Aerolineas.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Reserva, ReservaSimpleDTO>();

        CreateMap<Usuario, UsuarioSimpleDTO>();
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Reservas, opt => opt.MapFrom(src => src.Reservas));

        CreateMap<Vuelo, VueloSimpleDTO>();

        CreateMap<Aeronave, AeronaveSimpleDTO>();

        CreateMap<Vuelo, VueloDTO>()
            .ForMember(dest => dest.Aeronave, opt => opt.MapFrom(src => src.Aeronave));

        CreateMap<Reserva, ReservaDTO>()
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Vuelo, opt => opt.MapFrom(src => src.Vuelo));

        CreateMap<Aeronave, AeronaveDTO>()
            .ForMember(dest => dest.Vuelos, opt => opt.MapFrom(src => src.Vuelos));
    }
}