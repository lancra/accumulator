using Accumulator.Domain.ItemDefinitions;
using Accumulator.Infrastructure.Modules.Domain;
using Accumulator.Infrastructure.Modules.IO;
using CsvHelper;

namespace Accumulator.Infrastructure.Domain.ItemDefinitions;

internal sealed class MiscellaneousItemGameDefinitionReader : GameDefinitionReader<ItemDefinition>
{
    public override string FileName { get; } = "misc.txt";

    protected override ItemDefinition? Read(IReader reader)
    {
        var code = reader.GetOptionalItemCodeField("code");
        var name = reader.GetField("name") ?? string.Empty;
        if (code is null &&
            !string.IsNullOrEmpty(name) &&
            name.Equals(ExpansionSeparatorName, StringComparison.OrdinalIgnoreCase))
        {
            return default;
        }

        var version = reader.GetItemVersionField("version");
        var level = reader.GetField<int>("level");
        var isStackable = reader.GetBlankBooleanField("stackable");

        var height = reader.GetField<int>("invheight");
        var width = reader.GetField<int>("invwidth");
        var size = ItemSize.Create(height, width);

        var gradeType = ItemGradeType.Normal;
        var itemDefinition = ItemDefinition.Create(code!, ItemType.Miscellaneous, name, gradeType, version, level, isStackable, size);
        return itemDefinition;
    }
}
