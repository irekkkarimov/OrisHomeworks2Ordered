using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TeamHost.Domain.Common;
using TeamHost.Domain.Common.Interfaces;

namespace TeamHost.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>()
            .AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
    }
}