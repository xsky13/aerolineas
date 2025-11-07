using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class AeronaveService : IAeronaveService
{
    private readonly AeroContext _context;

    public AeronaveService(AeroContext context)
    {
        _context = context;
    }

    public async Task<Result<List<Aeronave>>> GetAllAeronaves()
    {
        var aeronaves = await _context.Aeronaves.ToListAsync();
        return Result<List<Aeronave>>.Ok(aeronaves);
    }

    public async Task<Result<Aeronave>> GetAeronaveById(int id)
    {
        var aeronave = await _context.Aeronaves.FindAsync(id);
        if (aeronave == null)
            return Result<Aeronave>.Fail("Aeronave no encontrada", 404);
        
        return Result<Aeronave>.Ok(aeronave);
    }

    public async Task<Result<Aeronave>> CreateAeronave(CreateAeronaveDTO dto)
    {
        // Verificar si ya existe una aeronave con la misma matrícula
        var existing = await _context.Aeronaves.FirstOrDefaultAsync(a => a.Matricula == dto.Matricula);
        if (existing != null)
            return Result<Aeronave>.Fail("Ya existe una aeronave con esta matrícula");

        var aeronave = new Aeronave
        {
            Matricula = dto.Matricula,
            Modelo = dto.Modelo,
            Capacidad = dto.Capacidad,
            AnioFabricacion = dto.AnioFabricacion,
            EstadoOperativo = dto.EstadoOperativo
        };

        _context.Aeronaves.Add(aeronave);
        await _context.SaveChangesAsync();

        return Result<Aeronave>.Ok(aeronave);
    }

    public async Task<Result<Aeronave>> UpdateAeronave(int id, UpdateAeronaveDTO dto)
    {
        var aeronave = await _context.Aeronaves.FindAsync(id);
        if (aeronave == null)
            return Result<Aeronave>.Fail("Aeronave no encontrada", 404);

        if (dto.Modelo != null)
            aeronave.Modelo = dto.Modelo;
        if (dto.Capacidad.HasValue)
            aeronave.Capacidad = dto.Capacidad.Value;
        if (dto.EstadoOperativo.HasValue)
            aeronave.EstadoOperativo = dto.EstadoOperativo.Value;

        await _context.SaveChangesAsync();
        return Result<Aeronave>.Ok(aeronave);
    }

    public async Task<Result<bool>> DeleteAeronave(int id)
    {
        var aeronave = await _context.Aeronaves.FindAsync(id);
        if (aeronave == null)
            return Result<bool>.Fail("Aeronave no encontrada", 404);

        _context.Aeronaves.Remove(aeronave);
        await _context.SaveChangesAsync();

        return Result<bool>.Ok(true);
    }
}