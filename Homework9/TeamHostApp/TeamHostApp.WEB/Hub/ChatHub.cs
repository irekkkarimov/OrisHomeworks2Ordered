using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace TeamHostApp.WEB.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
    private static readonly List<string> OnlineUsers = new();

    public override async Task OnConnectedAsync()
    {
        var userId = Context.GetHttpContext()!.User.Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!.Value;
        
        OnlineUsers.Add(userId);

        await Clients.All.SendAsync("OnUserConnection", new
        {
            OnlineUsers
        });
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.GetHttpContext()!.User.Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)!.Value;

        OnlineUsers.Remove(userId);

        await Clients.All.SendAsync("OnUserDisconnected", new
        {
            userId
        });
    }
}