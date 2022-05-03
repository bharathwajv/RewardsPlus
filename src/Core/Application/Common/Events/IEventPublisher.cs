using RewardsPlus.Shared.Events;

namespace RewardsPlus.Application.Common.Events;

public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}