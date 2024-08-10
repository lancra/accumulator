using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Accumulator.Infrastructure.Modules.Domain;

internal abstract class GameDefinitionReader<TDefinition> : IGameDefinitionReader<TDefinition>
    where TDefinition : class
{
    protected const string ExpansionSeparatorName = "Expansion";

    public abstract string FileName { get; }

    public IReadOnlyCollection<TDefinition> ReadAll(string path)
    {
        using var reader = CreateReader(path);
        reader.Read();
        reader.ReadHeader();

        var definitions = new List<TDefinition>();
        while (reader.Read())
        {
            var definition = Read(reader);
            if (definition is not null)
            {
                definitions.Add(definition);
            }
        }

        return definitions;
    }

    protected abstract TDefinition? Read(IReader reader);

    private static CsvReader CreateReader(string path)
        => new(
            new StreamReader(path),
            new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = "\t",
            });
}
