namespace Aerolineas.Models;

using Microsoft.EntityFrameworkCore;

public class AeroContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Reserva> Reservas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONN")
            ?? throw new InvalidOperationException("Database connection string is not set.");
        options.UseSqlServer(connectionString);
    }
}