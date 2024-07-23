using Accumulator.Application;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    where TCommand : Command
{
    private readonly ICommandHandler<TCommand> _decoratedHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> decoratedHandler, IUnitOfWork unitOfWork)
    {
        _decoratedHandler = decoratedHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(TCommand command, CancellationToken cancellationToken)
    {
        await _decoratedHandler.Handle(command, cancellationToken)
            .ConfigureAwait(false);

        await _unitOfWork.CommitAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
