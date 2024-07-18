namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Provides an object that does not have a distinct identity and is equality-comparable based on properties.
/// </summary>
public abstract record ValueObject
{
    /// <summary>
    /// Determines whether the value object is equal to another value object.
    /// </summary>
    /// <param name="other">The other value object to compare.</param>
    /// <returns><c>true</c> if the two value objects are equal; otherwise, <c>false</c>.</returns>
    public virtual bool Equals(ValueObject? other)
        => ReferenceEquals(this, other) ||
        (other is not null &&
        GetEqualityComponents().SequenceEqual(other.GetEqualityComponents()));

    /// <summary>
    /// Serves as the value object hash function.
    /// </summary>
    /// <returns>A hash code for the current value object.</returns>
    public override int GetHashCode()
    {
        var builder = default(HashCode);
        foreach (var component in GetEqualityComponents())
        {
            builder.Add(component?.GetHashCode() ?? 0);
        }

        return builder.ToHashCode();
    }

    /// <summary>
    /// Gets the components which determine equality when comparing to other value objects.
    /// </summary>
    /// <returns>The equality components for the current value object.</returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();
}
