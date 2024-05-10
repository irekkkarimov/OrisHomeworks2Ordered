using TeamHost.Application.DTOs.StaticFile;

namespace TeamHost.Application.DTOs.Chats;

public class GetChatDtoItem
{
    public int ChatId { get; set; }
    public string Title { get; set; } = null!;
    public GetStaticFileDto? Image { get; set; }
    public bool IsGroup { get; set; }
    public Guid? FriendId { get; set; }
    public GetChatMessageLittle? LastMessage { get; set; }
}