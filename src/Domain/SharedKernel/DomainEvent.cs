using MediatR;

namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Represents an event that has occurred in the domain.
/// </summary>
public abstract class DomainEvent : INotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// </summary>
    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets the unique identifier for the event.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Gets the date when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
}
