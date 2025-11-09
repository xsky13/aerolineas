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
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Reservas)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vuelo>()
            .HasMany(u => u.Reservas)
            .WithOne(t => t.Vuelo)
            .HasForeignKey(t => t.VueloId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vuelo>()
            .HasOne(v => v.Slot)
            .WithMany()
            .HasForeignKey(v => v.SlotId)
            .OnDelete(DeleteBehavior.SetNull); // or Cascade

        // When deleting a user → delete their tickets
        modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Tickets)
            .WithOne(t => t.Usuario)
            .HasForeignKey(t => t.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        // When deleting a reservation handle in application code
        modelBuilder.Entity<Reserva>()
            .HasMany(r => r.Tickets)
            .WithOne(t => t.Reserva)
            .HasForeignKey(t => t.ReservaId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}