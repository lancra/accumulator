using Accumulator.Application;
using Autofac;
using MediatR;

namespace Accumulator.Infrastructure;

internal sealed class AccumulatorExecutor : IAccumulatorExecutor
{
    public async Task ExecuteCommandAsync(Command command, CancellationToken cancellationToken)
    {
        using var scope = AccumulatorCompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();

        await mediator.Send(command, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<TResult> ExecuteCommandAsync<TResult>(Command<TResult> command, CancellationToken cancellationToken)
    {
        using var scope = AccumulatorCompositionRoot.BeginLifetimeScope();
        var mediator = scope.Resolve<IMediator>();

        var result = await mediator.Send(command, cancellationToken)
            .ConfigureAwait(false);
        return result;
    }
}
