using Ardalis.SmartEnum;

namespace Accumulator.Domain.ItemDefinitions;

/// <summary>
/// Represents the grade type of an item.
/// </summary>
public class ItemGradeType : SmartEnum<ItemGradeType>
{
    /// <summary>
    /// Specifies that the item is in the normal grade.
    /// </summary>
    public static readonly ItemGradeType Normal = new("Normal", 1);

    /// <summary>
    /// Specifies that the item is in the exceptional grade.
    /// </summary>
    public static readonly ItemGradeType Exceptional = new("Exceptional", 2);

    /// <summary>
    /// Specifies that the item is in the elite grade.
    /// </summary>
    public static readonly ItemGradeType Elite = new("Elite", 3);

    /// <summary>
    /// Specifies that the item is from a quest and is not part of the typically grading scale.
    /// </summary>
    public static readonly ItemGradeType Quest = new("Quest", 4);

    private ItemGradeType(string name, int value)
        : base(name, value)
    {
    }

    /// <summary>
    /// Gets the grade associated with the specified codes.
    /// </summary>
    /// <param name="code">The code of the item.</param>
    /// <param name="normalCode">The normal code of the item.</param>
    /// <param name="exceptionalCode">The exceptional code of the item.</param>
    /// <param name="eliteCode">The elite code of the item.</param>
    /// <returns>The grade associated with the specified codes.</returns>
    /// <exception cref="ArgumentException">Thrown when no grade type can be identified for the provided codes.</exception>
    public static ItemGradeType FromCodes(ItemCode code, ItemCode normalCode, ItemCode exceptionalCode, ItemCode eliteCode)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(normalCode);
        ArgumentNullException.ThrowIfNull(exceptionalCode);
        ArgumentNullException.ThrowIfNull(eliteCode);

        if (code.Equals(normalCode))
        {
            return Normal;
        }
        else if (code.Equals(exceptionalCode))
        {
            return Exceptional;
        }
        else if (code.Equals(eliteCode))
        {
            return Elite;
        }

        return string.IsNullOrEmpty(exceptionalCode.Value) || string.IsNullOrEmpty(eliteCode.Value)
            ? Quest
            : throw new ArgumentException(
                $"Cannot determine grade due to no codes matching {code}. Normal: {normalCode}, Nightmare: {exceptionalCode}, " +
                    $"Hell: {eliteCode}.",
                nameof(code));
    }
}
