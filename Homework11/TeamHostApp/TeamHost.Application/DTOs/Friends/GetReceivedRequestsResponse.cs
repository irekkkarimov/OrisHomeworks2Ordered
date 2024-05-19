namespace TeamHost.Application.DTOs.Friends;

public class GetReceivedRequestsResponse
{
    public List<GetReceivedRequestsResponseItem> ReceivedRequests { get; set; } = new();
}