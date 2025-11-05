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
    Task<bool> VerificarOrigen(int id);
    Task<bool> VerificarDestino(int id);
    Task<bool> ConfirmarVuelo(int id);
    Task<Vuelo> CambiarOrigen(int id);
    Task<Vuelo> CambiarDestino(int id);
}