using Accumulator.Domain.ItemDefinitions;
using Accumulator.Domain.Items;
using CsvHelper;

namespace Accumulator.Infrastructure.Modules.IO;

internal static class CsvReaderExtensions
{
    public static ItemCode? GetOptionalItemCodeField(this IReader reader, string name)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var codeText = reader.GetField(name);
        return !string.IsNullOrEmpty(codeText)
            ? new(codeText)
            : default;
    }

    public static ItemCode GetItemCodeField(this IReader reader, string name)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var codeText = reader.GetField(name) ?? string.Empty;
        return new(codeText);
    }

    public static ItemVersion GetItemVersionField(this IReader reader, string name)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var versionText = reader.GetField(name);
        if (string.IsNullOrEmpty(versionText))
        {
            throw new InvalidOperationException("Unable to read the item version.");
        }

        var versionValue = Convert.ToByte(versionText, 2);
        return ItemVersion.FromValue(versionValue);
    }

    public static bool GetBlankBooleanField(this IReader reader, string name)
    {
        ArgumentNullException.ThrowIfNull(reader);

        var booleanText = reader.GetField(name);
        return !string.IsNullOrEmpty(booleanText) && booleanText.Equals("1", StringComparison.OrdinalIgnoreCase);
    }
}
