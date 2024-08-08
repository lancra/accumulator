using Accumulator.Domain.SharedKernel;

namespace Accumulator.Domain.ItemDefinitions;

/// <summary>
/// Provides a grade within the same group as an <see cref="ItemDefinition"/>.
/// </summary>
public class ItemDefinitionGrade : Entity
{
    private ItemDefinitionGrade(ItemCode parentCode, ItemCode code, ItemGradeType type)
    {
        ParentCode = parentCode;
        Code = code;
        Type = type;
    }

    /// <summary>
    /// Gets the code of the item related to the grade.
    /// </summary>
    public ItemCode ParentCode { get; }

    /// <summary>
    /// Gets the code of the item the grade represents.
    /// </summary>
    public ItemCode Code { get; }

    /// <summary>
    /// Gets the type of grade the item is in.
    /// </summary>
    public ItemGradeType Type { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ItemDefinitionGrade"/> class.
    /// </summary>
    /// <param name="parentCode">The code of the item related to the grade.</param>
    /// <param name="code">The code of the item the grade represents.</param>
    /// <param name="type">The type of grade the item is in.</param>
    /// <returns>The new instance of the <see cref="ItemDefinitionGrade"/> class.</returns>
    public static ItemDefinitionGrade Create(ItemCode parentCode, ItemCode code, ItemGradeType type)
        => new(parentCode, code, type);
}
