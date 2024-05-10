namespace TeamHost.Application.Models;

public class Message
{
    public Guid SenderId { get; set; }
    public string SenderName { get; set; } = null!;
    public List<Guid> ReceiverIds { get; set; } = new();
    public string Content { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public bool IsSender { get; set; }
}