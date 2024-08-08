using Accumulator.Domain.SharedKernel;

namespace Accumulator.Domain.ItemDefinitions;

/// <summary>
/// Provides the size of an item within a panel.
/// </summary>
public record ItemSize : ValueObject
{
    private ItemSize(int height, int width)
    {
        Height = height;
        Width = width;
    }

    /// <summary>
    /// Gets the height of an item in a panel.
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Gets the width of an item in a panel.
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Creates a new instance of the <see cref="ItemSize"/> class.
    /// </summary>
    /// <param name="height">The height of an item in a panel.</param>
    /// <param name="width">The width of an item in a panel.</param>
    /// <returns>The new instance of the <see cref="ItemSize"/> class.</returns>
    public static ItemSize Create(int height, int width)
        => new(height, width);

    /// <summary>
    /// Gets the components which determine equality when comparing to other <see cref="ItemSize"/> value objects.
    /// </summary>
    /// <returns>The equality components for the current <see cref="ItemSize"/> value object.</returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Height;
        yield return Width;
    }
}
