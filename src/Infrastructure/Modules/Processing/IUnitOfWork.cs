namespace Accumulator.Infrastructure.Modules.Processing;

/// <summary>
/// Represents the component responsible for managing the lifecycle of database transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Commits any pending database operations.
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task CommitAsync(CancellationToken cancellationToken);
}
