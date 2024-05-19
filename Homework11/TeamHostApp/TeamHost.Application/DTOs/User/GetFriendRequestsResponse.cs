using TeamHost.Application.DTOs.Friends;

namespace TeamHost.Application.DTOs.User;

public class GetFriendRequestsResponse
{
    public GetSentRequestsResponse SentRequests { get; set; } = null!;
    public GetReceivedRequestsResponse ReceivedRequests { get; set; } = null!;
}