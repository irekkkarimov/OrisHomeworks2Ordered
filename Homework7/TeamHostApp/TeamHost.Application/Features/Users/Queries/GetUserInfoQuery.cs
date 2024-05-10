using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Queries;

public class GetUserInfoQuery : IRequest<GetUserInfoResponse>
{
}

internal class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    private readonly IGenericRepository<UserInfo> _userInfoRepository;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;

    public GetUserInfoQueryHandler(IGenericRepository<UserInfo> userInfoRepository, SignInManager<User> signInManager, IMapper mapper)
    {
        _userInfoRepository = userInfoRepository;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        var currentUserEmailClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.Email));

        if (currentUserEmailClaim is null)
            throw new Exception("Email claim is missing");

        var currentUser = await _signInManager.UserManager.FindByEmailAsync(currentUserEmailClaim.Value);
        
        if (currentUser is null)
            throw new Exception("Current User not found");

        var userInfo = await _userInfoRepository.Entities
            .Include(i => i.Country)
            .FirstOrDefaultAsync(i => i.UserId == currentUser.Id, cancellationToken);

        if (userInfo is null)
            throw new Exception("User Info is null");

        Console.WriteLine(userInfo.Country?.Name);
        
        return _mapper.Map<GetUserInfoResponse>(currentUser.UserInfo);
    }
}