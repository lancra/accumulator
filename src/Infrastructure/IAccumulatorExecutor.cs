using Accumulator.Application;

namespace Accumulator.Infrastructure;

/// <summary>
/// Represents the component used to execute application functionality.
/// </summary>
public interface IAccumulatorExecutor
{
    /// <summary>
    /// Executes a command.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>The <see cref="Task"/> that represents the asynchronous operation.</returns>
    Task ExecuteCommandAsync(Command command, CancellationToken cancellationToken);

    /// <summary>
    /// Executes a command with a result.
    /// </summary>
    /// <typeparam name="TResult">The type of the command result.</typeparam>
    /// <param name="command">The command to execute.</param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// The <see cref="Task"/> that represents the asynchronous operation,
    /// containing the command result.
    /// </returns>
    Task<TResult> ExecuteCommandAsync<TResult>(Command<TResult> command, CancellationToken cancellationToken);
}
