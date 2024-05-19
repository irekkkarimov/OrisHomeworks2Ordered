using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Wallet;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Wallet.Commands;

public class DepositWalletCommand : IRequest
{
    public DepositWalletCommand(DepositWalletRequest request)
    {
        Props = request;
    }

    public DepositWalletRequest Props { get; set; }
}

internal class DepositWalletCommandHandler : IRequestHandler<DepositWalletCommand>
{
    private readonly IGenericRepository<Domain.Entities.WalletEntities.Wallet> _walletRepository;
    private readonly SignInManager<User> _signInManager;

    public DepositWalletCommandHandler(IGenericRepository<Domain.Entities.WalletEntities.Wallet> walletRepository, SignInManager<User> signInManager)
    {
        _walletRepository = walletRepository;
        _signInManager = signInManager;
    }

    public async Task Handle(DepositWalletCommand request, CancellationToken cancellationToken)
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

        walletFromDb.Money += props.Money;
        await _walletRepository.Context.SaveChangesAsync(cancellationToken);
    }
}