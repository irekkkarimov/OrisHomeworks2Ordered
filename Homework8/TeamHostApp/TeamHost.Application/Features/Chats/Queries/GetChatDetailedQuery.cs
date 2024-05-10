using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Chats;
using TeamHost.Application.DTOs.Message;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Chats.Queries;

public class GetChatDetailedQuery : IRequest<GetChatDetailedResponse>
{
    public GetChatDetailedQuery(int chatId)
    {
        ChatId = chatId;
    }
    
    public int ChatId { get; set; }
}

internal class GetChatDetailedQueryHandler : IRequestHandler<GetChatDetailedQuery, GetChatDetailedResponse>
{
    private readonly IGenericRepository<Chat> _chatRepository;
    private readonly IGenericRepository<Message> _messageRepository;
    private readonly SignInManager<User> _signInManager;

    public GetChatDetailedQueryHandler(IGenericRepository<Chat> chatRepository, IGenericRepository<Message> messageRepository, SignInManager<User> signInManager)
    {
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _signInManager = signInManager;
    }

    public async Task<GetChatDetailedResponse> Handle(GetChatDetailedQuery request, CancellationToken cancellationToken)
    {
        var chatId = request.ChatId;
        var currentUserId = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!
            .Value;
        
        var chatFromDb = await _chatRepository.Entities
            .Include(i => i.Messages.Take(20))
            .ThenInclude(i => i.SenderInfo)
            .Where(i => i.UserInfos.Select(e => e.UserId.ToString()).Contains(currentUserId))
            .FirstOrDefaultAsync(i => i.Id == chatId, cancellationToken: cancellationToken);

        if (chatFromDb is null)
            throw new ArgumentException("Chat not found");

        return new GetChatDetailedResponse
        {
            ChatId = chatFromDb.Id,
            Title = chatFromDb.Title,
            Image = chatFromDb.Image,
            IsGroup = false,
            Messages = chatFromDb.Messages
                .Select(i => new GetMessageResponse
                {
                    SenderName = i.SenderInfo.FirstName ?? "",
                    SenderImage = null,
                    Content = i.MessageContent,
                    IsSender = i.SenderInfo.UserId.ToString().Equals(currentUserId)
                })
                .ToList()
        };
    }
}