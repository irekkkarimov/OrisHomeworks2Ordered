using TeamHost.Application.Models;

namespace TeamHost.Application.Interfaces;

public interface IHubService
{
    Task SendMessageAsync(Message message);
}