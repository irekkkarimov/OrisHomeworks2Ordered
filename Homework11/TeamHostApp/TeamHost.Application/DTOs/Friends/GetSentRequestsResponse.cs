namespace TeamHost.Application.DTOs.Friends;

public class GetSentRequestsResponse
{
    public List<GetSentRequestsResponseItem> SentRequests { get; set; } = new();
}