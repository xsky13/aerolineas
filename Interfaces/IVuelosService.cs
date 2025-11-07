using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface IVuelosService
{
    Task<List<Vuelo>> ConsultarVuelos();
    Task<Vuelo?> ConsultarVuelo(int id);
    Task<Vuelo> RegistrarVuelo(VueloDTO vuelo);
    Task<Result<Vuelo>> ModificarVuelo(int id, UpdateVueloDTO vuelo);
    Task<Result<bool>> EliminarVuelo(int id);
    Task<Result<Vuelo>> CancelarVuelo(int id);
    Task<Result<Vuelo>> ConfirmarVuelo(int id);
    Task<Result<int>> BuscarSlot();
    Task<Result<Vuelo>> AsignarAeronave(int id, int aeronaveId);
    Task<Result<Vuelo>> AsignarSlot(int id, int slotId);
    Task<Result<Vuelo>> ProgramarVuelo(int id);
}