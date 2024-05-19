using Microsoft.AspNetCore.SignalR;
using TeamHost.Application.Interfaces;
using TeamHost.Application.Models;

namespace TeamHostApp.WEB.Hub;

public class HubService : IHubService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public HubService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMessageAsync(Message message)
    {
        Console.WriteLine(message.ReceiverIds.Count);
        if (!message.ReceiverIds.Any())
            return;

        Console.WriteLine(2);
        await _hubContext.Clients.Users(
                message.ReceiverIds
                    .Select(i => i.ToString())
                    .ToList())
            .SendAsync("ReceiveMessage", new
            {
                message.Content,
                message.SenderId,
                Images = message.ImageUrl,
                message.SenderName
            });
    }
}