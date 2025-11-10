using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class ReservaService(AeroContext dbContext, IMapper mapper, IUserService userService, ITicketService ticketService) : IReservaService
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

    public async Task<ReservaDTO?> GetFull(int id)
    {
        var reserva = await dbContext.Reservas.ProjectTo<ReservaDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(r => r.Id == id);
        return reserva;
    }

    public async Task<List<ReservaDTO>> GetAllFull()
    {
        var reservas = await dbContext.Reservas.ProjectTo<ReservaDTO>(mapper.ConfigurationProvider).ToListAsync();
        return reservas;
    }

    public async Task<Result<Reserva>> Create(CrearReservaDTO reserva)
    {
        var usuario = await userService.GetFull(reserva.UsuarioId);
        if (usuario == null) return Result<Reserva>.Fail("Usuario no existe");

        var vuelo = await dbContext.Vuelos.ProjectTo<VueloDTO>(mapper.ConfigurationProvider).FirstOrDefaultAsync(v => v.Id == reserva.VueloId);
        if (vuelo == null) return Result<Reserva>.Fail("Vuelo no existe");
        if (vuelo.Estado != "confirmado" || vuelo.SlotId == null) return Result<Reserva>.Fail("Vuelo no esta confirmado");


        Reserva dbReserva = new Reserva()
        {
            UsuarioId = reserva.UsuarioId,
            VueloId = reserva.VueloId,
            FechaReserva = reserva.FechaReserva,
            Confirmado = false,
            AsientoId = reserva.AsientoId
        };
        dbContext.Reservas.Add(dbReserva);
        await dbContext.SaveChangesAsync();
        return Result<Reserva>.Ok(dbReserva);
    }

    public async Task<Result<Reserva>> Update(Reserva reserva)
    {
        var existingReserva = await Get(reserva.Id);
        if (existingReserva == null) return Result<Reserva>.Fail("La reserva no existe");

        existingReserva.UsuarioId = reserva.UsuarioId;
        existingReserva.VueloId = reserva.VueloId;
        existingReserva.FechaReserva = reserva.FechaReserva;
        existingReserva.Confirmado = reserva.Confirmado;

        await dbContext.SaveChangesAsync();
        return Result<Reserva>.Ok(existingReserva);
    }

    public async Task<Result<bool>> Delete(int id)
    {
        var reserva = await dbContext.Reservas.Include(r => r.Tickets).FirstOrDefaultAsync(r => r.Id == id);
        if (reserva == null) return Result<bool>.Fail("La reserva no existe");

        var ticketsToDelete = reserva.Tickets.ToList();
        foreach (var ticket in ticketsToDelete)
        {
            await ticketService.EliminarTicket(ticket.Id);
        }

        dbContext.Reservas.Remove(reserva);
        await dbContext.SaveChangesAsync();
        return Result<bool>.Ok(true);
    }

    public async Task<Result<ReservaDTO>> ConfirmarReserva(int id, int vueloId)
    {
        var reserva = await Get(id);
        if (reserva == null) return Result<ReservaDTO>.Fail("La reserva no existe");

        if (reserva.Confirmado)
            return Result<ReservaDTO>.Fail("La reserva ya está confirmada");

        var vuelo = await dbContext.Vuelos.FirstOrDefaultAsync(v => v.Id == reserva.VueloId);
        if (vuelo == null) return Result<ReservaDTO>.Fail("El vuelo no existe");
        if (vuelo.Estado != "confirmado") return Result<ReservaDTO>.Fail("El vuelo no esta confirmado");

        reserva.Confirmado = true;
        await dbContext.SaveChangesAsync();
        return Result<ReservaDTO>.Ok(mapper.Map<ReservaDTO>(reserva));
    }

    public async Task<Result<ReservaDTO>> CancelarReserva(int id)
    {
        var reserva = await dbContext.Reservas.Include(r => r.Tickets).FirstOrDefaultAsync(r => r.Id == id);
        if (reserva == null) return Result<ReservaDTO>.Fail("La reserva no existe");

        reserva.Confirmado = false;
        dbContext.Tickets.RemoveRange(reserva.Tickets);


        await dbContext.SaveChangesAsync();
        return Result<ReservaDTO>.Ok(mapper.Map<ReservaDTO>(reserva));
    }



}