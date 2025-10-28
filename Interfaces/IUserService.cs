using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Models;

namespace Aerolineas.Interfaces;

public interface IUserService
{
    Task<Usuario?> Get(int id);
    Task<Usuario?> GetByEmail(string email);
    Task<List<Usuario>> GetAll();
    Task<Result<Usuario>> Create(RegisterDTO registerDTO);
    Task<Result<Usuario>> Update(UpdateDTO updateDTO, int userId);
    Task<Result<bool>> Delete(int userId);
}