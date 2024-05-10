namespace TeamHost.Application.DTOs.Message;

public class GetMessageResponse
{
    public string SenderName { get; set; } = null!;
    public Domain.Entities.StaticFile? SenderImage { get; set; }
    public string Content { get; set; } = null!;
    public bool IsSender { get; set; }
}