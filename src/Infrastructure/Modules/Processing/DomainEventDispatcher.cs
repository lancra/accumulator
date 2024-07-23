using MediatR;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;
    private readonly IDomainEventAccessor _domainEventAccessor;

    public DomainEventDispatcher(IMediator mediator, IDomainEventAccessor domainEventAccessor)
    {
        _mediator = mediator;
        _domainEventAccessor = domainEventAccessor;
    }

    public async Task DispatchEventsAsync(CancellationToken cancellationToken)
    {
        foreach (var domainEvent in _domainEventAccessor.GetAll())
        {
            await _mediator.Publish(domainEvent, cancellationToken)
                .ConfigureAwait(false);
        }

        _domainEventAccessor.ClearAll();
    }
}
