using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class VueloService(AeroContext db, IAeronaveService aeronaveService) : IVuelosService
{
    public async Task<Result<Vuelo>> AsignarSlot(int id, int slotId)
    {
        Vuelo? vuelo = await ConsultarVuelo(id);
        if (vuelo == null) return Result<Vuelo>.Fail("El vuelo no existe");

        vuelo.SlotId = slotId;
        await db.SaveChangesAsync();
        return Result<Vuelo>.Ok(vuelo);
    }

    public async Task<Result<int>> BuscarSlot()
    {
        return Result<int>.Ok(1);
    }

    public async Task<Result<bool>> EliminarVuelo(int id)
    {
        Vuelo? vuelo = await ConsultarVuelo(id);
        if (vuelo == null) return Result<bool>.Fail("El vuelo no existe");

        db.Vuelos.Remove(vuelo);
        await db.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }

    public async Task<Result<Vuelo>> CancelarVuelo(int id)
    {
        Vuelo? vuelo = await ConsultarVuelo(id);
        if (vuelo == null) return Result<Vuelo>.Fail("El vuelo no existe");

        vuelo.Estado = "cancelado";
        await db.SaveChangesAsync();

        // llamar servicio cancelar reserva
        return Result<Vuelo>.Ok(vuelo);
    }

    public async Task<Result<Vuelo>> ConfirmarVuelo(int id)
    {
        Vuelo? vuelo = await ConsultarVuelo(id);
        if (vuelo == null) return Result<Vuelo>.Fail("El vuelo no existe");

        vuelo.Estado = "confirmado";
        await db.SaveChangesAsync();
        return Result<Vuelo>.Ok(vuelo);
    }

    public async Task<Result<Vuelo>> ProgramarVuelo(int id)
    {
        Vuelo? vuelo = await ConsultarVuelo(id);
        if (vuelo == null) return Result<Vuelo>.Fail("El vuelo no existe");

        vuelo.Estado = "programado";
        await db.SaveChangesAsync();
        return Result<Vuelo>.Ok(vuelo);
    }

    public async Task<Vuelo?> ConsultarVuelo(int id)
    {
        return await db.Vuelos.Include(v => v.Aeronave).FirstOrDefaultAsync(v => v.Id == id);
    }

    public Task<List<Vuelo>> ConsultarVuelos()
    {
        return db.Vuelos.Include(v => v.Aeronave).ToListAsync();
    }

    public async Task<Result<Vuelo>> ModificarVuelo(int id, UpdateVueloDTO vuelo)
    {
        Vuelo? dbVuelo = await ConsultarVuelo(id);
        if (dbVuelo == null) return Result<Vuelo>.Fail("El vuelo no existe");

        if (vuelo.Origen != null) dbVuelo.Origen = vuelo.Origen;
        if (vuelo.Destino != null) dbVuelo.Destino = vuelo.Destino;
        if (vuelo.HorarioSalida != null && vuelo.HorarioSalida != default) dbVuelo.HorarioSalida = (DateTime)vuelo.HorarioSalida;
        if (vuelo.HorarioLlegada != null && vuelo.HorarioLlegada != default) dbVuelo.HorarioLlegada = (DateTime)vuelo.HorarioLlegada;

        await db.SaveChangesAsync();
        return Result<Vuelo>.Ok(dbVuelo);
    }

    public async Task<Vuelo> RegistrarVuelo(CrearVueloDTO vuelo)
    {
        Vuelo dbVuelo = new()
        {
            Id = 0,
            Estado = "programado",
            Origen = vuelo.Origen,
            Destino = vuelo.Destino,
            HorarioSalida = vuelo.HorarioSalida,
            HorarioLlegada = vuelo.HorarioLlegada
        };

        db.Vuelos.Add(dbVuelo);
        await db.SaveChangesAsync();

        return dbVuelo;
    }

    public async Task<Result<Vuelo>> AsignarAeronave(int id, CambiarAeronaveDTO aeronaveDTO)
    {
        Vuelo? vuelo = await ConsultarVuelo(id);
        var aeronaveResponse = await aeronaveService.GetAeronaveById(aeronaveDTO.AeronaveId);
        if (!aeronaveResponse.Success) return Result<Vuelo>.Fail(aeronaveResponse.Error, 404);
        if (vuelo == null) return Result<Vuelo>.Fail("No existe el vuelo", 404);


        vuelo.Aeronave = aeronaveResponse.Value;
        var responseUpdateAeronave = await aeronaveService.UpdateAeronave(aeronaveDTO.AeronaveId, new UpdateAeronaveDTO() { Vuelo = vuelo });

        if (!responseUpdateAeronave.Success) return Result<Vuelo>.Fail(responseUpdateAeronave.Error);

        await db.SaveChangesAsync();
        return Result<Vuelo>.Ok(vuelo);
    }
}