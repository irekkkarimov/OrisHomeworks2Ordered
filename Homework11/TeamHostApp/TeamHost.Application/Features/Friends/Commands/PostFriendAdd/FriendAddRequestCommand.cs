using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Friends;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Friends.Commands.PostFriendAdd;

public class FriendAddRequestCommand : IRequest
{
    public FriendAddRequestCommand(MakeFriendRequestRequest request)
    {
        Props = request;
    }

    public MakeFriendRequestRequest Props { get; set; }
}

internal class FriendAddRequestCommandHandler : IRequestHandler<FriendAddRequestCommand>
{
    private readonly IGenericRepository<FriendRequest> _friendRequestRepository;
    private readonly SignInManager<User> _signInManager;

    public FriendAddRequestCommandHandler(IGenericRepository<FriendRequest> friendRequestRepository, SignInManager<User> signInManager)
    {
        _friendRequestRepository = friendRequestRepository;
        _signInManager = signInManager;
    }

    public async Task Handle(FriendAddRequestCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var currentUserFromDb = await _signInManager.UserManager.Users
            .Include(i => i.RequestsSent)
            .Include(i => i.RequestsReceived)
            .Include(i => i.MyFriends)
            .Include(i => i.FriendsWith)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (currentUserFromDb is null)
            throw new ArgumentException("Current User not found");

        if (currentUserFromDb.MyFriends.Select(i => i.Id).Contains(props.NewFriendId)
            || currentUserFromDb.FriendsWith.Select(i => i.Id).Contains(props.NewFriendId))
            throw new ArgumentException("User is already a friend!");

        if (currentUserFromDb.RequestsSent.Select(i => i.ReceiverId).Contains(props.NewFriendId))
            throw new ArgumentException("You already sent friend request to this user!");
        
        if (currentUserFromDb.RequestsReceived.Select(i => i.SenderId).Contains(props.NewFriendId))
            throw new ArgumentException("You already have a friend request from this user!");

        var userToAddFromDb = await _signInManager.UserManager.Users
            .FirstOrDefaultAsync(i => i.Id == props.NewFriendId, cancellationToken);

        if (userToAddFromDb is null)
            throw new ArgumentException("User to add not found");

        var newFriendRequest = new FriendRequest
        {
            Sender = currentUserFromDb,
            Receiver = userToAddFromDb,
            RequestTime = DateTime.UtcNow
        };

        currentUserFromDb.RequestsSent.Add(newFriendRequest);
        await _friendRequestRepository.Context.SaveChangesAsync(cancellationToken);
    }
}