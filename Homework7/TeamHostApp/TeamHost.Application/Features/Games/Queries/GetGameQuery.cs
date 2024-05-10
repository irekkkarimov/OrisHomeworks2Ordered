using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Game;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Application.Features.Games.Queries;

/// <summary>
/// Запрос на получение игры
/// </summary>
public class GetGameQuery : IRequest<GetGameDto>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="gameName">Название игры</param>
    public GetGameQuery(string gameName)
    {
        GameName = gameName;
    }
    
    /// <summary>
    /// Название игры
    /// </summary>
    public string GameName { get; set; } = null!;
}

/// <summary>
/// Обработчик запроса на получение игры
/// </summary>
internal class GetGameQueryHandler : IRequestHandler<GetGameQuery, GetGameDto>
{
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="gameRepository">Репозиторий игр</param>
    /// <param name="mapper">Маппер объектов</param>
    public GetGameQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<GetGameDto> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        var gameByName = await _gameRepository.Entities
            .Include(i => i.Platforms)
            .Include(i => i.Categories)
            .Include(i => i.Companies)
            .Include(i => i.Images)
            .FirstOrDefaultAsync(i => i.Name.ToLower().Equals(request.GameName.ToLower()),
                cancellationToken: cancellationToken);

        if (gameByName is null)
            throw new Exception("Game is null");

        return _mapper.Map<GetGameDto>(gameByName);
    }
}