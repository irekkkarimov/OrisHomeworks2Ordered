namespace TeamHost.Application.DTOs.Chats;

public class GetChatMessageLittle
{
    public Guid SenderId { get; set; }
    public string? SenderName { get; set; }
    public string Content { get; set; }
}