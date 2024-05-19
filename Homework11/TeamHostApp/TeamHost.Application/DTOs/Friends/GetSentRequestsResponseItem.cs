namespace TeamHost.Application.DTOs.Friends;

public class GetSentRequestsResponseItem
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}