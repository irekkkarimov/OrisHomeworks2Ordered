using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TeamHost.Application.DTOs.Chats;
using TeamHost.Application.DTOs.StaticFile;
using TeamHost.Application.Interfaces.Repositories;
using TeamHost.Domain.Entities.ChatEntities;
using TeamHost.Domain.Entities.UserEntities;

namespace TeamHost.Application.Features.Chats.Queries;

/// <summary>
/// Запрос на получение всех чатов пользователя
/// </summary>
public class GetAllChatsQuery : IRequest<GetChatDto>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    public GetAllChatsQuery(Guid userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
}

/// <summary>
/// Обработчик запроса на получение всех чатов пользователя
/// </summary>
internal class GetAllChatsQueryHandler : IRequestHandler<GetAllChatsQuery, GetChatDto>
{
    private readonly IGenericRepository<Chat> _chatRepository;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="chatRepository">Репозиторий чатов</param>
    /// <param name="mapper">Маппер объектов</param>
    public GetAllChatsQueryHandler(IGenericRepository<Chat> chatRepository, IMapper mapper,
        SignInManager<User> signInManager)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    /// <inheritdoc/>
    public async Task<GetChatDto> Handle(GetAllChatsQuery request, CancellationToken cancellationToken)
    {
        var userId = _signInManager.Context.User.Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!.Value;

        var chats = await _chatRepository
            .Entities
            .Where(i => i.UserInfos.Any(e => e.UserId == request.UserId))
            .Select(i => new GetChatDtoItem
            {
                ChatId = i.Id,
                Title = i.UserInfos.Count == 2
                    ? i.UserInfos.FirstOrDefault(e => e.UserId != new Guid(userId))!.FirstName ?? i.Title
                    : i.Title,
                Image = i.Image != null
                    ? new GetStaticFileDto
                    {
                        Path = i.Image.Path,
                        Size = i.Image.Size,
                        Name = i.Image.Name,
                        Extension = i.Image.Extension,
                        IsMainImage = i.Image.IsMainImage
                    }
                    : null,
                IsGroup = i.UserInfos.Count != 2,
                FriendId = i.UserInfos.Count == 2
                    ? i.UserInfos.FirstOrDefault(e => e.UserId != new Guid(userId))!.UserId
                    : null,
                LastMessage = i.Messages.Any()
                    ? new GetChatMessageLittle
                    {
                        SenderId = i.Messages.OrderByDescending(e => e.CreatedDate).First().SenderInfo.UserId,
                        SenderName = i.Messages.OrderByDescending(e => e.CreatedDate).First().SenderInfo.FirstName,
                        Content = i.Messages.OrderByDescending(e => e.CreatedDate).First().MessageContent
                    }
                    : null
            })
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetChatDto
        {
            CurrentUserId = new Guid(userId),
            Chats = chats
        };
    }
}