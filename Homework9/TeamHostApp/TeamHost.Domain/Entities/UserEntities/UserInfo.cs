using TeamHost.Domain.Common;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Domain.Entities.UserEntities;

/// <summary>
/// Информация о пользователе
/// </summary>
public class UserInfo : BaseAuditableEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь, которому принадлежит информация
    /// </summary>
    public User User { get; set; } = null!;
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// Био (Описание)
    /// </summary>
    public string? Bio { get; set; }
    
    /// <summary>
    /// Id страны
    /// </summary>
    public int? CountryId { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public Country? Country { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? BirthDate { get; set; }

    public List<Chat> Chats { get; set; }
}