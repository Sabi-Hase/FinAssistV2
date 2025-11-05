using Microsoft.EntityFrameworkCore;
using FinAssist.Shared.Models;


namespace FinAssist.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Despesa> Despesas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Nome).IsRequired().HasMaxLength(150);
        });

        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.Descricao).HasMaxLength(250);
            entity.Property(d => d.Valor).HasColumnType("decimal(18,2)");
            entity.HasOne(d => d.Usuario)
                  .WithMany(u => u.Despesas)
                  .HasForeignKey(d => d.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
