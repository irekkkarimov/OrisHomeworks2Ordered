using TeamHost.Application.DTOs.Category;
using TeamHost.Application.DTOs.Company;
using TeamHost.Application.DTOs.Platform;
using TeamHost.Application.DTOs.StaticFile;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Application.DTOs.Game;

public class GetGameDto
{
    public int GameId { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Цена (в вонах)
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Короткое описание
    /// </summary>
    public string? ShortDescription{ get; set; }

    public GetStaticFileDto? MainImage { get; set; }

    /// <summary>
    /// Изображения
    /// </summary>
    public List<GetStaticFileDto>? Images { get; set; }

    /// <summary>
    /// Рейтинг
    /// </summary>
    public float Rating { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public List<GetGameCategoryDto> Categories { get; set; } = new();

    /// <summary>
    /// Дата релиза
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Платформа
    /// </summary>
    public List<GetPlatformDto> Platforms { get; set; } = new();

    /// <summary>
    /// Компания-разработчик
    /// </summary>
    public List<GetCompanyDto> Companies { get; set; } = null!;
}