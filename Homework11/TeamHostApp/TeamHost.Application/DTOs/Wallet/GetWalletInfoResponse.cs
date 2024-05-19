namespace TeamHost.Application.DTOs.Wallet;

public class GetWalletInfoResponse
{
    public Guid UserId { get; set; }
    public double Balance { get; set; }
    public List<GetWalletInfoResponseGamePurchase> Purchases { get; set; } = new();
}