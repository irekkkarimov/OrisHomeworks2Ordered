using System.ComponentModel.DataAnnotations;
using TeamHost.Domain.Common;
using TeamHost.Domain.Entities.WalletEntities;

namespace TeamHost.Domain.Entities.GameEntities;

/// <summary>
/// Игра
/// </summary>
public class Game : BaseAuditableEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Цена (в долларах)
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Описание
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Короткое описание
    /// </summary>
    [MaxLength(256)]
    public string? ShortDescription{ get; set; }

    /// <summary>
    /// Изображения
    /// </summary>
    public List<StaticFile> Images { get; set; } = new();

    /// <summary>
    /// Рейтинг
    /// </summary>
    public float Rating { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public List<Category> Categories { get; set; } = new();

    /// <summary>
    /// Дата релиза
    /// </summary>
    public DateTime ReleaseDate { get; set; }

    /// <summary>
    /// Платформа
    /// </summary>
    public List<Platform> Platforms { get; set; } = new();

    /// <summary>
    /// Компания
    /// </summary>
    public List<Company> Companies { get; set; } = new();

    public List<GamePurchase> GamePurchases { get; set; } = new();
}