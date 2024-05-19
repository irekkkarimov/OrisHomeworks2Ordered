using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Wallet;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Wallet.Commands;

public class WithdrawWalletCommand : IRequest
{
    public WithdrawWalletCommand(WithdrawWalletRequest request)
    {
        Props = request;
    }

    public WithdrawWalletRequest Props { get; set; }
}

internal class WithdrawWalletCommandHandler : IRequestHandler<WithdrawWalletCommand>
{
    private readonly IGenericRepository<Domain.Entities.WalletEntities.Wallet> _walletRepository;
    private readonly SignInManager<User> _signInManager;

    public WithdrawWalletCommandHandler(IGenericRepository<Domain.Entities.WalletEntities.Wallet> walletRepository, SignInManager<User> signInManager)
    {
        _walletRepository = walletRepository;
        _signInManager = signInManager;
    }

    public async Task Handle(WithdrawWalletCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        
        if (props.Money <= 0)
            throw new ArgumentException("Deposit should be positive number");
        
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var walletFromDb = await _walletRepository.Entities
            .FirstOrDefaultAsync(i => i.UserId == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (walletFromDb is null)
            throw new ArgumentException("Current User Wallet not found");

        if (walletFromDb.Money < props.Money)
            throw new ArgumentException("Not enough money to withdraw!");

        walletFromDb.Money -= props.Money;
        await _walletRepository.Context.SaveChangesAsync(cancellationToken);
    }
}