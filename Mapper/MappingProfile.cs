using Aerolineas.DTO;
using Aerolineas.DTO.Asiento;
using Aerolineas.DTO.Ticket;
using Aerolineas.Models;
using AutoMapper;
namespace Aerolineas.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<Asiento, AsientoSimpleDTO>();
        CreateMap<Asiento, AsientoDTO>()
            .ForMember(dest => dest.Vuelo, opt => opt.MapFrom(src => src.Vuelo));

        CreateMap<Reserva, ReservaSimpleDTO>();

        CreateMap<Usuario, UsuarioSimpleDTO>();
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
            .ForMember(dest => dest.Reservas, opt => opt.MapFrom(src => src.Reservas));

        CreateMap<Ticket, TicketDTO>()
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Reserva, opt => opt.MapFrom(src => src.Reserva));
        CreateMap<Ticket, TicketSimpleDTO>();

        CreateMap<Vuelo, VueloSimpleDTO>();

        CreateMap<Aeronave, AeronaveSimpleDTO>();

        CreateMap<Vuelo, VueloDTO>()
            .ForMember(dest => dest.Aeronave, opt => opt.MapFrom(src => src.Aeronave))
            .ForMember(dest => dest.Slot, opt => opt.MapFrom(src => src.Slot))
            .ForMember(dest => dest.Asientos, opt => opt.MapFrom(src => src.Asientos))
            .ForMember(dest => dest.Reservas, opt => opt.MapFrom(src => src.Reservas));

        CreateMap<Reserva, ReservaDTO>()
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario))
            .ForMember(dest => dest.Tickets, opt => opt.MapFrom(src => src.Tickets))
            .ForMember(dest => dest.Asiento, opt => opt.MapFrom(src => src.Asiento))
            .ForMember(dest => dest.Vuelo, opt => opt.MapFrom(src => src.Vuelo));

        CreateMap<Aeronave, AeronaveDTO>()
            .ForMember(dest => dest.Vuelos, opt => opt.MapFrom(src => src.Vuelos));
    }
}