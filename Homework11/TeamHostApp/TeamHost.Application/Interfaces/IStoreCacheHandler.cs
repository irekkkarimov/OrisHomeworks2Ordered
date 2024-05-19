using TeamHost.Application.DTOs.Game;

namespace TeamHost.Application.Interfaces;

public interface IStoreCacheHandler
{
    Task AddToCacheAsync(GetGamesByFilterResponse response);
    Task ClearCacheAsync();
    Task<GetGamesByFilterResponse?> GetFromCacheAsync(string filter);
}