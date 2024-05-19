using Redis.OM.Searching;
using Redis.OM;
using TeamHost.Application.DTOs.Game;
using TeamHost.Application.Interfaces;

namespace TeamHost.Infrastructure.Services;

public class StoreCacheHandler : IStoreCacheHandler
{
    private readonly RedisCollection<GetGamesByFilterResponse> _getGamesCollection;

    public StoreCacheHandler(RedisConnectionProvider provider)
    {
        _getGamesCollection =
            (RedisCollection<GetGamesByFilterResponse>)provider.RedisCollection<GetGamesByFilterResponse>();
    }

    public async Task AddToCacheAsync(GetGamesByFilterResponse response)
    {
        await _getGamesCollection.InsertAsync(response);
    }

    public async Task ClearCacheAsync()
    {
        foreach (var gamesByFilterResponse in _getGamesCollection)
            await _getGamesCollection.DeleteAsync(gamesByFilterResponse);
    }

    public async Task<GetGamesByFilterResponse?> GetFromCacheAsync(string filter)
    {
        return await _getGamesCollection.FindByIdAsync(filter);
    }
}