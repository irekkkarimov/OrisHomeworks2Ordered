using System.Reflection;
using DataLayer.Services;
using DataLayer.Services.PokeApiFetcher;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Entities.Type;

namespace DataLayer.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Pokemon> Pokemons { get; set; } = null!;
    public DbSet<Type> Types { get; set; } = null!;
    public DbSet<Move> Moves { get; set; } = null!;
    public DbSet<Ability> Abilities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}