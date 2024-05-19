using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Friends;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Friends.Commands.PostFriendAccept;

public class FriendAddConfirmCommand : IRequest
{
    public FriendAddConfirmCommand(ConfirmFriendRequestRequest request)
    {
        Props = request;
    }

    public ConfirmFriendRequestRequest Props { get; set; }
}

internal class FriendAddConfirmCommandHandler : IRequestHandler<FriendAddConfirmCommand>
{
    private readonly SignInManager<User> _signInManager;
    private readonly IGenericRepository<FriendRequest> _friendRequestRepository;

    public FriendAddConfirmCommandHandler
        (SignInManager<User> signInManager,
            IGenericRepository<FriendRequest> friendRequestRepository)
    {
        _signInManager = signInManager;
        _friendRequestRepository = friendRequestRepository;
    }

    public async Task Handle(FriendAddConfirmCommand request, CancellationToken cancellationToken)
    {
        var props = request.Props;
        
        var currentUserIdClaim = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier));

        if (currentUserIdClaim is null)
            throw new ArgumentException("Current User Id not found");

        var currentUserFromDb = await _signInManager.UserManager.Users
            .Include(i => i.RequestsReceived)
            .ThenInclude(i => i.Sender)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserIdClaim.Value), cancellationToken);

        if (currentUserFromDb is null)
            throw new ArgumentException("Current User not found");

        Console.WriteLine(props.NewFriendId);
        Console.WriteLine(currentUserFromDb.RequestsReceived[0].SenderId);
        var friendRequest = currentUserFromDb.RequestsReceived
            .FirstOrDefault(i => i.SenderId == props.NewFriendId);
        
        if (friendRequest is null)
            throw new ArgumentException("No request from this user!");

        if (friendRequest.Sender is null)
            throw new ArgumentException("Friend to add not found");

        await _friendRequestRepository.DeleteAsync(friendRequest);
        currentUserFromDb.MyFriends.Add(friendRequest.Sender);
        friendRequest.Sender.MyFriends.Add(currentUserFromDb);
        await _friendRequestRepository.Context.SaveChangesAsync(cancellationToken);
    }
}