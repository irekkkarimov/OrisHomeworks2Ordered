using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamHost.Application.DTOs.Chats;
using TeamHost.Application.Features.Chats.Commands;
using TeamHost.Application.Features.Chats.Queries;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHostApp.WEB.Controllers;

[Route("[controller]")]
[Authorize]
public class ChatsController : Controller
{
    private readonly IMediator _mediator;
    private readonly SignInManager<User> _signInManager;

    public ChatsController(SignInManager<User> signInManager, IMediator mediator)
    {
        _signInManager = signInManager;
        _mediator = mediator;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var currentUserEmail = _signInManager.Context.User.Claims.FirstOrDefault(i => i.Type.Equals(ClaimTypes.Email));
        if (currentUserEmail is null)
            throw new ArgumentException("Email Claim not found");
        
        var currentUser = await _signInManager.UserManager.FindByEmailAsync(currentUserEmail.Value);
        if (currentUser is null)
            throw new ArgumentException("User by Email Claim not found");
        
        var getAllChatsQuery = new GetAllChatsQuery(currentUser.Id);
        var allChats = await _mediator.Send(getAllChatsQuery);
        
        return View(allChats);
    }

    [HttpGet]
    [Route("[action]")]
    public async Task<IActionResult> GetChats()
    {
        var currentUserEmail = _signInManager.Context.User.Claims.FirstOrDefault(i => i.Type.Equals(ClaimTypes.Email));
        if (currentUserEmail is null)
            throw new ArgumentException("Email Claim not found");
        
        var currentUser = await _signInManager.UserManager.FindByEmailAsync(currentUserEmail.Value);
        if (currentUser is null)
            throw new ArgumentException("User by Email Claim not found");
        
        var getAllChatsQuery = new GetAllChatsQuery(currentUser.Id);
        var allChats = await _mediator.Send(getAllChatsQuery);
        
        return PartialView("_ChatsPartial", allChats.Chats);
    }

    [HttpGet]
    [Route("[action]/{chatId:int}")]
    public async Task<IActionResult> GetDetailed(int chatId)
    {
        var query = new GetChatDetailedQuery(chatId);
        var chatDetailed = await _mediator.Send(query);

        return PartialView("_ChatBox", chatDetailed);
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageRequest request)
    {
        var command = new SendMessageCommand(request);
        await _mediator.Send(command);

        return Ok();
    }
}