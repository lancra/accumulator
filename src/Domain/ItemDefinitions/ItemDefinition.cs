using Accumulator.Domain.Items;
using Accumulator.Domain.SharedKernel;

namespace Accumulator.Domain.ItemDefinitions;

/// <summary>
/// Provides the definition of an item provided by the game.
/// </summary>
public class ItemDefinition : Entity, IAggregateRoot
{
    private readonly List<ItemDefinitionGrade> _grades = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor.
    private ItemDefinition()
#pragma warning restore CS8618 // Required for Entity Framework.
    {
    }

    private ItemDefinition(
        ItemCode code,
        ItemType type,
        string name,
        ItemGradeType gradeType,
        ItemVersion version,
        int level,
        bool isStackable,
        ItemSize size)
    {
        Code = code;
        Type = type;
        Name = name;
        GradeType = gradeType;
        Version = version;
        Level = level;
        IsStackable = isStackable;
        Size = size;
    }

    /// <summary>
    /// Gets the code which provides unique identification.
    /// </summary>
    public ItemCode Code { get; }

    /// <summary>
    /// Gets the type of the item.
    /// </summary>
    public ItemType Type { get; }

    /// <summary>
    /// Gets the display name of the item.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the type of grade the item is in.
    /// </summary>
    public ItemGradeType GradeType { get; }

    /// <summary>
    /// Gets the version that the item is defined in.
    /// </summary>
    public ItemVersion Version { get; }

    /// <summary>
    /// Gets the base item level.
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// Gets a value indicating whether the item can be stacked.
    /// </summary>
    public bool IsStackable { get; }

    /// <summary>
    /// Gets the size of the item in a panel.
    /// </summary>
    public ItemSize Size { get; }

    /// <summary>
    /// Gets the other item grades within the same item group.
    /// </summary>
    public IReadOnlyCollection<ItemDefinitionGrade> Grades => _grades.AsReadOnly();

    /// <summary>
    /// Creates a new instance of the <see cref="ItemDefinition"/> class.
    /// </summary>
    /// <param name="code">The code which provides unique identification.</param>
    /// <param name="type">The type of the item.</param>
    /// <param name="name">The display name of the item.</param>
    /// <param name="gradeType">The type of grade the item is in.</param>
    /// <param name="version">The version that the item is defined in.</param>
    /// <param name="level">The base item level.</param>
    /// <param name="isStackable">A value indicating whether the item can be stacked.</param>
    /// <param name="size">The size of the item in a panel.</param>
    /// <returns>The new instance of the <see cref="ItemDefinition"/> class.</returns>
    public static ItemDefinition Create(
        ItemCode code,
        ItemType type,
        string name,
        ItemGradeType gradeType,
        ItemVersion version,
        int level,
        bool isStackable,
        ItemSize size)
        => new(code, type, name, gradeType, version, level, isStackable, size);

    /// <summary>
    /// Adds a grade within the same group as the item.
    /// </summary>
    /// <param name="code">The code of the other item.</param>
    /// <param name="type">The type of grade the other item is in.</param>
    public void AddGrade(ItemCode code, ItemGradeType type)
    {
        var grade = ItemDefinitionGrade.Create(Code, code, type);
        _grades.Add(grade);
    }
}
