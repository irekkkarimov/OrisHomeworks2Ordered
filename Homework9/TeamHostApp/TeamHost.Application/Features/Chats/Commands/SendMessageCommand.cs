using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Chats;
using TeamHost.Application.Interfaces;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Chats.Commands;

public class SendMessageCommand : IRequest
{
    public SendMessageCommand(SendMessageRequest request)
    {
        Request = request;
    }

    public SendMessageRequest Request { get; set; }
}

internal class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
{
    private readonly IGenericRepository<Chat> _chatRepository;
    private readonly IGenericRepository<Message> _messageRepository;
    private readonly IHubService _hubService;
    private readonly SignInManager<User> _signInManager;

    public SendMessageCommandHandler(IGenericRepository<Chat> chatRepository,
        IGenericRepository<Message> messageRepository, SignInManager<User> signInManager, IHubService hubService)
    {
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _signInManager = signInManager;
        _hubService = hubService;
    }

    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var props = request.Request;
        var currentUserId = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!.Value;

        var currentUser = await _signInManager.UserManager.Users
            .Include(i => i.UserInfo)
            .FirstOrDefaultAsync(i => i.Id == new Guid(currentUserId), cancellationToken);

        if (currentUser is null)
            throw new ArgumentException("Current user not found");
        
        var chatFromDb = await _chatRepository.Entities
            .Include(i => i.Messages)
            .Include(i => i.UserInfos)
            .Where(i => i.UserInfos.Contains(currentUser.UserInfo!))
            .FirstOrDefaultAsync(i => i.Id == props.ChatId, cancellationToken);

        if (chatFromDb is null)
            throw new ArgumentException("Chat not found");

        var newMessage = new Message
        {
            CreatedBy = 0,
            CreatedDate = DateTime.UtcNow,
            UpdatedBy = 0,
            UpdatedDate = DateTime.UtcNow,
            MessageContent = props.Content,
            SenderInfo = currentUser.UserInfo!,
            Chat = chatFromDb
        };

        var newMessageForHub = new Models.Message
        {
            SenderId = newMessage.SenderInfo.UserId,
            SenderName = newMessage.SenderInfo.FirstName ?? "",
            ReceiverIds = chatFromDb.UserInfos.Select(i => i.UserId).ToList(),
            Content = newMessage.MessageContent,
            ImageUrl = ""
        };

        chatFromDb.Messages.Add(newMessage);
        await _hubService.SendMessageAsync(newMessageForHub);
        await _chatRepository.Context.SaveChangesAsync(cancellationToken);
    }
}