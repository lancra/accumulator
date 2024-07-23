using Accumulator.Domain.SharedKernel;

namespace Accumulator.Infrastructure.Modules.Processing;

/// <summary>
/// Represents the component responsible for accessing domain events.
/// </summary>
public interface IDomainEventAccessor
{
    /// <summary>
    /// Gets all domain events.
    /// </summary>
    /// <returns>A collection of domain events.</returns>
    IReadOnlyCollection<DomainEvent> GetAll();

    /// <summary>
    /// Clears all domain events.
    /// </summary>
    void ClearAll();
}
