using Accumulator.Domain.ItemDefinitions;
using Microsoft.EntityFrameworkCore;

namespace Accumulator.Infrastructure.Modules.Domain;

internal sealed class ItemGameDefinitionMigrationStrategy : IGameDefinitionMigrationStrategy
{
    private readonly AccumulatorDbContext _dbContext;
    private readonly IEnumerable<IGameDefinitionReader<ItemDefinition>> _readers;

    public ItemGameDefinitionMigrationStrategy(
        AccumulatorDbContext dbContext,
        IEnumerable<IGameDefinitionReader<ItemDefinition>> readers)
    {
        _dbContext = dbContext;
        _readers = readers;
    }

    public async Task MigrateAsync(string directoryPath, CancellationToken cancellationToken)
    {
        var newItemDefinitions = new List<ItemDefinition>();
        foreach (var reader in _readers)
        {
            var path = Path.Combine(directoryPath, reader.FileName);
            newItemDefinitions.AddRange(reader.ReadAll(path));
        }

        await _dbContext.ItemDefinitions.ExecuteDeleteAsync(cancellationToken)
            .ConfigureAwait(false);

        _dbContext.ItemDefinitions.AddRange(newItemDefinitions);
    }
}
