namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Provides a domain object with a distinct identity and lifecycle.
/// </summary>
public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = [];

    /// <summary>
    /// Gets the domain events that will be sent when the entity is persisted.
    /// </summary>
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Clears the pending domain events for the entity.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();

    /// <summary>
    /// Checks whether a rule is enforced for the entity.
    /// </summary>
    /// <param name="rule">The rule to check.</param>
    /// <exception cref="DomainRuleEvaluationException">Thrown when the <paramref name="rule"/> is broken.</exception>
    protected static void CheckRule(IDomainRule rule)
    {
        ArgumentNullException.ThrowIfNull(rule);
        if (rule.IsBroken())
        {
            throw new DomainRuleEvaluationException(rule);
        }
    }

    /// <summary>
    /// Adds a domain event for the entity.
    /// </summary>
    /// <param name="domainEvent">The domain event to add.</param>
    protected void AddDomainEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}
