using Microsoft.AspNetCore.Identity;
using TeamHost.Domain.Entities.WalletEntities;

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
    public Wallet? Wallet { get; set; }
    public List<User> MyFriends { get; set; } = new();
    public List<User> FriendsWith { get; set; } = new();
    public List<FriendRequest> RequestsSent { get; set; } = new();
    public List<FriendRequest> RequestsReceived { get; set; } = new();
}