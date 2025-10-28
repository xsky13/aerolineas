using Aerolineas.Config;
using Aerolineas.DTO;

namespace Aerolineas.Interfaces;

public interface IAuthService
{
    Task<Result<string>> Login(LoginDTO emailDTO);
    Task<Result<string>> Register(RegisterDTO registerDTO);
}