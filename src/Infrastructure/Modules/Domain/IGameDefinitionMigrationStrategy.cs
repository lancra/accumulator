namespace Accumulator.Infrastructure.Modules.Domain;

/// <summary>
/// Represents a migration strategy for refreshing game definitions.
/// </summary>
internal interface IGameDefinitionMigrationStrategy
{
    /// <summary>
    /// Migrates specified game definitions into the system.
    /// </summary>
    /// <param name="directoryPath">The path of the directory containing game definition files.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task MigrateAsync(string directoryPath, CancellationToken cancellationToken);
}
