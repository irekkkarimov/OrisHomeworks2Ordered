using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.StaticFile;
using TeamHost.Application.DTOs.Wallet;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Wallet.Queries;

public class GetWalletInfoQuery : IRequest<GetWalletInfoResponse>
{
    
}

internal class GetWalletInfoQueryHandler : IRequestHandler<GetWalletInfoQuery, GetWalletInfoResponse>
{
    private readonly IGenericRepository<Domain.Entities.WalletEntities.Wallet> _walletRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public GetWalletInfoQueryHandler(IGenericRepository<Domain.Entities.WalletEntities.Wallet> walletRepository, SignInManager<User> signInManager, IMapper mapper)
    {
        _walletRepository = walletRepository;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<GetWalletInfoResponse> Handle(GetWalletInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");
        
        var userWallet = await _walletRepository.Entities
            .Include(i => i.GamePurchases)
            .ThenInclude(i => i.Game)
            .ThenInclude(i => i!.Images)
            .FirstOrDefaultAsync(i => i.UserId == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (userWallet is null)
            throw new ArgumentException("User Wallet not found");

        var result = new GetWalletInfoResponse
        {
            UserId = userWallet.UserId,
            Balance = userWallet.Money,
            Purchases = userWallet.GamePurchases
                .Select(i => new GetWalletInfoResponseGamePurchase
                {
                    GameId = i.GameId ?? 0,
                    GameName = i.Game?.Name ?? "null",
                    PurchaseDate = i.PurchaseDate,
                    Price = i.Price,
                    ImageUrl = i.Game?.Images.FirstOrDefault(e => e.IsMainImage) is not null
                        ? _mapper.Map<GetStaticFileDto>(i.Game?.Images.FirstOrDefault(e => e.IsMainImage))
                        : new GetStaticFileDto()
                })
                .ToList()
        };

        return result;
    }
}