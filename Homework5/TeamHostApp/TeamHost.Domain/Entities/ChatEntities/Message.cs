using TeamHost.Domain.Common;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Domain.Entities.ChatEntities;

/// <summary>
/// Сообщение
/// </summary>
public class Message : BaseAuditableEntity
{
    /// <summary>
    /// Содержимое сообщения
    /// </summary>
    public string MessageContent { get; set; } = null!;

    /// <summary>
    /// Id <see cref="UserInfo"/>
    /// </summary>
    public int SenderInfoId { get; set; }
    
    /// <summary>
    /// Информация об отправителе
    /// </summary>
    public UserInfo SenderInfo { get; set; } = null!;

    /// <summary>
    /// Id чата
    /// </summary>
    public int ChatId { get; set; }

    /// <summary>
    /// Чат
    /// </summary>
    public Chat Chat { get; set; } = null!;
}