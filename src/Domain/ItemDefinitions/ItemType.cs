using Ardalis.SmartEnum;

namespace Accumulator.Domain.ItemDefinitions;

/// <summary>
/// Represents the type of an item.
/// </summary>
public class ItemType : SmartEnum<ItemType>
{
    /// <summary>
    /// Specifies that the item is worn armor.
    /// </summary>
    public static readonly ItemType Armor = new("Armor", 1);

    /// <summary>
    /// Specifies that the item is not worn or wielded by a character.
    /// </summary>
    public static readonly ItemType Miscellaneous = new("Miscellaneous", 2);

    /// <summary>
    /// Specifies that the item is wielded weaponry.
    /// </summary>
    public static readonly ItemType Weapon = new("Weapon", 3);

    private ItemType(string name, int value)
        : base(name, value)
    {
    }
}
