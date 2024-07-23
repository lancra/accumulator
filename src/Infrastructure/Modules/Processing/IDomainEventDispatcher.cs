namespace Accumulator.Infrastructure.Modules.Processing;

/// <summary>
/// Represents the component for dispatching domain events.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches all domain events.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task DispatchEventsAsync(CancellationToken cancellationToken);
}
