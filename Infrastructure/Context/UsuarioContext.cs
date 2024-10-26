using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;

public class UsuariosContext : DbContext
{
    private readonly string _connectionString;
    public DbSet<UsuarioModel> Usuarios { get; set; }


    public UsuariosContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Agendamentos");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UsuarioModel>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Definindo o tipo e tamanho da coluna Email explicitamente
        modelBuilder.Entity<UsuarioModel>()
            .Property(u => u.Email)
            .HasColumnType("nvarchar(256)");
    }
}
