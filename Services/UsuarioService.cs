using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class UsuarioService(AeroContext dbContext, IAuthService authService) : IUserService
{
    public async Task<Usuario?> Get(int id)
    {
        var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        return usuario;
    }

    public async Task<Usuario?> GetByEmail(string email)
    {
        var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        return usuario;
    }

    public async Task<List<Usuario>> GetAll()
    {
        var usuarios = await dbContext.Usuarios.ToListAsync();
        return usuarios;
    }

    public async Task<Result<string>> Create(RegisterDTO registerDTO)
    {
        var userWithEmail = await GetByEmail(registerDTO.Email);
        if (userWithEmail != null)
            return Result<string>.Fail("Ya hay un usuario con este email");

        Usuario nuevoUsuario = new()
        {
            Nombre = registerDTO.Nombre,
            Apellido = registerDTO.Apellido,
            DNI = registerDTO.DNI,
            Email = registerDTO.Email,
            PasswordHash = registerDTO.Password,
            Rol = "Pasajero"
        };

        await dbContext.SaveChangesAsync();
        string token = authService.CreateToken(nuevoUsuario.Id, nuevoUsuario.Rol);
        return Result<string>.Ok(token);
    }
}