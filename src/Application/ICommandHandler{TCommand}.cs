using MediatR;

namespace Accumulator.Application;

/// <summary>
/// Represents the handler for a command.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand>
    where TCommand : Command
{
}
