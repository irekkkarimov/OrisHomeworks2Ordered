using MediatR;

namespace TeamHost.Domain.Common;

public class BaseEvent : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}