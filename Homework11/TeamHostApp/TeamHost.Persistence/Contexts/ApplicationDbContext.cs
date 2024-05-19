using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamHost.Domain.Common.Interfaces;
using TeamHost.Domain.Entities;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.GameEntities;
using TeamHost.Domain.Entities.UserEntities;
using TeamHost.Domain.Entities.WalletEntities;

namespace TeamHost.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventDispatcher dispatcher) : base(options)
    {
    }

    public ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "Data Source=karantuligor_teamhost.mssql.somee.com;Initial Catalog=karantuligor_teamhost;User ID=karantuligor_SQLLogin_2;Password=2uga85dom9;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        optionsBuilder.EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<UserInfo> UserInfos { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<StaticFile> StaticFiles { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;
}