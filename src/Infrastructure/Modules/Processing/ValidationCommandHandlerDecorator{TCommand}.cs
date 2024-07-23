using Accumulator.Application;
using FluentValidation;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly ICommandHandler<TCommand> _decoratedHandler;
    private readonly IReadOnlyCollection<IValidator<TCommand>> _validators;

    public ValidationCommandHandlerDecorator(
        ICommandHandler<TCommand> decoratedHandler,
        IReadOnlyCollection<IValidator<TCommand>> validators)
    {
        _decoratedHandler = decoratedHandler;
        _validators = validators;
    }

    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        var errors = _validators.Select(validator => validator.Validate(command))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToArray();

        if (errors.Length > 0)
        {
            throw new InvalidCommandException(errors.Select(error => error.ErrorMessage)
                .ToArray());
        }

        await _decoratedHandler.Handle(command, cancellationToken)
            .ConfigureAwait(false);
    }
}
