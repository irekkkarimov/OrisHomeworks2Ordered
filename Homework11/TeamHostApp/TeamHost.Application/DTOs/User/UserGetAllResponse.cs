namespace TeamHost.Application.DTOs.User;

public class UserGetAllResponse
{
    public List<UserGetAllResponseItem> Users { get; set; } = new();
    public GetFriendRequestsResponse FriendRequests { get; set; } = null!;
}