namespace TeamHost.Application.DTOs.Chats;

public class SendMessageRequest
{
    public int ChatId { get; set; }
    public string Content { get; set; } = null!;
}