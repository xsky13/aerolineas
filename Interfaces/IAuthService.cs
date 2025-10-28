using Aerolineas.DTO;

namespace Aerolineas.Interfaces;

public interface IAuthService
{
    Task<string?> Login(LoginDTO emailDTO);
    Task<string?> Register(RegisterDTO registerDTO);
}