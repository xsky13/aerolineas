using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface IVuelosService
{
    Task<List<Vuelo>> ConsultarVuelos();
    Task<Vuelo?> ConsultarVuelo(int id);
    Task<Vuelo> RegistrarVuelo(VueloDTO vuelo);
    Task<Result<Vuelo>> ModificarVuelo(int id, Vuelo vuelo);
    Task<Result<bool>> CancelarVuelo(int id);
    Task<bool> ConfirmarVuelo(int id);
    Task<Result<int>> BuscarSlot();
    Task<Vuelo> AsignarSlot();
}