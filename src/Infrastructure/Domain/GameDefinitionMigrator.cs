using Accumulator.Domain;
using Accumulator.Infrastructure.Modules.Domain;

namespace Accumulator.Infrastructure.Domain;

internal sealed class GameDefinitionMigrator : IGameDefinitionMigrator
{
    private readonly IEnumerable<IGameDefinitionMigrationStrategy> _strategies;

    public GameDefinitionMigrator(IEnumerable<IGameDefinitionMigrationStrategy> strategies)
        => _strategies = strategies;

    public async Task MigrateAllAsync(string directoryPath, CancellationToken cancellationToken)
    {
        foreach (var strategy in _strategies)
        {
            await strategy.MigrateAsync(directoryPath, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
