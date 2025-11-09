namespace Aerolineas.Models;

using Microsoft.EntityFrameworkCore;

public class AeroContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Aeronave> Aeronaves { get; set; }
    public DbSet<Vuelo> Vuelos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Slot> Slots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONN")
            ?? throw new InvalidOperationException("Database connection string is not set.");
        options.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vuelo>()
            .HasOne(v => v.Slot)
            .WithMany()
            .HasForeignKey(v => v.SlotId)
            .OnDelete(DeleteBehavior.SetNull); // or Cascade
    }
}