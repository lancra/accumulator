namespace Accumulator.Domain;

/// <summary>
/// Represents the migrator for refreshing game definitions.
/// </summary>
public interface IGameDefinitionMigrator
{
    /// <summary>
    /// Migrates all defined game definitions into the system.
    /// </summary>
    /// <param name="directoryPath">The path of the directory containing game definition files.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task MigrateAllAsync(string directoryPath, CancellationToken cancellationToken);
}
