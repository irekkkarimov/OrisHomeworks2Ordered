using Microsoft.Extensions.Hosting;
using Redis.OM;
using TeamHost.Application.DTOs.Game;

namespace TeamHost.Application.Services;

public class IndexCreationService : IHostedService
{
    private readonly RedisConnectionProvider _provider;

    public IndexCreationService(RedisConnectionProvider provider)
    {
        _provider = provider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _provider.Connection.CreateIndexAsync(typeof(GetGamesByFilterResponse));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}