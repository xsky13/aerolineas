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
}