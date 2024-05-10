using TeamHost.Application.DTOs.StaticFile;
using TeamHost.Domain.Entities.ChatEntities;

namespace TeamHost.Application.DTOs.Chats;

public class GetChatDto
{
    public Guid CurrentUserId { get; set; }
    public List<GetChatDtoItem> Chats { get; set; } = new();
}