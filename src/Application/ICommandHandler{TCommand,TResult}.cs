using MediatR;

namespace Accumulator.Application;

/// <summary>
/// Represents the handler for a command.
/// </summary>
/// <typeparam name="TCommand">The type of the command to handle.</typeparam>
/// <typeparam name="TResult">The type of the command result.</typeparam>
public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
    where TCommand : Command<TResult>
{
}
