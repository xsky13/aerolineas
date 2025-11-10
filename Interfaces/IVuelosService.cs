using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface IVuelosService
{
    Task<List<Vuelo>> GetVuelos();
    Task<Vuelo?> GetVuelo(int id);
    Task<VueloDTO> RegistrarVuelo(CrearVueloDTO vuelo);
    Task<Result<Vuelo>> ModificarVuelo(int id, UpdateVueloDTO vuelo);
    Task<Result<bool>> EliminarVuelo(int id);
    Task<Result<VueloDTO>> CancelarVuelo(int id);
    Task<Result<VueloDTO>> ConfirmarVuelo(int id);
    Task<Result<VueloDTO>> AsignarAeronave(int id, CambiarAeronaveDTO aeronaveDTO);
    Task<Result<VueloDTO>> AsignarSlot(int id, SlotResponse slotResponse);
    Task<Result<VueloDTO>> ProgramarVuelo(int id);
    Task<VueloDTO?> GetVueloFull(int id);
    Task<List<VueloDTO>> GetVuelosFull();
    Task<VueloDTO?> GetVueloByFlightCode(string flightCode);
}