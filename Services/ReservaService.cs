using Aerolineas.Config;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class ReservaService(AeroContext dbContext) : IReservaService
{
    public async Task<Reserva?> Get(int id)
    {
        var reserva = await dbContext.Reservas.FirstOrDefaultAsync(r => r.Id == id);
        return reserva;
    }

    public async Task<List<Reserva>> GetAll()
    {
        var reservas = await dbContext.Reservas.ToListAsync();
        return reservas;
    }

    public async Task<Result<Reserva>> Create(Reserva reserva)
    {
        dbContext.Reservas.Add(reserva);
        await dbContext.SaveChangesAsync();
        return Result<Reserva>.Ok(reserva);
    }

    public async Task<Result<Reserva>> Update(Reserva reserva)
    {
        var existingReserva = await Get(reserva.Id);
        if (existingReserva == null) return Result<Reserva>.Fail("La reserva no existe");

        existingReserva.UsuarioId = reserva.UsuarioId;
        existingReserva.VueloId = reserva.VueloId;
        existingReserva.FechaReserva = reserva.FechaReserva;
        existingReserva.Confirmado = reserva.Confirmado;
        existingReserva.NumeroAsiento = reserva.NumeroAsiento;

        await dbContext.SaveChangesAsync();
        return Result<Reserva>.Ok(existingReserva);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var reserva = await Get(id);
        if (reserva == null) return Result<bool>.Fail("La reserva no existe");

        dbContext.Reservas.Remove(reserva);
        await dbContext.SaveChangesAsync();
        return Result<bool>.Ok(true);
    }

    public async Task<Result<Reserva>> ConfirmarReserva(int id)
    {
        var reserva = await Get(id);
        if (reserva == null) return Result<Reserva>.Fail("La reserva no existe");

        if (reserva.Confirmado)
            return Result<Reserva>.Fail("La reserva ya está confirmada");

        reserva.Confirmado = true;
        await dbContext.SaveChangesAsync();
        return Result<Reserva>.Ok(reserva);
    }
}