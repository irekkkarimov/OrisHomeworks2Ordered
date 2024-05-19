using TeamHost.Domain.Common;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Domain.Entities.WalletEntities;

public class Wallet : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public double Money { get; set; }
    public List<GamePurchase> GamePurchases { get; set; } = new();
}