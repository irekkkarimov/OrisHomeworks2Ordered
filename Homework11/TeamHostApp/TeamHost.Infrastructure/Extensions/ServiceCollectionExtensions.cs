using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis.OM;
using TeamHost.Application.Interfaces;
using TeamHost.Application.Services;
using TeamHost.Domain.Common;
using TeamHost.Domain.Common.Interfaces;
using TeamHost.Infrastructure.Services;

namespace TeamHost.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddServices(); 

        services.AddSingleton(
            new RedisConnectionProvider(configuration.GetConnectionString("REDIS_CONNECTION_STRING")!));

        services.AddHostedService<IndexCreationService>();

        services.AddScoped<IStoreCacheHandler, StoreCacheHandler>();

        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}