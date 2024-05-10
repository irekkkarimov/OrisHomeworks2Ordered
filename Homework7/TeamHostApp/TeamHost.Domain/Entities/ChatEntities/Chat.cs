using TeamHost.Domain.Common;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Domain.Entities.ChatEntities;

/// <summary>
/// Чат
/// </summary>
public class Chat : BaseAuditableEntity
{
    /// <summary>
    /// Название чата
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Картинка чата
    /// </summary>
    public StaticFile? Image { get; set; }
    
    /// <summary>
    /// Информация о пользователях
    /// </summary>
    public List<UserInfo> UserInfos { get; set; } = new();

    /// <summary>
    /// Сообщения с этого чата
    /// </summary>
    public List<Message> Messages { get; set; }
}