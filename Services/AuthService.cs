using Aerolineas.DTO;
using Aerolineas.Interfaces;

namespace Aerolineas.Services;

public class AuthService : IAuthService
{
    public Task<string> Login(LoginDTO emailDTO)
    {
        throw new NotImplementedException();
    }

    public Task<string> Register(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }
}