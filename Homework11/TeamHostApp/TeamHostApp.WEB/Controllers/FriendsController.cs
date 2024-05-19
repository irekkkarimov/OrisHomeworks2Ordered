using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Application.DTOs.Friends;
using TeamHost.Application.Features.Friends.Commands.PostFriendAccept;
using TeamHost.Application.Features.Friends.Commands.PostFriendAdd;
using TeamHost.Application.Features.Users.Queries;

namespace TeamHostApp.WEB.Controllers;

[Route("[controller]")]
public class FriendsController : Controller
{
    private readonly IMediator _mediator;

    public FriendsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var getAllUserQuery = new UserGetAllQuery();
        var allUsers = await _mediator.Send(getAllUserQuery);

        var getAllFriendRequestsQuery = new UserGetFriendRequestsQuery();
        var allRequests = await _mediator.Send(getAllFriendRequestsQuery);

        allUsers.FriendRequests = allRequests;
        
        return View(allUsers);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> MakeRequest([FromQuery] Guid newFriendId)
    {
        var command = new FriendAddRequestCommand(new MakeFriendRequestRequest
        {
            NewFriendId = newFriendId
        });
        await _mediator.Send(command);

        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> ConfirmFriend([FromQuery] Guid newFriendId)
    {
        var command = new FriendAddConfirmCommand(new ConfirmFriendRequestRequest
        {
            NewFriendId = newFriendId
        });
        await _mediator.Send(command);

        return RedirectToAction("Index");
    }
}