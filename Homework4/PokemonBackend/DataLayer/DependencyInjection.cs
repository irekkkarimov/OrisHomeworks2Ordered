using DataLayer.Contexts;
using DataLayer.Services;
using DataLayer.Services.DbSeeder;
using DataLayer.Services.PokeApiFetcher;
using DataLayer.Services.PokemonSeedHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("NPGSQL_CONNECTION_STRING")));

        services.AddScoped<IPokeApiFetcher, PokeApiFetcher>();

        services.AddScoped<IPokemonSeedHandler, PokemonSeedHandler>();
        
        services.AddScoped<IDbSeeder, DbSeeder>();
        
        return services;
    }    
}