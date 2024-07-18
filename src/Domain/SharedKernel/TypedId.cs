namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Provides a strongly-type identifier.
/// </summary>
/// <typeparam name="TId">The identifier type.</typeparam>
public abstract class TypedId<TId> : IEquatable<TypedId<TId>>
    where TId : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TypedId{TId}"/> class.
    /// </summary>
    /// <param name="value">The identifier value.</param>
    protected TypedId(TId value) => Value = value;

    /// <summary>
    /// Gets the identifier value.
    /// </summary>
    public TId Value { get; }

    /// <summary>
    /// Determines whether the current identifier is equal to another identifier.
    /// </summary>
    /// <param name="other">The other identifier to compare.</param>
    /// <returns><c>true</c> if the two identifiers are equal; otherwise, <c>false</c>.</returns>
    public virtual bool Equals(TypedId<TId>? other) => other is not null && Value.Equals(other.Value);

    /// <summary>
    /// Determines whether the current identifier is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns><c>true</c> if the identifier and object are equal; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj) => Equals(obj as TypedId<TId>);

    /// <summary>
    /// Serves as the identifier hash function.
    /// </summary>
    /// <returns>A hash code for the current identifier.</returns>
    public override int GetHashCode() => Value.GetHashCode();
}
