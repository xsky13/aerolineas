using Aerolineas.Config;
using Aerolineas.Models;

namespace Aerolineas.Services;

public interface IReservaService
{
    Task<Reserva?> Get(int id);
    Task<List<Reserva>> GetAll();
    Task<Result<Reserva>> Create(Reserva reserva);
    Task<Result<Reserva>> Update(Reserva reserva);
    Task<Result<bool>> Delete(int id);
    Task<Result<Reserva>> ConfirmarReserva(int id);
}