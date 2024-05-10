using AutoMapper;
using MediatR;
using TeamHost.Application.DTOs.Game;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Application.Features.Games.Queries;

/// <summary>
/// Запрос на получение всех игр
/// </summary>
public class GetAllGamesQuery : IRequest<List<GetGameDto>>
{
}

/// <summary>
/// Обработчик запроса на получение всех игр
/// </summary>
internal class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, List<GetGameDto>>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструткор
    /// </summary>
    /// <param name="gameRepository">Репозиторий игр</param>
    /// <param name="mapper">Маппер объектов</param>
    public GetAllGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<List<GetGameDto>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
    {
        var allGames = await _gameRepository.GetAllAsync();

        var allGamesMapped = allGames
            .Select(i => _mapper.Map<GetGameDto>(i))
            .ToList();

        Console.WriteLine(allGamesMapped[0].MainImage);

        return allGamesMapped;
    }
}