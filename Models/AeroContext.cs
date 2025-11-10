namespace Aerolineas.Models;

using Microsoft.EntityFrameworkCore;

public class AeroContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Aeronave> Aeronaves { get; set; }
    public DbSet<Vuelo> Vuelos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Asiento> Asientos { get; set; }

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
            .HasMany(v => v.Reservas)
            .WithOne(t => t.Vuelo)
            .HasForeignKey(t => t.VueloId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vuelo>()
            .HasMany(v => v.Asientos)
            .WithOne(t => t.Vuelo)
            .HasForeignKey(t => t.VueloId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Vuelo>()
            .HasOne(v => v.Slot)
            .WithMany()
            .HasForeignKey(v => v.SlotId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Asiento)
            .WithMany()
            .HasForeignKey(r => r.AsientoId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Asiento)
            .WithMany()
            .HasForeignKey(t => t.AsientoId)
            .OnDelete(DeleteBehavior.NoAction);

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