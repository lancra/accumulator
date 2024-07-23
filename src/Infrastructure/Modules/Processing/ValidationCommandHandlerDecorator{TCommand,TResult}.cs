using Accumulator.Application;
using FluentValidation;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class ValidationCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : Command<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _decoratedHandler;
    private readonly IReadOnlyCollection<IValidator<TCommand>> _validators;

    public ValidationCommandHandlerDecorator(
        ICommandHandler<TCommand, TResult> decoratedHandler,
        IReadOnlyCollection<IValidator<TCommand>> validators)
    {
        _decoratedHandler = decoratedHandler;
        _validators = validators;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var errors = _validators.Select(validator => validator.Validate(command))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToArray();

        return errors.Length > 0
            ? throw new InvalidCommandException(errors.Select(error => error.ErrorMessage)
                .ToArray())
            : await _decoratedHandler.Handle(command, cancellationToken)
            .ConfigureAwait(false);
    }
}
