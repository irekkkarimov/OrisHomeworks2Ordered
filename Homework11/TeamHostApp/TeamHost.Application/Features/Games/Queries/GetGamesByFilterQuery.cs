using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Game;
using TeamHost.Application.Interfaces;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Application.Features.Games.Queries;

/// <summary>
/// Запрос на получение всех игр
/// </summary>
public class GetGamesByFilterQuery : IRequest<GetGamesByFilterResponse>
{
    public GetGamesByFilterQuery(GetGamesByFilterRequest request)
    {
        Request = request;
    }

    public GetGamesByFilterRequest Request { get; set; }
}

/// <summary>
/// Обработчик запроса на получение всех игр
/// </summary>
internal class GetGamesByFilterQueryHandler : IRequestHandler<GetGamesByFilterQuery, GetGamesByFilterResponse>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;
    private readonly IStoreCacheHandler _cacheHandler;

    /// <summary>
    /// Конструткор
    /// </summary>
    /// <param name="gameRepository">Репозиторий игр</param>
    /// <param name="mapper">Маппер объектов</param>
    /// <param name="cacheHandler">Сервис для работы с кэшем</param>
    public GetGamesByFilterQueryHandler(IGameRepository gameRepository, IMapper mapper, IStoreCacheHandler cacheHandler)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _cacheHandler = cacheHandler;
    }

    /// <inheritdoc/>
    public async Task<GetGamesByFilterResponse> Handle(GetGamesByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = request.Request.Filter;

        GetGamesByFilterResponse result;

        if (string.IsNullOrWhiteSpace(filter))
            filter = "all__games";
        else
            filter = filter.Trim();
        
        var resultFromCache = await _cacheHandler.GetFromCacheAsync(filter);
        if (resultFromCache is not null)
            result = resultFromCache;
        else
        {
            var allGames = (await _gameRepository.GetAllAsync())
                .Where(i => filter.Equals("all__games") || i.Name.ToLower().Contains(filter.ToLower()))
                .ToList();
            var allGamesMapped = allGames
                .Select(i => _mapper.Map<GetGameDto>(i))
                .ToList();
            Console.WriteLine(allGames.Count);
            result = new GetGamesByFilterResponse
            {
                Filter = filter,
                Games = allGamesMapped
            };

            await _cacheHandler.AddToCacheAsync(result);   
        }

        return result;
    }
}