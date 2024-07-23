using MediatR;

namespace Accumulator.Application;

/// <summary>
/// Represents a command to perform data modification.
/// </summary>
public abstract class Command : IRequest
{
    /// <summary>
    /// Gets the unique identifier for the command.
    /// </summary>
    public Guid Id { get; } = Guid.NewGuid();
}
