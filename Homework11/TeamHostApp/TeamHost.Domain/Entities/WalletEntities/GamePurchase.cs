using TeamHost.Domain.Common;
using TeamHost.Domain.Entities.GameEntities;

namespace TeamHost.Domain.Entities.WalletEntities;

public class GamePurchase : BaseEntity
{
    public int? GameId { get; set; }
    public Game? Game { get; set; }
    public int? WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public DateTime PurchaseDate { get; set; }
    public double Price { get; set; }
}