using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeamHost.Domain.Common;
using TeamHost.Domain.Common.Interfaces;
using TeamHost.Domain.Entities;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.GameEntities;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    private readonly IDomainEventDispatcher? _dispatcher;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IDomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;
    }

    public ApplicationDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
        optionsBuilder.EnableDetailedErrors();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // if (_dispatcher is null)
        //     return result;
        //
        // var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
        //     .Select(i => i.Entity)
        //     .Where(i => i.DomainEvents.Any())
        //     .ToArray();
        //
        // await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        return result;
    }

    public override int SaveChanges()
    {
        return SaveChangesAsync().GetAwaiter().GetResult();
    }

    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<StaticFile> StaticFiles { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}