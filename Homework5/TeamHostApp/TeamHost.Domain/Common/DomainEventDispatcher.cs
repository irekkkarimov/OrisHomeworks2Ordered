using TeamHost.Domain.Common.Interfaces;

namespace TeamHost.Domain.Common;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    public Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents)
    {
        throw new NotImplementedException();
    }
}