using Aerolineas.Config;
using Aerolineas.DTO;
using Aerolineas.Interfaces;
using Aerolineas.Models;
using Microsoft.EntityFrameworkCore;

namespace Aerolineas.Services;

public class UsuarioService(AeroContext dbContext) : IUserService
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

    public async Task<Result<Usuario>> Create(RegisterDTO registerDTO)
    {
        bool exists = await dbContext.Usuarios.AnyAsync(u => u.Email == registerDTO.Email);
        if (exists) return Result<Usuario>.Fail("Ya hay un usuario con este email");

        Usuario nuevoUsuario = new()
        {
            Nombre = registerDTO.Nombre,
            Apellido = registerDTO.Apellido,
            DNI = registerDTO.DNI,
            Email = registerDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
            Rol = "Pasajero"
        };

        dbContext.Usuarios.Add(nuevoUsuario);
        await dbContext.SaveChangesAsync();
        return Result<Usuario>.Ok(nuevoUsuario);
    }
}