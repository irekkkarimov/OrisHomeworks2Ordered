using System.ComponentModel.DataAnnotations.Schema;
using TeamHost.Domain.Common;

namespace TeamHost.Domain.Entities.GameEntities;

/// <summary>
/// Компания
/// </summary>
public class Company : BaseEntity
{
    /// <summary>
    /// Название компании
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    /// Описание компании
    /// </summary>
    public string Description { get; set; } = null!;
    
    /// <summary>
    /// Id страны
    /// </summary>
    public int? CountryId { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public Country? Country { get; set; } = null!;
    
    /// <summary>
    /// Список игр компании
    /// </summary>
    public List<Game> Games { get; set; }
}