using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Friends;
using TeamHost.Application.DTOs.User;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Users.Queries;

public class UserGetFriendRequestsQuery : IRequest<GetFriendRequestsResponse>
{
}

internal class UserGetFriendRequestsQueryHandler
    : IRequestHandler<UserGetFriendRequestsQuery, GetFriendRequestsResponse>
{
    private readonly SignInManager<User> _signInManager;

    public UserGetFriendRequestsQueryHandler(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<GetFriendRequestsResponse> Handle(UserGetFriendRequestsQuery request,
        CancellationToken cancellationToken)
    {
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var currentUserFromDb = await _signInManager.UserManager.Users
            .Include(i => i.RequestsSent)
            .ThenInclude(i => i.Receiver)
            .Include(i => i.RequestsReceived)
            .ThenInclude(i => i.Sender)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (currentUserFromDb is null)
            throw new ArgumentException("Current User not found");

        var sentRequestsMapped = currentUserFromDb.RequestsSent
            .Select(i => new GetSentRequestsResponseItem
            {
                UserId = i.ReceiverId,
                UserName = i.Receiver.UserName!,
                FirstName = i.Receiver.UserInfo?.FirstName,
                LastName = i.Receiver.UserInfo?.LastName
            })
            .ToList();

        Console.WriteLine(currentUserFromDb.RequestsSent.Count);
        Console.WriteLine(currentUserFromDb.RequestsReceived.Count);
        
        var receivedRequestsMapped = currentUserFromDb.RequestsReceived
            .Select(i => new GetReceivedRequestsResponseItem
            {
                UserId = i.SenderId,
                UserName = i.Receiver.UserName!,
                FirstName = i.Receiver.UserInfo?.FirstName,
                LastName = i.Receiver.UserInfo?.LastName
            })
            .ToList();

        return new GetFriendRequestsResponse
        {
            SentRequests = new GetSentRequestsResponse
            {
                SentRequests = sentRequestsMapped
            },
            ReceivedRequests = new GetReceivedRequestsResponse
            {
                ReceivedRequests = receivedRequestsMapped
            }
        };
    }
}