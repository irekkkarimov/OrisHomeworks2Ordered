using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TeamHost.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper();
        services.AddMediator();
        services.AddValidators();
        return services;
    }

    private static void AddCustomIdentity(this IServiceCollection services)
    {
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}