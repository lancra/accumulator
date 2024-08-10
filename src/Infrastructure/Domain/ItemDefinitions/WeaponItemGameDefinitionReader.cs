using Accumulator.Domain.ItemDefinitions;
using Accumulator.Infrastructure.Modules.Domain;
using Accumulator.Infrastructure.Modules.IO;
using CsvHelper;

namespace Accumulator.Infrastructure.Domain.ItemDefinitions;

internal sealed class WeaponItemGameDefinitionReader : GameDefinitionReader<ItemDefinition>
{
    public override string FileName { get; } = "weapons.txt";

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

        var normalCode = reader.GetItemCodeField("normcode");
        var exceptionalCode = reader.GetItemCodeField("ubercode");
        var eliteCode = reader.GetItemCodeField("ultracode");
        var gradeType = ItemGradeType.FromCodes(code!, normalCode, exceptionalCode, eliteCode);

        var itemDefinition = ItemDefinition.Create(code!, ItemType.Weapon, name, gradeType, version, level, isStackable, size);

        if (gradeType != ItemGradeType.Normal)
        {
            itemDefinition.AddGrade(normalCode, ItemGradeType.Normal);
        }

        if (gradeType != ItemGradeType.Exceptional && !string.IsNullOrEmpty(exceptionalCode.Value))
        {
            itemDefinition.AddGrade(exceptionalCode, ItemGradeType.Exceptional);
        }

        if (gradeType != ItemGradeType.Elite && !string.IsNullOrEmpty(eliteCode.Value))
        {
            itemDefinition.AddGrade(eliteCode, ItemGradeType.Elite);
        }

        return itemDefinition;
    }
}
