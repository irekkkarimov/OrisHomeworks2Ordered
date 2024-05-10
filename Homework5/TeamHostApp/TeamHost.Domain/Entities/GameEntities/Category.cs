using TeamHost.Domain.Common;

namespace TeamHost.Domain.Entities.GameEntities;

/// <summary>
/// Категория игры (Жанр)
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Список игр, которые связаны с этой категорией
    /// </summary>
    public List<Game> Games { get; set; }
}