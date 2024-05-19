using TeamHost.Application.DTOs.StaticFile;

namespace TeamHost.Application.DTOs.Wallet;

public class GetWalletInfoResponseGamePurchase
{
    public int GameId { get; set; }
    public string GameName { get; set; } = null!;
    public DateTime PurchaseDate { get; set; }
    public double Price { get; set; }
    public GetStaticFileDto? ImageUrl { get; set; }
}