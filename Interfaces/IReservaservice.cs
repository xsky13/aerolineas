using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Services;

public interface IReservaService
{
    Task<Reserva?> Get(int id);
    Task<List<Reserva>> GetAll();
    Task<ReservaDTO?> GetFull(int id);
    Task<List<ReservaDTO>> GetAllFull();
    Task<Result<Reserva>> Create(CrearReservaDTO reserva);
    Task<Result<Reserva>> Update(Reserva reserva);
    Task<Result<bool>> Delete(int id);
    Task<Result<Reserva>> ConfirmarReserva(int id, int vueloId);
    Task<Result<Reserva>> CancelarReserva(int id, int vueloId);
}