using Microsoft.AspNetCore.Identity;

namespace TeamHost.Domain.Entities.UserEntities;

/// <summary>
/// Пользователь
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public UserInfo? UserInfo { get; set; }
}