using Ardalis.SmartEnum;

namespace Accumulator.Domain.Items;

/// <summary>
/// Represents the version of an item.
/// </summary>
public class ItemVersion : SmartEnum<ItemVersion, byte>
{
    /// <summary>
    /// Specifies that the item is available in Classic mode.
    /// </summary>
    public static readonly ItemVersion Classic = new("Classic", 0);

    /// <summary>
    /// Specifies that the item is available in Expansion mode.
    /// </summary>
    public static readonly ItemVersion Expansion = new("Expansion", 4);

    private const int BinaryNumberBase = 2;

    private ItemVersion(string name, byte value)
        : base(name, value)
    {
    }

    /// <summary>
    /// Gets the type associated with the specified binary string.
    /// </summary>
    /// <param name="binary">The binary string of the version.</param>
    /// <returns>The type associated with the specified binary string.</returns>
    public static ItemVersion FromBinaryString(string binary)
        => FromValue(Convert.ToByte(binary, BinaryNumberBase));
}
