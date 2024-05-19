using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.User;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Queries;

public class UserGetAllQuery : IRequest<UserGetAllResponse>
{
    
}

internal class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, UserGetAllResponse>
{
    private readonly SignInManager<User> _signInManager;

    public UserGetAllQueryHandler(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<UserGetAllResponse> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
    {
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var currentUserFromDb = await _signInManager.UserManager.Users
            .Include(i => i.MyFriends)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (currentUserFromDb is null)
            throw new ArgumentException("Current User not found");

        var currentUserFriendIds = currentUserFromDb.MyFriends
            .Select(i => i.Id);
        
        var allUsersWithoutCurrent = await _signInManager.UserManager.Users
            .Include(i => i.UserInfo)
            .Where(i => i.Id != new Guid(currentUserIdClaim.Value))
            .ToListAsync(cancellationToken);
        return new UserGetAllResponse
        {
            Users = allUsersWithoutCurrent
                .Select(i => new UserGetAllResponseItem
                {
                    UserId = i.Id,
                    UserName = i.UserName!,
                    FirstName = i.UserInfo?.FirstName,
                    LastName = i.UserInfo?.LastName,
                    IsFriend = currentUserFriendIds.Contains(i.Id)
                })
                .ToList()
        };
    }
}