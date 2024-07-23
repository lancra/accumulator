using MediatR;

namespace Accumulator.Application;

/// <summary>
/// Represents a command to perform data modification.
/// </summary>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public abstract class Command<TResult> : IRequest<TResult>
{
    /// <summary>
    /// Gets the unique identifier for the command.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();
}
