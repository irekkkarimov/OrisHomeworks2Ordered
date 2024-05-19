using TeamHost.Domain.Common;

namespace TeamHost.Domain.Entities.UserEntities;

public class UserFriend : BaseEntity
{
    public Guid SenderId { get; set; }
    public User Sender { get; set; }
}