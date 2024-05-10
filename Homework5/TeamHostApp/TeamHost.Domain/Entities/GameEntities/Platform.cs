using TeamHost.Domain.Common;

namespace TeamHost.Domain.Entities.GameEntities;

/// <summary>
/// Платформа (ОС)
/// </summary>
public class Platform : BaseEntity
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Список игр, связанных с этой платформой
    /// </summary>
    public List<Game> Games { get; set; }
}