using TeamHost.Domain.Common;

namespace TeamHost.Domain.Entities.UserEntities;

public class FriendRequest : BaseEntity
{
    public Guid SenderId { get; set; }
    public User Sender { get; set; } = null!;
    public Guid ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;
    public DateTime RequestTime { get; set; }
}