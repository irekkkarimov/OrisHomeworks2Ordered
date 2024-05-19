using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.GamePurchase;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.GameEntities;
using TeamHost.Domain.Entities.UserEntities;
using TeamHost.Domain.Entities.WalletEntities;

namespace TeamHost.Application.Features.Wallet.Commands;

public class BuyGameCommand : IRequest
{
    public BuyGameCommand(MakePurchaseDto request)
    {
        Props = request;
    }

    public MakePurchaseDto Props { get; set; }
}

internal class BuyGameCommandHandler : IRequestHandler<BuyGameCommand>
{
    private readonly IGenericRepository<Domain.Entities.WalletEntities.Wallet> _walletRepository;
    private readonly IGenericRepository<Game> _gameRepository;
    private readonly SignInManager<User> _signInManager;

    public BuyGameCommandHandler(IGenericRepository<Domain.Entities.WalletEntities.Wallet> walletRepository, IGenericRepository<Game> gameRepository, SignInManager<User> signInManager)
    {
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _signInManager = signInManager;
    }

    public async Task Handle(BuyGameCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var walletFromDb = await _walletRepository.Entities
            .Include(i => i.GamePurchases)
            .FirstOrDefaultAsync(i => i.UserId == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (walletFromDb is null)
            throw new ArgumentException("Current User Wallet not found");

        if (walletFromDb.GamePurchases.Select(i => i.GameId).Contains(props.GameId))
            throw new ArgumentException("User already has this game");
        
        var gameFromDb = await _gameRepository.Entities
            .FirstOrDefaultAsync(i => i.Id == props.GameId, cancellationToken);

        if (gameFromDb is null)
            throw new ArgumentException("Requested Game not found");

        if (walletFromDb.Money < decimal.ToDouble(gameFromDb.Price))
            throw new ArgumentException("Not enough money in wallet");
        
        var newPurchase = new GamePurchase
        {
            Game = gameFromDb,
            Wallet = walletFromDb,
            PurchaseDate = DateTime.UtcNow,
            Price = decimal.ToDouble(gameFromDb.Price)
        };

        walletFromDb.Money -= decimal.ToDouble(gameFromDb.Price);

        walletFromDb.GamePurchases.Add(newPurchase);
        await _walletRepository.Context.SaveChangesAsync(cancellationToken);
    }
}