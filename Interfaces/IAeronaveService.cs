using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface IAeronaveService
{
    Task<Result<List<Aeronave>>> GetAllAeronaves();
    Task<Result<Aeronave>> GetAeronaveById(int id);
    Task<Result<Aeronave>> CreateAeronave(CreateAeronaveDTO dto);
    Task<Result<Aeronave>> UpdateAeronave(int id, UpdateAeronaveDTO dto);
    Task<Result<bool>> DeleteAeronave(int id);
}