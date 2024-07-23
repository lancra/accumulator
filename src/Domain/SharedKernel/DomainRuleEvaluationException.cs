namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Provides the exception thrown when a domain rule is broken.
/// </summary>
public class DomainRuleEvaluationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainRuleEvaluationException"/> class.
    /// </summary>
    /// <param name="rule">The rule that was broken.</param>
    public DomainRuleEvaluationException(IDomainRule rule)
        : base(rule?.Message)
    {
        ArgumentNullException.ThrowIfNull(rule);
        Rule = rule;
    }

    /// <summary>
    /// Gets the rule that was broken.
    /// </summary>
    public IDomainRule Rule { get; }
}
