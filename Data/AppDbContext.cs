using FinAssistAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinAssistAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Despesa> Despesas => Set<Despesa>();
    }
}