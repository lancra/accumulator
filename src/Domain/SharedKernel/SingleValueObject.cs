using System.Diagnostics.CodeAnalysis;

namespace Accumulator.Domain.SharedKernel;

/// <summary>
/// Provides a value object with a single property.
/// </summary>
/// <typeparam name="TProperty">The type of the property.</typeparam>
public abstract record SingleValueObject<TProperty> : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SingleValueObject{TProperty}"/> class.
    /// </summary>
    /// <param name="value">The single property value.</param>
    protected SingleValueObject(TProperty value) => Value = value;

    /// <summary>
    /// Gets the value of the single property.
    /// </summary>
    public TProperty Value { get; }

    /// <summary>
    /// Converts the value object into the corresponding property type.
    /// </summary>
    /// <param name="value">The value object to convert.</param>
    [SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "Provided by the Value property.")]
    public static implicit operator TProperty(SingleValueObject<TProperty> value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Value;
    }

    /// <summary>
    /// Gets the components which determine equality when comparing to other value objects.
    /// </summary>
    /// <returns>The equality components for the current value object.</returns>
    protected sealed override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
