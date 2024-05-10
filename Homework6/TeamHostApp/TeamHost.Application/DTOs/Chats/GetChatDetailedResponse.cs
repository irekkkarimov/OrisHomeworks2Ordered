using TeamHost.Application.DTOs.Message;

namespace TeamHost.Application.DTOs.Chats;

public class GetChatDetailedResponse
{
    public int ChatId { get; set; }
    public string Title { get; set; } = null!;
    public Domain.Entities.StaticFile? Image { get; set; }
    public bool IsGroup { get; set; }
    public List<GetMessageResponse> Messages { get; set; } = new();
}