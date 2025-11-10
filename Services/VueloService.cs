using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class VueloService(AeroContext db, IAeronaveService aeronaveService, ISlotService slotService, IMapper mapper, IReservaService reservaService) : IVuelosService
{
    public async Task<Result<VueloDTO>> AsignarSlot(int id, SlotResponse slotResponse)
    {
        var vuelo = await GetVuelo(id);
        if (vuelo == null) return Result<VueloDTO>.Fail("El vuelo no existe", 404);

        Slot slot = new Slot()
        {
            SlotId = slotResponse.Id,
            FlightCode = slotResponse.FlightCode,
            Runway = slotResponse.Runway,
            Status = slotResponse.Status,
            GateId = slotResponse.GateId,
            Date = slotResponse.Date,
        };

        db.Slots.Add(slot);
        await db.SaveChangesAsync();

        vuelo.SlotId = slot.Id;
        await db.SaveChangesAsync();

        return Result<VueloDTO>.Ok(mapper.Map<VueloDTO>(vuelo));
    }

    public async Task<Result<bool>> EliminarVuelo(int id)
    {
        var vuelo = await db.Vuelos
            .Include(v => v.Reservas)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vuelo == null)
            return Result<bool>.Fail("El vuelo no existe");

        var reservasToDelete = vuelo.Reservas.ToList();
        foreach (var reserva in reservasToDelete)
            await reservaService.Delete(reserva.Id);

        if (vuelo.Slot != null)
            await slotService.CancelSlot(vuelo.Slot.Id);

        db.Vuelos.Remove(vuelo);
        await db.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }
    public async Task<Result<VueloDTO>> CancelarVuelo(int id)
    {
        var vuelo = await db.Vuelos
            .Include(v => v.Reservas)
            .Include(v => v.Slot)
            .FirstOrDefaultAsync(v => v.Id == id);

        if (vuelo == null)
            return Result<VueloDTO>.Fail("El vuelo no existe");

        var reservasToCancel = vuelo.Reservas.ToList();
        foreach (var reserva in reservasToCancel)
            await reservaService.CancelarReserva(reserva.Id);

        vuelo.Estado = "cancelado";
        if (vuelo.Slot != null)
            await slotService.CancelSlot(vuelo.Slot.Id);

        vuelo.SlotId = null;
        vuelo.Slot = null;
        await db.SaveChangesAsync();

        return Result<VueloDTO>.Ok(mapper.Map<VueloDTO>(vuelo));
    }


    public async Task<Result<VueloDTO>> ConfirmarVuelo(int id)
    {
        Vuelo? vuelo = await GetVuelo(id);
        if (vuelo == null) return Result<VueloDTO>.Fail("El vuelo no existe");

        vuelo.Estado = "confirmado";
        await db.SaveChangesAsync();
        return Result<VueloDTO>.Ok(mapper.Map<VueloDTO>(vuelo));
    }

    public async Task<Result<VueloDTO>> ProgramarVuelo(int id)
    {
        Vuelo? vuelo = await GetVuelo(id);
        if (vuelo == null) return Result<VueloDTO>.Fail("El vuelo no existe");

        vuelo.Estado = "programado";
        await db.SaveChangesAsync();
        return Result<VueloDTO>.Ok(mapper.Map<VueloDTO>(vuelo));
    }

    public async Task<Vuelo?> GetVuelo(int id)
    {
        return await db.Vuelos.Include(v => v.Aeronave).FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<List<Vuelo>> GetVuelos()
    {
        return db.Vuelos.Include(v => v.Aeronave).ToListAsync();
    }

    public async Task<VueloDTO?> GetVueloFull(int id)
    {
        return await db.Vuelos
            .ProjectTo<VueloDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<VueloDTO?> GetVueloByFlightCode(string flightCode)
    {
        return await db.Vuelos
            .ProjectTo<VueloDTO>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(v => v.FlightCode == flightCode);
    }

    public async Task<List<VueloDTO>> GetVuelosFull()
    {
        return await db.Vuelos
            .ProjectTo<VueloDTO>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<Result<Vuelo>> ModificarVuelo(int id, UpdateVueloDTO vuelo)
    {
        Vuelo? dbVuelo = await GetVuelo(id);
        if (dbVuelo == null) return Result<Vuelo>.Fail("El vuelo no existe");

        if (vuelo.Origen != null) dbVuelo.Origen = vuelo.Origen;
        if (vuelo.Destino != null) dbVuelo.Destino = vuelo.Destino;
        if (vuelo.Precio != null) dbVuelo.Precio = (int)vuelo.Precio;
        if (vuelo.HorarioSalida != null && vuelo.HorarioSalida != default) dbVuelo.HorarioSalida = (DateTime)vuelo.HorarioSalida;
        if (vuelo.HorarioLlegada != null && vuelo.HorarioLlegada != default) dbVuelo.HorarioLlegada = (DateTime)vuelo.HorarioLlegada;

        await db.SaveChangesAsync();
        return Result<Vuelo>.Ok(dbVuelo);
    }

    public async Task<VueloDTO> RegistrarVuelo(CrearVueloDTO vuelo)
    {

        Vuelo dbVuelo = new()
        {
            FlightCode = vuelo.FlightCode,
            Estado = "programado",
            Origen = vuelo.Origen,
            Destino = vuelo.Destino,
            HorarioSalida = vuelo.HorarioSalida,
            HorarioLlegada = vuelo.HorarioLlegada,
            Precio = vuelo.Precio,
            Asientos = [.. Enumerable.Range(1, 50).Select(i => new Asiento { NumeroAsiento = i })]
        };

        db.Vuelos.Add(dbVuelo);
        await db.SaveChangesAsync();
        await db.SaveChangesAsync();


        return mapper.Map<VueloDTO>(dbVuelo);
    }

    public async Task<Result<VueloDTO>> AsignarAeronave(int id, CambiarAeronaveDTO aeronaveDTO)
    {
        var vuelo = await GetVuelo(id);
        var aeronaveResponse = await aeronaveService.GetAeronaveById(aeronaveDTO.AeronaveId);
        if (!aeronaveResponse.Success) return Result<VueloDTO>.Fail(aeronaveResponse.Error, 404);
        if (vuelo == null) return Result<VueloDTO>.Fail("No existe el vuelo", 404);


        vuelo.AeronaveId = aeronaveResponse.Value.Id;

        VueloDTO vueloResponse = mapper.Map<VueloDTO>(vuelo);

        await db.SaveChangesAsync();
        return Result<VueloDTO>.Ok(vueloResponse);
    }
}