using Accumulator.Application;

namespace Accumulator.Infrastructure.Modules.Processing;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    where TCommand : Command<TResult>
{
    private readonly ICommandHandler<TCommand, TResult> _decoratedHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand, TResult> decoratedHandler, IUnitOfWork unitOfWork)
    {
        _decoratedHandler = decoratedHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResult> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var result = await _decoratedHandler.Handle(command, cancellationToken)
            .ConfigureAwait(false);

        await _unitOfWork.CommitAsync(cancellationToken)
            .ConfigureAwait(false);

        return result;
    }
}
