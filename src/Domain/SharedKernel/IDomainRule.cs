namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Represents a rule placed on the domain.
/// </summary>
public interface IDomainRule
{
    /// <summary>
    /// Gets the message shown when the rule has been broken.
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Determines whether the rule has been broken.
    /// </summary>
    /// <returns><c>true</c> when the rule has been broken; otherwise, <c>false</c>.</returns>
    bool IsBroken();
}
