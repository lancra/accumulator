using Accumulator.Domain.SharedKernel;

namespace Accumulator.Domain.ItemDefinitions;

/// <summary>
/// Provides the code which identifies an item.
/// </summary>
public class ItemCode : TypedId<string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemCode"/> class.
    /// </summary>
    /// <param name="value">The code value.</param>
    public ItemCode(string value)
        : base(value)
    {
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="ItemCode"/>.
    /// </summary>
    /// <returns>A string that represents the current <see cref="ItemCode"/>.</returns>
    public override string ToString() => Value;
}
